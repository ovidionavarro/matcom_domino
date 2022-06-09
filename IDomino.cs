namespace matcom_domino
{
    public interface Domino<T>
    {
        //orden en el k se juega,como se reparten las fichas,cuando fializa
        //*no se le asigna valor a la fichas cuando se tranca xq eso lo hacen las fichas
        //error en crear uun campo conjunto de fichs ,nose me almacenan las fichas ahi

        void GeneratedCards();
        List<IFichas<T>> ConjuntodeFichas { get; }
        List<IPlayer<T>> Jugadores { get; }
        void AgregarJugador(IPlayer<T> a);
        void RepartirFichas();
        void GameOrden();
        bool EndGame();
        void Wins();
    }

    public class Referee9 : Domino<int>
    {
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

        public Referee9()
        {
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
                    int k = (int)r.NextInt64(this.conjuntodeFichas.Count);
                    this.jugadores[i].ManoDeFichas.Add(conjuntodeFichas[k]);
                    conjuntodeFichas.RemoveAt(k);
                }
            }
        }

        public bool EndGame()
        {
            throw new NotImplementedException();
        }

        public void GameOrden()
        {
            throw new NotImplementedException();
        }

        public void Wins()
        {
            throw new NotImplementedException();
        }
    }
}