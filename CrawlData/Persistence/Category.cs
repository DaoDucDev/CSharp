public class Category
{
    public int Id{get;set;}
    public string Title{get;set;}
    public string Link{get;set;}
    public string Description{get;set;}
    public int NumberOfPost{get;set;}

    public Category(){}
    public Category(int _id, string _title, string _link, int _numberOfPost)
    {
        Id = _id;
        Title = _title;
        Link = _link;
        NumberOfPost = _numberOfPost;
    }

    public Category(string _title, string _link, int _numberOfPost)
    {
        Title = _title;
        Link = _link;
        NumberOfPost = _numberOfPost;
    }
}