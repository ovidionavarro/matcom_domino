namespace matcom_domino
{
    public interface IMesa
    {
        //jugar la ficha ,balidar la jugada, ver fichas en la mesa
        bool IsValido(IFichas a); //boliano
        List<IFichas> CardinTable { get; }

        void RecibirJugada(IFichas ficha);




    }


    public class Mesa : IMesa
    {
        private List<IFichas> cardintable;

        public Fichas fichaJugable;

        public Mesa()
        {
            cardintable = new List<IFichas>();
        }

        public void RecibirJugada(IFichas ficha)
        {
            if (cardintable.Count == 0)
                fichaJugable = ficha;

            else if (fichaJugable.GetFace(1) == ficha.GetFace(1))
                fichaJugable = new Fichas9(fichaJugable.GetFace(2),ficha.GetFace(2));

            else if (fichaJugable.GetFace(2) == ficha.GetFace(2))
                fichaJugable = new Fichas9(fichaJugable.GetFace(1),ficha.GetFace(1));

            else if (fichaJugable.GetFace(1) == ficha.GetFace(2))
                fichaJugable = new Fichas9(fichaJugable.GetFace(2),ficha.GetFace(1));

            else if (fichaJugable.GetFace(2) == ficha.GetFace(1))
                fichaJugable = new Fichas9(fichaJugable.GetFace(1),ficha.GetFace(2));
        }

        public bool IsValido(IFichas a) // No actualizar ficha Jugable en esta comprobacion
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

        public List<IFichas> CardinTable
        {
            get => this.cardintable;
        }
    }
}