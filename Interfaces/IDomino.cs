namespace matcom_domino.Interfaces;

public interface IDomino<T>
{
    //orden en el k se juega,como se reparten las fichas,cuando fializa
    //*no se le asigna valor a la fichas cuando se tranca xq eso lo hacen las fichas
    //error en crear uun campo conjunto de fichs ,nose me almacenan las fichas ahi

    //public bool Tranke();

    //void GeneratedCards(int k); //
    List<IFichas<T>> ConjuntodeFichas { get; }
    List<IPlayer<T>> Jugadores { get; }
    void Robar();
    ITranke<T> _tranke { get; }
    void AgregarJugador(IPlayer<T> a);
    IRepartirFichas<T> _repartirFichas { get; }
    void RepartirFichas(int k);

    IGenerarFichas<T> _generarFichas { get; }
    IGameOrden<int> orden { get; }
    void StartGame();
    bool EndGame();

    IWinner<T> Winner { get; set; }
}