namespace matcom_domino;

public class TypeFace

{
    private string nombre;
    private int valor;

    public TypeFace(string nombre ,int valor)
    {
        this.nombre = nombre;
        this.valor = valor;
    }

    public string Nombre
    {
        get => nombre;
    }
    

    public int Valor
    {
        get => valor;
    }
}