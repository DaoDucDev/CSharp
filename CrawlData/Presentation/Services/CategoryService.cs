using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;

public class CategoryServices
{
    public async Task<List<Category>> GetCategoriesFromWebAsync()
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

    public async Task UpdateCategoriesDataAsync()
    {
        CategoryServices services = new CategoryServices();
        List<Category> categoriesFromWeb = await services.GetCategoriesFromWebAsync();


        CategoryBL categoryBL = new CategoryBL();
        List<Category> categoriesFromDatabase = categoryBL.GetAllCategories();

    }
}