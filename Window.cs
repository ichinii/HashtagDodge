using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_game
{
    class Window
    {
        public Window(int sizeX, int sizeY)
        {
            m_Frontbuffer = new Framebuffer(sizeX, sizeY);
            m_Backbuffer = new Framebuffer(sizeX, sizeY);

            Console.SetWindowSize(sizeX, sizeY);
            Console.SetBufferSize(sizeX, sizeY);

            m_SizeX = sizeX;
            m_SizeY = sizeY;

            ClearConsole();
        }

        public int m_SizeX;
        public int m_SizeY;

        public Framebuffer m_Frontbuffer;
        public Framebuffer m_Backbuffer;

        public char this[int x, int y]
        {
            get
            {
                return m_Backbuffer[x, y];
            }
            set
            {
                if (x >= m_SizeX || y >= m_SizeY)
                    return;
                m_Backbuffer[x, y] = value;
            }
        }

        public void ClearConsole()
        {
            for (int y = 0; y < m_SizeY; y++)
            {
                SetCursor(0, y);
                for (int x = 0; x < m_SizeX; x++)
                    Console.Write(' ');
            }
                    
        }

        public void Clear()
        {
            m_Backbuffer.Clear();
        }

        public void SetCursor(int x, int y)
        {
            System.Console.SetCursorPosition(x, y);
        }

        public void Display()
        {
            for (int y = 0; y < m_SizeY; y++)
            {
                for (int x = 0; x < m_SizeX; x++)
                {
                    if (m_Frontbuffer[x, y] != m_Backbuffer[x, y])
                    {
                        SetCursor(x, y);
                        Console.Write(m_Backbuffer[x, y]);
                    }
                }
            }

            Framebuffer tempFramebuffer = m_Frontbuffer;
            m_Frontbuffer = m_Backbuffer;
            m_Backbuffer = tempFramebuffer;
        }
    }
}
