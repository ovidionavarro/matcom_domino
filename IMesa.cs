namespace matcom_domino
{
    public interface IMesa<T>
    {
         //jugar la ficha ,balidar la jugada, ver fichas en la mesa
        void PlayCard(IFichas <T> a);
        bool EsValido(IFichas<T> a);//boliano
        void CardinTable();

    }
}