namespace matcom_domino
{
    public interface IMesa<T>
    {
        //jugar la ficha ,balidar la jugada, ver fichas en la mesa
        bool IsValido(IFichas<T> a); //boliano
        List<IFichas<T>> CardinTable { get; }
    }


    public class Mesa : IMesa<int>
    {
        private List<IFichas<int>> cardintable;

        public Mesa()
        {
            cardintable = new List<IFichas<int>>();
        }

        public bool IsValido(IFichas<int> a)
        {
            throw new NotImplementedException();
        }

        public List<IFichas<int>> CardinTable
        {
            get => this.cardintable;
        }
    }
}