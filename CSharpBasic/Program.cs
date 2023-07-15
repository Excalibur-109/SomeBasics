using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Console;

namespace CSharpBasic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Role Jerry = new Magician("Jerry", 5);
            Jerry.SetData(5, 2, 1);

            Role tom = new Knight("Tom");
            tom.SetData(3, 2, 1);

            string winner = string.Empty;
            while (true)
            {
                string s = ReadLine();
                if (s == "A")
                {
                    tom.Attack(Jerry);
                    if (Jerry.isDead)
                    {
                        winner = tom.name;
                        break;
                    }
                }
                else if (s == "B")
                {
                    Jerry.Attack(tom);
                    if (tom.isDead)
                    {
                        winner = Jerry.name;
                        break;
                    }
                }
                else
                {
                    WriteLine("停止");
                    break;
                }
            }

            WriteLine("{0}赢了", winner);

            //ReadKey();
        }
    }

    public interface RoleBehaviour
    {
        void Attack (Role role);
        void Damage (Role role);
        void Walk ();
        void Dead ();
    }

    public abstract class Role : RoleBehaviour
    {
        protected string _name;
        private int
            _hp, _attack, _defence;

        private bool _isDead;

        public int hpp => _hp;
        public int attack => _attack;
        public string name => _name;
        public bool isDead => _isDead;

        public Role (string name)
        {
            _name = name;
        }

        public void SetData (int hp, int attack, int defence)
        {
            _hp = hp;
            _attack = attack;
            _defence = defence;
        }

        public virtual void Attack(Role victim)
        {
            WriteLine("{0}攻击了{1}", _name, victim.name);
            victim.Damage(this);
        }

        public void Damage(Role attacker)
        {
            int change = attacker.attack - _defence;
            if (change > 0)
            {
                _hp -= change;
                _hp = Math.Max(_hp, 0);
                WriteLine("{0}受到了{1}的攻击", _name, attacker.name);
                PrintData();
                if (_hp == 0)
                {
                    Dead();
                }
            }
        }

        public void Dead()
        {
            _isDead = true;
            WriteLine("{0}死掉了", _name);
        }

        public void Walk()
        {
            WriteLine("{0}在移动", _name);
        }
        
        public void PrintData ()
        {
            WriteLine("{0}：hp = {1}", _name, _hp);
        }
    }

    public class Magician : Role
    {
        private int _mp;

        public Magician(string name, int mp) : base(name)
        {
            _mp = mp;
        }

        public override void Attack(Role victim)
        {
            WriteLine("{0}用魔法攻击了{1}", name, victim.name);
            victim.Damage(this);
        }
    }

    public class Knight : Role
    {
        public Knight(string name) : base(name)
        {
        }

        public override void Attack(Role victim)
        {
            WriteLine("{0}用剑砍了{1}", name, victim.name);
            victim.Damage(this);
        }
    }
}
