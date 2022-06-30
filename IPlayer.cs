using System.Runtime.CompilerServices;

namespace matcom_domino
{
    public interface IPlayer<T>
    {
        //mismo errror que con la lsita de la clase referee del inumeroable

        //cambie el repardor de fichas xq lo tenia puesto en la clase referee pero pienso k el jugador es el k tenga la funcion de coher la fichas
        
        int player_score { get; set; }
        bool Pasarse { get; }
        List<IFichas<T>> ManoDeFichas { get; }
        void SelectCard();
        public bool in_turn { get; set; }
        string name { get; }
        void Play(IFichas<T> ficha);
        
        int time_passed { get; set; }
    }

    public class Player : IPlayer<int> //despues hay k hacerlo generico
    {
        public int player_score { get; set; }
        private List<IFichas<int>> manoficha;
        public List<IFichas<int>> ManoDeFichas
        {
            get => this.manoficha;
        }
        public int time_passed { get; set; }
        public IMesa<int> table { get; }

        public bool in_turn { get; set; }

        public string name { get; set; }

        public Player(IMesa<int> Table, string Name)
        {
            manoficha = new List<IFichas<int>>();
            this.table = Table;
            this.name = Name;
            this.in_turn = false;
            time_passed = 0;
            player_score = 0;
        }

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
                table.Log.Add($"EL Jugador {name} ha jugado la ficha: {ficha}");
                table.RecibirJugada(ficha);
                manoficha.Remove(ficha);
                player_score += ficha.FichaValue();
                in_turn = false;

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
                time_passed++;
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
                    time_passed++;
                }
            }
        }
    }

   class PlayerSobreviviente : Player
    {
        public PlayerSobreviviente(Mesa table, string name) : base(table, name)
        {
            ValorJugable = new List<int>();
        }

        private List<int> ValorJugable;

        public void Posivilidad()
        {
            for (int i = 0; i < ManoDeFichas.Count(); i++)
            {
                int count = 0;
                for (int j = 0; j < ManoDeFichas.Count(); j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    else if (ManoDeFichas[i].GetFace(1) == ManoDeFichas[j].GetFace(1)||
                             ManoDeFichas[i].GetFace(1) == ManoDeFichas[j].GetFace(2))
                    {
                        count++;
                        if (ManoDeFichas[i].GetFace(2) == ManoDeFichas[j].GetFace(1) ||
                            ManoDeFichas[i].GetFace(2) == ManoDeFichas[j].GetFace(2))
                        {
                            count+= 5;
                        }
                    }
                    else if (ManoDeFichas[i].GetFace(2) == ManoDeFichas[j].GetFace(1)||
                             ManoDeFichas[i].GetFace(2) == ManoDeFichas[j].GetFace(2))
                    {
                        count++;
                        if (ManoDeFichas[i].GetFace(1) == ManoDeFichas[j].GetFace(1) ||
                            ManoDeFichas[i].GetFace(1) == ManoDeFichas[j].GetFace(2))
                        {
                            count+= 5;
                        }
                    }
                }
                ValorJugable[i] = count;
            }
        }

        public int Posicion(int a)
        {
            for (int i = 0; i < ValorJugable.Count(); i++)
            {
                if (a == ValorJugable[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public override void SelectCard()
        {
            Posivilidad();
            in_turn = true;
            List<int> temp_valor_jugable = new List<int>();
            List<IFichas<int>> temp_fichas = new List<IFichas<int>>();
            bool[] FichasValidas = new bool[ManoDeFichas.Count()];
            //poniendo en true las k son jugables
            for (int i = 0; i < ManoDeFichas.Count(); i++)
            {
                if (table.IsValido(ManoDeFichas[i]))
                {
                    FichasValidas[i] = true;
                }
            }
            //eliminando los k estan en false
            for (int i = 0; i < ManoDeFichas.Count(); i++)
            {
                if (FichasValidas[i])
                {
                    temp_valor_jugable.Add(ValorJugable[i]);
                    temp_fichas.Add(ManoDeFichas[i]);
                }
            }
            Play(ManoDeFichas[Posicion(temp_valor_jugable.Max())]);
            ValorJugable.Remove(temp_valor_jugable.Max());







        }

        




    }
}