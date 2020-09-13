using System;
using ConsoleTables;
using System.Collections.Generic;
using System.Linq;
public class Menu
{
    private static List<Category> listCategories;
    public static void MainMenu1()
    {
        CategoryBL categoryBL = new CategoryBL();
        listCategories = categoryBL.GetAllCategories();

        var table = new ConsoleTable("ID", "Title", "Link", "Number of Post");

        foreach (Category item in listCategories)
        {
            table.AddRow(item.Id, item.Title, item.Link, item.NumberOfPost);
        }
        table.Write();
    }

    public static async System.Threading.Tasks.Task DisplayMainMenuAsync()
    {
        Console.WriteLine("-----BEAUTIFUL GIRLS-----");
        Console.WriteLine("1. Show categories from database!");
        Console.WriteLine("2. Get all categories from web!");
        Console.WriteLine("3. Update category from web");
        Console.WriteLine("0. Exits");
        Console.WriteLine("-------------------------");
        int choice = InputChoice();
        switch (choice)
        {
            case 1:
                MainMenu1();
                DisplaySubMenu1Async();
                break;
            case 2:
                CategoryBL categoryBL = new CategoryBL();
                CategoryServices services = new CategoryServices();
                List<Category> categories = await services.GetCategoriesFromWebAsync();
                categoryBL = new CategoryBL();
                bool result = categoryBL.AddCategories(categories);
                Console.WriteLine(result);
                break;
            case 3:
                CategoryServices updateService = new CategoryServices();
                bool updateResult = await updateService.UpdateCategoriesDataAsync();
                if (updateResult == true)
                {
                    Console.WriteLine("Update complete!!!");
                }
                break;
            case 4:
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }
    public static void DisplaySubMenu1Async()
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine("1. Get posts of category.");
        Console.WriteLine("2. Update new post of category");
        Console.WriteLine("0. Back to main menu");
        Console.WriteLine("-------------------------");

        int subMenu1Choice = InputChoice();

        switch (subMenu1Choice)
        {
            case 1:
                Console.Write("Enter id of category: ");
                int id = Int32.Parse(Console.ReadLine());

                Category category = listCategories.Single(s => s.Id == id);
                
                int numberOfPage = 0;
                if(category.NumberOfPost % 10 != 0)
                {
                    numberOfPage = category.NumberOfPost / 10 + 1;
                }
                else
                {
                    numberOfPage = category.NumberOfPost / 10;
                }
                Console.WriteLine("Your choice is {0} with {1} pages!", category.Title, numberOfPage);
                //Console.WriteLine(category.Link);

                break;

            case 0:

                break;
            default:
                break;
        }
    }

    public static int InputChoice()
    {
        Console.Write("Enter your choice: ");
        int choice = Int32.Parse(Console.ReadLine());
        return choice;
    }
}