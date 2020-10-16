using System;
using ConsoleTables;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Menu
{
    private static CategoryBL categoryBL = new CategoryBL();
    private static List<Category> listCategories;
    // private static CategoryServices services = new CategoryServices();

    public static void MainMenu1()
    {
        
        listCategories = categoryBL.GetAllCategories();

        var table = new ConsoleTable("ID", "Title", "Link", "Number of Post");

        foreach (Category item in listCategories)
        {
            table.AddRow(item.Id, item.Title, item.Link, item.NumberOfPost);
        }
        table.Write();
    }

    public static async Task DisplayMainMenuAsync()
    {
        while(true)
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
                    await DisplaySubMenu1Async();
                    break;
                case 2:
                    //CategoryBL categoryBL = new CategoryBL();
                    
                    List<Category> categories = await CategoryServices.GetCategoriesFromWebAsync();
                    //categoryBL = new CategoryBL();
                    bool result = categoryBL.AddCategories(categories);
                    Console.WriteLine("ABC");
                    Console.WriteLine(result);
                    break;
                case 3:
                    bool updateResult = await CategoryServices.UpdateCategoriesDataAsync();
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
    }
    public static async Task DisplaySubMenu1Async() 
    {
        while(true)
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
                    if (category.NumberOfPost % 20 != 0)
                    {
                        numberOfPage = category.NumberOfPost / 20 + 1;
                    }
                    else
                    {
                        numberOfPage = category.NumberOfPost / 20;
                    }
                    Console.WriteLine("Your choice is {0} with {1} pages!", category.Title, numberOfPage);
                    List<Post> allPosts = new List<Post>();
                    PostService postService = new PostService();
                    Console.WriteLine("Getting post...");
                    for (int i = 1; i <= numberOfPage; i++)
                    {
                        string html = postService.GetHtmlData(category, i);
                        List<Post> postsOfPage = await postService.GetPostsOfCategoryAsync(html);

                        allPosts.AddRange(postsOfPage);

                        await Task.Delay(1000);
                    }
                    // Console.WriteLine(allPosts.Count);
                    PostBL postBL = new PostBL();
                    bool result = postBL.AddPostsIntoDatabase(allPosts, category.Id);
                    Console.WriteLine("Done!!!");
                    break;

                case 0:
                    await DisplayMainMenuAsync();
                    break;
                default:
                    break;
            }
        }
    }

    public static int InputChoice()
    {
        Console.Write("Enter your choice: ");
        int choice = Int32.Parse(Console.ReadLine());
        return choice;
    }
}