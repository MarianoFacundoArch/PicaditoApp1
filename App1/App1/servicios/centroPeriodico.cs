using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using App1.clasesObjetos;

namespace App1.Activities
{
    [Service]
    public class centroPeriodico : Service
    {

        static readonly int TimerWait = 4000;
        Timer timer;
        DateTime startTime;
        bool isStarted = false;

        public override void OnCreate()
        {
            base.OnCreate();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {

            if (isStarted)
            {
                TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);

            }
            else
            {
                startTime = DateTime.UtcNow;

                timer = new Timer(HandleTimerCallback, startTime, 0, TimerWait);
                isStarted = true;
            }
            return StartCommandResult.NotSticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            // This is a started service, not a bound service, so we just return null.
            return null;
        }


        public override void OnDestroy()
        {
            timer.Dispose();
            timer = null;
            isStarted = false;

            TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);

            base.OnDestroy();
        }
         int notificationId = 0;
        void HandleTimerCallback(object state)
        {

            string notificaciones = ContenedorComun.pedirSitio("damenotificaciones");

            if (notificaciones.StartsWith(ContenedorComun.accionCorrecta))
            {
                notificaciones = ContenedorComun.removerCabeceraCorrecto(notificaciones);

                foreach (string equipoActual in ContenedorComun.parseJsonList(notificaciones))
                {
                    notificacion tmp = new notificacion(equipoActual);
                    /*
                     * Creo clase y la cargo en el diccionario
                     * */

                    Notification.Builder builder = new Notification.Builder(this)
        .SetContentTitle(ContenedorComun.tituloAplicacion)
        .SetContentText(tmp.TextoNotificacion)
        .SetSmallIcon(Resource.Drawable.Icon);
                    // Build the notification:
                    Notification notification = builder.Build();

                    // Get the notification manager:
                    NotificationManager notificationManager =
                        GetSystemService(Context.NotificationService) as NotificationManager;

                    // Publish the notification:
                    
                    notificationManager.Notify(++notificationId, notification);


                    //Toast.MakeText(Android.App.Application.Context, tmp.Nombre, ToastLength.Long).Show();

                }


            }
            
            TimeSpan runTime = DateTime.UtcNow.Subtract(startTime);

        }
    }
}