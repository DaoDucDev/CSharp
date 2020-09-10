using System;

public class Menu
{
    public static int MainMenu()
    {
        Console.WriteLine("-----BEAUTIFUL GIRLS-----");
        Console.WriteLine("1. Show categories from database!");
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
        Console.WriteLine("1. Get posts of category.");
        Console.WriteLine("2. Update new post of category");
        Console.WriteLine("0. Back to main menu");
        Console.WriteLine("-------------------------");
        Console.Write("Enter your choice: ");
        int choice = Int32.Parse(Console.ReadLine());
        return choice;
    }
}