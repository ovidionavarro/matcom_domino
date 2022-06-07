namespace matcom_domino
{
    public interface Domino<T>
    {
        //orden en el k se juega,como se reparten las fichas,cuando fializa
        //*no se le asigna valor a la fichas cuando se tranca xq eso lo hacen las fichas
        //error en crear uun campo conjunto de fichs ,nose me almacenan las fichas ahi

        //IEnumerable<IFichas<T>> ConjuntoCards {get; } 
        //IEnumerable<IFichas<T>> GeneratedCards();        
        //void TipodeReparticion();
        List<IFichas<T>> ConjuntodeFichas { get; }
        void GameOrden();
        void GeneratedCards();
        void RepartirFichas();
        bool EndGame();
        void Wins();
    }
    public class Referee9 : Domino<int>
    {
        public Referee9()
        {
            this.conjuntodeFichas = new List<IFichas<int>>();
        }
        public List<IFichas<int>> ConjuntodeFichas { get => this.conjuntodeFichas; }
        private List<IFichas<int>> conjuntodeFichas;
        
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
        public void RepartirFichas()
        {
            throw new NotImplementedException();
        }


        //public static List<IFichas<int>> ConjuntodeFichas=new List<IFichas<int>>();
        //public IEnumerable<IFichas<int>> ConjuntoCards { get ; set; }

        public bool EndGame()
        {
            throw new NotImplementedException();
        }

        public void GameOrden()
        {
            throw new NotImplementedException();
        }

        /*public void TipodeReparticion()
        {
            throw new NotImplementedException();
        }*/

        public void Wins()
        {
            throw new NotImplementedException();
        }



    }
}