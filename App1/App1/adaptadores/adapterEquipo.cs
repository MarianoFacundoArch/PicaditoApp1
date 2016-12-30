using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    public class adapterEquipo : BaseAdapter<equipo>
    {
        List<equipo> items;
        Activity context;
        Dictionary<string, Bitmap> imagenesCargadas = new Dictionary<string, Bitmap>();
        public adapterEquipo(Activity context, List<equipo> items)
       : base()
   {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public equipo GetEquipo(int position)
        {
            return items[position];
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.rowEquipo, null);
            view.FindViewById<TextView>(Resource.Id.textNombreEquipo).Text = item.Nombre;
            view.FindViewById<TextView>(Resource.Id.textDatosEquipo).Text = "PG = " + item.PartidosGanados + " - PE = " + item.PartidosEmpatados + " - PP = " + item.PartidosPerdidos;
            view.FindViewById<TextView>(Resource.Id.textIdEquipo).Text = "Id equipo = " + item.IdEquipo + " - Fecha alta = " + item.FechaAlta.ToString("dd-MM-yyyy") + " - Ultima vez = " + item.UltimaActividad.ToString("dd-MM-yyyy");

            Bitmap imageBitmap;

            //TODO: Hace 3 dias... ponerlo asi.
            if (imagenesCargadas.ContainsKey(item.Imagen))
            {
                imageBitmap = imagenesCargadas[item.Imagen];
            }
            else
            {
                imageBitmap = ContenedorComun.GetImageBitmapFromUrl(item.Imagen);
                if (imageBitmap != null)
                    imagenesCargadas.Add(item.Imagen, imageBitmap);
            }

            view.FindViewById<ImageView>(Resource.Id.imageEquipo).SetImageBitmap(Bitmap.CreateScaledBitmap(imageBitmap, 200, 200, false));


            return view;
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override equipo this[int position]
        {
            get { return items[position]; }
        }
    }
}