namespace matcom_domino
{
    public interface IFichas
    {
        string GetFace(int a);
        int ValueFace(int a);

        List<TypeFace> Caras { get; }

    }
    

    public class Fichas : IFichas //se puede implementar tambien con arrays
    {
        #region metods viejos
        /*public Fichas9(int a, int b)
        {
            //ficha[0]=a;
            //ficha[1]=b;
            ficha = new Tuple<int, int>(a, b);

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

        }*/
        #endregion
        public Fichas(List<TypeFace>typeFaces)
        {
            this.caras = typeFaces;
        }

        public int ValueFace(int a)
        {
            return this.Caras[a].Valor;
        }


        public string GetFace(int a)
        {
            return this.caras[a].Nombre;
        }
        public List<TypeFace> Caras
        {
            get => this.caras;
        }
       

        private List<TypeFace> caras;
    }
}
