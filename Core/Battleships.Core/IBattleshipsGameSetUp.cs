using System.Collections.Generic;

namespace Battleships.Core
{
   public interface IBattleshipsGameSetUp
   {
      IOpponentBoard OpponentBoard
      {
         get;
      }
      IPlayerBoard PlayerBoard
      {
         get;
      }
      IList<ShipClass> ShipsToAdd { get; }

      bool TryPlaceShip( char column, int row, bool isVertical, ShipClass shipClass );
      IBattleshipsGame StartGame();
   }
}
