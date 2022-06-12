namespace matcom_domino
{
    public interface IPlayer
    {
        //mismo errror que con la lsita de la clase referee del inumeroable

        //cambie el repardor de fichas xq lo tenia puesto en la clase referee pero pienso k el jugador es el k tenga la funcion de coher la fichas

        List<IFichas> ManoDeFichas { get; }
        void SelectCard();

        string name { get; }
        void Play(IFichas<T> ficha);
    }

    public class Player : IPlayer<int> //despues hay k hacerlo generico
    {
        public IMesa<int> table { get; }

        public string name
        {
            get;
            set;
        }

        public Player(IMesa<int> Table, string Name)
        {
            manoficha = new List<IFichas<int>>();
            this.table = Table;
            this.name = Name;
        }

        public List<IFichas<int>> ManoDeFichas
        {
            get => this.manoficha;
        }

        private List<IFichas<int>> manoficha;

        public virtual void SelectCard()
        {
            throw new NotImplementedException();
        }

        public void Play(IFichas<int> ficha) // Esto hay que arreglarlo... no puede ser un bool
        {

            if (table.IsValido(ficha))
            {
                table.RecibirJugada(ficha);
                manoficha.Remove(ficha);
                table.CardinTable.Add(ficha);  
            }
                
            
        }
    }

    class PlayerRandom : Player
    {
        public PlayerRandom(IMesa<int> table, string name) : base(table, name)
        {
        }

        public override void SelectCard()
        {
            List<IFichas<int>> fichasjugables = new List<IFichas<int>>();
            Random r = new Random();
            foreach (var ficha in ManoDeFichas)
            {
                if (table.IsValido(ficha))
                {
                    fichasjugables.Add(ficha);
                    
                }
            }

            if (fichasjugables.Count>0)
            {
                Play(fichasjugables[r.Next(fichasjugables.Count)]);
            }

            else
            {
                Console.WriteLine("Paso!!");
            }

        }
    }

    class PlayerBotaGorda : Player
    {
        public PlayerBotaGorda(IMesa<int> table, string name) : base(table, name)
        {
        }

        public int SumaFicha(IFichas<int> Ficha)
        {
            int suma = Ficha.ValueFace(1) + Ficha.ValueFace(2);
            return suma;
        }

        public void SortHand()
        {
            for (int i = 0; i < ManoDeFichas.Count; i++)
            {
                for (int j = i + 1; j < ManoDeFichas.Count; j++)
                {
                    if (SumaFicha(ManoDeFichas[i]) < SumaFicha(ManoDeFichas[j]))
                    {
                        Fichas9 repuesto = new Fichas9(ManoDeFichas[i].GetFace(1), ManoDeFichas[i].GetFace(2));
                        ManoDeFichas[i] = ManoDeFichas[j];
                        ManoDeFichas[j] = repuesto;
                    }
                }
            }
        }

        public override void SelectCard()
        {
            SortHand();
            foreach (var ficha in ManoDeFichas)
            {
                if (table.IsValido(ficha))
                {
                   
                    Play(ficha);
                    break;
                }
                
                // Imprime en consola cuando no puede jugar
                if (ficha.Equals(ManoDeFichas[ManoDeFichas.Count - 1]))
                    Console.WriteLine("Paso!!");
            }
        }
    }

    class PlayerIntelligent:Player
    {
        public PlayerIntelligent(IMesa<int> table, string name) : base(table, name)
        {
        }
    }
}