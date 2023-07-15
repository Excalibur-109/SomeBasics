using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using static System.Console;

namespace SmartGame
{
    public class Position
    {
        public int x, y;
        public Position(int x, int y) { this.x = x; this.y = y; }

        public bool Equals(Position other)
        {
            return this.x == other.x && this.y == other.y;
        }
    }

    public class Map : Singleton<Map>
    {
        const char SYMBOL = '*';
        private Position _border;
        private readonly Position _offset = new Position(30, 30);

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
            while (p.x++ < _offset.y)
            {
                WriteLine();
            }

            p.x = -1;
            // 打印地图每一行
            while (p.x++ < _border.y - 1)
            {
                p.y = 0;
                // 列偏移
                while (p.y++ < _offset.x)
                {
                    _BuildRow(' ');
                }
                p.y = -1;
                // 打印地图
                while (p.y++ < _border.x - 1)
                {
                    if (p.x == 0 || p.x == _border.y - 1)
                    {
                        _BuildRow(SYMBOL);
                    }
                    else if (p.y == 0 || p.y == _border.x - 1)
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

            //int i, j;
            //for (i = 0; i < _border.y; ++i)
            //{
            //    if (i == 0 || i == _border.y - 1)
            //    {
            //        // 第一行 || 最后一行
            //        for (j = 0; j < _border.x; ++j)
            //        {
            //            for (int k = 0; k < _offset.x; k++)
            //            {
            //                _BuildRow(' ');
            //            }
            //            _BuildRow(SYMBOL);
            //        }
            //        WriteLine();
            //    }
            //    else
            //    {
            //        // 中间
            //        j = 0;
            //        while (j < _border.x)
            //        {
            //            for (int k = 0; k < _offset.x; k++)
            //            {
            //                _BuildRow(' ');
            //            }
            //            if (j == 0 || j == _border.x - 1)
            //            {
            //                _BuildRow(SYMBOL);
            //            }
            //            else
            //            {
            //                _BuildRow(' ');
            //            }
            //            ++j;
            //        }
            //        WriteLine();
            //    }
            //}
        }

        private void _BuildRow(char symbol)
        {
            Write(string.Format("{0} ", symbol));
        }
    }
}
