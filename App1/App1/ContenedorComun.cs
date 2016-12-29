using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    public class ContenedorComun
    {
        public static datosUsuario datos;

        public static string urlsite = "http://192.168.1.107/picadito/PicaditoWeb/";
        public static string loginCorrectoInicio = "correcto:";
        public static string loginIncorrecto = "invalidlogin";

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
    }


}