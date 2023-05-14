using System;

namespace Battleships.Core
{
   internal class AIPlayer : IAIPlayer
   {
      private readonly Random _rand = new Random();

      public void PlaceSheep( ShipClass shipClass, IPlayerBoard board )
      {
         var ship = new Ship( shipClass );
         while ( !board.TryPlaceShip( GetRandomColumn(), GetRandomRow(), _rand.Next( 0, 1 ) == 1, ship ) )
         {
         }
      }

      public (char, int) MakeMove( IOpponentBoard opponentBoard )
      {
         var column = GetRandomColumn();
         var row = GetRandomRow();
         while ( opponentBoard.GetStatus( column, row ) != CellStatus.Undescovered )
         {
            column = GetRandomColumn();
            row = GetRandomRow();
         }
         return (column, row);
      }

      private char GetRandomColumn()
      {
         return (char) ( BattleshipsGameConstans.FirstColumnLetter + _rand.Next( 0, BattleshipsGameConstans.BoardSideSize ) );
      }

      private int GetRandomRow()
      {
         return _rand.Next( BattleshipsGameConstans.BoardFirstRowNumber, BattleshipsGameConstans.BoardLastRowNumber );
      }
   }
}
