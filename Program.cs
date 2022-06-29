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

            IMesa<int> mesa = new Mesa();
            MesaDobleSupremo mesaw = new MesaDobleSupremo();
           

            // Domino<int> c = new DominoClassic(table, 9);
            IDomino<int> robaito = new DominoClassic(mesa, 9);
            IPlayer<int> P1 = new Player(mesa, "PlayerNormal1");
            IPlayer<int> B1 = new PlayerBotaGorda(mesa, "PlayerBotaG1");
            IPlayer<int> B2 = new PlayerBotaGorda(mesa, "PlayerBotaG2");
            IPlayer<int> R1 = new PlayerRandom(mesa, "PlayerRandom1");
            //IPlayer<int> B2 = new PlayerSobreviviente(table, "PlayerSobreviviente");
            
            
            //Agregando Jugadores al Juego
            //c.Jugadores.Add(P1);
            robaito.Jugadores.Add(B1);
            robaito.Jugadores.Add(B2);
            robaito.Jugadores.Add(R1);

            // Repartiendo las fichas
            robaito.RepartirFichas(10);

            //robaito.StartGame();
            while (!robaito.EndGame())
            {
                MostrarMano(B1);
                B1.SelectCard();
                robaito.Robar();
                MostrarMesa(mesa);
                Console.WriteLine("Ficha Jugable: " + mesa.fichaJugable);
                if (robaito.EndGame())
                    break;
            
                Console.Read();
                MostrarMano(B2);
                B2.SelectCard();
                robaito.Robar();
                MostrarMesa(mesa);
                Console.WriteLine("Ficha Jugable: " + mesa.fichaJugable);
                if (robaito.EndGame())
                    break;
            
                Console.Read();
                MostrarMano(R1);
                R1.SelectCard();
                robaito.Robar();
                MostrarMesa(mesa);
                Console.WriteLine("Ficha Jugable: " + mesa.fichaJugable); 
                Console.Read();
            }

            foreach (var log in mesa.Log)
            {
                Console.WriteLine(log);
            }
            //Iesvalodo a=new doblesupremo;
            //imesa table=new mesa(a)
            
        }
    }
}