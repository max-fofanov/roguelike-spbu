using System;
using System.IO;
using System.Text;

namespace roguelike_spbu {
    public enum GUIBorderTypes
    {
        None,
        Above,
        Below,
        OnRight,
        OnLeft
    }
    public enum GameState
    {
        None,
        Game,
        Inventory,
        InventoryDescription,
        Attack,
        AttackDescription,
        Chest,
        ChestInventory,
        ChestDescription,
        Menu,
        Controls,
        StatingScreen
    }
    public static class GUIElements
    {
        public static string cornerBorders = "╔╚╗╝╠╣╩╦╬";
        public static string lineBorders = "║═";
        public static string verticalBorder = lineBorders[0].ToString(); //"║"; //ud
        public static string horizontalBorder = lineBorders[1].ToString(); //"═"; //rl
        public static string downrightBorder = cornerBorders[0].ToString(); //"╔"; //dr
        public static string uprightBorder = cornerBorders[1].ToString(); //"╚"; //ur
        public static string downleftBorder = cornerBorders[2].ToString(); //"╗"; //dl
        public static string upleftBorder = cornerBorders[3].ToString(); //"╝"; //ur
        public static string verticalrightBorder = cornerBorders[4].ToString(); //"╠"; //udr
        public static string verticalleftBorder = cornerBorders[5].ToString(); //"╣"; //udl
        public static string horizontalupBorder = cornerBorders[6].ToString(); //"╩"; //url
        public static string horizontaldownBorder = cornerBorders[7].ToString(); //"╦"; //drl
        public static string crossBorder = cornerBorders[8].ToString(); //"╬"; //udrl

        public static string upBorders = verticalBorder + uprightBorder + upleftBorder + verticalrightBorder + verticalleftBorder + horizontalupBorder + crossBorder;
        public static string downBorders = verticalBorder + downrightBorder + downleftBorder + verticalrightBorder + verticalleftBorder + horizontaldownBorder + crossBorder;
        public static string rightBorders = horizontalBorder + downrightBorder + uprightBorder + verticalrightBorder + horizontalupBorder + horizontaldownBorder + crossBorder;
        public static string leftBorders = horizontalBorder + downleftBorder + upleftBorder + verticalleftBorder + horizontalupBorder + horizontaldownBorder + crossBorder;
        public static bool IsTypeOfBorder(string input, GUIBorderTypes expectedType)
        {
            if (input.Length != 1) return false;
            if (expectedType == GUIBorderTypes.Above) return upBorders.IndexOf(input) != -1;
            if (expectedType == GUIBorderTypes.Below) return downBorders.IndexOf(input) != -1;
            if (expectedType == GUIBorderTypes.OnRight) return rightBorders.IndexOf(input) != -1;
            if (expectedType == GUIBorderTypes.OnLeft) return leftBorders.IndexOf(input) != -1;
            return false;
        }

