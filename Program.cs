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
            Mesa table = new Mesa();
            IPlayer<int> r = new Player(table);
            PlayerBotaGorda t = new PlayerBotaGorda(table);
            PlayerBotaGorda p = new PlayerBotaGorda(table);
            IPlayer<int> pramd = new PlayerRandom(table);
            p.SortHand();
            
            //Agregando Jugadores al Juego
            c.Jugadores.Add(pramd);
            c.Jugadores.Add(r);
            c.Jugadores.Add(t);
            c.Jugadores.Add(p);
            
            //Generando las Fichas del juego    
            c.GeneratedCards();
            
            // Repartiendo las fichas
            c.RepartirFichas();

            
            
            while (true)
            {
                foreach (var ficha in pramd.ManoDeFichas)
                {
                    Console.Write(ficha+", ");
                }

                pramd.SelectCard();
                Console.WriteLine("fichajugable: "+table.fichaJugable);
                foreach (var ficha in table.CardinTable)
                {
                    Console.Write(ficha+", ");
                }
                
                Console.WriteLine("Player 1 Jugo!");
                p.SelectCard();
                Console.WriteLine("Mesa");
                Console.WriteLine("fichajugable: "+table.fichaJugable);
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
                Console.WriteLine("fichajugable: "+table.fichaJugable);
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