using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using static System.Console;

namespace SmartGame
{
    public class Game : Singleton<Game>
    {
        public void Start()
        {
            ConsoleKey key = ConsoleKey.Spacebar;

            while (key != ConsoleKey.Enter && key != ConsoleKey.Escape)
            {
                Clear();
                WriteLine("Press \"Enter\" to start Game(esc to exit):");
                key = PlayerInput();
            }

            switch (key)
            {
                case ConsoleKey.Enter:
                    GameLoop();
                    break;
                case ConsoleKey.Escape:
                    WriteLine("Game Exit...");
                    break;
            }
        }

        private void GameLoop()
        {
            Map.Instance.SetBorder(new Position(30, 30));
            //Obstacle o1 = new Obstacle();
            //o1.SetPos(new Position(4, 5));
            //Obstacle o2 = new Obstacle();
            //o2.SetPos(new Position(11, 22));
            //Obstacle o3 = new Obstacle();
            //o3.SetPos(new Position(25, 23));
            //Map.Instance.AddObject(o1);
            //Map.Instance.AddObject(o2);
            //Map.Instance.AddObject(o3);
            Player player = new Player();
            player.SetPos(new Position(15, 15));
            Map.Instance.AddObject(player);
            while (true)
            {
                Clear();
                player.UpdateInput(ReadKey().Key);
                Map.Instance.Build();

                if (Exit())
                {
                    WriteLine("Game Exit...");
                    break;
                }
            }
        }

        private ConsoleKey PlayerInput()
        {
            ConsoleKeyInfo info = ReadKey();
            return info.Key;
        }

        public bool Exit()
        {
            ConsoleKeyInfo info = ReadKey();
            return info.Key == ConsoleKey.Escape;
        }
    }
}
