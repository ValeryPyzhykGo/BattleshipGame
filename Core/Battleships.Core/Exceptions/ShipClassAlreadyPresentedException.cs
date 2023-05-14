namespace Battleships.Core.Exceptions
{
   public class ShipClassAlreadyPresentedException : BattleshipsGameException
   {
      public ShipClassAlreadyPresentedException() : base( "Cannot the ship with this class for it's already added." )
      {
      }
   }
}
