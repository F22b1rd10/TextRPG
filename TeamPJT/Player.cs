﻿using System;
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

        public Player(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }
}
