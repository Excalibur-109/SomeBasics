using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SmartGame
{
    public abstract class MapObject
    {
        protected Position _position;

        public Position position => _position;

        public void SetPos(Position position)
        {
            _position = position;
        }

        public abstract char Symbol();
    }

    public class Obstacle : MapObject
    {
        public override char Symbol()
        {
            return 'O';
        }
    }

    public class Character : MapObject
    {
        public override char Symbol()
        {
            return 'N';
        }
    }

    public class Player : MapObject
    {
        public void UpdateInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    --position.x;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    --position.y;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    ++position.x;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    ++position.y;
                    break;
            }
        }

        public override char Symbol()
        {
            return 'P';
        }
    }
}
