using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Xamarin.Forms;
using YerelBildirimler.Arabirimler;


using AndroidApp = Android.App.Application;

[assembly: Dependency(typeof(YerelBildirimler.Droid.Arabirimler.AndroidYerelBildirimYoneticisi))]
namespace YerelBildirimler.Droid.Arabirimler
{
    public class AndroidYerelBildirimYoneticisi : IYerelBildirimYoneticisi
    {
        const string kanalID = "varsayilan";
        const string kanalAdi = "Varsayilan";
        const string kanalTanimi = "Bildirimler için varsayılan kanal";
        const int bekleyenIntentId = 0;

        public const string BaslikAnahtarı = "başlık";
        public const string MesajAnahtarı = "mesaj";

        bool kanalBaslatildi = false;
        int mesajID = -1;
        NotificationManager bildirimYonetimi;

        public event EventHandler YerelBildirimAlindi;

        public void BildirimiAl(string bildirimBasligi, string bildirimMesaji)
        {
            var olayVerisi = new BildirimOlayiArgumanlari()
            {
                BasklikArg = bildirimBasligi,
                MesajArg = bildirimMesaji,
            };
            YerelBildirimAlindi?.Invoke(null, olayVerisi);
        }

        public int BildirimiZamanla(string bildirimBasligi, string bildirimMesaji)
        {
            if (!kanalBaslatildi)
            {
                BildirimKanaliOlustur();
            }

            mesajID++;

            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(BaslikAnahtarı, bildirimBasligi);
            intent.PutExtra(MesajAnahtarı, bildirimMesaji);

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, bekleyenIntentId, intent, PendingIntentFlags.OneShot);

            NotificationCompat.Builder bildirimOlusturucu = new NotificationCompat.Builder(AndroidApp.Context, kanalID)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(bildirimBasligi)
                .SetContentText(bildirimMesaji)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.xamarinBlue))
                .SetSmallIcon(Resource.Drawable.xamarinBlue)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            var bildirim = bildirimOlusturucu.Build();
            bildirimYonetimi.Notify(mesajID, bildirim);

            return mesajID;
        }

        public void BildirimiHazirla()
        {
            BildirimKanaliOlustur();
        }

        void BildirimKanaliOlustur()
        {
            bildirimYonetimi = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);
            

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var javaKanalAdi = new Java.Lang.String(kanalAdi);
                var kanal = new NotificationChannel(kanalID, javaKanalAdi, NotificationImportance.Default)
                {
                    Description = kanalTanimi
                };
                bildirimYonetimi.CreateNotificationChannel(kanal);
            }

            kanalBaslatildi = true;
        }
    }
}