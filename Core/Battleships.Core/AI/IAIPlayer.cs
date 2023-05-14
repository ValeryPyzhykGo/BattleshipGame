namespace Battleships.Core
{
   internal interface IAIPlayer
   {
      void PlaceSheep( ShipClass shipClass, IPlayerBoard board );
      (char, int) MakeMove( IOpponentBoard opponentBoard );
   }
}