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

            Mesa table = new Mesa();
            //DominoRobaito R = new DominoRobaito(table, 9);
            Domino<int> c = new DominoClassic(table, 9);
            IPlayer<int> P1 = new Player(table, "PlayerNormal1");
            PlayerBotaGorda B1 = new PlayerBotaGorda(table, "PlayerBotaG1");
            PlayerBotaGorda B2 = new PlayerBotaGorda(table, "PlayerBotaG2");
            IPlayer<int> R1 = new PlayerRandom(table, "PlayerRandom1");


            //Agregando Jugadores al Juego
            //c.Jugadores.Add(P1);
            c.Jugadores.Add(B1);
            c.Jugadores.Add(B2);
            c.Jugadores.Add(R1);

            // Repartiendo las fichas
            c.RepartirFichas(10);


            while (!c.EndGame())
            {
                MostrarMano(B1);
                B1.SelectCard();
                //R.Robar(B1);
                MostrarMesa(table);
                Console.WriteLine("Ficha Jugable: " + table.fichaJugable);
                if (c.EndGame())
                    break;

                //Console.Read();
                MostrarMano(B2);
                B2.SelectCard();
                //R.Robar(B2);
                MostrarMesa(table);
                Console.WriteLine("Ficha Jugable: " + table.fichaJugable);
                if (c.EndGame())
                    break;

                //Console.Read();
                MostrarMano(R1);
                R1.SelectCard();
                //R.Robar(R1);
                MostrarMesa(table);
                Console.WriteLine("Ficha Jugable: " + table.fichaJugable);
               // Console.Read();
            }

            /*foreach (var log in table.Log)
            {
                Console.WriteLine(log);
            }*/
        }
    }
}