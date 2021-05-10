using System;
using System.Linq;
using System.Threading;
using AlfredBr.ConsoleUtilities;

public class Program
{
    public static void Main()
    {
        var menuItems = new string[] { "Foo", "Bar", "Baz" };
        var consoleMenu = new ConsoleMenu();
        consoleMenu.Items = menuItems;
        consoleMenu.Prompt = "What say you?";
        consoleMenu.OnSelection = p => { Console.WriteLine($"#{p} was selected"); };
        var result = consoleMenu.Show();
        Console.WriteLine($"result={result}");

        var colors = new[] { "Red", "Green", "Blue", "Yellow" };
        new ConsoleMenu(colors, (p) => Console.WriteLine($"I like {colors[p]} too!")).Show("What is your favorite color?");

        var items = Enumerable.Range(65, 26).Select(t => $"{new string((char)t, 15)}").ToArray();
        var m = new ConsoleMenu(items, (p) => Console.WriteLine($"#{p} was selected")).Show();
        Console.WriteLine($"m={m}");
    }
}
