using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_game
{
    class Input
    {
        public Input()
        {
            m_Dir = 0;
        }

        public int m_Dir;
        public bool m_Start;

        public void Tick()
        {
            m_Dir = 0;
            m_Start = false;

            while(Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.A:
                        m_Dir = -1;
                        break;
                    case ConsoleKey.D:
                        m_Dir = 1;
                        break;
                    case ConsoleKey.Spacebar:
                        m_Start = true;
                        break;
                }
            }
        }
    }
}