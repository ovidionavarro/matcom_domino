using System.Security.Cryptography.X509Certificates;

namespace matcom_domino
{
    public interface Domino<T>
    {
        //orden en el k se juega,como se reparten las fichas,cuando fializa
        //*no se le asigna valor a la fichas cuando se tranca xq eso lo hacen las fichas
        //error en crear uun campo conjunto de fichs ,nose me almacenan las fichas ahi

        public bool Tranke();
        void GeneratedCards(int k);
        List<IFichas<T>> ConjuntodeFichas { get; }
        List<IPlayer<T>> Jugadores { get; }
        IMesa<T> Table { get; }
        void AgregarJugador(IPlayer<T> a);
        void RepartirFichas(int k);
        void GameOrden();
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

        public void GeneratedCards(int k)
        {
            for (int i = 0; i <= k; i++)
            {
                for (int j = i; j <= k; j++)
                {
                    this.conjuntodeFichas.Add(new Fichas9(i, j));
                }
            }
        }

        public void AgregarJugador(IPlayer<int> a)
        {
            jugadores.Add(a);
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

        public bool EndGame()
        {
            if (Tranke())
            {
                Wins();
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
            int index = Array.IndexOf(CalcPtos(), CalcPtos().Min());
            Console.WriteLine("El Ganador es: " + jugadores[index].name + " con " + CalcPtos().Min() + " Pts");
        }
    }

    class DominoRobaito : DominoClassic
    {
        public DominoRobaito(IMesa<int> Table, int cant) : base(Table, cant)
        {
        }

        public void Robar(IPlayer<int> Player)
        {
            Random r = new Random();
            while (Player.Pasarse)
            {
                if (ConjuntodeFichas.Count > 0)
                {
                    int index = r.Next(this.ConjuntodeFichas.Count);
                    IFichas<int> ficha = this.ConjuntodeFichas[index];
                    Player.ManoDeFichas.Add(ficha);
                    ConjuntodeFichas.RemoveAt(index);
                    Console.WriteLine("El Jugador {0} robo la ficha {1}", Player.name, ficha);
                    Player.Play(ficha);
                }
                else
                {
                    Console.WriteLine("No quedan fichas para robar");
                    break;
                }
            }
        }
    }
}