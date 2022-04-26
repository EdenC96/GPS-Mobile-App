using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Locations;

namespace GPS_App
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.Design.NoActionBar", MainLauncher = true)]
    public class MainActivity : Activity, ILocationListener
    {
        private Button button;
        private String sendtxt;

        private LocationManager lm;
        private string lp;
        private Location here;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Draw controls
            SetContentView(Resource.Layout.activity_main);

            // Connect controls
            button = FindViewById<Button>(Resource.Id.button1);

            //Events
            button.Click += GetGPS;
            SetUpGPS();
        }
        private void SetUpGPS()
        {
            lm = (LocationManager)GetSystemService(LocationService);
            Criteria c = new Criteria { Accuracy = Accuracy.Fine };
            IList<string> providers = lm.GetProviders(c, true);
            if (providers.Count > 0)
            {
                lp = providers[0];
            }
            else
            {
                lp = string.Empty;
            }
        }
        protected override void OnResume()
        {
            base.OnResume();
            lm.RequestLocationUpdates(lp, 0, 0, this);
        }
        protected override void OnPause()
        {
            base.OnResume();
            lm.RemoveUpdates(this);
        }
        private void GetGPS(object sender, EventArgs e)
        {
            if (here == null)
            {
                button.Text = "Error Getting Location!";
                return;
            }
            sendtxt = "/" + here.Latitude + "/" + here.Longitude;

            var nextpage = new Intent(this, typeof(mappage));
            nextpage.PutExtra("DataSend", sendtxt);
            this.StartActivity(nextpage);

        }

        public void OnLocationChanged(Location location)
        {
            here = location;
            if (here == null)
            {
              button.Text = "Error Getting Location!";
            }
        }
        public void OnProviderEnabled(string provider)
        {
        }
        public void OnProviderDisabled(string provider)
        {
        }
        public void OnStatusChanged(string provider, Availability status,
        Bundle extras)
        {
        }


    }
}