namespace matcom_domino
{
    public interface Domino<T>
    {
        //orden en el k se juega,como se reparten las fichas,cuando fializa
        //*no se le asigna valor a la fichas cuando se tranca xq eso lo hacen las fichas
        //error en crear uun campo conjunto de fichs ,nose me almacenan las fichas ahi
        bool Tranke();
        void GeneratedCards();
        List<IFichas<T>> ConjuntodeFichas { get; }
        List<IPlayer<T>> Jugadores { get; }
        IMesa<T> Table { get; }
        void AgregarJugador(IPlayer<T> a);
        void RepartirFichas();
        void GameOrden();
        bool EndGame();
        void Wins();
    }

    public class Referee9 : Domino<int>
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

        public Referee9(IMesa<int> Table)
        {
            this.Table = Table;
            this.conjuntodeFichas = new List<IFichas<int>>();
            this.jugadores = new List<IPlayer<int>>();
        }

        public void GeneratedCards()
        {
            for (int i = 0; i <= 9; i++)
            {
                for (int j = i; j <= 9; j++)
                {
                    //ConjuntodeFichas.Add(new Fichas9(i,j));
                    //a.Add(new Fichas9(i,j))
                    this.conjuntodeFichas.Add(new Fichas9(i, j));
                }
            }
        }

        public void AgregarJugador(IPlayer<int> a)
        {
            jugadores.Add(a);
        }

        public void RepartirFichas()
        {
            Random r = new Random();

            while (this.jugadores[this.jugadores.Count - 1].ManoDeFichas.Count != 10)
            {
                for (int i = 0; i < this.jugadores.Count; i++)
                {
                    int k = r.Next(this.conjuntodeFichas.Count);
                    this.jugadores[i].ManoDeFichas.Add(conjuntodeFichas[k]);
                    conjuntodeFichas.RemoveAt(k);
                }
            }
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
        
        public bool EndGame()
        {
            if (Tranke())
                return true;
            foreach (var player in jugadores)
            {
                if (player.ManoDeFichas.Count==0)
                {
                    return true;
                }
            }

            return false;
        }

        public void GameOrden()
        {
            throw new NotImplementedException();
        }

        public void Wins()
        {
            if (EndGame())
            {
                int index = Array.IndexOf(CalcPtos(),CalcPtos().Min());
                Console.WriteLine("El Ganador es: "+jugadores[index].name+" con "+CalcPtos().Min()+" Pts");
            }
        }
    }
}