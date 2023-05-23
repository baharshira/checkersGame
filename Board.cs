using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    // $G$ SFN-012 (+5) Bonus: Events in the Logic layer are handled by the UI.
	public class Board
    {
		private readonly int r_BoardSize;
		public eCell[,] Matrix { get; set; }
		public Board(int i_BoardSize)
		{
			Matrix = InitMatrix(i_BoardSize);
			r_BoardSize = i_BoardSize;
		}

		public int BoardSize
		{ 
			get { return r_BoardSize; }
		}

		private static int numberOfRowsToFillWithPlayers(int i_BoardSize)
		{
			int numberOfRowsToFillWithPlayers = 0;

			switch (i_BoardSize)
			{
				case 6:
					numberOfRowsToFillWithPlayers = 2;
					break;
				case 8:
					numberOfRowsToFillWithPlayers = 3;
					break;
				case 10:
					numberOfRowsToFillWithPlayers = 4;
					break;
			}

			return numberOfRowsToFillWithPlayers;
		}

		public eCell[,] InitMatrix(int i_BoardSize)
		{
			eCell[,] Matrix = new eCell[i_BoardSize, i_BoardSize];
			int numberOfRowsToFill = numberOfRowsToFillWithPlayers(i_BoardSize);
			int startPoint = numberOfRowsToFill + 2;

			for (int i = 0; i < numberOfRowsToFill; i++)
			{
				for (int j = 0; j < i_BoardSize; j++)
				{
					if ((i % 2 == 0 && j % 2 == 1) || (i % 2 == 1 && j % 2 == 0))
					{
						Matrix[i, j] = eCell.O;
					}
					else
					{
						Matrix[i, j] = eCell.Empty; 
					}
				}
			}

			for (int i = numberOfRowsToFill; i < startPoint; i++)
			{
				for (int j = 0; j < i_BoardSize; j++)
				{
					Matrix[i, j] = eCell.Empty; 
				}
			}

			for (int i = startPoint; i < i_BoardSize; i++)
			{
				for (int j = 0; j < i_BoardSize; j++)
				{
					if ((i % 2 == 0 && j % 2 == 1) || (i % 2 == 1 && j % 2 == 0))
					{
						Matrix[i, j] = eCell.X;
					}
					else
					{
						Matrix[i, j] = eCell.Empty; 
					}
				}
			}

			return Matrix;
		}

		public static List<int[]> ReturnCells(Board i_Board, eCell i_Type, eCell i_KingType)
		{
			List<int[]> returnCells = new List<int[]>();

			for (int i = 0; i < i_Board.BoardSize; i++)
			{
				for (int j = 0; j < i_Board.BoardSize; j++)
				{
					if (i_Board.Matrix[i, j] == i_Type || i_Board.Matrix[i, j] == i_KingType)
					{
						int[] indexes = new int[] { i, j };
						returnCells.Add(indexes);
					}
				}
			}

			return returnCells;
		}

		public static List<int[]> AvailableCellsICanMoveTo(Board i_Board, eCell i_Type, eCell i_KingType)
		{
			List<int[]> availablePawns = ReturnCells(i_Board, i_Type, i_KingType);
			List<int[]> availableCellsICanMoveTo = new List<int[]>();

			for (int k = 0; k < availablePawns.Count; k++)
			{
				Player tempPlayer = new Player();
				tempPlayer.MyType = i_Type;
				tempPlayer.MyKingType = i_KingType;
				tempPlayer.CurrentCellRow = availablePawns[k][0];
				tempPlayer.CurrentCellCol = availablePawns[k][1];

				for (int i = 0; i < i_Board.BoardSize; i++)
				{
					for (int j = 0; j < i_Board.BoardSize; j++)
					{
						tempPlayer.NextCellRow = i;
						tempPlayer.NextCellCol = j;
						if (GameLogicAndMoves.IsValidMove(i_Board, tempPlayer))
						{
							int[] indexes = new int[] { tempPlayer.CurrentCellRow, tempPlayer.CurrentCellCol, i, j };

							availableCellsICanMoveTo.Add(indexes);
						}
					}
				}
			}

			return availableCellsICanMoveTo;
		}
	}
}
