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



namespace App1.Activities
{
    [Activity(Label = "crearEquipoActivity")]
    public class crearEquipoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.crearEquipo);

            ListView listView = FindViewById<ListView>(Resource.Id.listJugadores);

            List<jugador> jugadores = new List<jugador>();

            for (int i = 0; i < 100; i++)
            {
                jugador nuevoJugador = new jugador(i, "jugador " + i, i + "@gmail.com",
                    "http://cloud10.todocoleccion.online/mascotas-futbol/tc/2013/07/22/38335151.jpg");
                jugadores.Add(nuevoJugador);
            }
            
            
            listView.Adapter = new adapterJugador(this, jugadores);

            Button botonInvitar = FindViewById<Button>(Resource.Id.buttonInvitarJugadores);
            botonInvitar.Click += BotonInvitarOnClick;


        }

        private void BotonInvitarOnClick(object sender, EventArgs eventArgs)
        {
            Intent nuevoIntent = new Intent(this, typeof(buscarJugadoresActivity));
            StartActivity(nuevoIntent);
        }
    }
}