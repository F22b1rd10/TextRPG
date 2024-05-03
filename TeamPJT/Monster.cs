using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamPJT
{
    internal class Monster
    {
        public int Level { get; }
        public string Name { get; }
        public int Atk { get; }
        public int Hp { get; set; }        
        public bool IsDead { get; private set; }

        public Monster(int level, string name, int atk, int hp, bool isDead = false)
        {
            Level = level;
            Name = name;
            Atk = atk;
            Hp = hp;            
            IsDead = isDead;
        }

        internal void PrintMonstersInfo(bool withNumber = false, int idx = 0)
        {
            CheckMonsterDead();
            if (withNumber) // 공격 시 몬스터별 숫자 표기
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }
            if (IsDead)  // 사망 시 해당 몬스터 관련 텍스트 색상 변경
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(ConsoleUtility.PadRightForMixedText("Lv." + Level.ToString(), 5));
                Console.Write(ConsoleUtility.PadRightForMixedText(" " + Name.ToString(), 12));
                Console.Write(ConsoleUtility.PadRightForMixedText("   Died", 5));
                Console.ResetColor();                
            }
            else
            {
                Console.Write(ConsoleUtility.PadRightForMixedText("Lv." + Level.ToString(), 5));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(ConsoleUtility.PadRightForMixedText(" " + Name.ToString(), 12));
                Console.ResetColor();
                Console.Write(ConsoleUtility.PadRightForMixedText($"  HP {Hp}", 5));                
            }
        }


        //public void PrintStoreItemDescription(bool withNumber = false, int idx = 0)
        //{
        //    Console.Write("- ");
        //    // 장착관리 전용
        //    if (withNumber)
        //    {
        //        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        //        Console.Write("{0} ", idx);
        //        Console.ResetColor();
        //    }
        //    else Console.Write(ConsoleUtility.PadRightForMixedText(Name, 12));

        //    Console.Write(" | ");

        //    if (Atk != 0) Console.Write($"공격력 {(Atk >= 0 ? "+" : "")}{Atk} ");
        //    if (Def != 0) Console.Write($"방어력 {(Def >= 0 ? "+" : "")}{Def} ");
        //    if (Hp != 0) Console.Write($"체  력 {(Hp >= 0 ? "+" : "")}{Hp}");

        //    Console.Write(" | ");

        //    Console.Write(ConsoleUtility.PadRightForMixedText(Desc, 12));

        //    Console.Write(" | ");

        //    if (IsPurchased)
        //    {
        //        Console.WriteLine("구매완료");
        //    }
        //    else
        //    {
        //        ConsoleUtility.PrintTextHighlights("", Price.ToString(), " G");
        //    }
        //}
        internal void CheckMonsterDead() // 사망 시 공격 불가 판정을 위한 메서드
        {
            if (Hp <= 0)
            {
                IsDead = true;
            }
        }

    }
}
