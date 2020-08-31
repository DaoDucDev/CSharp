using System.Collections.Generic;
using System.Net;

public class PostService
{
    public List<Post> GetPostsOfCategory(Category cate, int page)
    {
        List<Post> listPost = new List<Post>();

        string url = cate.Link + "/page/$page";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.UserAgent = Configs.UserAgent;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();



        return listPost;
    }
}