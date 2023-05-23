using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Player
    {
        public string Name { get; set; }
        private int m_Score = 0;
        public eCell MyType { get; set; }
        public eCell MyKingType { get; set; }
        public List<int[]> AvailablePawns { get; set; }
        public List<int[]> AvailableMoves { get; set; }
        public int CurrentCellRow { get; set; }
        public int CurrentCellCol { get; set; }
        public int NextCellRow { get; set; }
        public int NextCellCol { get; set; }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public static eCell[] InitEnemies(Player i_Player)
        {
            eCell[] enemies = new eCell[2];

            if (i_Player.MyType == eCell.X)
            {
                enemies[0] = eCell.O;
                enemies[1] = eCell.Q;
            }
            else if (i_Player.MyType == eCell.O)
            {
                enemies[0] = eCell.X;
                enemies[1] = eCell.Z;
            }

            return enemies;
        }
    }
}
