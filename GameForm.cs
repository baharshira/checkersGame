using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic;

namespace GameUI
{
    // $G$ SFN-012 (+4) Bonus: Rich ui.
	public partial class GameForm : Form
	{

        // $G$ SFN-006 (-4) This data member should have been readonly.
        private Player m_Player1 = new Player();
		private Player m_Player2 = new Player();
        // $G$ SFN-006 (-4) Size variable should be readonly as well.
		private int m_BoardSize;
        public Board LogicBoard { get; set; }
        public CellButton[,] UIBoard { get; set; }
		private Game m_Game;
		private CellButton m_ChosenPawn = null;
		public LoginForm LoginForm { get; set; }

		public GameForm(LoginForm i_LoginForm)
		{
			LoginForm = i_LoginForm;
			initGame();
			InitializeComponent();
			ShowDialog();
		}

		private void initGame()
        {
			m_Player1.Name = LoginForm.Player1Name;
			m_Player2.Name = LoginForm.Player2Name;
			m_BoardSize = LoginForm.BoardSize;
			m_Game = new Game (m_BoardSize, m_Player1, m_Player2);
			LogicBoard = m_Game.Board;
			UIBoard = new CellButton[m_BoardSize, m_BoardSize];
			initUIBoard();
            m_Game.ReloadBoard += UIBoard_ReloadBoard;
            m_Game.ReloadInvalidMoveMsg += UIBoard_ReloadInvalidMoveMsg;
			m_Game.ReloadEndOfGameMsg += Game_ReloadEndOfGameMsg;
		}

		public void Game_ReloadEndOfGameMsg(bool i_GamesEndsBecauseOfATie)
        {
			StringBuilder msgAtTheEndOfAGame = new StringBuilder();

			if (i_GamesEndsBecauseOfATie)
            {
				msgAtTheEndOfAGame.Append("Game ends with a tie!");
            }
			else
            {
				msgAtTheEndOfAGame.Append("Congratuilations to the winner!!!!!\nWinner is " + m_Game.Winner.Name);
				System.Media.SoundPlayer winVoiceMsg = new System.Media.SoundPlayer(Resource.WinnerVoiceMsg);
				winVoiceMsg.Play();
			}

			msgAtTheEndOfAGame.Append("\nDo you Want another game?");
			
			string msg = msgAtTheEndOfAGame.ToString();
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult msgBox = MessageBox.Show(msg, null, buttons);

			this.Close();
			if (msgBox == DialogResult.Yes)
			{
				
				GameForm newGame = new GameForm(LoginForm);
			}
            else
            {
				MessageBox.Show("Goodbye!");
            }
		}

        private void initUIBoard()
		{
			CellButton cellButton;

			for (int i = 0; i < m_BoardSize; i++)
			{
				for (int j = 0; j < m_BoardSize; j++)
				{
					cellButton = new CellButton(i, j);
					cellButton.CellType = LogicBoard.Matrix[i, j];
					this.UIBoard[i, j] = cellButton;
					editImage(i, j);
					this.Controls.Add(cellButton);
					cellButton.Click += currentCell_Click;
                    cellButton.MouseHover += CellButton_MouseHover;
					initDisebaledButtons(cellButton, i, j);
				}
			}

			this.CenterToScreen();
		}

        private void CellButton_MouseHover(object sender, EventArgs e)
        {
			CellButton cellButton = sender as CellButton;
			cellButton.BackColor = Color.MediumPurple;
		}

		private void currentCell_Click(object sender, EventArgs e)
        {
			CellButton cellButton = sender as CellButton;
			int countClicks = 0;

			if (m_Game.Turn.MyType == cellButton.CellType || m_Game.Turn.MyKingType == cellButton.CellType)
            {
				m_ChosenPawn = cellButton;
				m_ChosenPawn.Click += ChosenPawn_Click;
				m_Game.Turn.CurrentCellRow = cellButton.Row;
				m_Game.Turn.CurrentCellCol = cellButton.Col;
				cellButton.BackColor = Color.LightBlue;
				countClicks = 1;
			}

			if (cellButton.CellType != eCell.Empty && cellButton.CellType != m_Game.Turn.MyType && cellButton.CellType != m_Game.Turn.MyKingType)
			{
				MessageBox.Show("Not your turn!");
            }

			if (m_ChosenPawn != null && cellButton.CellType == eCell.Empty)
            {
				cellButton.BackColor = Color.LightGreen;
				m_Game.Turn.NextCellRow = cellButton.Row;
				m_Game.Turn.NextCellCol = cellButton.Col;
				m_ChosenPawn = null;
				countClicks = 2;
			}

			if (countClicks == 2)
            {
				m_Game.PlayGame(m_Game);
			}
		}


        private void ChosenPawn_Click(object sender, EventArgs e)
        {
            CellButton cellButton = sender as CellButton;

			cellButton.BackColor = Color.White;
        }

        private void initDisebaledButtons(CellButton i_CellButton, int i_Row, int i_Col)
        {
			if (i_Row % 2 == i_Col % 2)
			{
				i_CellButton.Enabled = false;
				i_CellButton.Text = string.Empty;
				i_CellButton.BackColor = Color.LightSteelBlue;
			}
		}

		private void editImage(int i, int j)
        {
			if (UIBoard[i, j].CellType != eCell.Empty)
			{
				addImage(UIBoard[i, j]);
			}
			else if (!(i % 2 == j % 2) && UIBoard[i, j].CellType == eCell.Empty)
			{
				Image white = Resource.white;
				Bitmap bitmap = new Bitmap(white, UIBoard[i, j].Width - 10, UIBoard[i, j].Height - 10);
				UIBoard[i, j].Image = bitmap;
			}
        }

		private void UIBoard_ReloadInvalidMoveMsg()
		{
			MessageBox.Show("Invalid Move! Please try again");
		}

		private void UIBoard_ReloadBoard()
		{
			for (int i = 0; i < m_BoardSize; i++)
			{
				for (int j = 0; j < m_BoardSize; j++)
				{
					UIBoard[i, j].CellType = LogicBoard.Matrix[i, j];
					editImage(i, j);
					UIBoard[i, j].BackColor = Color.White;
					initDisebaledButtons(UIBoard[i, j], i, j);
				}
			}
		}

		private void addImage(CellButton i_CellButton)
        {
			int width = i_CellButton.Width - 10;
			int height = i_CellButton.Height - 10;
			Image imgX = Resource.X;
			Bitmap bitmapX = new Bitmap(imgX, width, height);
			Image imgZ = Resource.XKING;
			Bitmap bitmapZ = new Bitmap(imgZ, width, height);
			Image imgO = Resource.O;
			Bitmap bitmapO = new Bitmap(imgO, width, height);
			Image imgQ = Resource.OKING;
			Bitmap bitmapQ = new Bitmap(imgQ, width, height);

			switch (i_CellButton.CellType)
			{
				case eCell.X:
					i_CellButton.Image = bitmapX;
					break;
				case eCell.Z:
					i_CellButton.Image = bitmapZ; 
					break;
				case eCell.O:
					i_CellButton.Image = bitmapO;
					break;
				case eCell.Q:
					i_CellButton.Image = bitmapQ;
					break;
            }
        }

		private void GameForm_Load(object sender, EventArgs e)
        {
		}
    }
}

