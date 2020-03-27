using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuvar
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("fuvar.csv");
            List<Ut> Utak = new List<Ut>();
            string fejlec = reader.ReadLine();
            string sor;
            while ((sor = reader.ReadLine()) != null)
            {
                Utak.Add(new Ut(sor));
            }
            reader.Close();

            Console.WriteLine($"3. feladat: {Utak.Count} fuvar");

            Console.Write($"4. feladat: ");
            int db = 0;
            float bevetel = 0;
            foreach (var item in Utak)
            {
                if (item.TaxiId == 6185)
                {
                    db++;
                    bevetel = bevetel + item.Viteldij + item.Borravalo;
                }
            }

            Console.WriteLine($"{db} fuvar alatt {bevetel} $");

            Console.WriteLine($"5. feladat: ");
            Dictionary<string, int> szamlaloFizModonkent = new Dictionary<string, int>();
            foreach (var item in Utak)
            {
                if (szamlaloFizModonkent.ContainsKey(item.FizetesModja))
                {
                    szamlaloFizModonkent[item.FizetesModja]++;
                }
                else
                {
                    szamlaloFizModonkent.Add(item.FizetesModja, 1);
                }
            }

            foreach (var item in szamlaloFizModonkent)
            {
                Console.WriteLine($"\t{item.Key}: {item.Value}");
            }

            Console.Write($"6. feladat: ");
            float osszkm = 0;
            foreach (var item in Utak)
            {
                osszkm += item.Tavolsag * (float)1.6;
            }
            Console.WriteLine($"{osszkm:F2} km");

            Console.WriteLine($"7. feladat: ");

            Ut maxFuvar = null;


            maxFuvar = Utak.OrderByDescending(x => x.Idotartam).First();
            Console.WriteLine($"\tA fuvar hossza: {maxFuvar.Idotartam} ");
            Console.WriteLine($"\tTaxi azonosító: {maxFuvar.TaxiId}");
            Console.WriteLine($"\tMegtett távolság: {maxFuvar.Tavolsag}");
            Console.WriteLine($"\tViteldíj: {maxFuvar.Viteldij}");

            Console.WriteLine("hibak.txt......");

            StreamWriter writer = new StreamWriter("hibak.txt");
            List<string> hibaLista = new List<string>();
            writer.WriteLine(fejlec);
            Utak = Utak.OrderBy(x => x.IndulasIdeje).ToList();
            foreach (var item in Utak)
            {
                if ((item.Idotartam > 0 && item.Tavolsag == 0) && item.Viteldij > 0)
                {
                    writer.WriteLine($"{item.TaxiId};{item.IndulasIdeje};{item.Idotartam};{item.Tavolsag};" +
                        $"{item.Viteldij};{item.Borravalo};{item.FizetesModja}");

                }
            }
            writer.Close();
            Console.ReadKey();
        }
    }
}