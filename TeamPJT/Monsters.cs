using System.Diagnostics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace TeamPJT
{
    internal class Monsters
    {
        public string Name { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; set; }
        public bool Isdead => Hp <= 0;

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
            if (Isdead)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"{idx}. {Name} 체력 : Dead");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"{idx}. {Name} 체력 : {Hp}");
            }
            
        }

        internal void TakeDamage(int damage)
        {
            //공격 및 회피 확률

            Random evasionrandom = new Random();
            int evasion = evasionrandom.Next(1, 101);
            Random criticalrandom = new Random();
            int critical = criticalrandom.Next(1, 101);

            // 치명타 계산 후 회피 계산
            // 치명타 확률은 if(critical < n) 에서 (n-1)*100% 의 확률임
            // 회피 확률은 두 개의 if(evasion > n) 에서 (n-1)*100% 의 확률임

            if (critical < 100)
            {
                if (evasion < 11)
                {
                    Console.WriteLine($"{Name}은 공격을 회피했습니다!");
                }
                else
                {
                    Hp -= damage * 2;
                    if (Isdead)
                    {
                        Console.WriteLine($"{Name}이(가) {damage * 2}의 치명타 데미지를 받았습니다.");
                        Console.WriteLine($"{Name}이(가) 죽었습니다.");
                    }
                    else
                    {
                        Console.WriteLine("치명적인 공격!!");
                        Console.WriteLine($"{Name}이(가) {damage * 2}의 치명타 데미지를 받았습니다. 남은 체력: {Hp}");
                    }
                }
            }
            else
            {
                if (evasion < 51)
                {
                    Console.WriteLine($"{Name}은 공격을 회피했습니다!");
                }
                else
                {
                    Hp -= damage;
                    if (Isdead)
                    {
                        Console.WriteLine($"{Name}이(가) {damage}의 데미지를 받았습니다.");
                        Console.WriteLine($"{Name}이(가) 죽었습니다.");
                    }
                    else Console.WriteLine($"{Name}이(가) {damage}의 데미지를 받았습니다. 남은 체력: {Hp}");
                }
            }

        }

        public Monsters BattleMonsters()
        {
            return new Monsters(Name, Level, Atk, Def, Hp);
        }


    }

}