using System;
using Newtonsoft.Json.Linq;

namespace App1
{
    public class jugador
    {
        private int id_usuario;
        private string nombre;
        private DateTime fecha_nacimiento;
        private DateTime fecha_alta;
        private string mail;
        private string pass;
        private string apodo;
        private string celular;
        private bool notificaciones_celular;
        private string ip;
        private string imei;
        private string imagen;

        public jugador(int idUsuario, string nombre, string mail, string imagen)
        {
            id_usuario = idUsuario;
            this.nombre = nombre;
            this.mail = mail;
            this.imagen = imagen;
        }

        public jugador(string json)
        {

            if (json.EndsWith("}}"))
            {
                json = json.Substring(0, json.Length - 1);
            }
            JObject o = JObject.Parse(json);
            id_usuario = (int)o["id_usuario"];

            nombre = (string)o["nombre"];




            if (!String.IsNullOrEmpty((string)o["fecha_alta"]))
            {
                fecha_alta = (DateTime)o["fecha_alta"];
            }
            else
            {
                fecha_alta = new DateTime();
            }


            if (!String.IsNullOrEmpty((string)o["fecha_nacimiento"]))
            {
                fecha_nacimiento = (DateTime)o["fecha_nacimiento"];
            }
            else
            {
                fecha_nacimiento = new DateTime();
            }


            mail = (string)o["mail"];
            pass = (string)o["pass"];
            apodo = (string)o["apodo"];
            celular = (string)o["celular"];
            imagen = (string)o["imagen"];
            notificaciones_celular = (String)o["notificaciones_celular"] == "1";

            ip = (string)o["ip"];
            imei = (string)o["imei"];


        }

        public int IdUsuario
        {
            get { return id_usuario; }
            set { id_usuario = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return fecha_nacimiento; }
            set { fecha_nacimiento = value; }
        }

        public DateTime FechaAlta
        {
            get { return fecha_alta; }
            set { fecha_alta = value; }
        }

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        public string Apodo
        {
            get { return apodo; }
            set { apodo = value; }
        }

        public string Celular
        {
            get { return celular; }
            set { celular = value; }
        }

        public bool NotificacionesCelular
        {
            get { return notificaciones_celular; }
            set { notificaciones_celular = value; }
        }

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public string Imei
        {
            get { return imei; }
            set { imei = value; }
        }

        public string Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }
    }
}