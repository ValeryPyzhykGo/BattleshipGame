using NUnit.Framework;
using Battleships.ConsoleWrapper;
using Moq;
using System.Collections.Generic;

namespace Battleships.EndToEndTests
{
   public class BattleshipsConsoleGameTests_HappyPath
   {
      IBattleshipsConsoleGameMessages _battleshipsMessages;
      Mock<IConsoleWraper> _consoleWrapperMock;

      [SetUp]
      public void SetUp()
      {
         _battleshipsMessages = new BattleshipsConsoleGameMessagesEng();
         _consoleWrapperMock = new Mock<IConsoleWraper>();

      }

      [Test]
      public void Play_OneGame_ShouldFinishGameWithoutErrors()
      {
         // Arrange

         var step = -1;
         var steps = new List<string>( Steps.CorrectShipPlaicing );
         steps.AddRange( Steps.CorrectSuccessiveSteps );
         steps.AddRange( Steps.NoNewGame );

         _consoleWrapperMock.Setup( x => x.ReadLine() ).Returns( () =>
         {
            step++;
            return steps[step];
         } );


         var game = new BattleshipsConsoleGame( _battleshipsMessages, _consoleWrapperMock.Object );

         // Act
         game.Start();

         // Assert
         AssertGameHasFinishedCorrectly( Times.Once() );
      }

      [Test]
      public void Play_ThreeGames_ShouldFinishGameWithoutErrorsThreeTimes()
      {
         // Arrange

         var step = -1;
         var steps = new List<string>( Steps.CorrectShipPlaicing );
         steps.AddRange( Steps.CorrectSuccessiveSteps );
         steps.AddRange( Steps.NewGame );

         steps.AddRange( Steps.CorrectShipPlaicing );
         steps.AddRange( Steps.CorrectSuccessiveSteps );
         steps.AddRange( Steps.NewGame );

         steps.AddRange( Steps.CorrectShipPlaicing );
         steps.AddRange( Steps.CorrectSuccessiveSteps );
         steps.AddRange( Steps.NoNewGame );

         _consoleWrapperMock.Setup( x => x.ReadLine() ).Returns( () =>
         {
            step++;
            return steps[step];
         } );


         var game = new BattleshipsConsoleGame( _battleshipsMessages, _consoleWrapperMock.Object );

         // Act
         game.Start();

         // Assert
         AssertGameHasFinishedCorrectly( Times.Exactly( 3 ) );
      }

      public void AssertGameHasFinishedCorrectly( Times times )
      {
         _consoleWrapperMock.Verify( m => m.WriteLine( It.Is<string>( input => input == _battleshipsMessages.GameOver ) ), times );
         _consoleWrapperMock.Verify( m => m.WriteLine( It.Is<string>( input => input == _battleshipsMessages.GameIsBrokenMessage ) ), Times.Never );
      }
   }
}