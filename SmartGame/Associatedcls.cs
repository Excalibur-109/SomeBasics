using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using static System.Console;

namespace SmartGame
{
    public struct Position
    {
        public int x, y;
        public Position(int x, int y) { this.x = x; this.y = y; }

        public bool Equals(Position other)
        {
            return this.x == other.x && this.y == other.y;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }
    }

    public class Map : Singleton<Map>
    {
        const char SYMBOL = '*';
        private Position _border;
        private readonly Position _offset = new Position(0, 0);

        List<MapObject> list = new List<MapObject>();

        public void AddObject(MapObject obj)
        {
            if (!list.Contains(obj))
            {
                list.Add(obj);
            }
        }

        public void DelObject(MapObject obj)
        {
            if (list.Contains(obj))
            {
                list.Remove(obj);
            }
        }

        public void SetBorder (Position border)
        {
            _border = border;
        }

        public void Build()
        {
            Position p = new Position(0, 0);
            // 行偏移
            while (p.y++ < _offset.y)
            {
                WriteLine();
            }

            p.y = -1;
            // 打印地图每一行
            while (p.y++ < _border.y - 1)
            {
                p.x = 0;
                // 列偏移
                while (p.x++ < _offset.x)
                {
                    _BuildRow(' ');
                }
                p.x = -1;
                // 打印地图
                while (p.x++ < _border.x - 1)
                {
                    if (p.y == 0 || p.y == _border.y - 1 || p.x == 0 || p.x == _border.x - 1)
                    {
                        _BuildRow(SYMBOL);
                    }
                    else
                    {
                        char symbol = ' ';
                        for (int i = 0; i < list.Count; ++i)
                        {
                            MapObject obj = list[i];
                            if (obj.position.Equals(p))
                            {
                                symbol = obj.Symbol();
                            }
                        }
                        _BuildRow(symbol);
                    }
                }
                // 换行
                WriteLine();
            }
        }

        public bool PositionValidate(Position pos)
        {
            return pos.x > 0 && pos.x < _border.x - 1 && pos.y > 0 && pos.y < _border.y - 1;
        }

        public MapObject Hit(Position p)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].position.Equals(p))
                {
                    return list[i];
                }
            }
            return null;
        }

        private void _BuildRow(char symbol)
        {
            Write(string.Format("{0} ", symbol));
        }
    }
}
