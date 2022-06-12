namespace matcom_domino
{
    public interface Domino
    {
        //orden en el k se juega,como se reparten las fichas,cuando fializa
        //*no se le asigna valor a la fichas cuando se tranca xq eso lo hacen las fichas
        //error en crear uun campo conjunto de fichs ,nose me almacenan las fichas ahi

        public bool Tranke();
        void GeneratedCards(int cantdecaras);
        List<Fichas> ConjuntodeFichas { get; }
        List<IPlayer> Jugadores { get; }
        IMesa Table { get; }
        void AgregarJugador(IPlayer a);
        void RepartirFichas();
        void GameOrden();
        bool EndGame();
        void Wins();
    }

    public class Referee9 : Domino
    {
        public IMesa Table { get; }
        public List<Fichas> ConjuntodeFichas
        {
            get => this.conjuntodeFichas;
        }

        private List<Fichas> conjuntodeFichas;

        public List<IPlayer> Jugadores
        {
            get => this.jugadores;
        }

        private List<IPlayer> jugadores;

        public Referee9(IMesa Table)
        {
            this.Table = Table;
            this.conjuntodeFichas = new List<IFichas>();
            this.jugadores = new List<IPlayer>();
        }

        public void GeneratedCards(int cantdecaras)
        {
            
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
                int index = Array.IndexOf(CalcPtos(),CalcPtos().Max());
                Console.WriteLine("El Ganador es: "+jugadores[index].name+" con "+CalcPtos().Max()+" Pts");
            }
        }
    }
}