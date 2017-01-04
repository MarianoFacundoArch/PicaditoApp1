using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
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
    class ContenedorComun
    {
        public static jugador datos;

        public static string urlsite = "http://192.168.1.109/picadito/PicaditoWeb/";
        public static string accionCorrecta = "correcto:";
        public static string loginIncorrecto = "invalidlogin";
        public static string tituloAplicacion = "Picadito";
        public static Dictionary<int, equipo> misEquipos = new Dictionary<int, equipo>();

        /*
         * Elementos de cache re utilizables
         * */
        public static Dictionary<int, jugador> jugadoresCache = new Dictionary<int, jugador>();
        public static Dictionary<string, Bitmap> imagenesCache = new Dictionary<string, Bitmap>();

        public static Dictionary<int, equipo> equiposCache = new Dictionary<int, equipo>(); //TODO:Implementar cache de adversarios recientes. NO ESTA EN USO AHORA

        //TODO: Limpiar caches cada x tiempo



        public static equipo dameEquipo(int id_equipo)
        {
            if (!ContenedorComun.equiposCache.ContainsKey(id_equipo))
            {
                string equipoQuery = ContenedorComun.pedirSitio("dameinfoequipo", "id_equipo=" + id_equipo);
                if (equipoQuery.StartsWith(ContenedorComun.accionCorrecta))
                {
                    equipoQuery = ContenedorComun.removerCabeceraCorrecto(equipoQuery);
                    equipo tmp = new equipo(equipoQuery);

                    ContenedorComun.equiposCache.Add(tmp.IdEquipo, tmp);
                }

            }

            return equiposCache[id_equipo];
        }

        public static string removerCabeceraCorrecto(string query)
        {
            query = query.Substring(ContenedorComun.accionCorrecta.Length,
                            query.Length - ContenedorComun.accionCorrecta.Length);
            return query;
        }

        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            try
            {


                Bitmap imageBitmap = null;

                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }


                return imageBitmap;
            }
            catch (Exception e)
            {
                return null;
                //throw;
            }
        }


        public static void agregarEquipo(equipo nuevoEquipo, bool propio = false)
        {
            if (!misEquipos.ContainsKey(nuevoEquipo.IdEquipo) && propio)
                ContenedorComun.misEquipos.Add(nuevoEquipo.IdEquipo, nuevoEquipo);

            if (!equiposCache.ContainsKey(nuevoEquipo.IdEquipo))
                ContenedorComun.equiposCache.Add(nuevoEquipo.IdEquipo, nuevoEquipo);
        }

        public static bool cargarEquipos()
        {
            try
            {

            
            string equiposQuery = ContenedorComun.pedirSitio("dameequipos");

                foreach (string equipoActual in ContenedorComun.parseJsonList(equiposQuery))
                {
                    equipo tmp = new equipo(equipoActual);
                    /*
                     * Creo clase y la cargo en el diccionario
                     * */

                    agregarEquipo(tmp,true);


                    //Toast.MakeText(Android.App.Application.Context, tmp.Nombre, ToastLength.Long).Show();

                }
                return true;
            }
            catch (Exception e)
            {
                return false;
                //throw;
            }
        }
        public static string pedirSitio(string accion, Dictionary<string, string> parametros)
        {
            try
            {
                HttpWebRequest peticionLogin = HttpWebRequest.CreateHttp(urlsite + "backend.php?mail=" + datos.Mail + "&pass=" + datos.Pass + "&consulta=" + accion);
                WebResponse respuestaObj = peticionLogin.GetResponse();
                Stream lectura = respuestaObj.GetResponseStream();
                StreamReader lecturaFinal = new StreamReader(lectura);

                return lecturaFinal.ToString();
            }
            catch (Exception e)
            {
                return "error:" + e;

            }
        }

        public static string pedirSitio(string accion, string parametros)
        {
            try
            {
                HttpWebRequest peticionLogin = HttpWebRequest.CreateHttp(urlsite + "backend.php?mail=" + datos.Mail + "&pass=" + datos.Pass + "&consulta=" + accion + "&" + parametros);
                WebResponse respuestaObj = peticionLogin.GetResponse();
                Stream lectura = respuestaObj.GetResponseStream();
                StreamReader lecturaFinal = new StreamReader(lectura);

                return lecturaFinal.ReadToEnd();
            }
            catch (Exception e)
            {
                return "error:" + e;

            }
        }


        public static string pedirPost(string accion, NameValueCollection parametros, bool incluirLogin = true)
        {
            try
            {

                using (WebClient client = new WebClient())
                {
                    byte[] response;
                    if (incluirLogin)
                    {
                        response =
                   client.UploadValues(urlsite + "backend.php?mail=" + datos.Mail + "&pass=" + datos.Pass + "&consulta=" + accion, parametros);
                    }
                    else
                    {
                        response =
                   client.UploadValues(urlsite + "backend.php?consulta=" + accion, parametros);
                    }

                    string result = System.Text.Encoding.UTF8.GetString(response);
                    return result;
                }

            }
            catch (Exception e)
            {
                return "error:" + e;

            }
        }


        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public static string pedirSitio(string accion)
        {
            try
            {
                HttpWebRequest peticionLogin = HttpWebRequest.CreateHttp(urlsite + "backend.php?mail=" + datos.Mail + "&pass=" + datos.Pass + "&consulta=" + accion);
                WebResponse respuestaObj = peticionLogin.GetResponse();
                Stream lectura = respuestaObj.GetResponseStream();
                StreamReader lecturaFinal = new StreamReader(lectura);

                return lecturaFinal.ReadToEnd();
            }
            catch (Exception e)
            {
                return "error:" + e;

            }
        }


        public static string[] parseJsonList(string datos)
        {
            datos = datos.Substring(1, datos.Length - 2);
            string[] partes = datos.Split(new string[] { "}," }, StringSplitOptions.None);

            string[] resultado = new string[partes.Length];
            int cnt = 0;
            foreach (string parte in partes)
            {
                resultado[cnt++] = parte + "}";
                
            }


            return resultado;
        }
    }


}