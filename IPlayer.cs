namespace matcom_domino
{
    public interface IPlayer<T>
    {
        //mismo errror que con la lsita de la clase referee del inumeroable
        
        //cambie el repardor de fichas xq lo tenia puesto en la clase referee pero pienso k el jugador es el k tenga la funcion de coher la fichas
        
         List<IFichas<T>>ManoDeFichas{get;}  
         IFichas <T> SelectCard();
         
         

         void Play(IFichas<T> ficha);

    }
    public class Player : IPlayer<int>   //despues hay k hacerlo generico
    {
        public IMesa<int> table
        {
            get;
        }

        public Player(IMesa<int> Table)
        {
            manoficha = new List<IFichas<int>>();
            this.table = Table;

        }

        public List<IFichas<int>> ManoDeFichas
        {
            get => this.manoficha;
        }

        private List<IFichas<int>> manoficha;
        
        public IFichas<int> SelectCard()
        {
            throw new NotImplementedException();
        }

        public void Play(IFichas<int> ficha)
        {
            if (table.IsValido(ficha))
            { 
                manoficha.Remove(ficha);
                table.CardinTable.Add(ficha);
                
            }

        }
    }
}