        public static string upArrow = "▲";
        public static string downArrow = "▼";
        public static string scrollPointer = "▓"; //"◄";
        public static string listPointer = "►";
    }
    public static class GameGUIWindows
    {
        public static List<Entity> EntitiesInRange
        {
            get { return SystemInfo.engine.GetEntitiesInRange(); }
        }
        public static Entity GetEntityInRange(int num)
        {
            if (num < 0 || num >= EntitiesInRange.Count())
                return new Entity();

            return EntitiesInRange[num];
        }
        public static List<Chest> ChestsInRange
        {
            get { return SystemInfo.engine.GetChestsInRange(); }
        }
        public static Chest GetChestInRange(int num)
        {
            if (num < 0 || num >= ChestsInRange.Count())
                return new Chest(0, 0);

            return ChestsInRange[num];
        }
        public static int SelectedChest = 0;
        public static List<Item> Inventory {
            get { return GameInfo.player.Inventory; }
        }
        public static Item GetItemInInventory(int num)
        {
            if (num < 0 || num >= Inventory.Count())
                return new Item();

            return Inventory[num];
        }
        public static Window MiniMap = new Window(0, 0, 17, 30);
        public static TextBox Statistics = new TextBox(16, 0, 36, 30, "Stats", "");
        public static Window GameBox = new GameBox(0, 29, 42, 152);
        public static ListBox UnderBar = new ListBox(41, 29, 11, 152, "", new List<string>());
        public static ListBox ListBox = new ListBox(0, 180, 26, 30, "", new List<string>());
        public static TextBox Description = new TextBox(25, 180, 25, 30, "Description", "");
        public static TextBox Mode = new TextBox(49, 180, 3, 30, "Mode", "");
        public static MenuBox MenuBox = new MenuBox(30, 30, "Menu", new List<string>() { "Resume", "Save Game", "Load Game", "Toggle music", "Controls", "Quit"});
        static string controlsText = "Escape - enter/exit menu\n\n" +
                                    "Up/down arrow (in windows) - navigate in window\n\n" +
                                    "Enter - choose selected item\n\n" +
                                    "A - open attack list\n\n" +
                                    "I - open inventory\n\n" +
                                    "D - go to description of item\n\n" +
                                    "Arrows (in game) - move player\n\n" +
                                    "Spacebar - pass turn\n\n" + 
                                    "Q - force quit";
        public static TextBox Control = new TextBox(0, 0, 40, 51, "Controls", controlsText, true, false);
        public static List<Window> GetWindows(){
            List<Window> windows = new List<Window>();
            windows.Add(MiniMap);
            windows.Add(Statistics);
            windows.Add(GameBox);
            windows.Add(UnderBar);
            windows.Add(ListBox);
            windows.Add(Description);
            windows.Add(Mode);
            windows.Add(MenuBox);
            windows.Add(Control);
            return windows;
        }
    }
    public class GUI
    {
        public GameState gameState = GameState.Game;
        public bool error = false;
        public string errorMessage = "";
        public string[,] layoutMatrix = new string[0, 0];
        public string[,] fullScreenMatrix = new string[0, 0];
        public int Height
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
        public List<Window> windows {
            get { return GameGUIWindows.GetWindows(); }
        }
        public GUI()
        {
            UpdateMode(GameState.Game);
            if (CheckIntersection(out (int, int) overlappingWindows))
            {
                error = true;
                errorMessage = $"There are overlapping windows: ({overlappingWindows.Item1}, {overlappingWindows.Item2})";
            }
            else
            {
                CalculateSize();

                layoutMatrix = new string[Height, Width];
                fullScreenMatrix = new string[Height, Width];

                EraseLayoutMatrix();
                EraseFullScreenMatrix();

                CreateLayout();
            }
        }
        public ConsoleKeyInfo GetKey()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            return keyInfo;
        }
        public void UpdateStats()
        {
            string description = "";
            description += String.Format("HP: {0}/{1}\n", GameInfo.player.HealthPoints, GameInfo.player.MaxHealthPoints);
            description += String.Format("Attack: {0}\n", GameInfo.player.GetTotalAttack());
            description += String.Format("Defence: {0}\n", GameInfo.player.GetTotalDefence());
            description += String.Format("ROA: {0}\n", GameInfo.player.RangeOfHit);
            description += "\n";
            description += String.Format("LVL: {0}\n", GameInfo.player.LVL);
            description += String.Format("XP: {0}/{1}\n", GameInfo.player.XP, GameInfo.player.XPToLevelUP);
            description += "\n";
            description += String.Format("Right hand: {0}\n", (GameInfo.player.RightHand ?? new Item()).Name);
            description += String.Format("Left hand: {0}\n", (GameInfo.player.LeftHand ?? new Item()).Name);
            description += String.Format("Body: {0}\n", (GameInfo.player.Body ?? new Item()).Name);

            GameGUIWindows.Statistics.UpdateText(description);
        }
        public void UpdateMode(GameState mode)
        {
            gameState = mode;

            string modeName = "";
            switch (mode)
            {
                case GameState.Game:
                    modeName = "Game";
                    break;
                case GameState.Inventory:
                    modeName = "Inventory";
                    break;
                case GameState.Attack:
                    modeName = "Attack";
                    break;
                case GameState.InventoryDescription:
                case GameState.AttackDescription:
                case GameState.ChestDescription:
                    modeName = "Description";
                    break;
                case GameState.Chest:
                    modeName = "Chest";
                    break;
                case GameState.ChestInventory:
                    modeName = "Chest Inventory";
                    break;
                case GameState.Menu:
                    modeName = "Menu";
                    break;
                case GameState.Controls:
                    modeName = "Controls";
                    break;
                default:
                    modeName = "None";
                    break;
            }

            GameGUIWindows.Mode.UpdateTitle(modeName);
        }
        public ActionInfo? DoAttackStuff(ConsoleKey? key = null)
        {
            UpdateMode(GameState.Attack);
        
            GameGUIWindows.ListBox.UpdateTitle("Attack");
            GameGUIWindows.ListBox.UpdateList((from entity in GameGUIWindows.EntitiesInRange select entity.Name ?? "NoName").ToList());

            Renderer.SelectedEntity = GameGUIWindows.GetEntityInRange(GameGUIWindows.ListBox.currentLine).ID;

            if (key != null)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        GameGUIWindows.ListBox.ScroolUp();
                        Renderer.SelectedEntity = GameGUIWindows.GetEntityInRange(GameGUIWindows.ListBox.currentLine).ID;
                        break;
                    case ConsoleKey.DownArrow:
                        GameGUIWindows.ListBox.ScroolDown();
                        Renderer.SelectedEntity = GameGUIWindows.GetEntityInRange(GameGUIWindows.ListBox.currentLine).ID;
                        break;
                    case ConsoleKey.Enter:
                        Renderer.SelectedEntity = Guid.Empty;
                        return new ActionInfo(Action.Attack, GameGUIWindows.GetEntityInRange(GameGUIWindows.ListBox.currentLine).ID);
                    default:
                        break;
                }
            }

            UpdateAttackDescription(GameGUIWindows.ListBox.currentLine);

            if (GameGUIWindows.EntitiesInRange.Count() == 0)
                ReturnToGame();

            return null;
        }
        public ActionInfo? DoInventoryStuff(ConsoleKey? key = null, bool justShow = false)
        {
            if (!justShow)
                UpdateMode(GameState.Inventory);

            GameGUIWindows.ListBox.UpdateTitle("Inventory");
            GameGUIWindows.ListBox.UpdateList((from item in GameGUIWindows.Inventory select ((item.Name ?? "NoName") + (GameInfo.player.IsItemAlreadyEquiped(item.ID) ? "*" : ""))).ToList());

            if (!justShow && key != null)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        GameGUIWindows.ListBox.ScroolUp();
                        break;
                    case ConsoleKey.DownArrow:
                        GameGUIWindows.ListBox.ScroolDown();
                        break;
                    case ConsoleKey.Enter:
                        return new ActionInfo(Action.UseItem, GameGUIWindows.GetItemInInventory(GameGUIWindows.ListBox.currentLine).ID, -2);
                    case ConsoleKey.D1:
                        return new ActionInfo(Action.UseItem, GameGUIWindows.GetItemInInventory(GameGUIWindows.ListBox.currentLine).ID, 0);
                    case ConsoleKey.D2:
                        return new ActionInfo(Action.UseItem, GameGUIWindows.GetItemInInventory(GameGUIWindows.ListBox.currentLine).ID, 1);
                    case ConsoleKey.D0:
                        return new ActionInfo(Action.UseItem, GameGUIWindows.GetItemInInventory(GameGUIWindows.ListBox.currentLine).ID);
                    case ConsoleKey.T:
                        GameInfo.player.RemoveFromInventory(GameGUIWindows.GetItemInInventory(GameGUIWindows.ListBox.currentLine).ID);
                        break;
                    default:
                        break;
                }
            }

            GameGUIWindows.ListBox.UpdateList((from item in GameGUIWindows.Inventory select ((item.Name ?? "NoName") + (GameInfo.player.IsItemAlreadyEquiped(item.ID) ? "*" : ""))).ToList());

            if (!justShow)
                UpdateInventoryDescription(GameGUIWindows.ListBox.currentLine);

            // if (GameGUIWindows.Inventory.Count() == 0)
                // ReturnToGame();

            return null;
        }
        public void UpdateAttackDescription(int num)
        {
            if (GameGUIWindows.EntitiesInRange.Count() == 0)
            {
                GameGUIWindows.Description.UpdateText("");
                return;
            }

            Entity enemy = GameGUIWindows.GetEntityInRange(num);

            string description = "";
            description += enemy.Name ?? "Noname";
            description += "\n\n";
            description += String.Format("HP: {0}/{1}\n", enemy.HealthPoints, enemy.MaxHealthPoints);
            description += String.Format("Attack: {0}\n", enemy.Damage);
            description += String.Format("ROW: {0}\n", enemy.RangeOfView);
            description += String.Format("ROA: {0}\n", enemy.RangeOfHit);
            description += String.Format("XP: {0}\n", enemy.XP);
            description += "\n";
            description += enemy.Description;

            GameGUIWindows.Description.UpdateText(description);

        }
        public void UpdateInventoryDescription(int num, bool isChest = false)
        {
            List<Item> itemList = new List<Item>();

            if (isChest)
                itemList = GameGUIWindows.GetChestInRange(GameGUIWindows.SelectedChest).Inventory;
            else
                itemList = GameGUIWindows.Inventory;


            if (num >= 0 && num < itemList.Count())
            {
                Item item = itemList[num];

                string description = "";
                description += item.Name ?? "Noname";
                description += "\n\n";
                //description += String.Format("HP: {0}\n", item.HealthPoints);
                description += String.Format("Attack: {0}\n", item.Damage);
                description += String.Format("Defence: {0}\n", item.Defence);
                description += String.Format("ROA: {0}\n", item.RangeOfHit);
                description += "\n";
                description += item.Description;

                GameGUIWindows.Description.UpdateText(description);
            }
            else
                GameGUIWindows.Description.UpdateText("");
        }
        public void DoDescriptionStuff(ConsoleKey? key = null)
        {
            if (gameState == GameState.Attack)
                UpdateMode(GameState.AttackDescription);
            if (gameState == GameState.Inventory)
                UpdateMode(GameState.InventoryDescription);
            if (gameState == GameState.ChestInventory)
                UpdateMode(GameState.ChestDescription);

            if (key != null)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        GameGUIWindows.Description.ScroolUp();
                        break;
                    case ConsoleKey.DownArrow:
                        GameGUIWindows.Description.ScroolDown();
                        break;
                    case ConsoleKey.Escape:
                        if (gameState == GameState.AttackDescription)
                            UpdateMode(GameState.Attack);
                        if (gameState == GameState.InventoryDescription)
                            UpdateMode(GameState.Inventory);
                        if (gameState == GameState.ChestDescription)
                            UpdateMode(GameState.ChestInventory);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (gameState == GameState.AttackDescription)
                    UpdateAttackDescription(GameGUIWindows.ListBox.currentLine);
                if (gameState == GameState.InventoryDescription)
                    UpdateInventoryDescription(GameGUIWindows.ListBox.currentLine);
                if (gameState == GameState.ChestDescription)
                    UpdateInventoryDescription(GameGUIWindows.UnderBar.currentLine, true);

            }
        }
        public void ResetUnderBar()
        {
            GameGUIWindows.UnderBar.UpdateTitle("");
            GameGUIWindows.UnderBar.UpdateList(new List<string>());
        }
        public void ReturnToGame()
        {
            Renderer.SelectedEntity = Guid.Empty;
            UpdateMode(GameState.Game);

            ResetUnderBar();

            GameGUIWindows.ListBox.UpdateTitle("");
            GameGUIWindows.ListBox.UpdateList(new List<string>());

            GameGUIWindows.Description.UpdateText("");
        }
        public bool ExecuteMenu(int num)
        {
            switch (num)
            {
                case 0:
                    GameGUIWindows.MenuBox.TurnOff();
                    ReturnToGame();
                    break;
                case 1:
                    GameGUIWindows.MenuBox.TurnOff();
                    ReturnToGame();
                    Saver.Save();
                    return true;
                case 2:
                    GameGUIWindows.MenuBox.TurnOff();
                    ReturnToGame();
                    Saver.Load();
                    return true;
                case 3:
                    if (Walkman.IsPlaying) Walkman.Stop();
                    else Walkman.Play();
                    break;
                case 4:
                    DoControlsMenuStuff();
                    break;
                case 5:
                    Program.NormilizeConsole();
                    break;
                default:
                    break;
            }

            return false;
        }
        public bool DoMenuStuff(ConsoleKey? key = null)
        {
            if (key == null)
            {
                if (!GameGUIWindows.MenuBox.Active)
                {
                    UpdateMode(GameState.Menu);
                    GameGUIWindows.MenuBox.TurnOn();
                } else
                {
                    GameGUIWindows.MenuBox.TurnOff();
                    ReturnToGame();
                }
            } else
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        GameGUIWindows.MenuBox.ScroolUp();
                        break;
                    case ConsoleKey.DownArrow:
                        GameGUIWindows.MenuBox.ScroolDown();
                        break;
                    case ConsoleKey.Enter:
                        return ExecuteMenu(GameGUIWindows.MenuBox.currentLine);
                    case ConsoleKey.Escape:
                        GameGUIWindows.MenuBox.TurnOff();
                        ReturnToGame();
                        break;
                    default:
                        break;
                }
            }

            return false;
        }
        public void DoControlsMenuStuff()
        {
            if (!GameGUIWindows.Control.Active)
            {
                UpdateMode(GameState.Controls);
                GameGUIWindows.MenuBox.TurnOff();
                GameGUIWindows.Control.TurnOn();
            }
            else
            {
                UpdateMode(GameState.Menu);
                GameGUIWindows.MenuBox.TurnOn();
                GameGUIWindows.Control.TurnOff();
            }
        }
        public void DoChestInventory(ConsoleKey? key = null)
        {
            UpdateMode(GameState.ChestInventory);

            GameGUIWindows.UnderBar.UpdateTitle("Chest");
            GameGUIWindows.UnderBar.UpdateList((from item in GameGUIWindows.GetChestInRange(GameGUIWindows.SelectedChest).Inventory select ((item.Name ?? "NoName") + (GameInfo.player.IsItemAlreadyEquiped(item.ID) ? "*" : ""))).ToList());

            if (key != null)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        GameGUIWindows.UnderBar.ScroolUp();
                        break;
                    case ConsoleKey.DownArrow:
                        GameGUIWindows.UnderBar.ScroolDown();
                        break;
                    case ConsoleKey.T:
                        //Console.Beep();
                        Item temp = GameGUIWindows.GetChestInRange(GameGUIWindows.SelectedChest).Inventory[GameGUIWindows.UnderBar.currentLine];
                        GameInfo.player.AddToInventory(temp);
                        GameGUIWindows.GetChestInRange(GameGUIWindows.SelectedChest).RemoveFromInventory(temp.ID);
                        break;
                    default:
                        break;
                }
            }

            GameGUIWindows.UnderBar.UpdateList((from item in GameGUIWindows.GetChestInRange(GameGUIWindows.SelectedChest).Inventory select ((item.Name ?? "NoName") + (GameInfo.player.IsItemAlreadyEquiped(item.ID) ? "*" : ""))).ToList());
            DoInventoryStuff(null, true);
            UpdateInventoryDescription(GameGUIWindows.UnderBar.currentLine, true);

            if (GameGUIWindows.GetChestInRange(GameGUIWindows.SelectedChest).Inventory.Count() == 0)
                ReturnToGame();
        }
        public void DoChestStuff(ConsoleKey? key = null)
        {
            UpdateMode(GameState.Chest);

            GameGUIWindows.UnderBar.UpdateTitle("Chest");
            GameGUIWindows.UnderBar.UpdateList((from chest in GameGUIWindows.ChestsInRange select ((chest.Name ?? "NoName") + "/" + chest.Inventory.Count())).ToList());

            if (key != null)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        GameGUIWindows.UnderBar.ScroolUp();
                        break;
                    case ConsoleKey.DownArrow:
                        GameGUIWindows.UnderBar.ScroolDown();
                        break;
                    case ConsoleKey.Enter:
                        GameGUIWindows.SelectedChest = GameGUIWindows.UnderBar.currentLine;
                        DoChestInventory();
                        break;
                    default:
                        break;
                }
            }

            if (GameGUIWindows.ChestsInRange.Count() == 0)
                ReturnToGame();

            //UpdateInventoryDescription(GameGUIWindows.UnderBar.currentLine, true);
            //GameInfo.entities.Add(new Chest(GameInfo.player.X, GameInfo.player.Y));
        }
        public ActionInfo GetAction()
        {
            if (gameState == GameState.Attack)
                DoAttackStuff();
            if (gameState == GameState.Inventory)
                DoInventoryStuff();
            if (gameState == GameState.AttackDescription)
                UpdateAttackDescription(GameGUIWindows.ListBox.currentLine);
            if (gameState == GameState.InventoryDescription)
                UpdateInventoryDescription(GameGUIWindows.ListBox.currentLine);
            if (gameState == GameState.ChestDescription)
                UpdateInventoryDescription(GameGUIWindows.UnderBar.currentLine, true);

            Print();

            while (true)
            {
                ConsoleKeyInfo key = GetKey();

                if (key.Key == ConsoleKey.Q)
                    return new ActionInfo(Action.Quit);
                else if (gameState == GameState.Controls)
                    DoControlsMenuStuff();
                // TODO if gamestate.startingscreen return something
                else {
                    if (gameState == GameState.Game)
                    {
                        switch (key.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                return new ActionInfo(Action.Left);
                            case ConsoleKey.RightArrow:
                                return new ActionInfo(Action.Right);
                            case ConsoleKey.UpArrow:
                                return new ActionInfo(Action.Up);
                            case ConsoleKey.DownArrow:
                                return new ActionInfo(Action.Down);
                            case ConsoleKey.Spacebar:
                                return new ActionInfo(Action.StayInPlace);
                            case ConsoleKey.C:
                                return new ActionInfo(Action.Cheat);
                            default:
                                break;
                        }
                    }
                    if (gameState == GameState.Attack)
                    {
                        ActionInfo? attackAction = DoAttackStuff(key.Key);
                        if (attackAction != null)
                            return attackAction;
                    }
                    if (gameState == GameState.Inventory)
                    {
                        ActionInfo? inventoryAction = DoInventoryStuff(key.Key);
                        if (inventoryAction != null)
                            return inventoryAction;
                    }
                    if (gameState == GameState.Chest)
                    {
                        DoChestStuff(key.Key);
                    }
                    if (gameState == GameState.ChestInventory)
                    {
                        DoChestInventory(key.Key);
                    }
                    if (gameState == GameState.AttackDescription || gameState == GameState.InventoryDescription || gameState == GameState.ChestDescription)
                        DoDescriptionStuff(key.Key);
                    if (gameState != GameState.Menu && gameState != GameState.Controls && gameState != GameState.StatingScreen)
                    {
                        if (key.Key == ConsoleKey.Escape && gameState != GameState.AttackDescription && gameState != GameState.InventoryDescription )
                        {
                            if (DoMenuStuff()) return new ActionInfo();
                        }
                        else
                            switch (key.Key)
                            {
                                case ConsoleKey.A:
                                    ResetUnderBar();
                                    DoAttackStuff();
                                    break;
                                case ConsoleKey.Escape:
                                    DoInventoryStuff();
                                    break;
                                case ConsoleKey.I:
                                    ResetUnderBar();
                                    DoInventoryStuff();
                                    break;
                                case ConsoleKey.D:
                                    DoDescriptionStuff();
                                    break;
                                case ConsoleKey.G:
                                    ReturnToGame();
                                    break;
                                case ConsoleKey.O:
                                    DoChestStuff();
                                    break;
                                case ConsoleKey.K:
                                    SystemInfo.engine.entities.Add(new Chest(GameInfo.player.X, GameInfo.player.Y));
                                    break;
                                default:
                                    break;
                            }
                    }
                    else if (gameState == GameState.Menu)
                        if (DoMenuStuff(key.Key)) return new ActionInfo();
                }
                
                Print();
            }
        }
        public void EraseLayoutMatrix()
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    layoutMatrix[i, j] = " ";
        }
        public void EraseFullScreenMatrix()
        {
            for (int i = 0; i < Height; i++) // fill screen buffer 
                for (int j = 0; j < Width; j++)
                    fullScreenMatrix[i, j] = " ";
        }
        public void Print()
        {
            Console.SetCursorPosition(0, 0);

            if (error)
            {
                Console.WriteLine(errorMessage);
                return;
            }

            if (Console.LargestWindowHeight <= Height || Console.LargestWindowWidth <= Width) {
                Console.WriteLine("Window is to small");
                return;
            }

            UpdateStats();

            bool isActiveFullscreen = false;
            foreach (Window window in windows)
            {
                if (!window.Active) continue;
                string[,] windowBuffer = window.GetInsides();
                if (window.FullSreen)
                {
                    isActiveFullscreen = true;
                    EraseFullScreenMatrix();

                    int startX = (Height - window.Height) / 2 - 1;
                    int startY = (Width - window.Width) / 2 - 1;

                    for (int i = startX + 1; i < startX + window.Height - 1; i++)
                    {
                        fullScreenMatrix[i, startY] = GUIElements.verticalBorder;
                        fullScreenMatrix[i, startY + window.Width - 1] = GUIElements.verticalBorder;
                    }
                    for (int i = startY + 1; i < startY + window.Width - 1; i++)
                    {
                        fullScreenMatrix[startX, i] = GUIElements.horizontalBorder;
                        fullScreenMatrix[startX + window.Height - 1, i] = GUIElements.horizontalBorder;
                    }

                    fullScreenMatrix[startX, startY] = GUIElements.downrightBorder;
                    fullScreenMatrix[startX + window.Height - 1, startY] = GUIElements.uprightBorder;
                    fullScreenMatrix[startX, startY + window.Width - 1] = GUIElements.downleftBorder;
                    fullScreenMatrix[startX + window.Height - 1, startY + window.Width - 1] = GUIElements.upleftBorder;

                    for (int i = 0; i < window.Height - 2; i++) // fill screen buffer 
                    {
                        for (int j = 0; j < window.Width - 2; j++)
                        {
                            fullScreenMatrix[i + startX + 1, j + startY + 1] = windowBuffer[i, j];
                        }
                    }
                }
                else
                    for (int i = 0; i < window.Height - 2; i++) // fill screen buffer 
                    {
                        for (int j = 0; j < window.Width - 2; j++)
                        {
                            layoutMatrix[i + window.X + 1, j + window.Y + 1] = windowBuffer[i, j];
                        }
                    }
            }

            StringBuilder screenBuffer = new StringBuilder();

            for (int i = 0; i < Height; i++) // fill screen buffer 
            {
                for (int j = 0; j < Width; j++)
                {
                    if (isActiveFullscreen)
                        screenBuffer.Append(fullScreenMatrix[i, j]);
                    else
                        screenBuffer.Append(layoutMatrix[i, j]);
                }
                screenBuffer.AppendLine();
            }

            Console.WriteLine(screenBuffer);
        }
        public bool IsTypeOfBorder((int, int) borderPoint, GUIBorderTypes expectedType)
        {
            int x = borderPoint.Item1;
            int y = borderPoint.Item2;
            if (x < 0 || y < 0 || x >= Height || y >= Width)
                return false;

            //if (layoutMatrix[x, y] == GUIElements.verticalBorder || layoutMatrix[x, y] == GUIElements.horizontalBorder)
            //Console.WriteLine("{0}:{1} {2}", x, y, GUIElements.IsTypeOfBorder(layoutMatrix[x, y], expectedType));
            return GUIElements.IsTypeOfBorder(layoutMatrix[x, y], expectedType);
        }
        public string GetCornerBorder((int, int) cornerPoint)
        {
            int x = cornerPoint.Item1;
            int y = cornerPoint.Item2;
            bool up = IsTypeOfBorder((x - 1, y), GUIBorderTypes.Below);
            bool down = IsTypeOfBorder((x + 1, y), GUIBorderTypes.Above);
            bool right = IsTypeOfBorder((x, y + 1), GUIBorderTypes.OnLeft);
            bool left = IsTypeOfBorder((x, y - 1), GUIBorderTypes.OnRight);

            if (up)
            {
                if (down)
                {
                    if (right && left)
                        return GUIElements.crossBorder;
                    if (right)
                        return GUIElements.verticalrightBorder;
                    if (left)
                        return GUIElements.verticalleftBorder;
                }
                else
                {
                    if (right && left)
                        return GUIElements.horizontalupBorder;
                    if (right)
                        return GUIElements.uprightBorder;
                    if (left)
                        return GUIElements.upleftBorder;
                }
            }
            else
            {
                if (right && left)
                    return GUIElements.horizontaldownBorder;
                if (right)
                    return GUIElements.downrightBorder;
                if (left)
                    return GUIElements.downleftBorder;
            }

            return "?";
        }
        public void CreateLayout()
        {
            List<(int, int)> cornerPoints = new List<(int, int)>();

            EraseFullScreenMatrix();

            foreach (Window window in windows)
            {
                if (window.FullSreen) continue;
                int x = window.X;
                int y = window.Y;
                int height = window.Height;
                int width = window.Width;
                cornerPoints.Add((x, y));
                cornerPoints.Add((x + height - 1, y));
                cornerPoints.Add((x, y + width - 1));
                cornerPoints.Add((x + height - 1, y + width - 1));
                for (int i = x + 1; i < x + height - 1; i++)
                {
                    layoutMatrix[i, y] = GUIElements.verticalBorder;
                    layoutMatrix[i, y + width - 1] = GUIElements.verticalBorder;
                }
                for (int i = y + 1; i < y + width - 1; i++)
                {
                    layoutMatrix[x, i] = GUIElements.horizontalBorder;
                    layoutMatrix[x + height - 1, i] = GUIElements.horizontalBorder;
                }
            }

            foreach ((int x, int y) in cornerPoints)
            {
                layoutMatrix[x, y] = GUIElements.crossBorder;
            }

            foreach ((int, int) cornerPoint in cornerPoints)
            {
                int x = cornerPoint.Item1;
                int y = cornerPoint.Item2;

                layoutMatrix[x, y] = GetCornerBorder(cornerPoint);
            }
        }
        public void CalculateSize()
        {
            Height = windows.Max(w => (w.X + w.Height));
            Width = windows.Max(w => (w.Y + w.Width));
        }
        public bool CheckIntersection(out (int, int) overlappingWindows)
        {
            overlappingWindows = (-1, -1);
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].FullSreen) continue;
                for (int j = i + 1; j < windows.Count; j++)
                {
                    if (windows[j].FullSreen) continue;
                    int minX;
                    int minHeight;
                    int maxX;

                    if (windows[i].X < windows[j].X)
                    {
                        minX = windows[i].X;
                        minHeight = windows[i].Height;
                        maxX = windows[j].X;
                    }
                    else
                    {
                        minX = windows[j].X;
                        minHeight = windows[j].Height;
                        maxX = windows[i].X;
                    }

                    bool Xintersection = (minX + minHeight - 1) > maxX;

                    int minY;
                    int minWidth;
                    int maxY;

                    if (windows[i].Y < windows[j].Y)
                    {
                        minY = windows[i].Y;
                        minWidth = windows[i].Width;
                        maxY = windows[j].Y;
                    }
                    else
                    {
                        minY = windows[j].Y;
                        minWidth = windows[j].Width;
                        maxY = windows[i].Y;
                    }

                    bool Yintersection = (minY + minWidth - 1) > maxY;
                    overlappingWindows = (i, j);
                    if (Xintersection && Yintersection) return true;
                }
            }

            return false;
        }
    }
    public class Window
    {
        public bool FullSreen = false;
        public bool Active = true;
        public int X;
        public int Y;
        public int Height;
        public int Width;

        public Window()
        {
            X = 0;
            Y = 0;
            Height = 0;
            Width = 0;
        }

        public Window(int x, int y, int h, int w, bool fullSreen = false, bool active = true)
        {
            X = x;
            Y = y;
            Height = h;
            Width = w;
            FullSreen = fullSreen;
            Active = active;
        }
        public virtual void TurnOn()
        {
            Active = true;
        }
        public virtual void TurnOff()
        {
            Active = false;
        }
        public virtual void GenerateInsides()
        {

        }
        public virtual string[,] GetInsides()
        {
            string[,] innerText = new string[Height - 2, Width - 2];

            for (int x = 0; x < Height - 2; x++)
            {
                for (int y = 0; y < Width - 2; y++)
                {
                    innerText[x, y] = " ";
                }
            }

            return innerText;
        }
    }
    public class GameBox : Window
    {
        public GameBox(int x, int y, int h, int w, bool active = true) : base(x, y, h, w, false, active)
        {
            SystemInfo.RenderHeight = h - 2;
            SystemInfo.RenderWidth = w - 2;
        }
        override public string[,] GetInsides()
        {
            return Renderer.Render(GameInfo.history[GameInfo.currentMap].map, GameInfo.history[GameInfo.currentMap].entities, GameInfo.player, GameInfo.allVisible);
        }
    }
    public class TextBox : Window
    {
        string title = "";
        public string Title
        {
            set
            {
                if (value.Length < (Width - 3))
                    title = value;
                else
                    title = value.Substring(0, Width - 3);
            }
            get { return title; }
        }
        public string Text = "";
        List<string> textLines = new List<string>();
        int currentLine = 0;
        int lastLine = 0;
        public TextBox(int x, int y, int h, int w, string title, string text, bool fullSreen = false, bool active = true) : base(x, y, h, w, fullSreen, active)
        {
            UpdateTitle(title);
            UpdateText(text);
        }
        public void UpdateTitle(string title)
        {
            Title = title;
        }
        public void UpdateText(string text)
        {
            Text = text;
            currentLine = 0;
            GenerateInsides();
            if ((Height - 2 - 2) > textLines.Count()) lastLine = 0;
            else lastLine = textLines.Count() - Height + 2 + 2; // two for borders, two for title
        }
        public void ScroolUp(bool scrollAll = false)
        {
            if (scrollAll) currentLine = 0;
            else if (currentLine > 0) currentLine--;
        }
        public void ScroolDown(bool scrollAll = false)
        {
            if (scrollAll) currentLine = lastLine;
            else if (currentLine < lastLine) currentLine++;
        }
        override public string[,] GetInsides()
        {
            string[,] innerText = new string[Height - 2, Width - 2];

            for (int x = 0; x < Height - 2; x++)
            {
                for (int y = 0; y < Width - 2; y++)
                {
                    innerText[x, y] = " ";
                }
            }

            if (Text.Length != 0)
            {
                if (currentLine > 0)
                    innerText[0, Width - 2 - 1] = GUIElements.upArrow;

                if (currentLine < lastLine)
                    innerText[Height - 2 - 1, Width - 2 - 1] = GUIElements.downArrow;

                if (lastLine != 0)
                {
                    int midPoint = (int)Math.Round(((double)(Height - 2 - 2 - 1) * currentLine) / lastLine, MidpointRounding.AwayFromZero);
                    innerText[1 + midPoint, Width - 2 - 1] = GUIElements.scrollPointer;
                }
            }

            int titleStaringPoint;
            if (Text.Length != 0)
                titleStaringPoint = (Width - 2 - title.Length - 1) / 2;
            else
                titleStaringPoint = (Width - 2 - title.Length) / 2;

            for (int y = 0; y < title.Length; y++)
            {
                innerText[0, y + titleStaringPoint] = title[y].ToString();
            }

            for (int x = 2; x < Height - 2; x++)
            {
                if ((currentLine + x - 2) >= textLines.Count()) break;
                for (int y = 0; y < textLines[currentLine + x - 2].Length; y++)
                {
                    innerText[x, y] = textLines[currentLine + x - 2][y].ToString();
                }
            }

            return innerText;
        }
        override public void GenerateInsides()
        {
            textLines = new List<string>();
            string lineBuffer = "";
            int currentLength = 0;

            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == '\n')
                {
                    textLines.Add(lineBuffer);
                    lineBuffer = "";
                    currentLength = 0;
                    continue;
                }
                if (currentLength < (Width - 4)) // from left border to gap between text and scrollbar
                {
                    lineBuffer += Text[i];
                    currentLength++;
                }
                else if (currentLength == (Width - 4))
                {
                    if (lineBuffer[currentLength - 1] != ' ' && i != (Text.Length - 2) && Text[i] != ' ')
                    {
                        lineBuffer += '-';
                    }

                    textLines.Add(lineBuffer);

                    lineBuffer = Text[i].ToString();
                    currentLength = 1;
                }
            }
            if (lineBuffer != "")
                textLines.Add(lineBuffer);

            // foreach (string line in textLines)
            //     Console.WriteLine("> {0}", line);
        }
    }
    public class ListBox : Window
    {
        string title = "";
        public string Title
        {
            set
            {
                if (value.Length < (Width - 3))
                    title = value;
                else
                    title = value.Substring(0, Width - 3);
            }
            get { return title; }
        }
        public string Text = "";
        List<string> textLines = new List<string>();
        public int currentLine = 0;
        int lastLine = 0;
        public ListBox() : this (0, 0, 0, 0, "", new List<string>())
        {

        }
        public ListBox(int x, int y, int h, int w, string title, List<string> textLines) : base(x, y, h, w)
        {
            UpdateTitle(title);
            UpdateList(textLines);
        }
        public void UpdateTitle(string title)
        {
            Title = title;
        }
        public void UpdateList(List<string> textLines)
        {
            if (this.textLines.Count() != textLines.Count())
                currentLine = 0;
            
            this.textLines = new List<string>();
            foreach (string line in textLines)
            {
                if (line.Length < (Width - 4))
                    this.textLines.Add(line);
                else
                    this.textLines.Add(line.Substring(0, Width - 4));
            }
            if (this.textLines.Count() != 0)
                lastLine = this.textLines.Count() - 1;
            else lastLine = 0;
        }
        public void ScroolUp(bool scrollAll = false)
        {
            if (scrollAll) currentLine = 0;
            else if (currentLine > 0) currentLine--;
        }
        public void ScroolDown(bool scrollAll = false)
        {
            if (scrollAll) currentLine = lastLine;
            else if (currentLine < lastLine) currentLine++;
        }
        override public string[,] GetInsides()
        {
            string[,] innerText = new string[Height - 2, Width - 2];

            for (int x = 0; x < Height - 2; x++)
            {
                for (int y = 0; y < Width - 2; y++)
                {
                    innerText[x, y] = " ";
                }
            }

            if (textLines.Count() > 0)
                innerText[2, 0] = GUIElements.listPointer;

            if (currentLine > 0)
                innerText[0, Width - 2 - 1] = GUIElements.upArrow;

            if (currentLine < lastLine)
                innerText[Height - 2 - 1, Width - 2 - 1] = GUIElements.downArrow;

            if (lastLine != 0)
            {
                int midPoint = (int)Math.Round(((double)(Height - 2 - 2 - 1) * currentLine) / lastLine, MidpointRounding.AwayFromZero);
                innerText[1 + midPoint, Width - 2 - 1] = GUIElements.scrollPointer;
            }

            int titleStaringPoint = (Width - 2 - title.Length - 1) / 2;

            for (int y = 0; y < title.Length; y++)
            {
                innerText[0, y + titleStaringPoint] = title[y].ToString();
            }

            for (int x = 2; x < Height - 2; x += 2)
            {
                if ((currentLine + x / 2 - 1) >= textLines.Count()) break;
                for (int y = 0; y < textLines[currentLine + x / 2 - 1].Length; y++)
                {
                    innerText[x, y + 1] = textLines[currentLine + x / 2 - 1][y].ToString();
                }
            }

            return innerText;
        }
    }
    public class MenuBox : Window
    {
        string title = "";
        public string Title
        {
            set
            {
                if (value.Length < (Width - 2))
                    title = value;
                else
                    title = value.Substring(0, Width - 2);
            }
            get { return title; }
        }
        public string Text = "";
        List<string> textLines = new List<string>();
        public int currentLine = 0;
        int lastLine = 0;
        public MenuBox(int h, int w, string title, List<string> textLines) : base(0, 0, h, w, true, false)
        {
            UpdateTitle(title);
            UpdateList(textLines);
        }
        public override void TurnOn()
        {
            currentLine = 0;
            Active = true;
        }
        public override void TurnOff()
        {
            Active = false;
        }
        public void UpdateTitle(string title)
        {
            Title = title;
        }
        public void UpdateList(List<string> textLines)
        {
            this.textLines = new List<string>();
            foreach (string line in textLines)
            {
                if (line.Length < (Width - 3))
                    this.textLines.Add(line);
                else
                    this.textLines.Add(line.Substring(0, Width - 3));
            }
            if (this.textLines.Count() != 0)
                lastLine = this.textLines.Count() - 1;
            else lastLine = 0;
        }
        public void ScroolUp(bool scrollAll = false)
        {
            if (scrollAll) currentLine = 0;
            else if (currentLine > 0) currentLine--;
        }
        public void ScroolDown(bool scrollAll = false)
        {
            if (scrollAll) currentLine = lastLine;
            else if (currentLine < lastLine) currentLine++;
        }
        override public string[,] GetInsides()
        {
            string[,] innerText = new string[Height - 2, Width - 2];

            for (int x = 0; x < Height - 2; x++)
            {
                for (int y = 0; y < Width - 2; y++)
                {
                    innerText[x, y] = " ";
                }
            }

            if (textLines.Count() > 0)
                innerText[2 + currentLine * 2, 0] = GUIElements.listPointer;

            int titleStaringPoint = (Width - 2 - title.Length) / 2;

            for (int y = 0; y < title.Length; y++)
            {
                innerText[0, y + titleStaringPoint] = title[y].ToString();
            }

            for (int x = 2; x < Height - 2; x += 2)
            {
                if ((x / 2 - 1) >= textLines.Count()) break;
                int lineStaringPoint = (Width - 2 - textLines[x / 2 - 1].Length) / 2;
                for (int y = 0; y < textLines[x / 2 - 1].Length; y++)
                {
                    innerText[x, y + lineStaringPoint] = textLines[x / 2 - 1][y].ToString();
                }
            }

            return innerText;
        }
    }
}