public class Post
{
    public int Id{get;set;}
    public string Title{get;set;}
    public string Thumbnail{get;set;}
    public string Link{get;set;}
    public int ViewNumber{get;set;}

    public Post(){}
    public Post(int _id, string _title, string _thumbnail, string _link)
    {
        Id = _id;
        Title = _title;
        Thumbnail = _thumbnail;
        Link = _link;
    }

    public Post(string _title, string _thumbnail, string _link, int _viewNumber)
    {
        Title = _title;
        Thumbnail = _thumbnail;
        Link = _link;
        ViewNumber = _viewNumber;
    }
}