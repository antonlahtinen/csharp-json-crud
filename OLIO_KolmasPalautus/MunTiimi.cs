using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace OLIO_KolmasPalautus
{
    class MunTiimi
    {
        public MunTiimi(string nimi, DateTime syntymaAika, List<PelaajaTiedot> pelaajaTiedot)
        {
            Nimi = nimi;
            SyntymaAika = syntymaAika;
            PelaajaTiedot = pelaajaTiedot;
        }

        public string Nimi { get; set; }
        public DateTime SyntymaAika { get; set; }
        public List<PelaajaTiedot> PelaajaTiedot { get; set; }    
    
    }
    public class PelaajaTiedot
    {
        public PelaajaTiedot(string pelipaikka, string pelinumero)
        {
            Pelipaikka = pelipaikka;
            Pelinumero = pelinumero;
        }

        public string Pelipaikka { get; set; }
        public string Pelinumero { get; set; }

    }




}
