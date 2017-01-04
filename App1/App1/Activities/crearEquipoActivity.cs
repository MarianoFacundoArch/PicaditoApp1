using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace App1.Activities
{
    [Activity(Label = "crearEquipoActivity")]
    public class crearEquipoActivity : Activity
    {


        Dictionary<int, jugador> jugadores = new Dictionary<int, jugador>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.SetSoftInputMode(SoftInput.StateHidden); // Evito auto apertura del keyboard
            SetContentView(Resource.Layout.crearEquipo);

            ListView listView = FindViewById<ListView>(Resource.Id.listJugadores);

            


            
            listView.Adapter = new adapterJugador(this, jugadores.Values.ToList());
            listView.ItemLongClick += ListViewOnItemLongClick;
            listView.ItemClick += delegate {  Toast.MakeText(this, "Mantenga pulsado para eliminar al jugador",ToastLength.Long).Show();};
            Button botonInvitar = FindViewById<Button>(Resource.Id.buttonInvitarJugadores);
            botonInvitar.Click += BotonInvitarOnClick;

            Button botonCrear = FindViewById<Button>(Resource.Id.buttonCrearEquipo);
            botonCrear.Click += BotonCrearOnClick;


        }

        private void BotonCrearOnClick(object sender, EventArgs eventArgs)
        {
            string jugadores = "";
            EditText nombreEquipo = FindViewById<EditText>(Resource.Id.editTextNombreEquipo);
            ListView listView = FindViewById<ListView>(Resource.Id.listJugadores);
            List<jugador> listaJugadores = ((adapterJugador) listView.Adapter).GetJugadores();

            if (string.IsNullOrEmpty(nombreEquipo.Text))
            {
                Toast.MakeText(this, "El nombre no puede estar vacio", ToastLength.Short).Show();
                return;
            }
            if (listaJugadores.Count > 0)
            {
                jugadores = listaJugadores[0].IdUsuario.ToString();

                for (int i = 1; i <= listaJugadores.Count - 1; i++)
                {
                    jugadores = jugadores + "|" + listaJugadores[i].IdUsuario;
                }

            }
            
            NameValueCollection parametros = new NameValueCollection
            {
           { "nombre", nombreEquipo.Text },
           { "jugadores", jugadores }
       };


           equipo  nuevoEquipo = new equipo(ContenedorComun.pedirPost("crearequipo", parametros));
            ContenedorComun.misEquipos.Add(nuevoEquipo.IdEquipo,nuevoEquipo);
            

            Intent nuevaVentana = new Intent(this, typeof(datosEquipoActivity));
            nuevaVentana.PutExtra("id", nuevoEquipo.IdEquipo);
            StartActivity(nuevaVentana);
            Finish();

        }

        private void BotonInvitarOnClick(object sender, EventArgs eventArgs)
        {
            Intent nuevoIntent = new Intent(this, typeof(buscarJugadoresActivity));
            StartActivityForResult(nuevoIntent, 100);
        }



        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {

  
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                int returnValue = data.GetIntExtra("jugadorID", 0);
                

                if (!jugadores.ContainsKey(returnValue) && ContenedorComun.datos.IdUsuario!=returnValue)
                {
                    jugadores.Add(returnValue, ContenedorComun.jugadoresCache[returnValue]);
                    ListView listView = FindViewById<ListView>(Resource.Id.listJugadores);
                    listView.Adapter = new adapterJugador(this, jugadores.Values.ToList());
                    
                }
                else
                {
                    Toast.MakeText(this, "El jugador ya habia agregado al usuario.", ToastLength.Long).Show();
                }

            }
            
        }

        private void ListViewOnItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            AlertDialog.Builder confirmacion = new AlertDialog.Builder(this);

            confirmacion.SetTitle("Desea eliminar al jugador?");
            confirmacion.SetPositiveButton("Si, eliminar", delegate
            {

                var listView = sender as ListView;
                var t = ((adapterJugador)listView.Adapter).GetJugador(e.Position);
                jugadores.Remove(t.IdUsuario);
                listView.Adapter = new adapterJugador(this, jugadores.Values.ToList());


            });
            confirmacion.SetNegativeButton("No", delegate { });
            confirmacion.Show();
        }
    }
}