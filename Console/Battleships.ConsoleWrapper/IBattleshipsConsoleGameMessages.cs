using Battleships.Core;

namespace Battleships.ConsoleWrapper
{
   public interface IBattleshipsConsoleGameMessages
   {
      string TryAgainMessage
      {
         get;
      }
      string GameIsBrokenMessage
      {
         get;
      }
      string TryAgainYesNoQuestionMessage
      {
         get;
      }
      char YesAnswer
      {
         get;
      }
      char NoAnswer
      {
         get;
      }
      string WantToRestartMessage
      {
         get;
      }
      string EnterColumnLetterMessage
      {
         get;
      }
      string TheColumLetterIsIncorrectMessage
      {
         get;
      }
      string EnterRowNumberMessage
      {
         get;
      }
      string TheRowNumberIsIncorrectMessage
      {
         get;
      }
      string PlaceShipVerticalyQuestionMessage
      {
         get;
      }
      string IncorrectPlaceForShip
      {
         get;
      }
      string UserName
      {
         get;
      }
      string AIName
      {
         get;
      }
      string GameOver
      {
         get;
      }

      string GetMissMessage( string playerName );
      string GetHitMessage( string playerName, ShipClass ship );
      string GetSankMessage( ShipClass shipClass );
      string GetPlaceShipMessage( ShipClass shipClass );
      char GetShipLetter( ShipClass shipClass );
      string GetCellIsAlreadyDiscoverdMessage( char column, int row );
      string GetPlayerWinMessage( Player v );
   }
}