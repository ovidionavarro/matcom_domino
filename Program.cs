using matcom_domino;
using System.Collections.Generic;

namespace matcom_domino
{
    /*class PlayerNumber : IPlayer<int>
     {
         public IEnumerable<IFichas<int>> HandCards { get ; set; }

         public PlayerNumber()
         {
             HandCards = new List<IFichas<int>>();
         }

         public IFichas<int> SelectCard()
         {
             return HandCards.ToArray()[0];
         }
     }*/

    // class PlayerString : IPlayer<string>
    // {
    //     public IEnumerable<IFichas<string>> Cards { get ; set; }

    //     public IFichas<string> SelectCard()
    //     {
    //         return Cards.ToArray()[0];            
    //     }
    // }


    class Program
    {
        static void Main(string[] args)
        {
            Domino<int> c = new Referee9();
            IPlayer<int> r = new PlayerRandomint9();
            IPlayer<int> t = new PlayerRandomint9();
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
                Console.Write(k +", ");
                
            }
        }
    }
}