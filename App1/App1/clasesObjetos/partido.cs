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
    public class partido
    {


        private int id_partido;
        private int id_equipo1;
        private int id_equipo2;
        private int canchaEstado;
        private int estado;
        private int id_cancha;
        private int competitivo;
        private DateTime fecha;


        public partido(string json)
        {

            if (json.EndsWith("}}"))
            {
                json = json.Substring(0, json.Length - 1);
            }

            JObject o = JObject.Parse(json);
            id_partido = (int) o["id_partido"];
            id_equipo1 = (int) o["id_equipo1"];
            id_equipo2 = (int) o["id_equipo2"];
            id_cancha = (int) o["id_cancha"];
            estado = (int) o["estado"];
            canchaEstado = (int) o["cancha_estado"];
            competitivo = (int) o["competitivo"];

            if (!String.IsNullOrEmpty((string) o["fecha"]))
            {
                fecha = (DateTime) o["fecha"];
            }
            else
            {
                fecha = new DateTime();
            }

        }

        public int IdPartido
        {
            get { return id_partido; }
            set { id_partido = value; }
        }

        public int IdEquipo1
        {
            get { return id_equipo1; }
            set { id_equipo1 = value; }
        }

        public int IdEquipo2
        {
            get { return id_equipo2; }
            set { id_equipo2 = value; }
        }

        public int CanchaEstado
        {
            get { return canchaEstado; }
            set { canchaEstado = value; }
        }

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public int IdCancha
        {
            get { return id_cancha; }
            set { id_cancha = value; }
        }

        public int Competitivo
        {
            get { return competitivo; }
            set { competitivo = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
    }
}