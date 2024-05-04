namespace TeamPJT
{
    internal class Program
    {
        public class GameManager
        {
            private Player player;
            private List<Item> inventory;

            private List<Item> storeInventory;
            private List<Monsters> monster;

            public GameManager()
            {
                InitializeGame();
            }

            private void InitializeGame()
            {
                player = new Player("Jiwon", "Programmer", 1, 10, 5, 100, 50, 15000);

                inventory = new List<Item>();

                storeInventory = new List<Item>();
                storeInventory.Add(new Item("낡은 검", "낡은 검", ItemType.WEAPON, 2, 0, 0, 700));
                storeInventory.Add(new Item("철검", "철로 만든 검", ItemType.WEAPON, 10, 0, 0, 2000));
                storeInventory.Add(new Item("무쇠갑옷", "튼튼한 갑옷", ItemType.ARMOR, 0, 5, 0, 500));
                storeInventory.Add(new Item("골든 헬름", "희귀한 투구", ItemType.ARMOR, 0, 9, 0, 2000));


                monster = new List<Monsters>();
                monster.Add(new Monsters("고블린", 1, new Random().Next(4,6) , 2, 20));
                monster.Add(new Monsters("고블린 전사", 2, new Random().Next(8, 12), 4, 30));
                monster.Add(new Monsters("고블린 족장", 4, new Random().Next(15, 20), 8, 50));
                monster.Add(new Monsters("놀 전사", 2, new Random().Next(10, 15), 6, 40));
                monster.Add(new Monsters("놀 주술사", 4, new Random().Next(30, 40), 4, 30));
                monster.Add(new Monsters("놀 대장", 7, new Random().Next(20, 30), 10, 60));
                monster.Add(new Monsters("드래곤", 30, new Random().Next(80, 100), 40, 200));
            }

            public void StartGame()
            {
                Console.Clear();
                ConsoleUtility.PrintGameHeader();
                MainMenu();
            }

            public void MainMenu()
            {
                // 구성
                // 0. 화면 정리
                Console.Clear();
                player.Hp = 100;

                // 1. 선택 멘트를 줌
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
                Console.WriteLine("");

                Console.WriteLine("1. 상태보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전");
                Console.WriteLine("");

                // 2. 선택한 결과를 검증함
                int choice = ConsoleUtility.PromptMenuChoice(1, 4);

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
                        DungeonEntrance();
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
                ConsoleUtility.PrintTextHighlights("마 나 : ", (player.Mp).ToString(),"");

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
                        inventory[KeyInput - 1].ItemStatApply(player);
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


            private void DungeonEntrance()
            {
                Console.Clear();
                ConsoleUtility.ShowTitle("■ 던전 입구 ■");
                Console.WriteLine("던전을 탐색하고 전리품을 획득해 더욱 강해지세요!");
                Console.WriteLine("");
                Console.WriteLine("1. 던전 입장");
                Console.WriteLine("0. 나가기");

                switch (ConsoleUtility.PromptMenuChoice(0, 1))
                {
                    case 0:
                        MainMenu();
                        break;
                    case 1:
                        Stage();
                        break;
                }
                Console.ReadKey();
            }

            private void Stage()
            {

                Console.Clear();
                // 리스트몬스터의 1~3번 고블린을 랜덤으로 1~4마리 뽑기
                // 1스테이지에서 등장할 1~3번 고블린을 몬스터풀 리스트에 추가
                List<Monsters> stage1monspool = new List<Monsters>();
                stage1monspool.Add(monster[0].BattleMonsters());
                stage1monspool.Add(monster[1].BattleMonsters());
                stage1monspool.Add(monster[2].BattleMonsters());


                // 실제 등장할 몬스터 리스트
                List<Monsters> selectedmonster = new List<Monsters>();

                // 랜덤으로 1~4마리 선택
                Random random = new Random();
                int monstercount = random.Next(1, 5);
                
                // 몬스터 풀에서 랜덤하게 선택하여 실제 등장할 몬스터 리스트에 추가
                for (int i = 0; i < monstercount; i++)
                {
                    int monsteridx = random.Next(0, monstercount-1);
                    selectedmonster.Add(stage1monspool[monsteridx].BattleMonsters());
                }
                PlayerTurn();

                //현황판
                void StatusBoard()
                {
                    Console.WriteLine("몬스터 조우!!");
                    Console.WriteLine($"{player.Name}의 체력 : {player.Hp}");
                    Console.WriteLine($"{player.Name}의 마나 : {player.Mp}");
                    Console.WriteLine("");
                    Console.WriteLine("등장 몬스터 : ");
                    for (int i = 0; i < selectedmonster.Count; i++)
                    {
                        selectedmonster[i].PrintMonsterDescription(i + 1);
                    }
                    Console.WriteLine("");
                }

                

                void PlayerTurn()
                {
                   Console.Clear();
                    StatusBoard();

                    Console.WriteLine("-------------------");
                    Console.WriteLine("1. 공격 ");
                    Console.WriteLine("2. 스킬 ");
                    Console.WriteLine("0. 도망간다");
                    Console.WriteLine("-------------------");
                    Random Getawayrandom = new Random();
                    int GetawayPercent = Getawayrandom.Next(1,13-(2*monstercount));
                    

                    int keyInput = ConsoleUtility.PromptMenuChoice(0, 2);
                    switch (keyInput)
                    {
                        case 0:
                            // 도망은 등장한 몬스터 수에 따라 최대 50%에서 최소 20% 확률로 실패함
                            if (GetawayPercent < 3)
                            {
                                Console.WriteLine("도망에 실패했다!");
                                MonstersTurn();
                            }
                            else
                            {
                                Console.WriteLine("당신은 마을로 도망쳤다.");
                                Thread.Sleep(1000);
                                MainMenu();
                            }
                            break;

                        case 1:
                            Console.WriteLine("누구를 공격하시겠습니까?");
                            PlayerAttack();
                            break;

                        case 2:
                            PlayerSkillSelect();
                            break;
                    }
                }

                void PlayerAttack()
                {
                    Console.Clear();
                    StatusBoard();
                    int damage = (int)(player.Atk * (0.9 + new Random().NextDouble() * 0.2));
                    Console.WriteLine("-------------------");
                    Console.WriteLine("0. 돌아가기");
                    Console.WriteLine("-------------------");
                    Console.WriteLine();
                    int chosenmonster = ConsoleUtility.AttackedMonsterChoice(0, monstercount);
                    switch (chosenmonster)
                    {
                        case 0:
                            PlayerTurn();
                            break;


                        default:
                        if (selectedmonster[chosenmonster - 1].Isdead)
                        {
                            Console.WriteLine("이미 죽은 몬스터입니다.");
                            PlayerAttack();
                        }
                        else
                        {
                            Console.WriteLine("플레이어의 공격");
                            selectedmonster[chosenmonster - 1].TakeDamage(damage);
                            Thread.Sleep(1000);
                            CheckVictory();
                        }
                            break;
                     }
                }

                void PlayerSkillSelect()
                {
                    Console.Clear();
                    StatusBoard();
                    Console.WriteLine("-------------------");
                    Console.WriteLine("0. 돌아가기");
                    Console.WriteLine("");
                    Console.WriteLine("1. 몸통박치기 - Mp : 10");
                    Console.WriteLine("선택한 적에게 방어력만큼의 데메지를 입힙니다.");
                    Console.WriteLine("");
                    Console.WriteLine("2. 알파 스트라이크 - Mp : 15");
                    Console.WriteLine("선택한 적에게 공격력x3의 데미지를 입힙니다.");
                    Console.WriteLine("");
                    Console.WriteLine("3. 더블 스트라이크 - Mp : 20");
                    Console.WriteLine("랜덤한 2명의 적에게 공격력x2의 데미지를 입힙니다.");
                    Console.WriteLine("-------------------");

                    int skillselect = ConsoleUtility.SkillSelect(0,3);
                    if(skillselect == 0 )
                    {
                        PlayerTurn();
                    }
                    else if(skillselect == 1)
                    {
                        int chosenmonster = ConsoleUtility.AttackedMonsterChoice(0, monstercount);
                        switch (chosenmonster)
                        {
                            case 0:
                                PlayerTurn();
                                break;


                            default:
                                if (selectedmonster[chosenmonster - 1].Isdead)
                                {
                                    Console.WriteLine("이미 죽은 몬스터입니다.");
                                    Thread.Sleep(1000);
                                    PlayerTurn();
                                }
                                else
                                {
                                    if (player.Mp > 9)
                                    {
                                        player.Mp -= 10;
                                        Console.WriteLine("플레이어의 몸통박치기");
                                        selectedmonster[chosenmonster - 1].TakeDamage(player.Skill1);
                                        Thread.Sleep(1000);
                                        CheckVictory();
                                    }
                                    else
                                    {
                                        Console.WriteLine("마나가 부족합니다");
                                        Thread.Sleep(1000);
                                        PlayerTurn();
                                    }
                                }
                                break;
                        }
                    }
                    else if (skillselect == 2)
                    {
                        int chosenmonster = ConsoleUtility.AttackedMonsterChoice(0, monstercount);
                        switch (chosenmonster)
                        {
                            case 0:
                                PlayerTurn();
                                break;


                            default:
                                if (selectedmonster[chosenmonster - 1].Isdead)
                                {
                                    Console.WriteLine("이미 죽은 몬스터입니다.");
                                    Thread.Sleep(1000);
                                    PlayerTurn();
                                }
                                else
                                {
                                    if (player.Mp > 14)
                                    {
                                        player.Mp -= 15;
                                        Console.WriteLine("플레이어의 알파 스트라이크");
                                        selectedmonster[chosenmonster - 1].TakeDamage(player.Skill2);
                                        Thread.Sleep(1000);
                                        CheckVictory();
                                    }
                                    else
                                    {
                                        Console.WriteLine("마나가 부족합니다");
                                        Thread.Sleep(1000);
                                        PlayerTurn();
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        if(player.Mp>19)
                        {
                            // 살아있는 몬스터 수
                            int aliveMonsterCount = selectedmonster.Count(monster => !monster.Isdead);

                            if (aliveMonsterCount > 1)
                            {
                                Random random = new Random();
                                int doubleAttackTarget1 = random.Next(0, monstercount);

                                // 죽은 적은 타겟하지 않는 첫 번째 타겟 설정
                                while (selectedmonster[doubleAttackTarget1].Isdead)
                                {
                                    doubleAttackTarget1 = random.Next(0, monstercount);
                                }

                                // 두 번째 타겟 선택하는데 첫 타겟과 중복되지 않으면서 죽은적도 타겟하지 않게
                                int doubleAttackTarget2;
                                do
                                {
                                    doubleAttackTarget2 = random.Next(0, monstercount);
                                } while (doubleAttackTarget2 == doubleAttackTarget1 || selectedmonster[doubleAttackTarget2].Isdead);

                                player.Mp -= 20;
                                Console.WriteLine("플레이어의 더블 스트라이크");
                                selectedmonster[doubleAttackTarget1].TakeDamage(player.Skill3);
                                selectedmonster[doubleAttackTarget2].TakeDamage(player.Skill3);
                                Thread.Sleep(1000);
                                CheckVictory();
                            }
                            else
                            {
                                // 솔직히 남은적 두번치게 하고 싶었는데 못하겠음 봐주셈
                                Console.WriteLine("더블 스트라이크를 사용할 수 없습니다. 살아있는 몬스터가 한마리뿐입니다.");
                                Thread.Sleep(1000);
                                PlayerTurn();
                            }

                        }
                        else
                        {
                            Console.WriteLine("마나가 부족합니다");
                            Thread.Sleep(1000);
                            PlayerSkillSelect();
                        }
                    }

                    Thread.Sleep(1000);
                    CheckVictory();
                }

                void CheckVictory()
                {
                    //todo
                    // 게임오버는 잘됨

                    int aliveMonsterCount = 0;
                    foreach (var monster in selectedmonster)
                    {
                        if (monster.Isdead)
                        {
                            aliveMonsterCount++;
                        }
                    }

                    if (aliveMonsterCount == monstercount)
                    {
                        Console.WriteLine("승리했습니다!");
                        Console.WriteLine("마을로 돌아갑니다.");
                        if(player.Mp < 41)
                        {
                            player.Mp += 10;
                        }
                        
                        Thread.Sleep(1000);
                        MainMenu();
                    }
                    else
                    {
                        MonstersTurn();
                    }
                }

                void MonstersTurn()
                {
                    Console.WriteLine("몬스터의 턴");
                    Thread.Sleep(1000);
                    for(int i =0; i < monstercount; i++)
                    {
                        if (!selectedmonster[i].Isdead)
                        {
                            player.TakeDamage(selectedmonster[i].Atk);
                        }
                        
                    }
                    Thread.Sleep(3000);
                    PlayerTurn();
                    
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
