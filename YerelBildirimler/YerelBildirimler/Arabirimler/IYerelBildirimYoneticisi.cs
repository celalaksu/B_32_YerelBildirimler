using System;
using System.Collections.Generic;
using System.Text;

namespace YerelBildirimler.Arabirimler
{
    public interface IYerelBildirimYoneticisi
    {
        event EventHandler YerelBildirimAlindi;

        void BildirimiHazirla();

        int BildirimiZamanla(string bildirimBasligi, string bildirimMesaji);

        void BildirimiAl(string bildirimBasligi, string bildirimMesaji);

        

    }
}
