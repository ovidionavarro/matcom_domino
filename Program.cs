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
            PlayerBotaGorda p = new PlayerBotaGorda(table);
            p.SortHand();

            c.Jugadores.Add(r);
            c.Jugadores.Add(t);
            c.Jugadores.Add(p);
            c.GeneratedCards();
            c.RepartirFichas();

            foreach (var ficha in p.ManoDeFichas)
            {
             Console.Write(ficha+", ");   
            }

            Console.ReadLine();
            
            p.SortHand();
            
            Console.WriteLine("Se ordeno la mano");
            foreach (var ficha in p.ManoDeFichas)
            {
                Console.Write(ficha+", ");   
            }






        }
    }
}