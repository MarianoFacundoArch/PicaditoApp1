using System;
using System.Collections.Generic;
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

        public static string urlsite = "http://192.168.1.107/picadito/PicaditoWeb/";
        public static string loginCorrectoInicio = "correcto:";
        public static string loginIncorrecto = "invalidlogin";

        public static Dictionary<string, Bitmap> imagenesCargadas = new Dictionary<string, Bitmap>();
        public static Dictionary<int, equipo> misEquipos = new Dictionary<int, equipo>();



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

                    misEquipos.Add(tmp.IdEquipo, tmp);


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