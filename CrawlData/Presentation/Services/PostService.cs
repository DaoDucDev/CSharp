using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;

public class PostService
{
    public async Task<List<Post>> GetPostsOfCategoryAsync(Category category, int pageNumber)
    {
        List<Post> listPost = new List<Post>();

        #region Get html
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

        #endregion

        #region Get data from html
        var context = BrowsingContext.New(Configuration.Default);

        //Create a document from a virtual request / response pattern
        var document = await context.OpenAsync(req => req.Content(htmlData));

        var nodes = document.QuerySelectorAll("article.item-list");

        foreach (var item in nodes)
        {
            
        }
        #endregion

        return listPost;
    }
}