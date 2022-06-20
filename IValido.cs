namespace matcom_domino;

public interface IValido
{
    void RecibirJugada(IFichas<int> ficha);
    bool IsValido(IFichas<int> ficha);
}

