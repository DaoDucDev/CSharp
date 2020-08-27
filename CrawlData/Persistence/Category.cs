public class Category
{
    public int Id{get;set;}
    public string Title{get;set;}
    public string Link{get;set;}
    public string Description{get;set;}

    public Category(){}
    public Category(int _id, string _title, string _link)
    {
        Id = _id;
        Title = _title;
        Link = _link;
    }
}