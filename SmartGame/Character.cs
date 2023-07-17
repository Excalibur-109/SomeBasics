using System;
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
        
        public void Shop(Player other)
        {
            string Buywhat;
            Buywhat = ReadLine();
            if (Buywhat == "A" && other.gold >= 30)
            {
                other.gold -= 30;
                other.attack += 10;
                Map.Instance.DelObject(this);
                other.SetHit(string.Format("当前角色属性HP：{0},Attack{1}", other.hp, other.attack));
            }
            else if (Buywhat == "B" && other.gold >= 20)
            {
                other.gold -= 20;
                other.hp += 10;
                Map.Instance.DelObject(this);
                other.SetHit(string.Format("当前角色属性HP：{0},Attack{1}", other.hp, other.attack));
            }
            else if (Buywhat == "A" && other.gold <= 30 || Buywhat == "B" && other.gold <= 20)
            {
                other.SetHit(string.Format("金币不够"));
            }
        }
    }

    public class Obstacle : MapObject
    {
        private int _goldCount;
        private int _num=1;

        public Obstacle(int goldCount)
        {
            _goldCount = goldCount;
        }

        public override void Interact(Player other)
        {
            _num--;
            other.SetHit("撞到了障碍物");
            Random ra = new Random();
            double num = ra.NextDouble();
            if (num >= 0.5 && _num >= 0)
            {
              other.gold += _goldCount;
              other.SetHit(string.Format("捡到一个宝箱,当前金币为：{0}",other.gold));
              Map.Instance.DelObject(this);
            }
        }

        public override char Symbol()
        {
            return 'O';
        }
    }

    public abstract class Enemy : MapObject
    {
    
        protected string _name;
        protected int _hp;
        protected int _attack;
        protected int _gold;

        public override char Symbol()
        {
            return 'E';
        }

        public override void Interact(Player other)
        {
            other.SetHit(string.Format("遭遇了{0},开始战斗", _name));
            other.hp -= _attack;
            _hp -= other.attack;
            _hp = Math.Max(_hp, 0);
            other.hp = Math.Max(other.hp, 0);
            other.SetHit(string.Format("{0}攻击了你，当前剩余HP{1},{2}当前HP为{3}", _name, other.hp, _name, _hp));
            if (_hp == 0)
            {
                other.SetHit(string.Format("{0}已经死亡", _name));
                //TODO死亡掉落金币
            }
        }


    }

    public class Boss : Enemy
    {
        public char symbolSelf;

        public Boss(string name, int hp, int attack, int gold)
        {
            this._name = name;
            this._hp = hp;
            this._attack = attack;
            this._gold = gold;
        }
        public override char Symbol()
        {
            return symbolSelf;
        }

        public override void Interact(Player other)
        {
           other.SetHit(string.Format("遭遇了{0},开始战斗", _name));
            other.hp -= _attack;
            _hp -= other.attack;
            _hp = Math.Max(_hp, 0);
            other.hp = Math.Max(other.hp, 0);
            other.SetHit(string.Format("{0}攻击了你，当前剩余HP{1},{2}当前HP为{3}", _name, other.hp, _name, _hp));
            if (_hp == 0)
            {
                other.gold += _gold;
                other.SetHit(string.Format("当前金币为{0}",other.gold));
                Map.Instance.DelObject(this);
            }
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
            Shop(other);
        }

        public override char Symbol()
        {
            return 'N';
        }

    }

    public class Player : MapObject
    {
        public bool Isdead = false;
        private string hitInfo = string.Empty;
        private int _hp;
        private int _attack;
        private int _gold;
        public Player(int hp, int attack,int gold)
        {
           _hp = hp;
           _attack = attack;
           _gold = gold;
        }
        public int hp { get { return _hp; } set { _hp = value; } }
        public int attack { get { return _attack; } set { _attack = value; } }
        public int gold { get { return _gold; } set {_gold = value; } }
        
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
            if (this.hp==0)
            {
                Isdead = true;
                this.SetHit(string.Format("你已经死亡"));
                Map.Instance.DelObject(this);
                
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
