namespace matcom_domino
{
    public interface IMesa<T>
    {
        //jugar la ficha ,balidar la jugada, ver fichas en la mesa
        bool IsValido(IFichas<T> a); //boliano
        List<IFichas<T>> CardinTable { get; }
        void RecibirJugada(IFichas<T> ficha, int side);
        List<string> Log { get; }

        IFichas<T> fichaJugable { get; }
    }


    public class Mesa : IMesa<int>
    {
        protected List<IFichas<int>> cardintable;

        public IFichas<int> fichaJugable { get; set; }

        public List<string> Log { get; }

        public Mesa()
        {
            cardintable = new List<IFichas<int>>();
            this.Log = new List<string>();
        }


        public virtual void RecibirJugada(IFichas<int> ficha, int side)
        {
            if (cardintable.Count == 0)
            {
                fichaJugable = ficha;
                CardinTable.Add(ficha);
                Log.Add($"La ficha jugable cambio a: {fichaJugable}");
            }

            if (side == -2)
            {
                if (fichaJugable.GetFace(1) == ficha.GetFace(1))
                {
                    fichaJugable = new Fichas9(ficha.GetFace(2), fichaJugable.GetFace(2));
                    CardinTable.Insert(0, new Fichas9(ficha.GetFace(2), ficha.GetFace(1)));
                    Log.Add($"La ficha jugable cambio a: {fichaJugable}");
                    return;
                }


                else if (fichaJugable.GetFace(2) == ficha.GetFace(2))
                {
                    fichaJugable = new Fichas9(fichaJugable.GetFace(1), ficha.GetFace(1));
                    CardinTable.Add(new Fichas9(ficha.GetFace(2), ficha.GetFace(1)));
                    Log.Add($"La ficha jugable cambio a: {fichaJugable}");
                    return;
                }


                else if (fichaJugable.GetFace(1) == ficha.GetFace(2))
                {
                    fichaJugable = new Fichas9(ficha.GetFace(1), fichaJugable.GetFace(2));
                    CardinTable.Insert(0, ficha);
                    Log.Add($"La ficha jugable cambio a: {fichaJugable}");
                    return;
                }


                else if (fichaJugable.GetFace(2) == ficha.GetFace(1))
                {
                    fichaJugable = new Fichas9(fichaJugable.GetFace(1), ficha.GetFace(2));
                    CardinTable.Add(ficha);
                    Log.Add($"La ficha jugable cambio a: {fichaJugable}");
                    return;
                }
            }

            if (side == 1)
            {
                if (fichaJugable.GetFace(2) == ficha.GetFace(1))
                {
                    cardintable.Add(ficha);
                    fichaJugable = new Fichas9(ficha.GetFace(2), fichaJugable.GetFace(2));
                }

                if (fichaJugable.GetFace(2) == ficha.GetFace(2))
                {
                    cardintable.Add(new Fichas9(ficha.GetFace(2), ficha.GetFace(1)));
                    fichaJugable = new Fichas9(ficha.GetFace(2), fichaJugable.GetFace(2));
                }
            }

            if (side == -1)
            {
                if (fichaJugable.GetFace(1) == ficha.GetFace(2))
                {
                    cardintable.Insert(0, ficha);
                    fichaJugable = new Fichas9(CardinTable[0].GetFace(1),
                        CardinTable[cardintable.Count - 1].GetFace(2));
                }

                if (fichaJugable.GetFace(1) == ficha.GetFace(1))
                {
                    cardintable.Insert(0, new Fichas9(ficha.GetFace(2), ficha.GetFace(1)));
                    fichaJugable = new Fichas9(CardinTable[0].GetFace(1),
                        CardinTable[cardintable.Count - 1].GetFace(2));
                }
            }
        }

        public virtual bool IsValido(IFichas<int> a)
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

    public class MesaDobleSupremo : Mesa
    {
        public MesaDobleSupremo() : base()
        {
        }

        public override bool IsValido(IFichas<int> ficha)
        {
            if (ficha.GetFace(1) == ficha.GetFace(2))
            {
                return true;
            }
            else
            {
                return base.IsValido(ficha);
            }
        }

        public override void RecibirJugada(IFichas<int> ficha, int side)
        {
            if (ficha.GetFace((1)) == ficha.GetFace(2))
            {
                if (cardintable.Count > 0)
                {
                    CardinTable.Insert(0, ficha);
                    fichaJugable = new Fichas9(CardinTable[0].GetFace(1),
                        CardinTable[cardintable.Count - 1].GetFace(2));
                    //CardinTable.Insert(0,new Fichas9(ficha.GetFace(2),ficha.GetFace(1)));
                    Log.Add($"La ficha jugable cambio a: {fichaJugable}");
                    
                }
                else
                {
                    cardintable.Add(ficha);
                    fichaJugable = ficha;
                }
            }
            else
            {
                base.RecibirJugada(ficha, side);
            }
        }
    }
}