namespace matcom_domino.Interfaces;

public interface ITranke<T>

{
    bool Tranke(List<IPlayer<T>> PlayersList, IMesa<T> table);
}
