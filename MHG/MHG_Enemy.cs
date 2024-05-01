using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamPJT
{
    internal class MHG_Enemy
    {
        public int Level { get; }
        public string Name { get; }

        public int Hp { get; set; }
        public int Atk { get; }

        public bool IsAtk { get; private set; }
        public bool IsDead { get; private set; }

        public MHG_Enemy(int level, string name, int hp, int atk, bool isAtk = false, bool isDead = false)
        {
            Level = level;
            Name = name;
            Hp = hp;
            Atk = atk;
            IsAtk = isAtk;
            IsDead = isDead;
        }

        public void PrintEnemyInfomation(bool withNumber = false, int idx = 0)
        {
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }

            Console.WriteLine($"Lv. {Level} {Name} HP {Hp}");

            if(Hp <= 0)
            {
                IsDead = true;
                //죽으면 공격 X
            }
        }
    }
}