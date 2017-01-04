using System;
using System.Collections.Specialized;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;

namespace App1.Activities
{
    [Activity(Label = "crearUsuarioActivity", NoHistory = true)]
    public class crearUsuarioActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.crearUsuario);
            Window.SetSoftInputMode(SoftInput.StateHidden); // Evito auto apertura del keyboard
            Button crearEquipo = FindViewById<Button>(Resource.Id.buttonCrearUsuario);
            crearEquipo.Click += CrearEquipoOnClick;
            // Create your application here
        }

        private void CrearEquipoOnClick(object sender, EventArgs eventArgs)
        {
            EditText contrasena = FindViewById<EditText>(Resource.Id.editTextPass);
            EditText contrasenarep = FindViewById<EditText>(Resource.Id.editTextRepetirPass);
            EditText nombre = FindViewById<EditText>(Resource.Id.editTextNombre);
            EditText mail = FindViewById<EditText>(Resource.Id.editTextMail);

            if (contrasena.Text != contrasenarep.Text)
            {

                Toast.MakeText(this, "Las contrasenas deben ser iguales", ToastLength.Short).Show();
                return;
            }


            if (!ContenedorComun.IsValidEmail(mail.Text))
            {
                Toast.MakeText(this, "El mail es invalido", ToastLength.Short).Show();
                return;
            }

            if (string.IsNullOrEmpty(mail.Text))
            {
                Toast.MakeText(this, "El mail no puede estar vacio", ToastLength.Short).Show();
                return;
            }

            if (string.IsNullOrEmpty(contrasena.Text))
            {
                Toast.MakeText(this, "La contrasena no puede estar vacia", ToastLength.Short).Show();
                return;
            }

            if (string.IsNullOrEmpty(nombre.Text))
            {
                Toast.MakeText(this, "El nombre no puede estar vacio", ToastLength.Short).Show();
                return;
            }

            NameValueCollection parametros = new NameValueCollection
            {
           { "nombre", nombre.Text },
           { "mail", mail.Text },
           { "pass", contrasena.Text }
       };



            string respuesta = ContenedorComun.pedirPost("crearusuario", parametros,false);

            Toast.MakeText(this,respuesta,ToastLength.Short).Show();
            if (respuesta == "repetido")
            {
                Toast.MakeText(this, "Ya existe una cuenta con este mail", ToastLength.Short).Show();
                return;
            }

            if (respuesta.StartsWith("correcto:"))
            {
                /*
                         * Guardo campos
                         * */
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = prefs.Edit();

                editor.PutString("mail", mail.Text);
                editor.PutString("pass", contrasena.Text);
                editor.Commit();


                respuesta = ContenedorComun.removerCabeceraCorrecto(respuesta);

                var pantallaPrincial = new Intent(this, typeof(pantallaPrincipalActivity));
                pantallaPrincial.PutExtra("rta", respuesta);

                pantallaPrincial.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTop |
                                          ActivityFlags.SingleTop);
                StartActivity(pantallaPrincial);
                Finish();
            }
        }
    }
}