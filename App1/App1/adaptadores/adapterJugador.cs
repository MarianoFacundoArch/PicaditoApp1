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
    public class adapterJugador : BaseAdapter<jugador>
    {
        List<jugador> items;
        Activity context;

        Dictionary<string, Bitmap> imagenesCargadas = new Dictionary<string, Bitmap>();

        public adapterJugador(Activity context, List<jugador> items)
       : base()
        {
           
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public jugador GetJugador(int position)
        {
            return items[position];
        }

        public List<jugador> GetJugadores()
        {
            return items;
        }

        public Bitmap GetImagen(string imagen)
        {
            //TODO: Verificar existencia
            if (imagenesCargadas.ContainsKey(imagen))
            {
                return imagenesCargadas[imagen];
            }
            else
            {
                Bitmap loadingImagen = Bitmap.CreateScaledBitmap(BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.jugadorunk), 200, 200, false);
                return loadingImagen;
            }
            

        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.rowJugador, null);
            view.FindViewById<TextView>(Resource.Id.textNombreJugador).Text = item.Nombre;
            view.FindViewById<TextView>(Resource.Id.textDatosJugador).Text = "mail = " + item.Mail ;
            view.FindViewById<TextView>(Resource.Id.textIdJugador).Text = "Id jugador = " + item.IdUsuario;

            //TODO: Hace 3 dias... ponerlo asi.

            if (!string.IsNullOrEmpty(item.Imagen))
            {
                Bitmap imageBitmap;

                if (imagenesCargadas.ContainsKey(item.Imagen))
                {
                    imageBitmap = imagenesCargadas[item.Imagen];
                    view.FindViewById<ImageView>(Resource.Id.imageJugador).SetImageBitmap(imageBitmap);
                }

                else
                {

                    if (ContenedorComun.imagenesCache.ContainsKey(item.Imagen))
                    {
                        imagenesCargadas.Add(item.Imagen, ContenedorComun.imagenesCache[item.Imagen]);
                        imageBitmap = ContenedorComun.imagenesCache[item.Imagen];
                        view.FindViewById<ImageView>(Resource.Id.imageJugador).SetImageBitmap(imageBitmap);
                    }
                    else
                    {

                        //imageBitmap = ContenedorComun.GetImageBitmapFromUrl(item.Imagen);

                    WebClient cliente = new WebClient();
                        Bitmap loadingImagen = Bitmap.CreateScaledBitmap(BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.jugadorunk), 200, 200, false);
                        view.FindViewById<ImageView>(Resource.Id.imageJugador).SetImageBitmap(loadingImagen);

                        cliente.DownloadDataCompleted += delegate(object sender, DownloadDataCompletedEventArgs args)
                        {
                            Bitmap imagenDescarga = null;
                            
                            using (var webClient = new WebClient())
                            {
                                var imageBytes = webClient.DownloadData(item.Imagen);
                                if (imageBytes != null && imageBytes.Length > 0)
                                {
                                    imagenDescarga = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                                    Bitmap imagenFinal = Bitmap.CreateScaledBitmap(imagenDescarga, 200, 200, false);


                                    view.FindViewById<ImageView>(Resource.Id.imageJugador).SetImageBitmap(imagenFinal);
                                    if (!imagenesCargadas.ContainsKey(item.Imagen))
                                        imagenesCargadas.Add(item.Imagen, imagenFinal);

                                }
                            }
                        };

                        cliente.DownloadDataAsync(new Uri(item.Imagen));


                    }


                    
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

        public override jugador this[int position]
        {
            get { return items[position]; }
        }
    }
}