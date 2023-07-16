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
        public abstract void Interact(Player other);
    }

    public class Obstacle : MapObject
    {
        public override void Interact(Player other)
        {
            other.SetHit("撞到了障碍物");
        }

        public override char Symbol()
        {
            return 'O';
        }
    }

    public class Character : MapObject
    {
        private string _name = string.Empty;

        public Character (string name)
        {
            _name = name;
        }

        public override void Interact(Player other)
        {
            other.SetHit(string.Format("Player你好，我是{0}", _name));
        }

        public override char Symbol()
        {
            return 'N';
        }
    }

    public class Player : MapObject
    {
        private string hitInfo = string.Empty;

        public void UpdateInput(ConsoleKey key)
        {
            Position p = position;
            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    --p.y;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    --p.x;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    ++p.y;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    ++p.x;
                    break;
            }
            if (Map.Instance.PositionValidate(p))
            {
                hitInfo = string.Empty;
                MapObject obj = Map.Instance.Hit(p);
                if (obj != null)
                {
                    // 交互
                    obj.Interact(this);
                }
                else
                {
                    _position = p;
                }
            }
        }

        public override char Symbol()
        {
            return 'P';
        }

        public override void Interact(Player other)
        {
        }

        public void PrintHit()
        {
            WriteLine(hitInfo);
        }


        public void SetHit(string info)
        {
            hitInfo = info;
        }
    }
}
