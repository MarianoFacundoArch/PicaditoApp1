using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1.clasesObjetos;

namespace App1.adaptadores
{
    public class adapterPartido : BaseAdapter<partido>
    {
        List<partido> items;
        Activity context;

        

        public adapterPartido(Activity context, List<partido> items)
       : base()
        {

            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public partido GetPartido(int position)
        {
            return items[position];
        }

        public List<partido> GetPartidos()
        {
            return items;
        }

       
        public override View GetView(int position, View convertView, ViewGroup parent)
        {


            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.rowPartido, null);


            string nombreEquipo1 = "unknown";
            string nombreEquipo2 = "unknown";
            try
            {


                
            

                nombreEquipo1 = ContenedorComun.dameEquipo(item.IdEquipo1).Nombre;
                nombreEquipo2 = ContenedorComun.dameEquipo(item.IdEquipo2).Nombre;
            }
            catch
            { }


            view.FindViewById<TextView>(Resource.Id.textNombreEquipo1).Text = "Equipo local: " + nombreEquipo1 + " (#" + item.IdEquipo1.ToString() + ")";
            view.FindViewById<TextView>(Resource.Id.textNombreEquipo2).Text = "Equipo VISITANTE: " + nombreEquipo2 + " (#" + item.IdEquipo2.ToString() + ")";
            view.FindViewById<TextView>(Resource.Id.textFecha).Text = "Fecha del partido: "+ item.Fecha.ToString();
            view.FindViewById<TextView>(Resource.Id.textPredio).Text = "Id de cancha: " + item.IdCancha.ToString();

            return view;
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override partido this[int position]
        {
            get { return items[position]; }
        }
    }
}