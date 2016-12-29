using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    [Activity(Label = "pantallaPrincipalActivity")]
    public class pantallaPrincipalActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pantallaPrincipal);
            string datosRecibidos = Intent.GetStringExtra("rta");

            ContenedorComun.datos = new datosUsuario(datosRecibidos);
            TextView textUsuario = FindViewById<TextView>(Resource.Id.textUsuario);
            textUsuario.Text = ContenedorComun.datos.Mail;

            Toast.MakeText(this, ContenedorComun.pedirSitio("dameequipos"), ToastLength.Long).Show();
            // Create your application here
        }
    }
}