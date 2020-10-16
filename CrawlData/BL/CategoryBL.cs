using AngleSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Configuration = AngleSharp.Configuration;

public class CategoryBL
{
    private CategoryDAL categoryDAL;

    public CategoryBL()
    {
        categoryDAL = new CategoryDAL();
    }

    public List<Category> GetAllCategories()
    {
        return categoryDAL.GetAllCategories();
    }

    public bool AddCategories(List<Category> categories)
    {
        return categoryDAL.AddCategories(categories);
    }

    public bool UpdateCategories(List<Category> categories)
    {
        return categoryDAL.UpdateCategory(categories);
    }

    private async Task<List<Category>> GetCategoriesFromWebAsync()
    {
        List<Category> listCategories = new List<Category>();

        string htmlData = null;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Configs.SetsUrl);
        request.UserAgent = Configs.UserAgent;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = null;

            if (String.IsNullOrWhiteSpace(response.CharacterSet))
            {
                readStream = new StreamReader(receiveStream);
            }
            else
            {
                readStream = new StreamReader(receiveStream, Encoding.UTF8);
            }

            htmlData = readStream.ReadToEnd();

        }

        var context = BrowsingContext.New(Configuration.Default);

        //Create a document from a virtual request / response pattern
        var document = await context.OpenAsync(req => req.Content(htmlData));

        var nodes = document.QuerySelectorAll("span.tag-counterz");
        int id = 0;
        foreach (var item in nodes)
        {
            id++;
            string title = item.QuerySelector("strong").TextContent.Trim();
            string link = item.QuerySelector("a").Attributes["href"].Value;
            var regex = @"(?<=\()(.*?)(?=\))";
            Match match = Regex.Match(item.TextContent, regex);

            int postNumber = Int32.Parse(match.ToString());

            Category category = new Category(id, title, link, postNumber);
            listCategories.Add(category);
        }

        return listCategories;
    }


    //public async Task<bool> UpdateCategoriesDataAsync()
    //{
    //    bool result = false;

    //    List<Category> categoriesFromWeb = await GetCategoriesFromWebAsync();
    //    List<Category> categoriesFromDatabase = categoryBL.GetAllCategories();

    //    bool hasNewData = false;
    //    for (int i = 0; i < categoriesFromDatabase.Count; i++)
    //    {
    //        if (categoriesFromDatabase[i].NumberOfPost != categoriesFromWeb[i].NumberOfPost)
    //        {
    //            hasNewData = true;
    //            categoriesFromDatabase[i].NumberOfPost = categoriesFromWeb[i].NumberOfPost;
    //        }
    //    }

    //    if (hasNewData == true)
    //    {
    //        result = categoryBL.UpdateCategories(categoriesFromDatabase);
    //    }
    //    else
    //    {
    //        result = false;
    //    }

    //    return result;
    //}
}