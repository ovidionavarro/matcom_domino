using System.Reflection;

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

        public void Play(IFichas<int> ficha) 
        {
            in_turn = true;
            if (table.IsValido(ficha))
            {
                this.paso = false;
                table.RecibirJugada(ficha);
                manoficha.Remove(ficha);

                //table.CardinTable.Add(ficha);
                if (!table.CardinTable.Any())
                {
                    table.CardinTable.Add(ficha);
                }
                else if (table.fichaJugable.GetFace(1) == ficha.GetFace(1))
                {
                    table.CardinTable.Insert(0, ficha);
                    //table.CardinTable.Insert(0,new Fichas9(ficha.GetFace(2),ficha.GetFace(1)));
                }
                else if (table.fichaJugable.GetFace(1) == ficha.GetFace(2))
                {
                    table.CardinTable.Insert(0, ficha.Reverse()); //new Fichas9(ficha.GetFace(2), ficha.GetFace(1)));
                    //table.CardinTable.Insert(0, ficha);
                }
                else if (table.fichaJugable.GetFace(2) == ficha.GetFace(1))
                {
                    table.CardinTable.Add(ficha.Reverse()); //new Fichas9(ficha.GetFace(2), ficha.GetFace(1)));
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

    public class PlayerBotaGorda : Player
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


    public class PlayerSmart : PlayerBotaGorda
    {
        private bool FirstPlay = true;

        public PlayerSmart(Mesa table, string name) : base(table, name)
        {
        }

        private Mesa fakeTable = new Mesa();

        
        // Get next player base on log
        public string GetNextPlayer()
        {
            string next_player = "";
            string[] _nextPLayerArray = new string[0];
            List<string> _player_logs = new List<string>();
            foreach (var log in table.Log)
            {
                if (log.Contains("EL Jugador"))
                    _player_logs.Add(log);
            }

            for (int i = 0; i < _player_logs.Count; i++)
            {
                if (_player_logs[i].Contains("EL Jugador " + name) && i + 1 < _player_logs.Count &&
                    !_player_logs[i + 1].Contains("EL Jugador " + name))
                {
                    next_player = _player_logs[i + 1];
                    _nextPLayerArray = next_player.Split();
                }
            }

            for (int i = 0; i < _nextPLayerArray.Length; i++)
            {
                if (_nextPLayerArray[i] == "Jugador")
                {
                    next_player = _nextPLayerArray[i + 1];
                    break;
                }
            }

            return next_player;
        }


        public override void SelectCard()
        {
            if (FirstPlay)
            {
                base.SelectCard();

                FirstPlay = false;
            }

            string nextPlayer = GetNextPlayer();

            List<IFichas<int>> _to_pass_next_player = new List<IFichas<int>>();

            List<IFichas<int>> _my_tokens = new List<IFichas<int>>();

            List<string> nextPlayerLog = new List<string>();

            foreach (var log in table.Log)
            {
                if (log.Contains("no lleva:"))
                    nextPlayerLog.Add(log);
            }

            if (nextPlayerLog.Count > 0)
            {
                foreach (var log in nextPlayerLog)
                {
                    if (log.Contains(nextPlayer))
                    {
                        string[] pSplit = log.Split(':');
                        string[] tokens = pSplit[1].Split('-');
                        _to_pass_next_player.Add(new Fichas9(int.Parse(tokens[0]), int.Parse(tokens[1])));
                    }
                }

                foreach (var token in _to_pass_next_player)
                {
                    if (table.fichaJugable.GetFace(1)==token.GetFace(1))
                    {
                        
                    }
                }
                
            }
            else
            {
                FirstPlay = true;
            }
        }
    }
}