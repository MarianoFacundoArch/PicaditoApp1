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
                view.FindViewById<ImageView>(Resource.Id.imageEquipo).SetImageBitmap(imageBitmap);
            }
            else
            {
                if (ContenedorComun.imagenesCache.ContainsKey(item.Imagen))
                {
                    imagenesCargadas.Add(item.Imagen, ContenedorComun.imagenesCache[item.Imagen]);
                    imageBitmap = imagenesCargadas[item.Imagen];
                    view.FindViewById<ImageView>(Resource.Id.imageEquipo).SetImageBitmap(imageBitmap);
                }
                else
                {


                    WebClient cliente = new WebClient();
                    Bitmap loadingImagen = Bitmap.CreateScaledBitmap(BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.teamunk), 200, 200, false);
                    view.FindViewById<ImageView>(Resource.Id.imageEquipo).SetImageBitmap(loadingImagen);
                    cliente.DownloadDataCompleted += delegate (object sender, DownloadDataCompletedEventArgs args)
                    {
                        Bitmap imagenDescarga = null;

                        using (var webClient = new WebClient())
                        {
                            var imageBytes = webClient.DownloadData(item.Imagen);
                            if (imageBytes != null && imageBytes.Length > 0)
                            {
                                imagenDescarga = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                                Bitmap imagenFinal = Bitmap.CreateScaledBitmap(imagenDescarga, 200, 200, false);


                                view.FindViewById<ImageView>(Resource.Id.imageEquipo).SetImageBitmap(imagenFinal);
                                if (!imagenesCargadas.ContainsKey(item.Imagen))
                                    imagenesCargadas.Add(item.Imagen, imagenFinal);

                            }
                        }
                    };

                    cliente.DownloadDataAsync(new Uri(item.Imagen));


                }
                
            }

            


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