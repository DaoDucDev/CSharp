using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;

public class PostService
{
    public string GetHhtmlData(Category category, int pageNumber)
    {
        string htmlData = null;
        string url = category.Link + "/page/$pageNumber";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
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

        return htmlData;
    }

    #region Get data from html
    public async Task<List<Post>> GetPostsOfCategoryAsync(string htmlData)
    {
        List<Post> listPost = new List<Post>();

        var context = BrowsingContext.New(Configuration.Default);

        //Create a document from a virtual request / response pattern
        var document = await context.OpenAsync(req => req.Content(htmlData));

        var nodes = document.QuerySelectorAll("article.item-list");

        foreach (var item in nodes)
        {
            String title = item.QuerySelector("h2.post-box-title").Children[0].TextContent;
            var img = item.QuerySelector("img").Attributes["src"].Value;
            var link = item.QuerySelector("h2.post-box-title").Children[0].Attributes["href"].Value;
            var view = int.Parse(item.QuerySelector("span.post-views").TextContent.Trim());

            Post p = new Post(title, img, link, view);
            listPost.Add(p);
        }


        return listPost;
    }
    #endregion
}