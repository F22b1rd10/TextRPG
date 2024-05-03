namespace TeamPJT
{
    internal partial class Program
    {
        public class GameManager
        {
            private Player player;
            private List<Monster> monsters;
            private List<Item> inventory;
            private List<Item> storeInventory;


            public GameManager()
            {
                InitializeGame();
            }

            private void InitializeGame()
            {
                player = new Player("Woohyeok", "Noob", 1, 10, 5, 100, 15000);
                monsters = new List<Monster>();
                monsters.Add(new Monster(2, "미니언", 5, 15));
                monsters.Add(new Monster(3, "공허충", 8, 10));
                monsters.Add(new Monster(5, "대포미니언", 15, 25));

                inventory = new List<Item>();

                storeInventory = new List<Item>();
                storeInventory.Add(new Item("무쇠갑옷", "튼튼한 갑옷", ItemType.ARMOR, 0, 5, 0, 500));
                storeInventory.Add(new Item("낡은 검", "낡은 검", ItemType.WEAPON, 2, 0, 0, 1000));
                storeInventory.Add(new Item("골든 헬름", "희귀한 투구", ItemType.ARMOR, 0, 9, 0, 2000));
            }

            public void StartGame()
            {
                Console.Clear();
                ConsoleUtility.PrintGameHeader();
                MainMenu();
            }

            private void MainMenu()
            {                              
                Console.Clear();
                                
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine("");

                Console.WriteLine("1. 상태보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 전투");
                Console.WriteLine("");

                int choice = ConsoleUtility.PromptMenuChoice(1, 4);
                                
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
                        BattleMain();
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
                    inventory[i].PrintItemStatDescription(true, i + 1);
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
                        if (storeInventory[keyInput - 1].IsPurchased)
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
            // TODO
            // 1) 개체 수 랜덤
            // 2) 표시 순서 랜덤
            // 3) 플레이어 정보 출력
            private void BattleMain()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("■ 전투 ■");
                Console.WriteLine("");
                Random spawnRandom = new Random();

                for (int i = 0; i < spawnRandom.Next(2, 5); i++)
                {
                    monsters[spawnRandom.Next(0,2)].PrintMonstersInfo(false);
                    Console.WriteLine("");                                
                }
                Console.WriteLine("");
                Console.WriteLine("");
                player.PrintPlayerInfo();
                Console.WriteLine("");
                Console.WriteLine("1. 공격");
                Console.WriteLine("");

                switch (ConsoleUtility.PromptMenuChoice(1, 1))
                {
                    case 1:
                        PlayerTurn();
                        break;
                }
            }

            private void PlayerTurn(string? prompt = null)
            {
                if (prompt != null)
                {                    
                    Console.Clear();
                    ConsoleUtility.ShowTitle(prompt);
                    Thread.Sleep(1000);
                }

                Console.Clear();

                ConsoleUtility.ShowTitle("■ 전투 ■");
                Console.WriteLine("");
                for (int i = 0; i < monsters.Count; i++)
                {
                    monsters[i].PrintMonstersInfo(true, i + 1);
                    Console.WriteLine("");
                    Console.WriteLine("");
                    player.PrintPlayerInfo();
                }
                Console.WriteLine("");
                Console.WriteLine("0. 취소");
                Console.WriteLine("");

                int keyInput = ConsoleUtility.PromptMenuChoice(0, monsters.Count);

                switch (keyInput)
                {
                    case 0:
                        BattleMain();
                        break;
                    default:
                        // 1 : 죽은 대상 선택
                        if (monsters[keyInput - 1].IsDead == true)
                        {
                            PlayerTurn("대상은 이미 죽었습니다.");
                        }
                        // 2 : 올바른 입력
                        else 
                        {                                                        
                            monsters[keyInput - 1].Hp -= PlayerAvgDmg();
                            PlayerTurnResult();
                        }
                        break;
                }
            }
            // 플레이어 공격 오차값 계산
            // TODO
            // 1) 소수점일 때 반올림 기능 
            private int PlayerAvgDmg()
            {
                int playerMinDmg = player.Atk - (int)(player.Atk * 0.1);
                int playerMaxDmg = player.Atk + (int)(player.Atk * 0.1);
                int playerAvgDmg;

                Random Atk = new Random();
                playerAvgDmg = Atk.Next(playerMinDmg, playerMaxDmg);

                return playerAvgDmg;
            }

            // TODO
            // 1) 위에서 공격한 몬스터 정보를 어떻게 가져오지? keyInput...
            // 2) 공격 후 체력 변화, 죽었다면 dead
            private void PlayerTurnResult()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("■ 전투 ■");
                Console.WriteLine("");
                for (int i = 0; i < monsters.Count; i++)
                {
                    monsters[i].PrintMonstersInfo(false);
                }
                Console.WriteLine("");
                Console.WriteLine("1. 턴 종료");
                Console.WriteLine("");

                switch (ConsoleUtility.PromptMenuChoice(1, 1))
                {
                    case 1:
                        MonsterTurn();
                        break;
                }
            }

            private void MonsterTurn()
            {
                Console.Clear();

                ConsoleUtility.ShowTitle("■ 전투 ■");
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
}
