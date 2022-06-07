using matcom_domino;
using System.Collections.Generic;
namespace matcom_domino
{

    /*class PlayerNumber : IPlayer<int>
     {
         public IEnumerable<IFichas<int>> HandCards { get ; set; }

         public PlayerNumber()
         {
             HandCards = new List<IFichas<int>>();
         }

         public IFichas<int> SelectCard()
         {
             return HandCards.ToArray()[0];
         }
     }*/

    // class PlayerString : IPlayer<string>
    // {
    //     public IEnumerable<IFichas<string>> Cards { get ; set; }

    //     public IFichas<string> SelectCard()
    //     {
    //         return Cards.ToArray()[0];            
    //     }
    // }


    class Program
    {



        static void Main(string[] args)
        {

            //Fichas9 a=new Fichas9(3,5);
            //System.Console.WriteLine("{0},{1},{2}",a.GetFace(2),a.GetFace(1),a.GetFace(3));
            Domino<int> c = new Referee9();
            /*for(int i=0;i<Referee9.ConjuntodeFichas.Count();i++){
                System.Console.WriteLine("{0}{1}",Referee9.ConjuntodeFichas[i].GetFace(1),Referee9.ConjuntodeFichas[i].GetFace(2));
            }*/
            //var x=c.GeneratedCards.ToList<IFichas<int>>();
            //List<IFichas<int>> x
            /*System.Console.WriteLine(Referee9.ToList<IFichas<int>>().Count());
            System.Console.WriteLine(Referee9.ConjuntodeFichas.ToList<IFichas<int>>()[3].GetFace(2));
            for(int i=0;i<Referee9.ConjuntodeFichas.ToList<IFichas<int>>().Count()-1;i++){
               System.Console.WriteLine("{0},{1}",c.ConjuntoCards.ToList<IFichas<int>>()[i].GetFace(1),c.ConjuntoCards.ToList<IFichas<int>>()[i].GetFace(2));
            } */
            c.GeneratedCards();
            //List<IFichas<int>> wh=new List<IFichas<int>>();
            //c.temp[2].GetFace(1);
            c.ConjuntodeFichas.RemoveAt(54);
            System.Console.WriteLine(c.ConjuntodeFichas.Count());
            foreach (var a in c.ConjuntodeFichas)
            {
                System.Console.WriteLine(a);
            }




        }
    }
}