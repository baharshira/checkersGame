using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class GameLogicAndMoves
    {
        private static bool nextCellIsOutOfBoundries(Board i_Board, Player i_Player)
        {

            return i_Player.NextCellRow < 0 || i_Player.NextCellRow >= i_Board.BoardSize
            || i_Player.NextCellCol < 0 || i_Player.NextCellCol >= i_Board.BoardSize;
        }

        private static bool currentCellIsOutOfBoundries(Board i_Board, Player i_Player)
        {

            return i_Player.CurrentCellRow < 0 || i_Player.CurrentCellRow >= i_Board.BoardSize
            || i_Player.CurrentCellCol < 0 || i_Player.CurrentCellCol >= i_Board.BoardSize;
        }

        private static bool isMyPawn(Board i_Board, Player i_Player)
        {
            int row = i_Player.CurrentCellRow;
            int col = i_Player.CurrentCellCol;

            return i_Player.MyType == i_Board.Matrix[row, col] || i_Player.MyKingType == i_Board.Matrix[row, col];
        }

        private static bool isEmptyCell(Board i_Board, Player i_Player)
        {
            int row = i_Player.NextCellRow;
            int col = i_Player.NextCellCol;

            return i_Board.Matrix[row, col] == eCell.Empty;
        }

        private static bool isValidRowChanging(Board i_Board, Player i_Player)
        {
            eCell PawnType = i_Board.Matrix[i_Player.CurrentCellRow, i_Player.CurrentCellCol];
            bool isValidRowChanging = false;

            if (PawnType == eCell.X)
            {
                isValidRowChanging = i_Player.CurrentCellRow == i_Player.NextCellRow + 1;
            }

            if (PawnType == eCell.O)
            {
                isValidRowChanging = i_Player.CurrentCellRow == i_Player.NextCellRow - 1;
            }

            if (PawnType == eCell.Q || PawnType == eCell.Z)
            {
                isValidRowChanging = (i_Player.CurrentCellRow == i_Player.NextCellRow - 1) || (i_Player.CurrentCellRow == i_Player.NextCellRow + 1);
            }

            return isValidRowChanging;
        }

        private static bool isValidColChanging(Board i_Board, Player i_Player)
        {
            return (i_Player.CurrentCellCol == i_Player.NextCellCol - 1) || (i_Player.CurrentCellCol == i_Player.NextCellCol + 1);
        }

        private static bool isValidMoveWithoutEating(Board i_Board, Player i_Player)
        {
            return isValidColChanging(i_Board, i_Player) && isValidRowChanging(i_Board, i_Player) && !nextCellIsOutOfBoundries(i_Board, i_Player) && isEmptyCell(i_Board, i_Player) && isMyPawn(i_Board, i_Player);
        }

        private static bool canEatEnemeyPawn(Board i_Board, Player i_Player)
        {
            bool canEatEnemyPawn = false;
            eCell[] enemies = Player.InitEnemies(i_Player);
            eCell empty = eCell.Empty;
            int row = i_Player.CurrentCellRow;
            int col = i_Player.CurrentCellCol;
            eCell currentCell = i_Board.Matrix[row, col];

            if (currentCell == eCell.X)
            {
                canEatEnemyPawn = xCanEat(i_Board, enemies, empty, row, col, canEatEnemyPawn);
            }

            if (currentCell == eCell.O)
            {
                canEatEnemyPawn = oCanEat(i_Board, enemies, empty, row, col, canEatEnemyPawn);
            }

            if (currentCell == eCell.Z)
            {
                canEatEnemyPawn = xCanEat(i_Board, enemies, empty, row, col, canEatEnemyPawn) || oCanEat(i_Board, enemies, empty, row, col, canEatEnemyPawn);
            }

            if (currentCell == eCell.Q)
            {
                canEatEnemyPawn = xCanEat(i_Board, enemies, empty, row, col, canEatEnemyPawn) || oCanEat(i_Board, enemies, empty, row, col, canEatEnemyPawn);
            }

            return canEatEnemyPawn;
        }

        private static bool xCanEat(Board i_Board, eCell[] i_Enemies, eCell i_Empty, int i_Row, int i_Col, bool i_CanEatEnemyPawn = false)
        {
            if (!IndexesOutOfBounds(i_Board, i_Row - 1, i_Col - 1) && !IndexesOutOfBounds(i_Board, i_Row - 2, i_Col - 2))
            {
                if (i_Enemies.Contains(i_Board.Matrix[i_Row - 1, i_Col - 1])) //left
                {
                    if (i_Board.Matrix[i_Row - 2, i_Col - 2] == i_Empty)
                    {
                        i_CanEatEnemyPawn = true;
                    }
                }
            }

            if (!IndexesOutOfBounds(i_Board, i_Row - 1, i_Col + 1) && !IndexesOutOfBounds(i_Board, i_Row - 2, i_Col + 2))
            {
                if (i_Enemies.Contains(i_Board.Matrix[i_Row - 1, i_Col + 1])) //right
                {
                    if (i_Board.Matrix[i_Row - 2, i_Col + 2] == i_Empty)
                    {
                        i_CanEatEnemyPawn = true;
                    }
                }
            }

            return i_CanEatEnemyPawn;
        }

        private static bool oCanEat(Board i_Board, eCell[] i_Enemies, eCell i_Empty, int i_Row, int i_Col, bool i_CanEatEnemyPawn = false)
        {
            if (!IndexesOutOfBounds(i_Board, i_Row + 1, i_Col - 1) && !IndexesOutOfBounds(i_Board, i_Row + 2, i_Col - 2))
            {
                if (i_Enemies.Contains(i_Board.Matrix[i_Row + 1, i_Col - 1])) //left
                {
                    if (i_Board.Matrix[i_Row + 2, i_Col - 2] == i_Empty)
                    {
                        i_CanEatEnemyPawn = true;
                    }
                }
            }

            if (!IndexesOutOfBounds(i_Board, i_Row + 1, i_Col + 1) && !IndexesOutOfBounds(i_Board, i_Row + 2, i_Col + 2))
            {
                if (i_Enemies.Contains(i_Board.Matrix[i_Row + 1, i_Col + 1])) //right
                {
                    if (i_Board.Matrix[i_Row + 2, i_Col + 2] == i_Empty)
                    {
                        i_CanEatEnemyPawn = true;
                    }
                }
            }

            return i_CanEatEnemyPawn;
        }

        private static void deleteEnemiesThatIAte(Board i_Board, Player i_Player)
        {
            int row = i_Player.CurrentCellRow;
            int col = i_Player.CurrentCellCol;

            if (!(i_Player.MyType == eCell.O) && i_Player.NextCellCol == col + 2 && i_Player.NextCellRow == row - 2)
            {
                i_Board.Matrix[row - 1, col + 1] = eCell.Empty; // right
            }

            if (!(i_Player.MyType == eCell.O) && i_Player.NextCellCol == col - 2 && i_Player.NextCellRow == row - 2)
            {
                i_Board.Matrix[row - 1, col - 1] = eCell.Empty; // left
            }

            if (!(i_Player.MyType == eCell.X) && i_Player.NextCellCol == col + 2 && i_Player.NextCellRow == row + 2)
            {
                i_Board.Matrix[row + 1, col + 1] = eCell.Empty; // right
            }

            if (!(i_Player.MyType == eCell.X) && i_Player.NextCellCol == col - 2 && i_Player.NextCellRow == row + 2)
            {
                i_Board.Matrix[row + 1, col - 1] = eCell.Empty; // left
            }
        }

        private static bool checkIfWantsToEatEnemy(Board i_Board, Player i_Player)
        {
            bool checkIfWantsToEatEnemy = false;
            int row = i_Player.CurrentCellRow;
            int col = i_Player.CurrentCellCol;
            eCell currentCell = i_Board.Matrix[row, col];

            if (currentCell == eCell.X)
            {
                checkIfWantsToEatEnemy = checkIfXWantsToEatEnemy(i_Player, row, col);
            }

            if (currentCell == eCell.O)
            {
                checkIfWantsToEatEnemy = checkIfOWantsToEatEnemy(i_Player, row, col);
            }

            if (currentCell == eCell.Z)
            {
                checkIfWantsToEatEnemy = checkIfXWantsToEatEnemy(i_Player, row, col) || checkIfOWantsToEatEnemy(i_Player, row, col);
            }

            if (currentCell == eCell.Q)
            {
                checkIfWantsToEatEnemy = checkIfXWantsToEatEnemy(i_Player, row, col) || checkIfOWantsToEatEnemy(i_Player, row, col);
            }

            return checkIfWantsToEatEnemy;
        }

        private static bool checkIfOWantsToEatEnemy(Player i_Player, int i_Row, int i_Col)
        {

            return (i_Player.NextCellRow == i_Row + 2) && (i_Player.NextCellCol == i_Col + 2 || i_Player.NextCellCol == i_Col - 2);
        }

        private static bool checkIfXWantsToEatEnemy(Player i_Player, int i_Row, int i_Col)
        {

            return (i_Player.NextCellRow == i_Row - 2) && (i_Player.NextCellCol == i_Col + 2 || i_Player.NextCellCol == i_Col - 2);
        }
        private static bool isValidEating(Board i_Board, Player i_Player)
        {

            return canEatEnemeyPawn(i_Board, i_Player) && checkIfWantsToEatEnemy(i_Board, i_Player);
        }


        public static bool IsValidMove(Board i_Board, Player i_Player)
        {
           bool isValidMove = true;

            if (nextCellIsOutOfBoundries(i_Board, i_Player) || currentCellIsOutOfBoundries(i_Board, i_Player))
            {
                isValidMove = false;
            }

            if (!isValidMoveWithoutEating(i_Board, i_Player) && !isValidEating(i_Board, i_Player))
            {
                isValidMove = false;
            }

            return isValidMove;
        }

        public static void MakeAMove(Board i_Board, Player i_Player)
        {
            if (IsValidMove(i_Board, i_Player))
            {
                if (isValidEating(i_Board, i_Player))
                {
                    deleteEnemiesThatIAte(i_Board, i_Player);
                }

                i_Board.Matrix[i_Player.NextCellRow, i_Player.NextCellCol] = i_Board.Matrix[i_Player.CurrentCellRow, i_Player.CurrentCellCol];
                i_Board.Matrix[i_Player.CurrentCellRow, i_Player.CurrentCellCol] = eCell.Empty;
                checkIfBecomeKing(i_Board, i_Player);
            }
        }

        private static void checkIfBecomeKing(Board i_Board, Player i_Player)
        {
            if (i_Board.Matrix[i_Player.NextCellRow, i_Player.NextCellCol] == eCell.X && i_Player.NextCellRow == 0)
            {
                i_Board.Matrix[i_Player.NextCellRow, i_Player.NextCellCol] = eCell.Z;
            }

            if (i_Board.Matrix[i_Player.NextCellRow, i_Player.NextCellCol] == eCell.O && (i_Player.NextCellRow == i_Board.BoardSize - 1))
            {
                i_Board.Matrix[i_Player.NextCellRow, i_Player.NextCellCol] = eCell.Q;
            }
        }

        public static bool IndexesOutOfBounds(Board i_Board, int i_Row, int i_Col)
        {
            return i_Row < 0 || i_Col >= i_Board.BoardSize || i_Row >= i_Board.BoardSize || i_Col < 0;
        }
    }
}
