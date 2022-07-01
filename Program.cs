using matcom_domino;
using System.Collections.Generic;

namespace matcom_domino
{
    class Program
    {
        //Muestra la Mesa 
        public static void MostrarMesa(IMesa<int> Table)
        {
            Console.WriteLine("Fichas en mesa");
            foreach (var ficha in Table.CardinTable)
            {
                Console.Write(ficha + ", ");
            }

            Console.WriteLine();
        }

        //Muestra la mano de un jugaddor
        public static void MostrarMano(IPlayer<int> Player)
        {
            Console.WriteLine("Fichas del jugador en turno: " + Player.name);
            foreach (var ficha in Player.ManoDeFichas)
            {
                Console.Write(ficha + ", ");
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // Inicializando Objetos del Juego

            IMesa<int> mesadoble = new Mesa();
            //IMesa<int> mesadoble = new MesaDobleSupremo();
            //IGameOrden<int> orden = new OrdenClasico();
            //IGameOrden<int> orden = new OrdenclasicoIinverso();
            IGameOrden<int> orden = new OrdenDespuesDePase();

            IDomino<int> c = new DominoClassic(mesadoble, 9, new DobleTranke(), new LowScoreWinner(),orden);
            IDomino<int> robaito = new DominoRobaito(mesadoble, 9, new DobleTranke(), new LowScoreWinner(),orden);
            IPlayer<int> P1 = new Player(mesadoble, "PlayerNormal1");
            IPlayer<int> B1 = new PlayerBotaGorda(mesadoble, "PlayerBotaG1");
            IPlayer<int> B2 = new PlayerBotaGorda(mesadoble, "PlayerBotaG2");
            IPlayer<int> R1 = new PlayerRandom(mesadoble, "PlayerRandom1");
            //IPlayer<int> B2 = new PlayerSobreviviente(table, "PlayerSobreviviente");

            
            // Clasico!!!!!!!!
            //Agregando Jugadores al Juego
            //c.Jugadores.Add(P1);
            c.Jugadores.Add(P1);
            c.Jugadores.Add(B1);
            c.Jugadores.Add(B2);
            c.Jugadores.Add(R1);

            // Repartiendo las fichas
            c.RepartirFichas(5);
            
            c.StartGame();
           /*while (!c.EndGame())
             {
                 // MostrarMano(B1);
                 B1.SelectCard();
                 MostrarMesa(mesadoble);
                 Console.WriteLine("Ficha Jugable: " + mesadoble.fichaJugable);
                 if (c.EndGame())
                     break;
            
                 Console.Read();
                 // MostrarMano(B2);
                 B2.SelectCard();
            
            MostrarMesa(mesadoble);
                 Console.WriteLine("Ficha Jugable: " + mesadoble.fichaJugable);
                 if (c.EndGame())
                     break;
            
                 Console.Read();
                 // MostrarMano(R1);
                 R1.SelectCard();
                 MostrarMesa(mesadoble);
                 Console.WriteLine("Ficha Jugable: " + mesadoble.fichaJugable);
                 Console.Read();
             }*/
            

             //Robaitoooooo!!!!!!!!!!!!!


            //Agregando Jugadores al Juego
            
             //robaito.Jugadores.Add(B1);
             //robaito.Jugadores.Add(B2);
             //robaito.Jugadores.Add(R1);
            
             // Repartiendo las fichas
             //robaito.RepartirFichas(10);
             //robaito.StartGame();
             /*while (!robaito.EndGame())
             {
                 // MostrarMano(B1);
                 B1.SelectCard();
                 robaito.Robar();
                 MostrarMesa(mesadoble);
                 Console.WriteLine("Ficha Jugable: " + mesadoble.fichaJugable);
                 if (robaito.EndGame())
                     break;
            
                 Console.Read();
                 // MostrarMano(B2);
                 B2.SelectCard();
                 robaito.Robar();
                 MostrarMesa(mesadoble);
                 Console.WriteLine("Ficha Jugable: " + mesadoble.fichaJugable);
                 if (robaito.EndGame())
                     break;
            
                 Console.Read();
                 // MostrarMano(R1);
                 R1.SelectCard();
                 robaito.Robar();
                 MostrarMesa(mesadoble);
                 Console.WriteLine("Ficha Jugable: " + mesadoble.fichaJugable);
                 Console.Read();
             }

            foreach (var log in mesadoble.Log)
            {
                Console.WriteLine(log);
            }

            foreach (var player in c.Jugadores)
            {
                foreach (var token in player.ManoDeFichas)
                {
                    Console.Write(token+", ");
                }
                Console.WriteLine();
            }*/
        }
    }
}