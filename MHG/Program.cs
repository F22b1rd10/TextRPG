using MHG;
using System;

namespace TeamPJT
{
    internal class Program
    {
        public class GameManager
        {
            private Player player;
            private List<Item> inventory;

            private List<Item> storeInventory;

            private List<Enemy> enemyInfo;

            private List<Quest> questList;

            public GameManager()
            {
                InitializeGame();
            }

            private void InitializeGame()
            {
                player = new Player("Jiwon", "Programmer", 1, 10, 5, 100, 15000);

                inventory = new List<Item>();

                storeInventory = new List<Item>();
                storeInventory.Add(new Item("무쇠갑옷", "튼튼한 갑옷", ItemType.ARMOR, 0, 5, 0, 500));
                storeInventory.Add(new Item("낡은 검", "낡은 검", ItemType.WEAPON, 2, 0, 0, 1000));
                storeInventory.Add(new Item("골든 헬름", "희귀한 투구", ItemType.ARMOR, 0, 9, 0, 2000));

                enemyInfo = new List<Enemy>();
                enemyInfo.Add(new Enemy(2, "미니언", 15, 5));
                enemyInfo.Add(new Enemy(3, "공허충", 10, 8));
                enemyInfo.Add(new Enemy(5, "대포미니언", 25, 8));

                questList = new List<Quest>();
                questList.Add(new Quest("마을을 위협하는 미니언 처치",
                    "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\r\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n모험가인 자네가 좀 처치해주게!",
                    "미니언 5마리 처치", "쓸만한 방패", 5));
                questList.Add(new Quest("장비를 장착해보자", "새로운 장비를 장착해보자!", 
                    "인벤토리에서 장비 장착", "아이템", 3));
            }

            public void StartGame()
            {
                Console.Clear();
                ConsoleUtility.PrintGameHeader();
                MainMenu();
            }

            private void MainMenu()
            {
                // 구성
                // 0. 화면 정리
                Console.Clear();

                // 1. 선택 멘트를 줌
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine("");

                Console.WriteLine("1. 상태보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 전투 시작");
                Console.WriteLine("5. 퀘스트");
                Console.WriteLine("");

                // 2. 선택한 결과를 검증함
                int choice = ConsoleUtility.PromptMenuChoice(1, 5);

                // 3. 선택한 결과에 따라 보내줌
                switch (choice)
                {
                    case 1:
                        StatusMenu();
                        break;
                    case 2:
                        InventoryMenu();
                        break;
                    case 3:
                        StoreMenu();
                        break;
                    case 4:
                        BattleMenu();
                        break;
                    case 5:
                        QuestMenu();
                        break;
                }
                MainMenu();
            }

            private void StatusMenu()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("■ 상태보기 ■");
                Console.WriteLine("캐릭터의 정보가 표기됩니다.");

                ConsoleUtility.PrintTextHighlights("Lv. ", player.Level.ToString("00"));
                Console.WriteLine("");
                Console.WriteLine($"{player.Name} ( {player.Job} )");

                // TODO : 능력치 강화분을 표현하도록 변경

                int bonusAtk = inventory.Select(item => item.IsEquipped ? item.Atk : 0).Sum();
                int bonusDef = inventory.Select(item => item.IsEquipped ? item.Def : 0).Sum();
                int bonusHp = inventory.Select(item => item.IsEquipped ? item.Hp : 0).Sum();

                ConsoleUtility.PrintTextHighlights("공격력 : ", (player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? $" (+{bonusAtk})" : "");
                ConsoleUtility.PrintTextHighlights("방어력 : ", (player.Def + bonusDef).ToString(), bonusDef > 0 ? $" (+{bonusDef})" : "");
                ConsoleUtility.PrintTextHighlights("체 력 : ", (player.Hp + bonusHp).ToString(), bonusHp > 0 ? $" (+{bonusHp})" : "");

                ConsoleUtility.PrintTextHighlights("Gold : ", player.Gold.ToString());
                Console.WriteLine("");

                Console.WriteLine("0. 뒤로가기");
                Console.WriteLine("");

                switch (ConsoleUtility.PromptMenuChoice(0, 0))
                {
                    case 0:
                        MainMenu();
                        break;
                }
            }

            private void InventoryMenu()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("■ 인벤토리 ■");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < inventory.Count; i++)
                {
                    inventory[i].PrintItemStatDescription();
                }

                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("1. 장착관리");
                Console.WriteLine("");

                switch (ConsoleUtility.PromptMenuChoice(0, 1))
                {
                    case 0:
                        MainMenu();
                        break;
                    case 1:
                        EquipMenu();
                        break;
                }
            }

