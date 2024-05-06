using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamPJT
{
    //Complete 조건 추가

    internal class Quest
    {
        public string Que;
        public string Des;
        public string Goal;
        //public string Monster;
        public int GoldReward;
        public int TargetCount;
        public bool IsAccept;
        public bool IsComplete;
        public int Progress;

        public Quest(string que, string des, string goal,/* string monster*/ int goldReward, int targetCount, bool isAccept = false, bool isComplete = false)
        {
            Que = que;
            Des = des;
            Goal = goal;
            //Monster = monster;
            GoldReward = goldReward;
            TargetCount = targetCount;
            IsAccept = isAccept;
            IsComplete = isComplete;
            Progress = 0;
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
                Console.Write($"(진행중) - {Progress}/{TargetCount} ");
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
            IsComplete = true;
        }

        internal void UpdateProgress()
        {
            Progress += 1;

            if(Progress >= TargetCount)
            {
                //Complete();
                IsComplete = true;
            }
        }
    }
}
