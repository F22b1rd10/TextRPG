using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamPJT
{
    internal class Player
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; }
        public int Hp { get; set; }
        public int Gold { get; set; }
        public bool IsDead { get; set; }

        public Player(string name, string job, int level, int atk, int def, int hp, int gold, bool isDead = false)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
            IsDead = isDead;
        }

        internal void PrintPlayerInfo()
        {
            CheckPlayerDead();
            if (IsDead)  // 사망 시 해당 몬스터 관련 텍스트 색상 변경
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(ConsoleUtility.PadRightForMixedText("Lv." + Level.ToString(), 5));
                Console.Write(ConsoleUtility.PadRightForMixedText(" " + Name.ToString(), 12));
                Console.WriteLine(ConsoleUtility.PadRightForMixedText("   Dead", 5));
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("[ 내 정보 ]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(ConsoleUtility.PadRightForMixedText("Lv." + Level.ToString(), 5));                
                Console.WriteLine(ConsoleUtility.PadRightForMixedText(" " + Name.ToString() + $"({Job})", 12));
                Console.ResetColor();
                Console.WriteLine(ConsoleUtility.PadRightForMixedText($"HP {Hp}/100", 5));                
            }
        }

        internal void CheckPlayerDead() // 사망 시 공격 불가 판정을 위한 메서드
        {
            if (Hp <= 0)
            {
                IsDead = true;
            }
        }
    }
}
