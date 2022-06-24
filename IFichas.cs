namespace matcom_domino
{
    public interface IFichas<T>
    {
        T GetFace(int a);
        int ValueFace(T a);
        int FichaValue();
        IFichas<T> Reverse();

    }
    

    public class Fichas9 : IFichas<int> //voidse puede implementar tambien con arrays
    {
        public Fichas9(int a, int b)
        {
            //ficha[0]=a;
            //ficha[1]=b;
            ficha = new Tuple<int, int>(a, b);

        }

        public IFichas<int> Reverse()
        {
            this.ficha = new Tuple<int, int>(ficha.Item2, ficha.Item1);
            return new Fichas9(ficha.Item1,ficha.Item2);
        }
        
        public int FichaValue()
        {
            return ValueFace(1) + ValueFace(2);
        }
        
        //public int []ficha=new int [2];
        private Tuple<int, int> ficha;

        public int GetFace(int a)
        {
            //return ficha[a];
            if (a == 1) return ficha.Item1;
            if (a == 2) return ficha.Item2;
            else
            {
                return 0;
            }

        }
        public override string ToString()
        {
            return this.ficha.Item1.ToString() + "-" + this.ficha.Item2.ToString();
        }

        public int ValueFace(int a)
        {
            if (a == 1) return ficha.Item1;
            if (a == 2) return ficha.Item2;
            else
            {
                return 0;
            }

        }
    }
}