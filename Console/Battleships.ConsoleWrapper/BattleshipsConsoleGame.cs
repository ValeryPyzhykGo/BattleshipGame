using Battleships.Core;
using Battleships.Core.Exceptions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo( "DynamicProxyGenAssembly2" )]
[assembly: InternalsVisibleTo( "Battleships.ConsoleWrapper.E2ETests" )]
[assembly: InternalsVisibleTo( "Battleships.ConsoleWrapper.UnitTests" )]
namespace Battleships.ConsoleWrapper
{
   public class BattleshipsConsoleGame
   {
      private readonly IConsoleWraper _console;
      private readonly IBoardsConsoleUI _boardsUI;
      private readonly IBattleshipsGameSetUpFactory _gameSetUpFactory;
      private readonly IBattleshipsConsoleGameMessages _messages;

      public BattleshipsConsoleGame( IBattleshipsConsoleGameMessages messages = null )
      {
         _messages = messages ?? new BattleshipsConsoleGameMessagesEng();
         _console = new ConsoleWraper();
         _boardsUI = new BoardsConsoleUI( _messages, _console );
      }

      internal BattleshipsConsoleGame( IBattleshipsConsoleGameMessages messages, IConsoleWraper console, IBoardsConsoleUI boardsUI = null, IBattleshipsGameSetUpFactory gameSetUpFactory = null )
      {
         _console = console;
         _messages = messages;
         _boardsUI = boardsUI ?? new BoardsConsoleUI( _messages, _console );
         _gameSetUpFactory = gameSetUpFactory;
      }

      #region Game
      public void Start()
      {
         try
         {
            do
            {
               PlayGame( SetUpGame() );
            } while ( AskBoolednQuestionWithRetry( _messages.WantToRestartMessage ) );
         }
         catch
         {
            _console.WriteLine( _messages.GameIsBrokenMessage );
         }
      }

      private IBattleshipsGame SetUpGame()
      {
         var gameSetUp = _gameSetUpFactory?.Create() ?? new BattleshipsGameSetup();
         foreach ( var shipClass in gameSetUp.ShipsToAdd )
         {
            _console.Clear();
            _boardsUI.ShowBoards( gameSetUp.PlayerBoard, gameSetUp.OpponentBoard );
            var result = TryPlaceShip( gameSetUp, shipClass );
            while ( !result )
            {
               _console.Clear();
               _boardsUI.ShowBoards( gameSetUp.PlayerBoard, gameSetUp.OpponentBoard );
               _console.WriteLine( _messages.IncorrectPlaceForShip );
               _console.WriteLine( _messages.TryAgainMessage );
               result = TryPlaceShip( gameSetUp, shipClass );
            }
         }

         return gameSetUp.StartGame();
      }

      private bool TryPlaceShip( IBattleshipsGameSetUp gameBuilder, ShipClass shipClass )
      {
         _console.WriteLine( _messages.GetPlaceShipMessage( shipClass ) );
         var (column, row) = AskForCoordinatesWithRetry();
         var isVertical = AskBoolednQuestionWithRetry( _messages.PlaceShipVerticalyQuestionMessage );
         return gameBuilder.TryPlaceShip( column, row, isVertical, shipClass );
      }

      private void PlayGame( IBattleshipsGame game )
      {
         _console.Clear();
         _boardsUI.ShowBoards( game.PlayerBoard, game.OpponentBoard );
         
         while ( !game.IsOver )
         {
            var (playerMoveResult, aiMoveResult) = MakeMoveWithRetryInCaseOfBadMove( game );
            _console.Clear();
            _boardsUI.ShowBoards( game.PlayerBoard, game.OpponentBoard );
            WriteMoveResult( _messages.UserName, playerMoveResult );
            WriteMoveResult( _messages.AIName, aiMoveResult );
            
         }
         _console.WriteLine( _messages.GameOver );
         _console.WriteLine( _messages.GetPlayerWinMessage( game.GetWinner().Value ) );
      }

      private (Ship, Ship) MakeMoveWithRetryInCaseOfBadMove( IBattleshipsGame game )
      {
         while ( true )
         {
            var (column, row) = AskForCoordinatesWithRetry();
            try
            {
               return game.MakeMoveAndLetOppoentMove( column, row );
            }
            catch ( CellIsAlreadyDiscoveredException )
            {
               _console.WriteLine( _messages.GetCellIsAlreadyDiscoverdMessage( column, row ) );
               _console.Write( _messages.TryAgainMessage );
            }
         }
      }
      #endregion

      #region Output

      private void WriteMoveResult( string player, Ship ship )
      {
         if ( ship == null )
         {
            _console.WriteLine( _messages.GetMissMessage( player ) );
         }
         else
         {
            _console.WriteLine( _messages.GetHitMessage( player, ship.ShipClass ) );
            if ( ship.LifesLeft < 1 )
            {
               _console.WriteLine( _messages.GetSankMessage( ship.ShipClass ) );
            }
         }
         _console.WriteLine();
      }

      #endregion

      #region Input
      private bool AskBoolednQuestionWithRetry( string question )
      {
         _console.WriteLine( question );
         var line = _console.ReadLine().ToUpper();
         while ( line.Length != 1 || ( line[0] != _messages.YesAnswer && line[0] != _messages.NoAnswer ) )
         {
            _console.WriteLine( _messages.TryAgainYesNoQuestionMessage );
            line = _console.ReadLine().ToUpper();
         }
         return line[0] == _messages.YesAnswer;
      }

      private (char, int) AskForCoordinatesWithRetry()
      {
         _console.WriteLine( _messages.EnterColumnLetterMessage );
         var columnString = _console.ReadLine();

         while ( columnString.Length != 1 || columnString[0] < BoardSize.FirstColumnLetter || columnString[0] > BoardSize.LastColumnLetter )
         {
            _console.WriteLine( _messages.TheColumLetterIsIncorrectMessage );
            _console.WriteLine( _messages.TryAgainMessage );
            _console.WriteLine( _messages.EnterColumnLetterMessage );
            columnString = _console.ReadLine();
         }

         _console.WriteLine( _messages.EnterRowNumberMessage );
         var rowStirng = _console.ReadLine();
         int row;

         while ( !int.TryParse( rowStirng, out row ) || row < BoardSize.BoardFirstRowNumber || row > BoardSize.BoardLastRowNumber )
         {
            _console.WriteLine( _messages.TheRowNumberIsIncorrectMessage );
            _console.WriteLine( _messages.TryAgainMessage );
            _console.WriteLine( _messages.EnterRowNumberMessage );
            rowStirng = _console.ReadLine();
         }

         return (columnString[0], row);
      }
      #endregion
   }
}
