using Android.App;
using Android.Widget;
using Android.OS;
using System;


namespace KiechenTimer
{
    [Activity(Label = "KiechenTimer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int _remainingMilliSec = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var add10MinButton = FindViewById<Button>(Resource.Id.Add10MinButton);
            add10MinButton.Click += Add10MinButton_Click;
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

