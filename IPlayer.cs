namespace matcom_domino
{
    public interface IPlayer<T>
    {
        //mismo errror que con la lsita de la clase referee del inumeroable

        //cambie el repardor de fichas xq lo tenia puesto en la clase referee pero pienso k el jugador es el k tenga la funcion de coher la fichas

        bool Pasarse { get; }
        List<IFichas<T>> ManoDeFichas { get; }
        void SelectCard();
        public bool in_turn { get; set; }
        string name { get; }
        void Play(IFichas<T> ficha);
    }

    public class Player : IPlayer<int> //despues hay k hacerlo generico
    {
        public Mesa table { get; }

        public bool in_turn { get; set; }

        public string name { get; set; }

        public Player(Mesa Table, string Name)
        {
            manoficha = new List<IFichas<int>>();
            this.table = Table;
            this.name = Name;
            this.in_turn = false;
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

        public bool Pasarse
        {
            get => paso;
            set { }
        }

        public bool paso = false;

        public void Play(IFichas<int> ficha) // Esto hay que arreglarlo... no puede ser un bool
        {
            in_turn = true;
            if (table.IsValido(ficha))
            {
                this.paso = false;
                table.RecibirJugada(ficha);
                manoficha.Remove(ficha);
                
                //table.CardinTable.Add(ficha);
                if (table.CardinTable.Count() == 0)
                {
                    table.CardinTable.Add(ficha);
                }
                else if (table.fichaJugable.GetFace(1) == ficha.GetFace(1))
                {
                    table.CardinTable.Insert(0,ficha);
                    //table.CardinTable.Insert(0,new Fichas9(ficha.GetFace(2),ficha.GetFace(1)));
                }
                else if (table.fichaJugable.GetFace(1) == ficha.GetFace(2))
                {
                    table.CardinTable.Insert(0, new Fichas9(ficha.GetFace(2), ficha.GetFace(1)));
                    //table.CardinTable.Insert(0, ficha);
                }
                else if (table.fichaJugable.GetFace(2) == ficha.GetFace(1))
                {
                    table.CardinTable.Add(new Fichas9(ficha.GetFace(2),ficha.GetFace(1)));
                    //table.CardinTable.Add(ficha);
                }
                else if (table.fichaJugable.GetFace(2) == ficha.GetFace(2))
                {
                    table.CardinTable.Add(ficha);
                    //table.CardinTable.Add(new Fichas9(ficha.GetFace(2),ficha.GetFace(1)));
                }

                in_turn = false;

                table.Log.Add($"EL Jugador {name} ha jugado la ficha: {ficha}");
            }
        }
    }

    class PlayerRandom : Player
    {
        public PlayerRandom(Mesa table, string name) : base(table, name)
        {
        }
        
        

        public override void SelectCard()
        {
            in_turn = true;
            List<IFichas<int>> fichasjugables = new List<IFichas<int>>();
            Random r = new Random();

            foreach (var ficha in ManoDeFichas)
            {
                if (table.IsValido(ficha))
                {
                    fichasjugables.Add(ficha);
                }
            }

            if (fichasjugables.Count > 0)
            {
                Play(fichasjugables[r.Next(fichasjugables.Count)]);
            }
            else
            {
                table.Log.Add($"EL Jugador {name} no lleva: {table.fichaJugable}");
                this.paso = true;
            }
        }
    }

    class PlayerBotaGorda : Player
    {
        public PlayerBotaGorda(Mesa table, string name) : base(table, name)
        {
        }

        public int SumaFicha(IFichas<int> Ficha)
        {
            return Ficha.FichaValue();
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
            in_turn = true;
            foreach (var ficha in ManoDeFichas)
            {
                if (table.IsValido(ficha))
                {
                    Play(ficha);
                    break;
                }

                // Si es la ultima ficha y no se jugo se pasa
                if (ficha == ManoDeFichas[ManoDeFichas.Count - 1])
                {
                    table.Log.Add($"El Jugador {name} no lleva: {table.fichaJugable}");
                    paso = true;
                }
            }
        }
    }
}