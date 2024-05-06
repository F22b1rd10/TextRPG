using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHG
{
    //Complete 조건 추가

    internal class Quest
    {
        public string Que;
        public string Des;
        public string Goal;
        public string ItemReward;
        public int GoldReward;
        public int TargetCount;
        public bool IsAccept;
        public bool IsComlete;

        public Quest(string que, string des, string goal, string itemReward, int goldReward, int targetCount, bool isAccept = false, bool isComplete = false)
        { 
            Que = que;
            Des = des;
            Goal = goal;
            GoldReward = goldReward;
            ItemReward = itemReward;
            TargetCount = targetCount;
            IsAccept = isAccept;
            IsComlete = isComplete;
        }

        public void PrintQuestDescription(bool withNumber = false, int idx = 0)
        {
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }

            if (IsAccept)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("(진행중)");
                Console.ResetColor();
            }

            Console.WriteLine(Que);
        }

        internal void Accept()
        {
            IsAccept = true;
        }
        
        internal void Complete()
        {
            IsComlete = true;
        }
    }
}
