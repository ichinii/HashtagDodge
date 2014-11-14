using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace cs_game
{
    class Game
    {
        public Game()
        {
            m_StartScreen = true;
            Reset();
        }

        private bool m_ShouldStopRunning;
        private float m_StartDifficulty;
        private float m_Difficulty;

        private bool m_GameOver;
        private bool m_StartScreen;

        private Stopwatch m_Stopwatch;
        private long m_CurTime;
        private long m_LastFPSTime;
        private int m_FPS;
        private int m_FPSTicks;

        public Window m_Window;
        public Map m_Map;
        public Player m_Player;
        public Input m_Input;

        public void Reset()
        {
            m_ShouldStopRunning = false;
            m_GameOver = false;

            m_FPS = 0;
            m_FPSTicks = 0;

            m_Map = new Map(25, 30);
            m_Window = new Window(m_Map.m_SizeX + 20, m_Map.m_SizeY);
            m_Player = new Player();
            m_Input = new Input();

            m_Player.m_PosX = m_Map.m_SizeX / 2;
            m_Player.m_PosY = m_Map.m_SizeY - 2;

            m_StartDifficulty = m_Difficulty = 7;

            m_Stopwatch = new Stopwatch();
            m_Stopwatch.Start();

            m_LastFPSTime = m_CurTime = m_Stopwatch.ElapsedMilliseconds;
        }

        public void Run()
        {
            while(!m_ShouldStopRunning)
            {
                int dif = (int)(m_Stopwatch.ElapsedMilliseconds - m_CurTime);
                m_CurTime = m_Stopwatch.ElapsedMilliseconds;

                if (m_CurTime - m_LastFPSTime > 1000)
                {
                    m_LastFPSTime = m_CurTime;
                    m_FPS = m_FPSTicks;
                    m_FPSTicks = 0;
                }

                m_FPSTicks++;

                Tick(dif);
                Draw();
            }
        }

        private void Tick(int dif)
        {
            m_Input.Tick();

            if (m_StartScreen)
            {
                if (m_Input.m_Start)
                    m_StartScreen = false;
                return;
            }

            if (m_GameOver)
            {
                if (m_Input.m_Start)
                    Reset();
                return;
            }
            else
            {

                m_Difficulty += (float)dif / 16000;
                m_Map.Tick(dif, m_Difficulty);
                m_Player.Move(m_Input.m_Dir, m_Map.m_SizeX - 1);

                if (m_Map[m_Player.m_PosX, m_Player.m_PosY] == true)
                    m_Player.m_Health = 0;

                if (WinConditions())
                {
                    m_GameOver = true;
                    return;
                }
            }
        }

        private void Draw()
        {
            if (m_StartScreen)
            {
                int centerX = m_Window.m_SizeX / 2;
                int centerY = m_Window.m_SizeY / 2;

                String[] str = {
                    "Welcome to HashtagDodge!",
                    "",
                    "This game is about dodging falling hashtags",
                    "The diffuclty increases by time",
                    "Have fun and good luck! :)",
                    "",
                    "Created by Ärix and Swag",
                    "",
                    "",
                    "Press SPACE to start"
                };

                WriteStr(str);
            }
            else if (m_GameOver)
            {
                String[] str = {
                    "Game over :(",
                    "Your Score: " + Score().ToString(),
                    "",
                    "Press SPACE to try again",
                };

                WriteStr(str);
            }
            else
            {
                DrawMap();
                DrawPlayer();
                WriteStr(m_Map.m_SizeX + 2, 3, "Score: " + Score());
            }

            WriteStr(m_Map.m_SizeX + 2, 1, "FPS: " + m_FPS);

            m_Window.Display();
            m_Window.Clear();
        }

        private int Score()
        {
            return (int)((m_Difficulty - m_StartDifficulty) * 50);
        }

        private bool WinConditions()
        {
            if (m_Player.m_Health <= 0)
                return true;
            return false;
        }

        private void DrawMap()
        {
            Random rand = new Random();
            for (int y = 0; y < m_Map.m_SizeY; y++)
                for (int x = 0; x < m_Map.m_SizeX; x++)
                    if (m_Map[x, y])
                        m_Window[x, y] = '#';

            for (int y = 0; y < m_Map.m_SizeY; y++)
            {
                if(y % 2 == 0)
                    m_Window[m_Map.m_SizeX, y] = '<';
                else
                    m_Window[m_Map.m_SizeX, y] = '>';
            }
        }

        private void DrawPlayer()
        {
            m_Window[m_Player.m_PosX, m_Player.m_PosY] = '^';
        }

        private void WriteStr(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
                WriteStr(m_Window.m_SizeX / 2 - str[i].Length / 2, m_Window.m_SizeY / 2 - str.Length / 2 + i, str[i]);
        }

        private void WriteStr(int x, int y, string str)
        {
            for (int nx = 0; nx < str.Length; nx++)
                m_Window[x + nx, y] = str[nx];
        }
    }
}