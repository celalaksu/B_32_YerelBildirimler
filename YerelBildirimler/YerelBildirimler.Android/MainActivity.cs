using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using YerelBildirimler.Droid.Arabirimler;
using Xamarin.Forms;
using YerelBildirimler.Arabirimler;

namespace YerelBildirimler.Droid
{
    [Activity(Label = "YerelBildirimler", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            IntenttenBildirimOlustur(Intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            
        }
        
        protected override void OnNewIntent(Intent intent)
        {
            IntenttenBildirimOlustur(intent);
        }

        void IntenttenBildirimOlustur(Intent intent)
        {
            if (intent?.Extras != null)
            {
                string bildirimBasligi = intent.Extras.GetString(AndroidYerelBildirimYoneticisi.BaslikAnahtarı);
                string bildirimMesaji = intent.Extras.GetString(AndroidYerelBildirimYoneticisi.MesajAnahtarı);

                DependencyService.Get<IYerelBildirimYoneticisi>().BildirimiAl(bildirimBasligi, bildirimMesaji);
            }
        }
        
    }
}