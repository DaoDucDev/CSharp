using System;
using System.Collections.Generic;
using ConsoleTables;

namespace Presentation
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // CategoryServices services = new CategoryServices();

            // await services.GetCategoriesFromWebAsync();
            while (true)
            {
                Console.WriteLine("-----BEAUTIFUL GIRLS-----");
                Console.WriteLine("1. Show categories!");
                Console.WriteLine("2. Get all categories from web!");

                Console.WriteLine("-------------------------");
                Console.Write("Enter your choice: ");
                int choice = Int32.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CategoryBL categoryBL = new CategoryBL();
                        List<Category> listCategories = categoryBL.GetAllCategories();

                        var table = new ConsoleTable("ID", "Title", "Link", "Number of Post");

                        foreach (Category item in listCategories)
                        {       
                            table.AddRow(item.Id, item.Title, item.Link, item.NumberOfPost);
                        }
                        table.Write();
                        break;
                    case 2: 
                        CategoryServices services = new CategoryServices();
                        List<Category> categories = await services.GetCategoriesFromWebAsync();
                        categoryBL = new CategoryBL();
                        bool result = categoryBL.AddCategories(categories);
                        Console.WriteLine(result);
                    break;
                    default:
                        break;
                }
            }
        }
    }
}
