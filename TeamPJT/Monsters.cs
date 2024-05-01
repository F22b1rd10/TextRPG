using System.Diagnostics;
using System.Reflection.Emit;

namespace TeamPJT
{
    internal class Monsters
    {
        public string Name { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; set; }
        public bool Isdead { get; }

        public Monsters(string name, int level, int atk, int def, int hp)
        {
            Name = name;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;

        }

        internal void PrintMonsterDescription(int idx = 0)
        {
            Console.WriteLine($"{idx}. {Name} 체력 : {Hp}");
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Isdead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else Console.WriteLine($"{Name}이(가) {damage}의 데미지를 받았습니다. 남은 체력: {Hp}");
        }
    }
}