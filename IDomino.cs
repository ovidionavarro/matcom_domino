using System.Security.Cryptography.X509Certificates;

namespace matcom_domino
{
    public interface Domino<T>
    {
        //orden en el k se juega,como se reparten las fichas,cuando fializa
        //*no se le asigna valor a la fichas cuando se tranca xq eso lo hacen las fichas
        //error en crear uun campo conjunto de fichs ,nose me almacenan las fichas ahi

        public bool Tranke();
        
        void GeneratedCards(int k);//
        List<IFichas<T>> ConjuntodeFichas { get; }

        List<IPlayer<T>> Jugadores { get; }
        
        void AgregarJugador(IPlayer<T> a);
        void RepartirFichas(int k);
        void GameOrden();
        void StartGame();
        bool EndGame();
        void Wins();
    }

    public class DominoClassic : Domino<int>
    {
        
        public IMesa<int> Table { get; }

        public List<IFichas<int>> ConjuntodeFichas
        {
            get => this.conjuntodeFichas;
        }

        private List<IFichas<int>> conjuntodeFichas;

        public List<IPlayer<int>> Jugadores
        {
            get => this.jugadores;
        }

        private List<IPlayer<int>> jugadores;

        public DominoClassic(IMesa<int> Table, int cant)
        {
            this.Table = Table;
            this.conjuntodeFichas = new List<IFichas<int>>();
            this.jugadores = new List<IPlayer<int>>();
            this.GeneratedCards(cant);
            
        }

        public virtual void GeneratedCards(int k)
        {
           
            
            for (int i = 0; i <k; i++)
            {
                for (int j = i; j <=k; j++)
                {
                    this.conjuntodeFichas.Add(new Fichas9(i,j));
                }
            }

            Table.Log.Add($"Se han generado todas las fichas hasta el doble {k}");
        }

        public void AgregarJugador(IPlayer<int> a)
        {
            jugadores.Add(a);
            Table.Log.Add($"El jugador {a.name} se ha unido a la partida");
        }

        public virtual void RepartirFichas(int l)
        {
            if (l * jugadores.Count() > ConjuntodeFichas.Count())
            {
                throw new Exception("estas repartiendo mas fichas de las que hay");
            }

            Random r = new Random();

            while (this.jugadores[this.jugadores.Count - 1].ManoDeFichas.Count != l)
            {
                for (int i = 0; i < this.jugadores.Count; i++)
                {
                    int k = r.Next(this.conjuntodeFichas.Count);
                    this.jugadores[i].ManoDeFichas.Add(conjuntodeFichas[k]);
                    conjuntodeFichas.RemoveAt(k);
                }
            }

            Table.Log.Add($"Se han repartido {l} fichas a cada jugador ");
        }

        int CalcManoJugador(IPlayer<int> player)
        {
            int Ptos = 0;
            foreach (var ficha in player.ManoDeFichas)
            {
                Ptos += ficha.ValueFace(1) + ficha.ValueFace(2);
            }

            return Ptos;
        }

        public int[] CalcPtos()
        {
            int[] PtosPlayers = new int[jugadores.Count];
            for (int i = 0; i < jugadores.Count; i++)
            {
                PtosPlayers[i] = CalcManoJugador(jugadores[i]);
            }

            return PtosPlayers;
        }

        public bool Tranke()
        {
            foreach (var player in jugadores)
            {
                foreach (var ficha in player.ManoDeFichas)
                {
                    if (Table.IsValido(ficha))
                        return false;
                }
            }

            return true;
        }

      

        public virtual void StartGame()
        {
            int turn = 1;
            while (!EndGame())
            {
                Table.Log.Add($"Turno: {turn}");
                foreach (var player in Jugadores)
                {
                    player.SelectCard();
                    if (EndGame())
                        break;
                }

                turn++;
            }
        }

        public virtual bool EndGame()
        {
            if (Tranke())
            {
                Wins();
                Table.Log.Add("Se ha Trancado el juego ");
                return true;
            }

            foreach (var player in jugadores)
            {
                if (player.ManoDeFichas.Count == 0)
                {
                    Wins();
                    return true;
                }
            }

            return false;
        }

        public virtual void GameOrden() //Aki cambiar el orden de la lista
        {
        }

        public void Wins()
        {
            int[] player_points = CalcPtos();
            int index = Array.IndexOf(player_points, player_points.Min());
            Table.Log.Add($"Ha ganado el jugador {jugadores[index].name} con {player_points.Min()} Pts");
        }
    }

    class DominoRobaito : DominoClassic
    {
        public DominoRobaito(Mesa Table, int cant) : base(Table, cant)
        {
        }

        public override void
            StartGame()
        {
            int turn = 1;
            while (!EndGame())
            {
                Table.Log.Add($"Turno: {turn}");
                foreach (var player in Jugadores)
                {
                    player.SelectCard();
                    Robar();
                    if (EndGame())
                        break;
                }

                turn++;
            }
        }

        public override bool EndGame()
        {
            foreach (var player in Jugadores)
            {
                if (player.ManoDeFichas.Count == 0)
                {
                    Wins();
                    return true;
                }
            }

            return false;
        }

        public void Robar()
        {
            for (int i = 0; i < Jugadores.Count; i++)
            {
                Random r = new Random();
                while (Jugadores[i].Pasarse && Jugadores[i].in_turn)
                {
                    if (ConjuntodeFichas.Count > 0)
                    {
                        int index = r.Next(this.ConjuntodeFichas.Count);
                        IFichas<int> ficha = this.ConjuntodeFichas[index];
                        Jugadores[i].ManoDeFichas.Add(ficha);
                        ConjuntodeFichas.RemoveAt(index);
                        Table.Log.Add($"EL Jugador {Jugadores[i].name} robo la ficha {ficha}");
                        Jugadores[i].Play(ficha);
                    }
                    else
                    {
                        Table.Log.Add("Se han acabado las fichas para robar");
                        if (i + 1 >= Jugadores.Count)
                        {
                            int index = r.Next(Jugadores[0].ManoDeFichas.Count);
                            IFichas<int> ficha = Jugadores[0].ManoDeFichas[index];
                            Jugadores[i].ManoDeFichas.Add(ficha);
                            Jugadores[0].ManoDeFichas.RemoveAt(index);
                            Table.Log.Add(
                                $"El jugador {Jugadores[i].name} le ha robado la ficha {ficha} a {Jugadores[0].name}");
                            if (Jugadores[0].ManoDeFichas.Count == 0)
                            {
                                EndGame();
                                break;
                            }

                            Jugadores[i].Play(ficha);
                        }
                        else if (i + 1 < Jugadores.Count)
                        {
                            int index = r.Next(Jugadores[i + 1].ManoDeFichas.Count);
                            IFichas<int> ficha = Jugadores[i + 1].ManoDeFichas[index];
                            Jugadores[i].ManoDeFichas.Add(ficha);
                            Jugadores[i + 1].ManoDeFichas.RemoveAt(index);
                            Table.Log.Add(
                                $"El jugador {Jugadores[i].name} le ha robado la ficha {ficha} a {Jugadores[i + 1].name}");
                            if (Jugadores[i + 1].ManoDeFichas.Count == 0)
                            {
                                EndGame();
                                break;
                            }

                            Jugadores[i].Play(ficha);
                        }
                    }
                }
            }
        }
    }

    

   
}