using System;

namespace AlfredBr.ConsoleUtilities
{
    public class ConsoleMenu
    {
        public string[] Items { get; set; }
        public ConsoleMenu()
        {
            // intentionally left blank
        }
        public ConsoleMenu(string[] items, Action<int> callback)
        {
            Items = items;
            OnSelection = callback;
        }
        public int Show(string prompt = null)
        {
            //Console.WriteLine($"{Console.BufferWidth}, {Console.BufferHeight}");
            Console.WriteLine();
            if (prompt is not null)
            {
                Console.WriteLine(prompt);
            }
            else if (Prompt is not null)
            {
                Console.WriteLine(Prompt);
            }
            DisplayMenu();
            SetMenuIndicatorToPosition(0);
            return WaitForUserMenuSelection();
        }
        public int WaitForUserMenuSelection()
        {
            var p = 0;
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        p = Math.Min(++p, Items.Length - 1);
                        SetMenuIndicatorToPosition(p);
                        break;
                    case ConsoleKey.UpArrow:
                        p = Math.Max(0, --p);
                        SetMenuIndicatorToPosition(p);
                        break;
                    case ConsoleKey.Enter:
                        this.OnSelection?.Invoke(p);
                        return p;
                    default:
                        // do nothing on other keys (for now)
                        break;
                }
            } while (keyInfo.Key != ConsoleKey.Escape);
            return -1;
        }
        private void DisplayMenu()
        {
            Console.WriteLine();
            for (var i = 0; i < Items.Length; i++)
            {
                Console.WriteLine($"   {Items[i]}");
            }
        }
        private void SetMenuIndicatorToPosition(int p)
        {
            var cp = Console.GetCursorPosition();
            Console.SetCursorPosition(0, Math.Max(0, cp.Top - Items.Length));

            for (var i = 0; i < Items.Length; i++)
            {
                var indicator = i == p ? " > " : "   ";
                Console.ForegroundColor = i == p ? ConsoleColor.White : ConsoleColor.Gray;
                Console.WriteLine($"{indicator}{Items[i]}");
            }
        }
        public Action<int> OnSelection;
        public string Prompt { get; set; }
    }
}