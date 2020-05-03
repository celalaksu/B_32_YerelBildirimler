using System;
using System.Collections.Generic;
using System.Text;

namespace YerelBildirimler
{
    public class BildirimOlayiArgumanlari : EventArgs
    {
        public string BasklikArg { get; set; }
        public string MesajArg { get; set; }
    }
}
