using System;

public class Menu
{
    public static int MainMenu()
    {
        Console.WriteLine("-----BEAUTIFUL GIRLS-----");
        Console.WriteLine("1. Show categories!");
        Console.WriteLine("2. Get all categories from web!");
        Console.WriteLine("3. Update category from web");

        Console.WriteLine("-------------------------");
        Console.Write("Enter your choice: ");
        int choice = Int32.Parse(Console.ReadLine());
        return choice;
    }

    public static int SubMenu1()
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine("1. Detail category: ");
        Console.WriteLine("-------------------------");
        Console.Write("Enter your choice: ");
        int choice = Int32.Parse(Console.ReadLine());
        return choice;
    }
}