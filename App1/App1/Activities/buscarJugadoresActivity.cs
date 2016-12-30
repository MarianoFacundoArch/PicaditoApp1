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
    [Activity(Label = "buscarJugadoresActivity")]
    public class buscarJugadoresActivity : Activity
    {
        List<jugador> jugadores = new List<jugador>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.buscarJugadores);

            Button botonBusqueda = FindViewById<Button>(Resource.Id.buttonBuscar);
            botonBusqueda.Click += BotonBusquedaOnClick;


            // Create your application here
        }

        private void BotonBusquedaOnClick(object sender, EventArgs eventArgs)
        {
            EditText campoBusqueda = FindViewById<EditText>(Resource.Id.editTextBuscarJugador);
            try
            {
                jugadores.Clear();

                string jugadoresQuery = ContenedorComun.pedirSitio("buscadorJugadores", "campoBusqueda=" +campoBusqueda.Text);
                Toast.MakeText(this, jugadoresQuery, ToastLength.Long).Show();
                foreach (string equipoActual in ContenedorComun.parseJsonList(jugadoresQuery))
                {
                    jugador tmp = new jugador(equipoActual);
                    /*
                     * Creo clase y la cargo en el diccionario
                     * */

                    jugadores.Add(tmp);


                    //Toast.MakeText(Android.App.Application.Context, tmp.Nombre, ToastLength.Long).Show();

                }

                ListView listView = FindViewById<ListView>(Resource.Id.listJugadores);
                listView.Adapter = new adapterJugador(this, jugadores);

            }
            catch (Exception e)
            {
                
                throw;
            }

        }
    }
}