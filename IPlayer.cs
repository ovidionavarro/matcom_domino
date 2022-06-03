namespace matcom_domino
{
    public interface IPlayer<T>
    {

        IEnumerable<IFichas<T>> Cards {get;  set;}

         IFichas <T> SelectCard();
    }
}