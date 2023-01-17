using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OLIO_KolmasPalautus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool jatkuu, poistoJatkuu;
            
            do
            {
                string polku = @"MunTiimi.json tiedoston polku tähän";
                List<MunTiimi> munTiimit = new List<MunTiimi>();
                

                using (StreamReader streamReader = new StreamReader(polku))
                {
                    var jsonMerkkijono = streamReader.ReadToEnd();
                    munTiimit = JsonConvert.DeserializeObject<List<MunTiimi>>(jsonMerkkijono);
                }

                    Console.Write("Tervetuloa muokkaamaan tiedostoa MunTiimi. Valitse vaihtoehdoista 1-5.\n \n" +
                    "Syötä 1, jos haluat nähdä kaikki pelaajat.\n" +
                    "Syötä 2, jos haluat lisätä uuden pelaajan tiimiisi.\n" +
                    "Syötä 3, jos haluat muuttaa pelaajan tietoja\n" +
                    "Syötä 4, jos haluat poistaa tietoja.\n" +
                    "Syötä 5, jos et halua tehdä mitään. " +
                    "\nValinta: ");

                    string annettu = Console.ReadLine();
                    int valittuNumero;
                    while (!Int32.TryParse(annettu, out valittuNumero) && valittuNumero > 0 && valittuNumero <= 5)
                    {
                        Console.Write("Väärä valinta yritä uudelleen 1-5.");
                        annettu = Console.ReadLine();
                    }
                    Console.WriteLine();

                if (valittuNumero == 1)
                {
                    //Katsotaan mitä tiedosto sisältää..
                    Console.WriteLine();
                    Console.WriteLine("Pelaajatiedoston sisältö:");
                    Console.WriteLine();
                    foreach (var item in munTiimit)
                    {

                        Console.WriteLine("Nimi: {0}, syntymäaika: {1}, pelipaikka: {2}, pelinumero: {3}", item.Nimi, item.SyntymaAika.ToShortDateString(), item.PelaajaTiedot[0].Pelipaikka, item.PelaajaTiedot[0].Pelinumero);


                    }
                    Console.WriteLine();

                }
                else if (valittuNumero == 2)
                {
                    //Uuden pelaajan luonti..
                    Console.WriteLine("Lisää uusi pelaaja: ");
                    Console.WriteLine();

                    Console.Write("Anna nimi: ");
                    string nimi = Console.ReadLine();
                    

                    Console.Write("Anna syntymäaika: ");
                    DateTime dateTime;
                    annettu = Console.ReadLine();
                    while (!DateTime.TryParse(annettu, out dateTime))
                    {
                        Console.Write("Ei kelpoinen, yritä uudelleen: ");
                        annettu = Console.ReadLine();
                    }
                    

                    Console.Write("Anna pelipaikka: ");
                    string peliPaikka = Console.ReadLine();
                    

                    Console.Write("Anna pelinumero: ");
                    string peliNumero = Console.ReadLine();
                    

                    List<PelaajaTiedot> pelaajaTiedots = new List<PelaajaTiedot>()
                    {
                        new PelaajaTiedot(peliPaikka, peliNumero)
                    };
                    
                    munTiimit.Add(new MunTiimi(nimi, dateTime, pelaajaTiedots));

                }
                else if (valittuNumero == 3)
                {
                    //Tietojen muokkaus
                    var valittu = munTiimit.Where(asi => asi.Nimi.Contains(""));
                    int lkm = munTiimit.Count();
                    if (valittu.Any())
                    {
                        
                        for (int i = 0; i < lkm; i++)
                        {
                            if (munTiimit[i].Nimi.Contains(""))
                            {
                                Console.WriteLine("Tiedostossa on pelaaja {0}, jonka syntymäpäivä on {1}, pelipaikka: {2}, pelinumero: {3}.", munTiimit[i].Nimi, munTiimit[i].SyntymaAika.ToShortDateString(), munTiimit[i].PelaajaTiedot[0].Pelipaikka, munTiimit[i].PelaajaTiedot[0].Pelinumero);
                                Console.Write("Haluatko muuttaa tietoja (K/E)?: ");
                                string annettuKirjain = Console.ReadLine().ToUpper();
                                if (annettuKirjain.StartsWith("K"))
                                {
                                    //Olet täällä vain, jos on valittu tietojen muuttaminen.
                                    Console.WriteLine("Seuraavaksi käydään kaikki pelaajat läpi.");
                                    Console.WriteLine("Jos et muuta jotakin tietoa, valitse ENTER siinä kohdin.");
                                    Console.WriteLine();

                                    Console.Write("Anna uusi nimi: ");
                                    string annettuNimi = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(annettuNimi))
                                        munTiimit[i].Nimi = annettuNimi;
                                    Console.WriteLine();

                                    Console.Write("Anna uusi syntymäpäivä: ");
                                    DateTime annettuPaiva;
                                    annettu = Console.ReadLine();
                                    if (string.IsNullOrEmpty(annettu))
                                    {
                                        
                                    }
                                    else
                                    {
                                        while (!DateTime.TryParse(annettu, out annettuPaiva))
                                        {
                                            Console.Write("Ei kelpoinen, yritä uudelleen: ");
                                            annettu = Console.ReadLine();

                                        }
                                        munTiimit[i].SyntymaAika = annettuPaiva;
                                    }
                                
                                    Console.WriteLine();

                                    Console.Write("Anna uusi pelipaikka: ");
                                    string annettuSahkoposti = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(annettuSahkoposti))
                                        munTiimit[i].PelaajaTiedot[0].Pelipaikka = annettuSahkoposti;
                                    Console.WriteLine();

                                    Console.Write("Anna uusi pelinumero: ");
                                    string annettuNumero = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(annettuNumero))
                                        munTiimit[i].PelaajaTiedot[0].Pelinumero = annettuNumero;
                                    Console.WriteLine();

                                }
                                else
                                    Console.WriteLine("Pelaajan {0} tietoja ei muutettu.", munTiimit[i].Nimi);
                                Console.WriteLine();

                            }

                        }

                    }
                    else
                    {
                        Console.WriteLine("Ei löytynyt sen nimistä pelaajaa.");
                        Console.WriteLine();
                    }

                }
                else if (valittuNumero == 4)
                {
                    //Tietojen poisto, jostain syystä viimeistä ei pysty poistamaan...
                    do
                    {
                        Console.Write("Anna pelaajan nimi, jonka mahdollisesti poistat: ");
                        string annettuNimi = Console.ReadLine();


                        var valittu = munTiimit.Where(asi => asi.Nimi.Contains(annettuNimi));
                        int lkm = munTiimit.Count();
                        if (valittu.Any())
                        {
                            //Tänne jos oli etsityn nimisiä...
                            for (int i = 0; i < lkm; i++)
                            {
                                if (munTiimit[i].Nimi.Contains(annettuNimi))
                                {
                                    Console.WriteLine("Tiedostossa on pelaaja {0}, symtymäaika: {1}, pelipaikka: {2}, pelinumero: {3}.", munTiimit[i].Nimi, munTiimit[i].SyntymaAika.ToShortDateString(), munTiimit[i].PelaajaTiedot[0].Pelipaikka, munTiimit[i].PelaajaTiedot[0].Pelinumero);
                                    Console.Write("Poistetaanko pelaaja (K/E)?: ");
                                    annettu = Console.ReadLine().ToUpper();
                                    if (annettu.StartsWith("K"))
                                    {

                                        bool onnistui = munTiimit.Remove(munTiimit[i]);
                                        if (onnistui)                                                                                
                                            Console.WriteLine("Pelaaja on poistettu tiedostosta.", munTiimit[i].Nimi);
                                            lkm = lkm - 1;
                                            i = i - 1;
                                        

                                    }
                                    else
                                        Console.WriteLine("Pelaajaa {0} ei poistettu tiedostosta.", munTiimit[i].Nimi);
                                }

                            }

                        }
                        else
                        {
                            Console.WriteLine("Ei löytynyt sen nimistä pelaajaa. HUOMIO KIRJOITUSASU!");
                            Console.WriteLine();
                        }

                        Console.WriteLine();
                        Console.Write("Poistetaanko lisää pelaajia? K/E? ");
                        annettu = Console.ReadLine().ToUpper();
                        if (annettu.StartsWith("K"))
                            poistoJatkuu = true;
                        else
                            poistoJatkuu = false;
                    } while (poistoJatkuu);

                }
                else if (valittuNumero == 5)
                {
                        Console.WriteLine();
                        Console.WriteLine("Et tee mitään.");
                        Console.WriteLine();
                }
                else
                    Console.Write("Väärä valinta, yritä uudelleen.");
                Console.WriteLine();


                //Nyt näkyvät muutokset, jos niitä on tehty valinnoissa 2-4.
                using (StreamWriter streamWriter = new StreamWriter(polku, false))
                {
                    string jsonMerkkijono = JsonConvert.SerializeObject(munTiimit);
                    streamWriter.Write(jsonMerkkijono);
                }
                
                List<MunTiimi> lopussa = new List<MunTiimi>();

                if (valittuNumero == 2 || valittuNumero == 3 || valittuNumero == 4)
                {
                    using (StreamReader streamReader = new StreamReader(polku))
                    {
                        var jsonMerkkijono = streamReader.ReadToEnd();                       
                        munTiimit = JsonConvert.DeserializeObject<List<MunTiimi>>(jsonMerkkijono);
                    }

                    Console.WriteLine("\nTiedoston sisältö mahdollisten muutosten jälkeen:");
                    Console.WriteLine();

                    foreach (var item in munTiimit)
                    {
                        Console.WriteLine("Nimi: {0}, syntymäaika: {1}, pelipaikka: {2}, pelinumero: {3}.",
                            item.Nimi, item.SyntymaAika.ToShortDateString(), item.PelaajaTiedot[0].Pelipaikka, item.PelaajaTiedot[0].Pelinumero);
                    }
                }


                Console.WriteLine();
                Console.Write("Jatketaanko K/E?");
                
                string valinta = Console.ReadLine().ToUpper();
                if (valinta.StartsWith("K"))
                    jatkuu = true;
                else
                    jatkuu = false;
                Console.WriteLine();
            } while (jatkuu);
            
        }
    }
}
