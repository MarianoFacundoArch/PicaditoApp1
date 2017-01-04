using System;
using System.IO;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Widget;
using App1.Activities;

namespace App1
{
    [Activity(Label = "Picadito", NoHistory = true, MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button botonLogin = FindViewById<Button>(Resource.Id.button1);
            EditText mail = FindViewById<EditText>(Resource.Id.mailText);
            EditText pass = FindViewById<EditText>(Resource.Id.passText);
            Button botonCrear = FindViewById<Button>(Resource.Id.buttonCrearUsuario);
            botonCrear.Click += BotonCrearOnClick;
            botonLogin.Click += BotonLogin_Click;

            if (ContenedorComun.datos != null)
            {
                var pantallaPrincial = new Intent(this, typeof(pantallaPrincipalActivity));
                pantallaPrincial.PutExtra("yacargado", true);
                pantallaPrincial.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTop |
                                          ActivityFlags.SingleTop);
                StartActivity(pantallaPrincial);
                Finish();
            }

            /*
             * Cargo datos guardados
             * */

            // this is an Activity
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();


           mail.Text = prefs.GetString("mail", null);
    
           pass.Text = prefs.GetString("pass", null);
           



        }

        private void BotonCrearOnClick(object sender, EventArgs eventArgs)
        {
            Intent crearIntent = new Intent(this, typeof(crearUsuarioActivity));
            StartActivity(crearIntent);
            Finish();
        }

        private void BotonLogin_Click(object sender, EventArgs e)
        {
            EditText mail = FindViewById<EditText>(Resource.Id.mailText);
            EditText pass = FindViewById<EditText>(Resource.Id.passText);

            try
            {
                HttpWebRequest peticionLogin = HttpWebRequest.CreateHttp(ContenedorComun.urlsite + "backend.php?mail=" + mail.Text + "&pass=" + pass.Text + "&consulta=dameinfousuario");
                WebResponse respuestaObj = peticionLogin.GetResponse();
                Stream lectura = respuestaObj.GetResponseStream();
                StreamReader lecturaFinal = new StreamReader(lectura);

                string respuesta = lecturaFinal.ReadToEnd();

                if (respuesta == ContenedorComun.loginIncorrecto)
                {
                    Toast.MakeText(this, "Datos incorrectos.", ToastLength.Long).Show();

                }
                else
                {

                    if (respuesta.StartsWith(ContenedorComun.accionCorrecta))
                    {


                        /*
                         * Guardo campos
                         * */
                        ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                        ISharedPreferencesEditor editor = prefs.Edit();

                        editor.PutString("mail", mail.Text);
                        editor.PutString("pass", pass.Text);
                        editor.Commit();


                        respuesta = ContenedorComun.removerCabeceraCorrecto(respuesta);

                        var pantallaPrincial = new Intent(this, typeof(pantallaPrincipalActivity));
                        pantallaPrincial.PutExtra("rta", respuesta);
                        pantallaPrincial.PutExtra("yacargado", false);
                        pantallaPrincial.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTop |
                                                  ActivityFlags.SingleTop);
                        StartActivity(pantallaPrincial);
                        Finish();
                    }
                    else
                    {
                        Toast.MakeText(this, "Error de login.", ToastLength.Long).Show();
                    }
                    
                }
                
            }
            catch (Exception exception)
            {
                Toast.MakeText(this, exception.ToString(), ToastLength.Long).Show();
                
            }
            

        }
    }
}

