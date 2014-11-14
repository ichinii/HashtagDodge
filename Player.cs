using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_game
{
    class Player
    {
        public Player()
        {
            m_Health = 1;
            m_PosX = 0;
            m_PosY = 0;
        }

        public int m_Health;
        public int m_PosX;
        public int m_PosY;

        public void Move(int dir, int maxX)
        {
            m_PosX = Math.Min(Math.Max(m_PosX + dir, 0), maxX);
        }
    }
}