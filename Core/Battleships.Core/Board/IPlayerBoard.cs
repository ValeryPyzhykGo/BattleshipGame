namespace Battleships.Core
{
   public interface IPlayerBoard
   {
      public int LifesLeft
      {
         get;
      }
      public bool TryPlaceShip( char column, int row, bool isVertical, Ship ship );
      public CellStatus GetStatus( char column, int row );
      public ShipClass? GetShipClass( char column, int row );
   }
}