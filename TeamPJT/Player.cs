using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TeamPJT.Program;

namespace TeamPJT
{
    internal class Player
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Gold { get; set; }
        public bool Isdead => Hp <= 0;

        public Player(string name, string job, int level, int atk, int def, int hp, int mp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Mp = mp;
            Gold = gold;
            
        }

        internal void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Isdead)
            {
                GameManager gamemanager = new GameManager();
                Console.WriteLine($"{Name}이(가) 죽었습니다.");
                Console.WriteLine("패배하였습니다");
                Console.Write("마을로 돌아갑니다..");
                Thread.Sleep(3000);
                gamemanager.MainMenu();
            }
            else Console.WriteLine($"{Name}이(가) {damage}의 데미지를 받았습니다. 남은 체력: {Hp}");
        }

    }
}
