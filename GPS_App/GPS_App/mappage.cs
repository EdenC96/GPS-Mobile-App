using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;using Android.Webkit;
namespace GPS_App
{
    [Activity(Label = "location", Theme = "@style/Theme.Design.NoActionBar")]
    public class mappage : Activity
    {
        //Declare Variables
        Button goback;
        WebView MapView;
        string DataValue;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Set Layout
            SetContentView(Resource.Layout.location_layout);
            // Connect controls
            MapView = FindViewById<WebView>(Resource.Id.webView1);
            MapView.Settings.JavaScriptEnabled = true;
            MapView.SetWebViewClient(new SubWebView());

            goback = FindViewById<Button>(Resource.Id.locbutton1);

            DataValue = Intent.GetStringExtra("DataSend");

            //Load Map
            MapView.LoadUrl("https://www.openstreetmap.org/#map=19" + DataValue + "&layers=N");

            //Return to MainActivity.cs
            goback.Click += BackBtn;   
        }

        public class SubWebView : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
            {
                view.LoadUrl(request.Url.ToString());
                return false;
            }

        }

        public void BackBtn(object sender, EventArgs e) {
            
            //Start intent to go back to MainActivity.cs
            var home = new Intent(this, typeof(MainActivity));
            this.StartActivity(home);
        }
    }
}