using matcom_domino;
using System.Collections.Generic;

namespace matcom_domino
{
    class Program
    {
        static void Main(string[] args)
        {
            Domino<int> c = new Referee9();
            IMesa<int> table = new Mesa();
            IPlayer<int> r = new Player(table);
            IPlayer<int> t = new Player(table);

            c.Jugadores.Add(r);
            c.Jugadores.Add(t);
            c.GeneratedCards();
            c.RepartirFichas();

            Console.WriteLine(c.ConjuntodeFichas.Count());

            

            while (true)
            {
                Console.WriteLine("Fichas player");
                foreach (var k in r.ManoDeFichas)
                {
                    Console.Write(k + ", ");
                }
                Console.WriteLine("\nFichas en mesa");
                foreach (var a in table.CardinTable)
                {
                    Console.WriteLine(a);
                }

                int m = int.Parse(Console.ReadLine()); 
                r.Play(r.ManoDeFichas[m]);
                Console.WriteLine("\nFichas en mesa");
                
                foreach (var a in t.ManoDeFichas)
                {
                    Console.WriteLine(a);
                }
                int l = int.Parse(Console.ReadLine());
                t.Play(t.ManoDeFichas[l]);
                
            }
        }
    }
}