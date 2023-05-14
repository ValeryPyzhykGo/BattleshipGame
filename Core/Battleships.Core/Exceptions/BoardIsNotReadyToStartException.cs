namespace Battleships.Core.Exceptions
{
   public class BoardIsNotReadyToStartException : BattleshipsGameException
   {
      public BoardIsNotReadyToStartException( string player ) : base( $"The game cannot be started for {player} board hasn't been set up." )
      {
      }
   }
}