            private void EquipMenu()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("■ 인벤토리 - 장착 관리 ■");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < inventory.Count; i++)
                {
                    inventory[i].PrintItemStatDescription(true, i + 1); // 나가기가 0번 고정, 나머지가 1번부터 배정
                }
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");

                int KeyInput = ConsoleUtility.PromptMenuChoice(0, inventory.Count);

                switch (KeyInput)
                {
                    case 0:
                        InventoryMenu();
                        break;
                    default:
                        inventory[KeyInput - 1].ToggleEquipStatus();
                        EquipMenu();
                        break;
                }
            }

            private void StoreMenu()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("■ 상점 ■");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine("");
                Console.WriteLine("[보유 골드]");
                ConsoleUtility.PrintTextHighlights("", player.Gold.ToString(), " G");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < storeInventory.Count; i++)
                {
                    storeInventory[i].PrintStoreItemDescription();
                }
                Console.WriteLine("");
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");
                switch (ConsoleUtility.PromptMenuChoice(0, 1))
                {
                    case 0:
                        MainMenu();
                        break;
                    case 1:
                        PurchaseMenu();
                        break;
                }
            }

            private void PurchaseMenu(string? prompt = null)
            {
                if (prompt != null)
                {
                    // 1초간 메시지를 띄운 다음에 다시 진행
                    Console.Clear();
                    ConsoleUtility.ShowTitle(prompt);
                    Thread.Sleep(1000);
                }

                Console.Clear();

                ConsoleUtility.ShowTitle("■ 상점 ■");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine("");
                Console.WriteLine("[보유 골드]");
                ConsoleUtility.PrintTextHighlights("", player.Gold.ToString(), " G");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < storeInventory.Count; i++)
                {
                    storeInventory[i].PrintStoreItemDescription(true, i + 1);
                }
                Console.WriteLine("");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");

                int keyInput = ConsoleUtility.PromptMenuChoice(0, storeInventory.Count);

                switch (keyInput)
                {
                    case 0:
                        StoreMenu();
                        break;
                    default:
                        // 1 : 이미 구매한 경우
                        if (storeInventory[keyInput - 1].IsPurchased) // index 맞추기
                        {
                            PurchaseMenu("이미 구매한 아이템입니다.");
                        }
                        // 2 : 돈이 충분해서 살 수 있는 경우
                        else if (player.Gold >= storeInventory[keyInput - 1].Price)
                        {
                            player.Gold -= storeInventory[keyInput - 1].Price;
                            storeInventory[keyInput - 1].Purchase();
                            inventory.Add(storeInventory[keyInput - 1]);
                            PurchaseMenu();
                        }
                        // 3 : 돈이 모자라는 경우
                        else
                        {
                            PurchaseMenu("Gold가 부족합니다.");
                        }
                        break;
                }
            }

            public void BattleMenu()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("Battle!!");
                Console.WriteLine("");

                //몬스터 정보 작성
                for (int i = 0; i < enemyInfo.Count; i++)
                {
                    enemyInfo[i].PrintEnemyInfomation();
                }

                Console.WriteLine("");
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} {player.Job}");
                Console.WriteLine($"HP {player.Hp}/{player.Hp}");

                Console.WriteLine("");
                Console.WriteLine("1. 공격");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("");

                switch (ConsoleUtility.PromptMenuChoice(0, 1))
                {
                    case 0:
                        MainMenu();
                        break;
                    case 1:
                        PlayerTurn();
                        break;
                }
            }

            private void PlayerTurn()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("Battle!!");
                Console.WriteLine("");

                for (int i = 0; i < enemyInfo.Count; i++)
                {
                    enemyInfo[i].PrintEnemyInfomation(true, i + 1);
                }
                Console.WriteLine("");
                Console.WriteLine("0. 취소");

                int keyInput = ConsoleUtility.PromptMenuChoice(0, enemyInfo.Count);

                switch (keyInput)
                {
                    case 0:
                        BattleMenu();
                        break;
                    default:
                        if (keyInput <= enemyInfo.Count)
                        {
                            Console.Clear();
                            ConsoleUtility.ShowTitle("Battle!!");
                            Console.WriteLine("");
                            AttackEnemy(enemyInfo[keyInput - 1]);
                        }
                        else
                        {
                            PlayerTurn();
                        }
                        break;
                }
            }

            private void AttackEnemy(Enemy enemy)
            {
                //Player의 공격력의 90% ~ 110% 사이의 랜덤한 값
                int damage = (int)(player.Atk * (0.9 + new Random().NextDouble() * 0.2));
                enemy.Hp -= damage;
                if (enemy.Hp <= 0)
                {
                    enemy.Hp = 0;
                    Console.WriteLine($"Lv.{enemy.Level} {enemy.Name}");
                    Console.WriteLine($"HP {enemy.Hp} -> Dead");
                }
                else
                {
                    Console.WriteLine($"{player.Name} 의 공격!");
                    Console.WriteLine($"Lv.{enemy.Level} {enemy.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
                }
                Console.WriteLine("");
                Console.WriteLine("0. 다음");

                switch (ConsoleUtility.PromptMenuChoice(0, 1))
                {
                    case 0:
                        if (enemy.IsDead)
                        {
                            Victory();  
                        }
                        else
                        {
                            EnemyTurn(enemy);
                        }
                        break;
                }
            }

            private void EnemyTurn(Enemy enemy)
            {
                Console.Clear();

                for(int i = 0; i < enemyInfo.Count; i++)
                {
                    ConsoleUtility.ShowTitle("Battle!!");
                    Console.WriteLine("");

                    if (!enemy.IsDead)
                    {
                        int damage = (int)(enemy.Atk * (0.9 + new Random().NextDouble() * 0.2));
                        player.Hp -= damage;

                        Console.WriteLine($"Lv. {enemy.Level} {enemy.Name} 의 공격!");
                        Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                        Console.WriteLine("");

                        Console.WriteLine($"Lv. {player.Level} {player.Name}");
                        Console.WriteLine($"HP {player.Hp} -> {player.Hp}");
                        Console.WriteLine("");

                        if(player.Hp <= 0)
                        {
                            player.Hp = 0;
                        }

                    }
                    Console.WriteLine("0. 다음");
                }

                switch (ConsoleUtility.PromptMenuChoice(0, 0))
                {
                    case 0:
                        if (player.Hp == 0)
                        {
                            Lose();
                        }
                        else
                        {
                            PlayerTurn();
                        }
                        break;
                }
            }


            private void Victory()
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("Battle!! - Result");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Victory");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine($"던전에서 몬스터 {enemyInfo.Count}마리를 잡았습니다.");
                Console.WriteLine("");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP 100 -> {player.Hp}");
                Console.WriteLine("");
                Console.WriteLine("0. 다음");

                switch (ConsoleUtility.PromptMenuChoice(0, 0))
                {
                    case 0:
                        MainMenu();
                        break;
                }
            }

            private void Lose()
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("Battle!! - Result");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("You Lose");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine("HP 100 -> 0");
                Console.WriteLine("");
                Console.WriteLine("0. 다음");

                switch (ConsoleUtility.PromptMenuChoice(0, 0))
                {
                    case 0:
                        MainMenu();
                        break;
                }
            }

            private void QuestMenu()
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("Quest!!");
                Console.WriteLine("");
                for (int i = 0; i < questList.Count; i++)
                {
                    bool isAccept = questList[i].IsAccept && !questList[i].IsComlete;
                    questList[i].PrintQuestDescription(true, i + 1);
                }
                Console.WriteLine("");
                Console.WriteLine("0. 돌아가기");
                Console.WriteLine("");
                
                int keyInput = ConsoleUtility.PromptMenuChoice(0, questList.Count);

                switch (keyInput)
                {
                    case 0:
                        MainMenu();
                        break;
                    default:
                        var selectedQuest = questList[keyInput - 1];
                        Console.Clear();
                        Console.WriteLine(selectedQuest.Que);
                        Console.WriteLine("");
                        Console.WriteLine(selectedQuest.Des);
                        Console.WriteLine("");
                        Console.WriteLine($"- {selectedQuest.Goal}");
                        Console.WriteLine("");
                        Console.WriteLine($"- 보상 - {selectedQuest.GoldReward} / {selectedQuest.ItemReward}");
                        Console.WriteLine("");
                        Console.WriteLine("1. 수락");
                        Console.WriteLine("2. 거절");
                        Console.WriteLine("");

                        switch (ConsoleUtility.PromptMenuChoice(1, 2))
                        {
                            case 1:
                                questList[keyInput - 1].Accept();
                                if (selectedQuest.IsComlete)
                                {
                                    Console.WriteLine("1. 보상받기");
                                    Console.WriteLine("2. 돌아가기");
                                    int rewardChoice = ConsoleUtility.PromptMenuChoice(1, 2);

                                    switch (rewardChoice)
                                    {
                                        case 1:
                                            //골드 지급
                                            player.Gold += selectedQuest.GoldReward;
                                            //아이템 지급
                                            //inventory.Add(selectedQuest.ItemReward);

                                            //퀘스트 삭제
                                            questList.RemoveAt(keyInput - 1);
                                            break;

                                        case 2:
                                            QuestMenu();
                                            break;
                                    }
                                }
                                break;

                            case 2:
                                QuestMenu();
                                break;
                        }
                        break;
                }
            }
        }

        public class Program1
        {
            public static void Main(string[] args)
            {
                GameManager gameManager = new GameManager();
                gameManager.StartGame();
            }
        }
    }
}