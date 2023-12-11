using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppSzamla
{
    public class Tetel
    {
        String termekNev;
        int egysegAr;
        String egyseg;
        double mennyiseg;

        public Tetel(string termekNev, int egysegAr, string egyseg, double mennyiseg)
        {
            this.termekNev = termekNev;
            this.egysegAr = egysegAr;
            this.egyseg = egyseg;
            this.mennyiseg = mennyiseg;
        }

        public string TermekNev { get => termekNev; }
        public int EgysegAr { get => egysegAr;}
        public string Egyseg { get => egyseg; }
        public double Mennyiseg { get => mennyiseg; }
    }
}
