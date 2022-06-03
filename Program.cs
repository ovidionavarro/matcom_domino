using matcom_domino;
namespace matcom_domino{

    class PlayerNumber : IPlayer<int>
    {
        public IEnumerable<IFichas<int>> Cards { get ;  }

        public PlayerNumber()
        {
            Cards = new List<IFichas<int>>();
        }

        public IFichas<int> SelectCard()
        {
            return Cards.ToArray()[0];
        }
    }

    // class PlayerString : IPlayer<string>
    // {
    //     public IEnumerable<IFichas<string>> Cards { get ; set; }

    //     public IFichas<string> SelectCard()
    //     {
    //         return Cards.ToArray()[0];            
    //     }
    // }


    class Program {

        

        static void Main(string[] args){
            
            IPlayer<int> playerInt = new PlayerNumber();
            var x = playerInt.Cards;

        }
    }
}