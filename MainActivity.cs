using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Threading;
using Android.Media;

namespace KiechenTimer
{
    [Activity(Label = "KiechenTimer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int _remainingMilliSec = 0;

        private bool _isStart = false;
        private Button _startButton;

        private Timer _timer;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var add10MinButton = FindViewById<Button>(Resource.Id.Add10MinButton);
            add10MinButton.Click += Add10MinButton_Click;

            var add1MinButton = FindViewById<Button>(Resource.Id.Add1MinButton);
            add1MinButton.Click += (s, e) =>
            {
                _remainingMilliSec += 60 * 1000;
                ShowRemainingTime();
            };

            var add10SecButton = FindViewById<Button>(Resource.Id.Add10SecButton);
            add10SecButton.Click += (s, e) =>
            {
                _remainingMilliSec += 10 * 1000;
                ShowRemainingTime();
            };

            var add1SecButton = FindViewById<Button>(Resource.Id.Add1SecButton);
            add1SecButton.Click += (s, e) =>
            {
                _remainingMilliSec += 1 * 1000;
                ShowRemainingTime();
            };

            var clearButton = FindViewById<Button>(Resource.Id.ClearButton);
            clearButton.Click += (s, e) =>
            {
                _isStart = false;
                _remainingMilliSec = 0;
                ShowRemainingTime();
            };

            _startButton = FindViewById<Button>(Resource.Id.StartButton);
            _startButton.Click += StartButton_Click;

            _timer = new Timer(Timer_OnTick, null, 0, 100);
        }

        private void Timer_OnTick(object state)
        {
            if (!_isStart)
            {
                return;
            }

            RunOnUiThread(() =>
            {
                _remainingMilliSec -= 100;
                if (_remainingMilliSec <= 0)
                {
                    _isStart = false;
                    _remainingMilliSec = 0;
                    _startButton.Text = "スタート";
                    var toneGenerator = new ToneGenerator(Stream.System, 50);
                    toneGenerator.StartTone(Tone.PropBeep);
                }
                ShowRemainingTime();
            });
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _isStart = !_isStart;
            if (_isStart)
            {
                _startButton.Text = "ストップ";
            }
            else
            {
                _startButton.Text = "スタート";
            }
        }

        private void Add10MinButton_Click(object sender, EventArgs e)
        {
            _remainingMilliSec += 600 * 100;
            ShowRemainingTime();
        }

        private void ShowRemainingTime()
        {
            var sec = _remainingMilliSec / 1000;
            FindViewById<TextView>(Resource.Id.RemainingTimeTextView).Text = string.Format("{0:f0}:{1:d2}", sec / 60, sec % 60);

        }

    }
}

