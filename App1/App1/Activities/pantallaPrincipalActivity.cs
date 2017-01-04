using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
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

            bool yaCargado = Intent.GetBooleanExtra("yacargado", false);

            string datosRecibidos = Intent.GetStringExtra("rta");


            if (!yaCargado)
            {

                /*
                 * Inicializo notificaciones
                 * 
                 * TODO: Que inicie el servicio cuando se prende telefono
                 * */
                StartService(new Intent(this, typeof(centroPeriodico)));



                /*
                 * Cargo datos del jugador
                 * */
                ContenedorComun.datos = new jugador(datosRecibidos);
                

                /*
                 * Cargo los equipos del jugador
                 * */
                ContenedorComun.cargarEquipos();

            }


            TextView textUsuario = FindViewById<TextView>(Resource.Id.textUsuario);
            textUsuario.Text = ContenedorComun.datos.Mail;
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