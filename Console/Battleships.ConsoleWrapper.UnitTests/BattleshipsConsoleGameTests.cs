using System.Collections.Generic;
using Battleships.Core;
using Moq;
using NUnit.Framework;

namespace Battleships.ConsoleWrapper.Tests
{
   [TestFixture]
   public class BattleshipsConsoleGameTests
   {
      private const string _gameOverMessage = "GameOverMessage";
      private const string _gameIsBroken = "Someone Won";
      private const string _someoneWonMessage = "Someone Won";

      private Mock<IConsoleWraper> _consoleWrapperMock;
      private Mock<IBattleshipsConsoleGameMessages> _battleshipsMessages;
      private Mock<IBoardsConsoleUI> _boardConsoleUI;
      private Mock<IBattleshipsGameSetUpFactory> _factory;
      private Mock<IBattleshipsGameSetUp> _gameSetUp;
      private Mock<IBattleshipsGame> _game;
      private Mock<IPlayerBoard> _playerBoard;
      private Mock<IOpponentBoard> _opponentBoard;

      [SetUp]
      public void SetUp()
      {
         _consoleWrapperMock = new Mock<IConsoleWraper>();
         _battleshipsMessages = new Mock<IBattleshipsConsoleGameMessages>();
         _boardConsoleUI = new Mock<IBoardsConsoleUI>();
         _factory = new Mock<IBattleshipsGameSetUpFactory>();
         _gameSetUp = new Mock<IBattleshipsGameSetUp>();
         _game = new Mock<IBattleshipsGame>();
         _playerBoard = new Mock<IPlayerBoard>();
         _opponentBoard = new Mock<IOpponentBoard>();

         _gameSetUp.Setup( x => x.StartGame() ).Returns( () => _game.Object );
         _gameSetUp.SetupGet( x => x.ShipsToAdd ).Returns( new List<ShipClass>() );
         _gameSetUp.SetupGet( x => x.PlayerBoard ).Returns(() => _playerBoard.Object );
         _gameSetUp.SetupGet( x => x.OpponentBoard ).Returns( () => _opponentBoard.Object );
         _game.SetupGet( x => x.PlayerBoard ).Returns( () => _playerBoard.Object );
         _game.SetupGet( x => x.OpponentBoard ).Returns( () => _opponentBoard.Object );
         _game.SetupGet( x => x.IsOver ).Returns( true );

         _factory.Setup( x => x.Create() ).Returns( () => _gameSetUp.Object );

         _battleshipsMessages.SetupGet( x => x.YesAnswer ).Returns( 'Y' );
         _battleshipsMessages.SetupGet( x => x.NoAnswer ).Returns( 'N' );

         _battleshipsMessages.SetupGet( x => x.GameOver ).Returns( _gameOverMessage );
         _battleshipsMessages.Setup( x => x.GetPlayerWinMessage( It.IsAny<Player>() ) ).Returns( _someoneWonMessage );


      }

      [Test]
      public void Start_()
      {
            new BattleshipsConsoleGame( _battleshipsMessages.Object, _consoleWrapperMock.Object, _boardConsoleUI.Object, _factory.Object).Start();
            AssertGameHasFinishedCorrectly(Times.Once());
      }

      public void AssertGameHasFinishedCorrectly( Times times )
      {
         _consoleWrapperMock.Verify( m => m.WriteLine( It.Is<string>( input => input == _gameOverMessage ) ), times );
         _consoleWrapperMock.Verify( m => m.WriteLine( It.Is<string>( input => input == _someoneWonMessage ) ), times );
         _consoleWrapperMock.Verify( m => m.WriteLine( It.Is<string>( input => input == _gameIsBroken ) ), Times.Never );
      }
   }
}
