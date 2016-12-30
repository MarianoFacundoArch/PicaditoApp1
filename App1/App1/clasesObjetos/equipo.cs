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

namespace App1
{
    public class equipo
    {
        private int id_equipo;
        private string nombre;
        private int partidos_ganados;
        private int partidos_perdidos;
        private int partidos_empatados;
        private int partidos_suspendidos;
        private DateTime fecha_alta;
        private DateTime ultima_actividad;
        private string imagen;

        public equipo(string json)
        {

            if (json.EndsWith("}}"))
            {
                json = json.Substring(0, json.Length - 1);
            }

            JObject o = JObject.Parse(json);
            id_equipo = (int)o["id_equipo"];

            nombre = (string)o["nombre"];




            if (!String.IsNullOrEmpty((string)o["fecha_alta"]))
            {
                fecha_alta = (DateTime)o["fecha_alta"];
            }
            else
            {
                fecha_alta = new DateTime();
            }


            if (!String.IsNullOrEmpty((string)o["ultima_actividad"]))
            {
                ultima_actividad = (DateTime)o["ultima_actividad"];
            }
            else
            {
                ultima_actividad = new DateTime();
            }



            partidos_ganados = (int)o["partidos_ganados"];
            partidos_empatados = (int)o["partidos_empatados"];
            partidos_perdidos = (int)o["partidos_perdidos"];
            partidos_suspendidos = (int)o["partidos_suspendidos"];
            imagen = (string)o["imagen"];


        }


        public int IdEquipo
        {
            get { return id_equipo; }
            set { id_equipo = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int PartidosGanados
        {
            get { return partidos_ganados; }
            set { partidos_ganados = value; }
        }

        public int PartidosPerdidos
        {
            get { return partidos_perdidos; }
            set { partidos_perdidos = value; }
        }

        public int PartidosEmpatados
        {
            get { return partidos_empatados; }
            set { partidos_empatados = value; }
        }

        public int PartidosSuspendidos
        {
            get { return partidos_suspendidos; }
            set { partidos_suspendidos = value; }
        }

        public DateTime FechaAlta
        {
            get { return fecha_alta; }
            set { fecha_alta = value; }
        }

        public DateTime UltimaActividad
        {
            get { return ultima_actividad; }
            set { ultima_actividad = value; }
        }

        public string Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }
    }
}