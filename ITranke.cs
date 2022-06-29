namespace matcom_domino
{

    public interface ITranke<T>

    {
        bool Tranke(List<IPlayer<T>> PlayersList, IMesa<T> table);
    }

    public class TrankeClasico : ITranke<int>
    {
        public TrankeClasico()
        {

        }

        public bool Tranke(List<IPlayer<int>> PlayersList, IMesa<int> Table)
        {

            foreach (var player in PlayersList)
            {
                if (player.Pasarse)
                    return true;
                foreach (var ficha in player.ManoDeFichas)
                {
                    if (Table.IsValido(ficha))
                        return false;
                }
            }

            return true;
        }
    }
}