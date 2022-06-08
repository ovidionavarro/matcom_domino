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
            IPlayer<int> r = new PlayerRandomint9(table);
            IPlayer<int> t = new PlayerRandomint9(table);

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
                
            }
        }
    }
}