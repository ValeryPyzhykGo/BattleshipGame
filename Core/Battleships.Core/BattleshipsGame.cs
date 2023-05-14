using Battleships.Core.Exceptions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo( "Batteships.Core.Tests" )]
namespace Battleships.Core
{
   public class BattleshipsGame : IBattleshipsGame
   {
      public IPlayerBoard PlayerBoard => _firstPlayerBoard;
      public IOpponentBoard OpponentBoard => _secondPlayerBoard;

      private readonly IBoard _firstPlayerBoard;
      private readonly IBoard _secondPlayerBoard;
      private readonly IAIPlayer _aIPlayer;

      public bool IsOver => ( _firstPlayerBoard as IOpponentBoard ).LifesLeft == 0
          || ( _secondPlayerBoard as IOpponentBoard ).LifesLeft == 0;

      internal BattleshipsGame( IBoard firstUserBoard, IBoard secondPlaeyerBoard, IAIPlayer aIPlayer )
      {
         _firstPlayerBoard = firstUserBoard;
         _secondPlayerBoard = secondPlaeyerBoard;
         _aIPlayer = aIPlayer;
      }
      public (Ship, Ship) MakeMoveAndLetOppoentMove( char column, int row )
      {
         if ( IsOver )
         {
            throw new MoveAfterGameOverException();
         }

         return (_secondPlayerBoard.MakeMove( column, row ), GetOpponentMove());
      }

      public Player? GetWinner()
      {
         if ( !IsOver )
         {
            return null;
         }
         return ( _firstPlayerBoard as IOpponentBoard ).LifesLeft == 0 ? Player.Computer : Player.User;
      }
      private Ship GetOpponentMove()
      {
         var (column, row) = _aIPlayer.MakeMove( _firstPlayerBoard );
         return _firstPlayerBoard.MakeMove( column, row );
      }
   }
}
