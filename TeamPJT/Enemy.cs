using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamPJT
{
    internal class Enemy
    {
        public int Level { get; }
        public string Name { get; }

        public int Hp { get; }
        public int Atk { get; }

        public bool IsAtk { get; private set; }

        public Enemy(int level, string name, int hp, int atk)
        {
            Level = level;
            Name = name;
            Hp = hp;
            Atk = atk;
        }

        public void PrintEnemyInfomation(bool withNumber = false, int idx = 0)
        {
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }

            if (Hp != 0) Console.WriteLine($"Lv. {Level} {Name} HP {Hp}");

            if (IsAtk)
            {
                
            }
        }
    }
}
