namespace matcom_domino
{
    public interface IMesa<T>
    {
        //jugar la ficha ,balidar la jugada, ver fichas en la mesa
        bool IsValido(IFichas<T> a); //boliano
        List<IFichas<T>> CardinTable { get; }

        void RecibirJugada(IFichas<T> ficha);
    }


    public class Mesa : IMesa<int>
    {
        private List<IFichas<int>> cardintable;

        public IFichas<int> fichaJugable;

        public List<string> Log { get; set; }

        public Mesa()
        {
            cardintable = new List<IFichas<int>>();
            this.Log = new List<string>();
        }

        public void RecibirJugada(IFichas<int> ficha)
        {
            if (cardintable.Count == 0)
                fichaJugable = ficha;

            else if (fichaJugable.GetFace(1) == ficha.GetFace(1))
            {
                fichaJugable = new Fichas9(fichaJugable.GetFace(2), ficha.GetFace(2));
                Log.Add($"La ficha jugable cambio a: {fichaJugable}");
            }


            else if (fichaJugable.GetFace(2) == ficha.GetFace(2))
            {
                fichaJugable = new Fichas9(fichaJugable.GetFace(1), ficha.GetFace(1));
                Log.Add($"La ficha jugable cambio a: {fichaJugable}");
            }


            else if (fichaJugable.GetFace(1) == ficha.GetFace(2))
            {
                fichaJugable = new Fichas9(fichaJugable.GetFace(2), ficha.GetFace(1));
                Log.Add($"La ficha jugable cambio a: {fichaJugable}");
            }


            else if (fichaJugable.GetFace(2) == ficha.GetFace(1))
            {
                fichaJugable = new Fichas9(fichaJugable.GetFace(1), ficha.GetFace(2));
                Log.Add($"La ficha jugable cambio a: {fichaJugable}");
            }
        }

        public bool IsValido(IFichas<int> a) 
        {
            if (cardintable.Count == 0)
                return true;

            else if (fichaJugable.GetFace(1) == a.GetFace(1))
                return true;

            else if (fichaJugable.GetFace(2) == a.GetFace(2))
                return true;

            else if (fichaJugable.GetFace(1) == a.GetFace(2))
                return true;

            else if (fichaJugable.GetFace(2) == a.GetFace(1))
                return true;

            return false;
        }

        public List<IFichas<int>> CardinTable
        {
            get => this.cardintable;
        }
    }
}