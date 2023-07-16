using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using static System.Console;

namespace SmartGame
{
    public class Game : Singleton<Game>
    {
        static ConsoleKey keyPressed = ConsoleKey.NoName;

        public void Start()
        {
            ConsoleKey key = ConsoleKey.Spacebar;

            while (key != ConsoleKey.Enter && key != ConsoleKey.Escape)
            {
                WriteLine("Press \"Enter\" to start Game(esc to exit):");
                key = ReadKey().Key;
                Clear();
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
            Map.Instance.SetBorder(new Position(30, 30));
            while (true)
            {
                keyPressed = ReadKey(true).Key;
                if (keyPressed == ConsoleKey.Escape)
                {
                    break;
                }
                else
                {
                    player.UpdateInput(keyPressed);
                }

                Clear();

                Map.Instance.Build();
                WriteLine(player.position.ToString());
            }
            WriteLine("Game Exit...");
        }
    }
}
