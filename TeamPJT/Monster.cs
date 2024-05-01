﻿using System;
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
        public bool IsDied { get; private set; }

        public Monster(int level, string name, int atk, int hp, bool isDied = false)
        {
            Level = level;
            Name = name;
            Atk = atk;
            Hp = hp;            
            IsDied = isDied;
        }

        internal void PrintMonsters(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{idx} ");
                Console.ResetColor();
            }
            if (IsDied)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(ConsoleUtility.PadRightForMixedText("Lv." + Level.ToString(), 5));
                Console.Write(ConsoleUtility.PadRightForMixedText(" " + Name.ToString(), 12));
                Console.Write(ConsoleUtility.PadRightForMixedText("Died", 5));
                Console.ResetColor();
                Console.WriteLine("");
            }
            else
            {
                Console.Write(ConsoleUtility.PadRightForMixedText("Lv." + Level.ToString(), 5));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(ConsoleUtility.PadRightForMixedText(" " + Name.ToString(), 12));
                Console.ResetColor();
                Console.Write(ConsoleUtility.PadRightForMixedText($"  HP {Hp}", 5));
                Console.WriteLine("");
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

        internal void Died()
        {
            if (Hp <= 0)
            {
                IsDied = true;
            }            
        }
    }
}
