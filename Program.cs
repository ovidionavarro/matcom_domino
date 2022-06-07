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

            foreach (var a in c.ConjuntodeFichas)
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("Fichas player");
            foreach (var k in r.ManoDeFichas)
            {
                Console.Write(k + ", ");
            }
        }
    }
}