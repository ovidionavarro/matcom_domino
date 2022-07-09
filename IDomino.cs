using System.Security.Cryptography.X509Certificates;

namespace matcom_domino
{
    public interface IDomino<T>
    {
        //orden en el k se juega,como se reparten las fichas,cuando fializa
        //*no se le asigna valor a la fichas cuando se tranca xq eso lo hacen las fichas
        //error en crear uun campo conjunto de fichs ,nose me almacenan las fichas ahi

        //public bool Tranke();

        //void GeneratedCards(int k); //
        List<IFichas<T>> ConjuntodeFichas { get; }
        List<IPlayer<T>> Jugadores { get; }
        void Robar();
        ITranke<T> _tranke { get; }
        void AgregarJugador(IPlayer<T> a);
        IRepartirFichas<T> _repartirFichas { get; }
        void RepartirFichas(int k);
        
        IGenerarFichas<T> _generarFichas { get; }
        IGameOrden<int> orden { get; }
        void StartGame();
        bool EndGame();

        IWinner<T> Winner { get; set; }
    }

    public class DominoClassic : IDomino<int>
    {
        public IRepartirFichas<int> _repartirFichas { get; }

        public IWinner<int> Winner { get; set; }

        public ITranke<int> _tranke { get; }
        public IMesa<int> Table { get; }
        public IGameOrden<int> orden { get; }
        
        public IGenerarFichas<int> _generarFichas { get; }
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

        public DominoClassic(IMesa<int> Table, int cant, ITranke<int> _tranke, IWinner<int> winner,
            IGameOrden<int> orden, IRepartirFichas<int> repartirFichas,IGenerarFichas<int> _generar)
        {
            this._repartirFichas = repartirFichas;
            this.Table = Table;
            this.conjuntodeFichas = new List<IFichas<int>>();
            this.jugadores = new List<IPlayer<int>>();
            //this.GeneratedCards(cant);
            this._tranke = _tranke;
            Winner = winner;
            this.orden = orden;
            this._generarFichas = _generar;
            conjuntodeFichas = _generarFichas.GenerateCards(cant);
        }

        

        public virtual void Robar()
        {
            //Console.WriteLine("en el clasico no se puede robar");
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

            _repartirFichas.Repartir(conjuntodeFichas, jugadores, l);
            // Random r = new Random();

            // while (this.jugadores[this.jugadores.Count - 1].ManoDeFichas.Count != l)
            // {
            //     for (int i = 0; i < this.jugadores.Count; i++)
            //     {
            //         int k = r.Next(this.conjuntodeFichas.Count);
            //         this.jugadores[i].ManoDeFichas.Add(conjuntodeFichas[k]);
            //         conjuntodeFichas.RemoveAt(k);
            //     }
            // }

            Table.FichasSobrantes = conjuntodeFichas;
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


        public virtual void StartGame()
        {
            int turn = 1;
            while (!EndGame())
            {
                foreach (var player in Jugadores)
                {
                    player.in_turn = true;
                    Table.Log.Add($"Turno: {turn}");
                    if (player.GetType() == new Player(Table, "yo").GetType())
                    {
                        //Console.Clear();
                        Console.WriteLine("La Mesa");

                        foreach (var token in Table.CardinTable)
                        {
                            Console.Write(token + ", ");
                        }

                        Console.WriteLine(" Ficha JUgable: " + Table.fichaJugable);

                        Console.WriteLine("Tus Fichas:");

                        foreach (var token in player.ManoDeFichas)
                        {
                            Console.Write(token + ", ");
                        }

                        Console.WriteLine();
                        // Aki se dice la ficha a jugar y el side -1 izq 1 dere
                        string[] index_side = Console.ReadLine().Split();
                        player.Play(player.ManoDeFichas[int.Parse(index_side[0]) - 1], int.Parse(index_side[1]));
                    }
                    else
                    {
                        Console.WriteLine($"{player.name} Tokens");
                        foreach (var token in player.ManoDeFichas)
                        {
                            Console.Write(token + ", ");
                        }

                        Console.WriteLine();
                        player.SelectCard();
                    }

                    if (this.EndGame())
                        break;
                    player.in_turn = false;
                    turn++;
                }
            }
        }

        public virtual bool EndGame()
        {
            if (_tranke.Tranke(jugadores, Table))
            {
                Wins(Winner.PlayerWinner(jugadores));
                Table.Log.Add("Se ha Trancado el juego ");
                return true;
            }

            foreach (var player in jugadores)
            {
                if (player.ManoDeFichas.Count == 0)
                {
                    Wins(Winner.PlayerWinner(jugadores));
                    return true;
                }
            }

            return false;
        }

        public virtual void GameOrden() //Aki cambiar el orden de la lista
        {
        }

        public void Wins(IPlayer<int> player_winner)
        {
            Table.Log.Add($"Ha ganado el jugador {player_winner.name} con {player_winner.player_score} Pts");
        }
    }

    class DominoRobaito : DominoClassic
    {
        public DominoRobaito(IMesa<int> Table, int cant, ITranke<int> _tranke, IWinner<int> winner,
            IGameOrden<int> orden, IRepartirFichas<int> repartirFichas,IGenerarFichas<int> _generarFichas) : base(Table, cant,
            _tranke, winner, orden, repartirFichas,_generarFichas)
        {
        }

        public override void
            StartGame()
        {
            int turn = 1;
            while (!EndGame())
            {
                orden.OrdendelJuego(Jugadores);
                Table.Log.Add($"Turno: {turn}");
                foreach (var player in Jugadores)
                {
                    player.in_turn = true;
                    if (player.GetType() == new Player(Table, "yo").GetType())
                    {
                        Console.WriteLine("La Mesa");

                        foreach (var token in Table.CardinTable)
                        {
                            Console.Write(token + ", ");
                        }

                        Console.WriteLine(" Ficha JUgable: " + Table.fichaJugable);

                        Console.WriteLine("Tus Fichas:");

                        foreach (var token in player.ManoDeFichas)
                        {
                            Console.Write(token + $":, ");
                        }

                        string[] index_side = Console.ReadLine().Split();
                        player.Play(player.ManoDeFichas[int.Parse(index_side[0]) - 1], int.Parse(index_side[1]));
                        Robar();
                        player.Pasarse = false;
                        if (EndGame())
                            break;
                    }
                    else
                    {
                        player.SelectCard();
                        Robar();
                        player.Pasarse = false;
                        if (EndGame())
                            break;
                    }

                    player.in_turn = false;
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
                    Wins(Winner.PlayerWinner(Jugadores));
                    return true;
                }
            }

            return false;
        }

        public override void Robar()
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
                        Console.WriteLine($"EL Jugador {Jugadores[i].name} robo la ficha {ficha}");
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
                            Console.WriteLine($"EL Jugador {Jugadores[i].name} robo la ficha {ficha}");
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