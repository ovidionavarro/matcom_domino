namespace matcom_domino
{
    public interface IReferee<T>
    {
        //orden en el k se juega,como se reparten las fichas,cuando fializa
        //*no se le asigna valor a la fichas cuando se tranca xq eso lo hacen las fichas

        IEnumerable<IFichas<T>> Cards {get; set;} 

         void GameOrden();
         void TipodeReparticion();
         bool EndGame();
         void Wins();
    }
}