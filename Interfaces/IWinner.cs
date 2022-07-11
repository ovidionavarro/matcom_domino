namespace matcom_domino.Interfaces;

public interface IWinner<T>
{
    IPlayer<T> PlayerWinner(List<IPlayer<T>> PlayerList);
}
