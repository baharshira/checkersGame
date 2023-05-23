using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class ComputerPlayer
    {
        public static void ComputersMove(Board i_Board, Player i_Player)
        {
            List<int[]> computersAvailableMoves = Board.AvailableCellsICanMoveTo(i_Board, eCell.O, eCell.Q);
            var random = new Random();
            int index = random.Next(computersAvailableMoves.Count);

            if (computersAvailableMoves.Count > 0)
            {
                i_Player.CurrentCellRow = computersAvailableMoves[index][0];
                i_Player.CurrentCellCol = computersAvailableMoves[index][1];
                i_Player.NextCellRow = computersAvailableMoves[index][2];
                i_Player.NextCellCol = computersAvailableMoves[index][3];
                i_Player.MyKingType = eCell.Q;
                i_Player.MyType = eCell.O;
                GameLogicAndMoves.MakeAMove(i_Board, i_Player);
            }
        }
    }
}
