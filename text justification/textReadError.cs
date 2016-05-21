using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace textjustification
{
    public class dosyadanOkumaHatasi : System.Exception
    {
        public dosyadanOkumaHatasi()
            : base("Dosyadan Okuma Hatasi")
        {
        }
        public dosyadanOkumaHatasi(string str)
            : base(str)
        {
        }
    }
}
