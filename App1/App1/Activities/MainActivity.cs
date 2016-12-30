﻿using System;
using System.IO;
using System.Net;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Preferences;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true, Icon = "@drawable/icon")]
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

            botonLogin.Click += BotonLogin_Click;



            /*
             * Cargo datos guardados
             * */

            // this is an Activity
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();


           mail.Text = prefs.GetString("mail", null);
    
           pass.Text = prefs.GetString("pass", null);
           



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

                    if (respuesta.StartsWith(ContenedorComun.loginCorrectoInicio))
                    {


                        /*
                         * Guardo campos
                         * */
                        ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                        ISharedPreferencesEditor editor = prefs.Edit();

                        editor.PutString("mail", mail.Text);
                        editor.PutString("pass", pass.Text);
                        editor.Commit();


                        respuesta = respuesta.Substring(ContenedorComun.loginCorrectoInicio.Length,
                            respuesta.Length - ContenedorComun.loginCorrectoInicio.Length);

                        var pantallaPrincial = new Intent(this, typeof(pantallaPrincipalActivity));
                        pantallaPrincial.PutExtra("rta", respuesta);

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
