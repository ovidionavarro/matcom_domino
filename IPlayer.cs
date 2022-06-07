namespace matcom_domino
{
    public interface IPlayer<T>
    {
        //mismo errror que con la lsita de la clase referee del inumeroable
        //IEnumerable<IFichas<T>> HandCards {get;  set;}
        //cambie el repardor de fichas xq lo tenia puesto en la clase referee pero pienso k el jugador es el k tenga la funcion de coher la fichas
         //IEnumerable<IFichas<T>> HandCards ();  
         List<IFichas<int>>ManoDeFichas{get;}  
         IFichas <T> SelectCard();
        
    }
    public class PlayerRandomint9 : IPlayer<int>//despues hay k hacerlo generico
    {
        public List<IFichas<int>>ManoDeFichas{get =>throw new NotImplementedException();}
        public IFichas<int> SelectCard()
        {
            throw new NotImplementedException();
        }
        public void TipodeReparticion()
        {
            throw new NotImplementedException();
        }
    }
}