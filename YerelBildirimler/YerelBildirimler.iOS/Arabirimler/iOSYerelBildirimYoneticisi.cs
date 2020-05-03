using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using UserNotifications;
using Xamarin.Forms;
using YerelBildirimler.Arabirimler;

[assembly: Dependency(typeof(YerelBildirimler.iOS.Arabirimler.iOSYerelBildirimYoneticisi))]
namespace YerelBildirimler.iOS.Arabirimler
{
    public class iOSYerelBildirimYoneticisi : IYerelBildirimYoneticisi
    {
        int mesajID = -1;

        bool bildirimIzniVarMi;

        public event EventHandler YerelBildirimAlindi;

        public void Initialize()
        {
            // request the permission to use local notifications
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) =>
            {
                bildirimIzniVarMi = approved;
            });
        }

        public int BildirimiZamanla(string bildirimBasligi, string bildirimMesaji)
        {
           

            // EARLY OUT: app doesn't have permissions
            if (!bildirimIzniVarMi)
            {
                return -1;
            }

            mesajID++;

            var bildirimIcerigi = new UNMutableNotificationContent()
            {
                Title = bildirimBasligi,
                Subtitle = "",
                Body = bildirimMesaji,
                Badge = 1
            };

            // Local notifications can be time or location based
            // Create a time-based trigger, interval is in seconds and must be greater than 0
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(0.25, false);

            var istek = UNNotificationRequest.FromIdentifier(mesajID.ToString(), bildirimIcerigi, trigger);
            UNUserNotificationCenter.Current.AddNotificationRequest(istek, (hata) =>
            {
                if (hata != null)
                {
                    throw new Exception($"Bildirim başarısız oldu: {hata}");
                }
            });

            return mesajID;
        }

        public void BildirimiAl(string bildirimBasligi, string bildirimMesaji)
        {
            var olayVerisi = new BildirimOlayiArgumanlari()
            {
                BasklikArg = bildirimBasligi,
                MesajArg = bildirimMesaji,
            };
            YerelBildirimAlindi?.Invoke(null, olayVerisi);
        }

        public void BildirimiHazirla()
        {
            // request the permission to use local notifications
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (onaylandi, hata) =>
            {
                bildirimIzniVarMi = onaylandi;
            });
        }

 
    }
}