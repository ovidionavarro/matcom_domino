namespace matcom_domino
{
    public interface IMesa<T>
    {
        //jugar la ficha ,balidar la jugada, ver fichas en la mesa
        bool IsValido(IFichas<T> a); //boliano
        List<IFichas<T>> CardinTable { get; }

        void RecibirJugada(IFichas<T> ficha);
        List<string> Log { get; set; }
        
    }


    public class Mesa : IMesa<int>
    {
        protected List<IFichas<int>> cardintable;

        public IFichas<int> fichaJugable;

        public List<string> Log { get; set; }
        

        public Mesa()
        {
            cardintable = new List<IFichas<int>>();
            this.Log = new List<string>();
        }

       

        public virtual void RecibirJugada(IFichas<int> ficha)
        {
            if (cardintable.Count == 0)
            {
                fichaJugable = ficha;
                CardinTable.Add(ficha);
                Log.Add($"La ficha jugable cambio a: {fichaJugable}");
            }

            else if (fichaJugable.GetFace(1) == ficha.GetFace(1))
            {
                fichaJugable = new Fichas9( ficha.GetFace(2),fichaJugable.GetFace(2));
                CardinTable.Insert(0,new Fichas9(ficha.GetFace(2),ficha.GetFace(1)));
                Log.Add($"La ficha jugable cambio a: {fichaJugable}");
            }


            else if (fichaJugable.GetFace(2) == ficha.GetFace(2))
            {
                fichaJugable = new Fichas9(fichaJugable.GetFace(1), ficha.GetFace(1));
                CardinTable.Add(new Fichas9(ficha.GetFace(2),ficha.GetFace(1)));
                Log.Add($"La ficha jugable cambio a: {fichaJugable}");
            }


            else if (fichaJugable.GetFace(1) == ficha.GetFace(2))
            {
                fichaJugable = new Fichas9(ficha.GetFace(1),fichaJugable.GetFace(2) );
                CardinTable.Insert(0, ficha);
                Log.Add($"La ficha jugable cambio a: {fichaJugable}");
            }


            else if (fichaJugable.GetFace(2) == ficha.GetFace(1))
            {
                fichaJugable = new Fichas9(fichaJugable.GetFace(1), ficha.GetFace(2));
                CardinTable.Add(ficha);
                Log.Add($"La ficha jugable cambio a: {fichaJugable}");
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
        public MesaDobleSupremo():base()
        {
            
        }

        public override bool IsValido(IFichas<int> a)
        {
            if (a.GetFace(1) == a.GetFace(2))
            {
                return true;
            }
            return base.IsValido(a);
            
        }

        public override void RecibirJugada(IFichas<int> ficha)
        {
            if (ficha.GetFace((1)) == ficha.GetFace(2))
            {
                fichaJugable = new Fichas9( ficha.GetFace(2),fichaJugable.GetFace(2));
                //CardinTable.Insert(0,new Fichas9(ficha.GetFace(2),ficha.GetFace(1)));
                Log.Add($"La ficha jugable cambio a: {fichaJugable}");
                CardinTable.Add(ficha);
            }
            else
            {
                base.RecibirJugada(ficha);
            }
        }
    }

    public class mesatemp : IMesa<int>
    {
        public bool IsValido(IFichas<int> a)
        {
            throw new NotImplementedException();
        }

        public List<IFichas<int>> CardinTable { get; }
        public void RecibirJugada(IFichas<int> ficha)
        {
            throw new NotImplementedException();
        }
    }



}