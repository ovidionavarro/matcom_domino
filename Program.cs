﻿using matcom_domino;
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

            IMesa<int> mesadoble = new MesaDobleSupremo();
            //IMesa<int> mesadoble = new MesaDobleSupremo();
            IGameOrden<int> orden = new OrdenDespuesDePase();
            //IGameOrden<int> orden = new OrdenclasicoIinverso();
            IGenerarFichas<int> generador = new GeneradorClasico();

           // IDomino<int> c = new DominoClassic(mesadoble, 9, new TrankeClasico(), new LowScoreWinner(), orden , new RepartirPar(),generador);
            IDomino<int> robaito = new DominoClassic(mesadoble, 9, new DobleTranke(), new LowScoreWinner(), orden, new RepartirEnOrden(),generador);
            IPlayer<int> P1 = new PlayerTramposo(mesadoble, "PlayerTramposo1");
            IPlayer<int> B1 = new PlayerBotaGorda(mesadoble, "PlayerBotaG1");
            IPlayer<int> B2 = new PlayerBotaGorda(mesadoble, "PlayerBotaG2");
            IPlayer<int> R1 = new PlayerRandom(mesadoble, "PlayerRandom1");
            IPlayer<int> S1 = new PlayerSobreviviente(mesadoble, "PlayerSobreviviente1");
            //IPlayer<int> B2 = new PlayerSobreviviente(table, "PlayerSobreviviente");


            // Clasico!!!!!!!!
            //Agregando Jugadores al Juego
            robaito.AgregarJugador(B1);
            // c.AgregarJugador(P1);
            robaito.AgregarJugador(R1);
            robaito.AgregarJugador(S1);
            robaito.AgregarJugador(P1);

            // Repartiendo las fichas
            robaito.RepartirFichas(5);

            robaito.StartGame();

            foreach (var log in mesadoble.Log)
            {
                Console.WriteLine(log);
            }
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