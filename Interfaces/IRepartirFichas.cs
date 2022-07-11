namespace matcom_domino.Interfaces;

public interface IRepartirFichas<T>
{
    void Repartir(List<IFichas<T>> AllTokens, List<IPlayer<T>> PlayerList, int TokenQty);
}