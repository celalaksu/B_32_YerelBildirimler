using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YerelBildirimler.Arabirimler;

namespace YerelBildirimler
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IYerelBildirimYoneticisi yerelBildirimYoneticisi;
        int bildirimNumarasi = 0;

        public MainPage()
        {
            InitializeComponent();

            yerelBildirimYoneticisi = DependencyService.Get<IYerelBildirimYoneticisi>();
            yerelBildirimYoneticisi.YerelBildirimAlindi += (sender, eventArgs) =>
            {
                var olayVerisi = (BildirimOlayiArgumanlari)eventArgs;
                BildirimiGoster(olayVerisi.BasklikArg, olayVerisi.MesajArg);
            };
        }

        void BildirimiGoster(string bildirimBasligi, string bildirimMesaji)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Bildirim alındı:\nBaşlık: {bildirimBasligi}\nMessage: {bildirimMesaji}"
                };
                stackLayout.Children.Add(msg);
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            bildirimNumarasi++;
            string bildirimBasligi = $"Yerel Bildirim #{bildirimNumarasi}";
            string bildirimMesaji = $" {bildirimNumarasi} adet yerel bildiriminiz var!";
            yerelBildirimYoneticisi.BildirimiZamanla(bildirimBasligi, bildirimMesaji);
        }
    }
}
