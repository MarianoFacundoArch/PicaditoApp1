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
using App1.adaptadores;
using App1.clasesObjetos;

namespace App1.Activities
{
    [Activity(Label = "partidosEquipoActivity")]
    public class partidosEquipoActivity : Activity
    {
        private int equipoId = 0;
        private equipo equipo;
        Dictionary<int, partido> partidosDelEquipo = new Dictionary<int, partido>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.partidosEquipo);
            equipoId = Intent.GetIntExtra("id", 0);
            equipo = ContenedorComun.misEquipos[equipoId]; //TODO: Cache de equipos comunes, etc si no fuera mi equipo y es de otra persona.
            TextView partidos = FindViewById<TextView>(Resource.Id.textPartidos);
            partidos.Text = "Partidos del equipo :" + equipo.Nombre;

            string partidosQuery = ContenedorComun.pedirSitio("damepartidos", "id_equipo=" + equipoId);
            if (partidosQuery.StartsWith(ContenedorComun.accionCorrecta))
            {

                partidosQuery = ContenedorComun.removerCabeceraCorrecto(partidosQuery);
                foreach (string partidoActual in ContenedorComun.parseJsonList(partidosQuery))
                {
                    partido tmp = new partido(partidoActual);
                    /*
                     * Creo clase y la cargo en el diccionario
                     * */

                    partidosDelEquipo.Add(tmp.IdPartido, tmp);



                    //Toast.MakeText(Android.App.Application.Context, tmp.Nombre, ToastLength.Long).Show();

                }
            }



            /*
 * Hora de la magia del listview
 * */
            //Initializing listview
            ListView listView = FindViewById<ListView>(Resource.Id.listPartidos);
            listView.Adapter = new adapterPartido(this, partidosDelEquipo.Values.ToList());



            // Create your application here
        }
    }
}