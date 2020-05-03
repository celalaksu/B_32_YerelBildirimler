using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YerelBildirimler.Arabirimler;

namespace YerelBildirimler
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // use the dependency service to get a platform-specific implementation and initialize it
            DependencyService.Get<IYerelBildirimYoneticisi>().BildirimiHazirla();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
