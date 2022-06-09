using matcom_domino;
using System.Collections.Generic;

namespace matcom_domino
{
    class Program
    {
        static void Main(string[] args)
        {
            // Inicializando Objetos del Juego
            Domino<int> c = new Referee9();
            IMesa<int> table = new Mesa();
            IPlayer<int> r = new Player(table);
            PlayerBotaGorda t = new PlayerBotaGorda(table);
            PlayerBotaGorda p = new PlayerBotaGorda(table);
            Player yo = new Player(table);
            p.SortHand();
            
            //Agregando Jugadores al Juego
            c.Jugadores.Add(yo);
            c.Jugadores.Add(r);
            c.Jugadores.Add(t);
            c.Jugadores.Add(p);
            
            //Generando las Fichas del juego    
            c.GeneratedCards();
            
            // Repartiendo las fichas
            c.RepartirFichas();

            
            
            while (true)
            {
                foreach (var ficha in yo.ManoDeFichas)
                {
                    Console.Write(ficha+", ");
                }

                int q = int.Parse(Console.ReadLine());
                
                yo.Play(yo.ManoDeFichas[q]);
                foreach (var ficha in table.CardinTable)
                {
                    Console.Write(ficha+", ");
                }
                
                Console.WriteLine("Player 1 Jugo!");
                p.SelectCard();
                Console.WriteLine("Mesa");
                foreach (var ficha in table.CardinTable)
                {
                    Console.Write(ficha+", ");
                }
                Console.WriteLine("\n"+"Mano Player 1");
                foreach (var ficha in p.ManoDeFichas)
                {
                    Console.Write(ficha + ", ");
                }
                
                string k = Console.ReadLine();
                
                Console.WriteLine("Player 2 Jugo!!");
                t.SelectCard();
                Console.WriteLine("Mesa");
                foreach (var ficha in table.CardinTable)
                {
                    Console.Write(ficha+", ");
                }
                Console.WriteLine("\n"+"Mano Player 2");
                foreach (var ficha in t.ManoDeFichas)
                {
                    Console.Write(ficha + ", ");
                }
                
                k = Console.ReadLine();
                
                Console.WriteLine();
                if (k == "-1")
                    break;
            }
        }
    }
}