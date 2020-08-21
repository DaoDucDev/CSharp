using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using Newtonsoft.Json;

public class Services
{
    public static async Task CreateJsonFileAsync()
    {
        List<Post> allPosts = new List<Post>();
        string jsonData = "";
        int i;
        if (File.Exists("post.json"))
        {
            
            using (StreamReader sr = new StreamReader("post.json"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    jsonData = line;
                    List<Post> oldPosts = JsonConvert.DeserializeObject<List<Post>>(jsonData);
                    allPosts.AddRange(oldPosts);
                }
            }

            for(i = 141; i <= 150; i++)
            {
                allPosts.AddRange(await GetPosts(i));
            }

            Console.WriteLine(allPosts.Count);
            jsonData = JsonConvert.SerializeObject(allPosts);
            using (StreamWriter sw = new StreamWriter("post.json"))
            {
                sw.WriteLine(jsonData);
            }
        }
        // else
        // {
            // for (i = 1; i <= 5; i++)
            // {
            //     allPosts.AddRange(await GetPosts(i));
            // }

            // jsonData = JsonConvert.SerializeObject(allPosts);
            // Console.WriteLine(allPosts.Count);

            // using (StreamWriter sw = new StreamWriter("post.json"))
            // {
            //     sw.WriteLine(jsonData);
            // }
        //}

    }

    public static async Task<List<Post>> GetAllPost()
    {
        List<Post> allPosts = new List<Post>();

        for (int i = 1; i <= 800; i++)
        {
            allPosts.AddRange(await GetPosts(i));
        }
        Console.WriteLine(allPosts.Count);
        return allPosts;
    }

    public static async Task<List<Post>> GetPosts(int page)
    {
        string url = "https://mrcong.com/category/nguoi-dep/nguoi-dep-trung-quoc/page/" + page;
        string htmlData = null;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36 Edg/84.0.522.61";

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
            response.Close();
            readStream.Close();
        }

        var context = BrowsingContext.New(Configuration.Default);

        //Create a document from a virtual request / response pattern
        var document = await context.OpenAsync(req => req.Content(htmlData));
        var nodes = document.QuerySelectorAll("article.item-list");

        List<Post> listPosts = new List<Post>();

        foreach (var item in nodes)
        {
            var img = item.QuerySelector("img").Attributes["src"].Value;
            var title = item.QuerySelector("h2.post-box-title").Children[0].TextContent;
            var view = item.QuerySelector("span.post-views").TextContent.Trim();
            var link = item
                .QuerySelector("div.post-thumbnail")
                .Children[0]
                .Attributes["href"]
                .Value;

            Post post = new Post("China", img, title, view, link);
            listPosts.Add(post);
        }

        return listPosts;

    }


}