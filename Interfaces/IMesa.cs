namespace matcom_domino.Interfaces;

public interface IMesa<T>
{
    //jugar la ficha ,balidar la jugada, ver fichas en la mesa
    bool IsValido(IFichas<T> a); //boliano
    List<IFichas<T>> CardinTable { get; }
    void RecibirJugada(IFichas<T> ficha, int side);
    List<string> Log { get; }
    List<IFichas<T>> FichasSobrantes { get; set; }
    IFichas<T> fichaJugable { get; }
}
