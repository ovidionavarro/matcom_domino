namespace matcom_domino.Interfaces;

public interface IGameOrden<T>
{
    void OrdendelJuego(List<IPlayer<T>> jugadores);
}
