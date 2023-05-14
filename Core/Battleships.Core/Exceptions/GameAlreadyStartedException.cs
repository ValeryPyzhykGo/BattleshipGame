namespace Battleships.Core.Exceptions
{
   public class GameAlreadyStartedException : BattleshipsGameException
   {
      public GameAlreadyStartedException() : base( "Cannot change game setup because it is already in progress." )
      {
      }
   }
}
