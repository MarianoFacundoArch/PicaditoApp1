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
using App1.Activities;

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


            /*
             * Cargo datos del jugador
             * */
            ContenedorComun.datos = new jugador(datosRecibidos);
            TextView textUsuario = FindViewById<TextView>(Resource.Id.textUsuario);
            textUsuario.Text = ContenedorComun.datos.Mail;

            /*
             * Cargo los equipos del jugador
             * */
            ContenedorComun.cargarEquipos();


            /*
             * Hora de la magia del listview
             * */
            //Initializing listview
            ListView listView = FindViewById<ListView>(Resource.Id.listMisEquipos);
            listView.Adapter = new adapterEquipo(this, ContenedorComun.misEquipos.Values.ToList());
            listView.ItemClick += ListViewOnItemClick;

            /*
             * Agrego handlers 
             * */
            Button botonCrearEquipo = FindViewById<Button>(Resource.Id.buttonCrearEquipo);
            botonCrearEquipo.Click += BotonCrearEquipoOnClick;


        }

        private void BotonCrearEquipoOnClick(object sender, EventArgs eventArgs)
        {
            Intent crearEquipoIntent = new Intent(this, typeof(crearEquipoActivity));
            StartActivity(crearEquipoIntent);
        }

        private void ListViewOnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = ((adapterEquipo)listView.Adapter).GetEquipo(e.Position);
            
            Intent nuevaVentana = new Intent(this, typeof(datosEquipoActivity));
            nuevaVentana.PutExtra("id", t.IdEquipo);
            StartActivity(nuevaVentana);
            
        }
    }
}