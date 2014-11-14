using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_game
{
    class Map
    {
        public Map(int sizeX, int sizeY)
        {
            m_Buffer = new bool[sizeX, sizeY];

            m_SizeX = sizeX;
            m_SizeY = sizeY;
            m_OffsetY = 0;
        }

        public int m_SizeX;
        public int m_SizeY;
        public int m_OffsetY;

        public bool[,] m_Buffer;

        public bool this[int x, int y]
        {
            get
            {
                return m_Buffer[x, y];
            }
            set
            {
                m_Buffer[x, y] = value;
            }
        }

        public void Tick(int dif, float difficulty)
        {
            m_OffsetY += dif;
            if (m_OffsetY < ((1.0 / difficulty) * 1000))
                return;

            m_OffsetY = 0;

            for (int y = m_SizeY - 1; y > 0; y--)
            {
                for (int x = 0; x < m_SizeX; x++)
                {
                    m_Buffer[x, y] = m_Buffer[x, y - 1];
                }
            }

            Random rand = new Random();
            for (int x = 0; x < m_SizeX; x++)
                m_Buffer[x, 0] = rand.Next(0, (int)((1.0 / difficulty) * 100)) == 0;
        }
    }
}
