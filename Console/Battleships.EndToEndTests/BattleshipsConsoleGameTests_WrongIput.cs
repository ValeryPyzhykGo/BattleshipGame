using NUnit.Framework;
using Battleships.ConsoleWrapper;
using Moq;
using System.Collections.Generic;

namespace Battleships.EndToEndTests
{
   public class BattleshipsConsoleGameTests_WrongIput
   {
      IBattleshipsConsoleGameMessages _battleshipsMessages;
      Mock<IConsoleWraper> _consoleWrapperMock;

      [SetUp]
      public void SetUp()
      {
         _battleshipsMessages = new BattleshipsConsoleGameMessagesEng();
         _consoleWrapperMock = new Mock<IConsoleWraper>();

      }

      private void SetUpSteps( params IEnumerable<string>[] listOfSteps )
      {
         var step = -1;
         var steps = new List<string>();
         foreach ( var s in listOfSteps )
         {
            steps.AddRange( s );
         }

         _consoleWrapperMock.Setup( x => x.ReadLine() ).Returns( () =>
         {
            step++;
            return steps[step];
         } );
      }

      [Test]
      public void Play_WithOverlapedShipPlacing_ShouldFinishGameWithoutErrors()
      {
         // Arrange
         var setUpStepsWithCollision = new List<string> {
                "A", "1", "Y",
                "A", "1", "Y",
                "C", "3", "N",
                "C", "3", "N",
                "D", "6", "N",
                "J", "4", "Y",
                "E", "7", "Y",
            };

         SetUpSteps( setUpStepsWithCollision, Steps.CorrectSuccessiveSteps, Steps.NoNewGame );

         // Act
         new BattleshipsConsoleGame( _battleshipsMessages, _consoleWrapperMock.Object ).Start();

         // Assert 
         AssertGameHasFinishedCorrectly( Times.Once() );
      }

      [Test]
      public void Play_WithSameMoveTwice_ShouldFinishGameWithoutErrors()
      {
         // Arrange
         var stepsWithRepeatedMove = new List<string> {
                    "A", "1",
                    "A", "1"
            };

         SetUpSteps(
             Steps.CorrectShipPlaicing,
             stepsWithRepeatedMove,
             Steps.CorrectSuccessiveSteps,
             Steps.NoNewGame );

         // Act
         new BattleshipsConsoleGame( _battleshipsMessages, _consoleWrapperMock.Object ).Start();

         // Assert 
         AssertGameHasFinishedCorrectly( Times.Once() );
      }

      public void AssertGameHasFinishedCorrectly( Times times )
      {
         _consoleWrapperMock.Verify( m => m.WriteLine( It.Is<string>( input => input == _battleshipsMessages.GameOver ) ), times );
         _consoleWrapperMock.Verify( m => m.WriteLine( It.Is<string>( input => input == _battleshipsMessages.GameIsBrokenMessage ) ), Times.Never );
      }
   }
}