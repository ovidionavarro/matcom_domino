namespace matcom_domino.Interfaces;

public interface IGenerarFichas<T>
{
    List<IFichas<T>> GenerateCards(int a);
    
}
