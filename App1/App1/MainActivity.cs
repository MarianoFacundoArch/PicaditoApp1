using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button botonLogin = FindViewById<Button>(Resource.Id.button1);

            botonLogin.Click += BotonLogin_Click;
        }

        private void BotonLogin_Click(object sender, EventArgs e)
        {
            EditText mail = FindViewById<EditText>(Resource.Id.mailText);
            EditText pass = FindViewById<EditText>(Resource.Id.mailText);

            Toast.MakeText(this, mail.Text, ToastLength.Long).Show();
        }
    }
}

