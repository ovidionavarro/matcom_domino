namespace matcom_domino.Interfaces;

public interface IFichas<T>
{
    T GetFace(int a);
    int ValueFace(T a);
    int FichaValue();
}