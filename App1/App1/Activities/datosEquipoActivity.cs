using Android.App;
using Android.OS;
using Android.Widget;

namespace App1.Activities
{
    [Activity(Label = "datosEquipoActivity")]
    public class datosEquipoActivity : Activity
    {
        private equipo equipoActual;
        private int equipoId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.datosEquipo);


            /*
             * Cargo el equipo del activity
             * */

            equipoId = Intent.GetIntExtra("id", -1);

            if (equipoId == -1) // Si hubiera algun error
            {
                Finish();
                return;
            }
                


            equipoActual = ContenedorComun.misEquipos[equipoId];

            TextView equipoNombre = FindViewById<TextView>(Resource.Id.textNombreEquipo);
            equipoNombre.Text = equipoActual.Nombre;



        }
    }
}