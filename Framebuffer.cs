using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_game
{
    class Framebuffer
    {
        public Framebuffer(int sizeX, int sizeY)
        {
            m_Buffer = new char[sizeX, sizeY];
        }

        public char[,] m_Buffer;

        public char this[int x, int y]
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

        public void Clear()
        {
            int sizeX = m_Buffer.GetLength(0);
            int sizeY = m_Buffer.GetLength(1);
            m_Buffer = new char[sizeX, sizeY];
        }
    }
}