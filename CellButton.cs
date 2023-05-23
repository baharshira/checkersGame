using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic;

namespace GameUI
{
    public class CellButton : Button
    {
        public eCell CellType { get; set; }
        private readonly int r_Row;
        private readonly int r_Col;
        private const int k_Margin = 30;

        public int Row
        {
            get { return r_Row; }
        }

        public int Col
        {
            get { return r_Col; }
        }

        public CellButton(int i_Row, int i_Col)
            : base()
        {
            this.r_Row = i_Row;
            this.r_Col = i_Col;
            this.Width = 50;
            this.Height = 50;
            this.Top = k_Margin + r_Row * 50;
            this.Left = k_Margin + r_Col * 50;
            this.BackColor = Color.White;
        }
    }
}
