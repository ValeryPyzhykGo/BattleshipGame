using Battleships.Core;
using System.Collections.Generic;

namespace Battleships.ConsoleWrapper
{
   public class BattleshipsConsoleGameMessagesEng : IBattleshipsConsoleGameMessages
   {
      static readonly Dictionary<ShipClass, char> _shipLetters = new Dictionary<ShipClass, char> {
            { ShipClass.Carrier,    'C' },
            { ShipClass.Battleship, 'B' },
            { ShipClass.Destroyer,  'D' },
            { ShipClass.Submarine,  'S' },
            { ShipClass.Cruiser,    'R' }
        };

      public string TryAgainMessage => "Please, try again!";
      public string GameIsBrokenMessage => "Sorry, the game has crushed.";
      public string TryAgainYesNoQuestionMessage => "Enter Y for yes or N for not";
      public char YesAnswer => 'Y';
      public char NoAnswer => 'N';
      public string WantToRestartMessage => "Do you want to start a new game? (Y/N)";
      public string EnterColumnLetterMessage => "Enter column letter!";
      public string TheColumLetterIsIncorrectMessage => "The column letter is incorrect!";
      public string EnterRowNumberMessage => "Enter row number!";
      public string TheRowNumberIsIncorrectMessage => "The number of row is incorrect!";
      public string PlaceShipVerticalyQuestionMessage => "Do you want to place a ship vertialy? (Y/N)";
      public string IncorrectPlaceForShip => "You cannot place a ship here.";
      public string GameOver => "Game Over!";
      public string UserName => "You";
      public string AIName => "Computer";

      public string GetPlaceShipMessage( ShipClass shipClass )
      {
         return $"Please place a {shipClass} ship!";
      }

      public char GetShipLetter( ShipClass shipClass )
      {
         return _shipLetters[shipClass];
      }
      public string GetCellIsAlreadyDiscoverdMessage( char column, int row )
      {
         return $"The cell {column}{row} is alrady discovered.";
      }

      public string GetMissMessage( string playerName )
      {
         return $"{playerName} missed!";
      }

      public string GetHitMessage( string playerName, ShipClass shipClass )
      {
         return $"{playerName} hit {shipClass}!";
      }

      public string GetSankMessage( ShipClass shipClass )
      {
         return $"{shipClass} sank!";
      }

      public string GetPlayerWinMessage( Player player )
      {
         var playerName = player == Player.User ? UserName : AIName;
         return $"{playerName} won!";
      }
   }
}
