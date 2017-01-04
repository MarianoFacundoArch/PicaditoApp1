using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
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

            Window.SetSoftInputMode(SoftInput.StateHidden); // Evito auto apertura del keyboard
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
                listView.ItemClick += ListViewOnItemClick;


                /*Ocultar teclado*/
                
                InputMethodManager inputManager = (InputMethodManager)GetSystemService(InputMethodService);

                inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);


            }
            catch (Exception e)
            {
                
                throw;
            }

        }

        private void ListViewOnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {


            AlertDialog.Builder confirmacion = new AlertDialog.Builder(this);
            confirmacion.SetTitle("Desea agregar al jugador?");
            confirmacion.SetPositiveButton("Si", delegate
            {
                var listView = sender as ListView;
                var t = ((adapterJugador) listView.Adapter).GetJugador(e.Position);

                if (!ContenedorComun.jugadoresCache.ContainsKey(t.IdUsuario))
                    ContenedorComun.jugadoresCache.Add(t.IdUsuario, t);

                if (!ContenedorComun.imagenesCache.ContainsKey(t.Imagen))
                    ContenedorComun.imagenesCache.Add(t.Imagen, ((adapterJugador) listView.Adapter).GetImagen(t.Imagen));

                Intent resultIntent = new Intent(this, typeof(crearEquipoActivity));
                // TODO Add extras or a data URI to this intent as appropriate.
                resultIntent.PutExtra("jugadorID", t.IdUsuario);
                SetResult(Result.Ok, resultIntent);
                Finish();
            });

            confirmacion.SetNegativeButton("No, no agregar", delegate { });
            confirmacion.Show();


        }
    }
}