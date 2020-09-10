using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleTables;

namespace Presentation
{
    class Program
    {
        private static List<Category> listCategories;
        static async Task Main(string[] args)
        {
            while (true)
            {
                switch (Menu.MainMenu())
                {
                    case 1:
                        CategoryBL categoryBL = new CategoryBL();
                        listCategories = categoryBL.GetAllCategories();

                        var table = new ConsoleTable("ID", "Title", "Link", "Number of Post");

                        foreach (Category item in listCategories)
                        {
                            table.AddRow(item.Id, item.Title, item.Link, item.NumberOfPost);
                        }
                        table.Write();
                        switch (Menu.SubMenu1())
                        {
                            case 1:
                                Console.Clear();
                                table.Write();
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        CategoryServices services = new CategoryServices();
                        List<Category> categories = await services.GetCategoriesFromWebAsync();
                        categoryBL = new CategoryBL();
                        bool result = categoryBL.AddCategories(categories);
                        Console.WriteLine(result);
                        break;
                    case 3:
                        CategoryServices updateService = new CategoryServices();
                        bool updateResult = await updateService.UpdateCategoriesDataAsync();
                        if(updateResult == true)
                        {
                            Console.WriteLine("Update complete!!!");
                        }
                        break;
                    default:
                        break;
                }
            }

            
        }
    }
}
