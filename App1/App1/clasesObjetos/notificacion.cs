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
using Newtonsoft.Json.Linq;

namespace App1.clasesObjetos
{
    class notificacion
    {


        private int id_notificacion; //
        private string textoNotificacion; //
        private int id_usuario; // 
        private int vista; //
        private string datosExtra; //
        private DateTime fecha; //


        public notificacion(string json)
        {

            if (json.EndsWith("}}"))
            {
                json = json.Substring(0, json.Length - 1);
            }

            JObject o = JObject.Parse(json);
            id_notificacion = (int)o["id_notificacion"];
            id_usuario = (int)o["id_usuario"];
            textoNotificacion = (string)o["textoNotificacion"];

            datosExtra = (string)o["datosExtra"];
            vista = (int)o["vista"];





            if (!String.IsNullOrEmpty((string)o["fecha"]))
            {
                fecha = (DateTime)o["fecha"];
            }
            else
            {
                fecha = new DateTime();
            }

            


        }

        public string TextoNotificacion
        {
            get { return textoNotificacion; }
            set { textoNotificacion = value; }
        }

        public int IdNotificacion
        {
            get { return id_notificacion; }
            set { id_notificacion = value; }
        }

        public int IdUsuario
        {
            get { return id_usuario; }
            set { id_usuario = value; }
        }

        public int Vista
        {
            get { return vista; }
            set { vista = value; }
        }

        public string DatosExtra
        {
            get { return datosExtra; }
            set { datosExtra = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
    }
}