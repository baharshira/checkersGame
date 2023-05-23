using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Game
    {
        public Board Board { get; set; }
        public Player Turn { get; set; }
        public Queue<Player> PlayerQueue { get; }
        private Player m_Winner = new Player();
        public event Action ReloadBoard;
        public event Action ReloadInvalidMoveMsg;
        public event Action<bool> ReloadEndOfGameMsg;

        public Player Winner
        {
            get { return m_Winner; }
            set { m_Winner = value; }
        }

        public Game(int i_BoardSize, Player i_m_Player1, Player i_Player2)
        {
            Board = new Board(i_BoardSize);
            PlayerQueue = new Queue<Player>();
            i_m_Player1.MyType = eCell.X;
            i_Player2.MyType = eCell.O;
            i_m_Player1.MyKingType = eCell.Z;
            i_Player2.MyKingType = eCell.Q;
            PlayerQueue.Enqueue(i_m_Player1);
            PlayerQueue.Enqueue(i_Player2);
            Turn = PlayerQueue.Dequeue();
            Winner = null;
        }

        private bool isThereATie()
        {
            return Turn.AvailableMoves.Count == 0 && PlayerQueue.Peek().AvailableMoves.Count == 0; 
        }

        private bool isThereAWinner()
        {
            Player notMyTurn = PlayerQueue.Peek();

            notMyTurn.AvailablePawns = Board.AvailableCellsICanMoveTo(Board, notMyTurn.MyType, notMyTurn.MyKingType);
            bool resultOfGame = notMyTurn.AvailablePawns.Count == 0;

            if (resultOfGame)
            {
                Winner = Turn;
            }

            return resultOfGame; 
        }

        private void computeScore(Game i_Game)
        {
            Player notMyTurn = i_Game.PlayerQueue.Peek();
            int countMyKingsScore = Board.ReturnCells(i_Game.Board, i_Game.Turn.MyKingType, i_Game.Turn.MyKingType).Count * 4;
            int countMyPawnsScore = Board.ReturnCells(i_Game.Board, i_Game.Turn.MyKingType, i_Game.Turn.MyKingType).Count;
            int countEnemyKingsScore = Board.ReturnCells(i_Game.Board, notMyTurn.MyKingType, notMyTurn.MyKingType).Count * 4;
            int countEnemyPawnsScore = Board.ReturnCells(i_Game.Board, notMyTurn.MyKingType, notMyTurn.MyKingType).Count;
            
            i_Game.Turn.Score += Math.Abs(countMyKingsScore + countMyPawnsScore - (countEnemyKingsScore + countEnemyPawnsScore));
        }
    
        public void PlayGame(Game i_Game)
        {
            bool GameEndsBecauseOfATie;

            i_Game.Turn.AvailablePawns = Board.ReturnCells(i_Game.Board, i_Game.Turn.MyType, i_Game.Turn.MyKingType);
            i_Game.Turn.AvailableMoves = Board.AvailableCellsICanMoveTo(i_Game.Board, i_Game.Turn.MyType, i_Game.Turn.MyKingType);

            if (isThereATie())
            {
                GameEndsBecauseOfATie = true;
                OnEndOfGame(GameEndsBecauseOfATie);
            }

            if (isThereAWinner())
            {
                GameEndsBecauseOfATie = false;
                computeScore(i_Game);
                OnEndOfGame(GameEndsBecauseOfATie);
            }

            if (GameLogicAndMoves.IsValidMove(Board, Turn))
            {
                GameLogicAndMoves.MakeAMove(Board, Turn);
                updateTurn();
                if (Turn.Name.Equals("Computer"))
                {
                    ComputerPlayer.ComputersMove(Board, Turn);
                    updateTurn();
                }
            }
            else
            {
                OnInvalidMove();
            }
        }

        private void updateTurn()
        {
            Player tempPlayer = Turn;
            Turn = PlayerQueue.Dequeue();
            PlayerQueue.Enqueue(tempPlayer);
            OnUpdateUIBoard();
        }

        protected virtual void OnUpdateUIBoard()
        {
            if (ReloadBoard != null)
            {
                ReloadBoard.Invoke();
            }
        }

        protected virtual void OnInvalidMove()
        {
            if (ReloadInvalidMoveMsg != null)
            {
                ReloadInvalidMoveMsg.Invoke();
            }
        }

        protected virtual void OnEndOfGame(bool i_GameEndsBecauseOfATie)
        {
            if (ReloadEndOfGameMsg != null)
            {
                ReloadEndOfGameMsg.Invoke(i_GameEndsBecauseOfATie);
            }
        }
    }
}


