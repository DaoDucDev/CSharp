
public class Post
{
    public string category{get;set;}
    public string imageThumbnail{get;set;}
    public string title{get;set;}
    public string numberOfViews{get;set;}
    public string link{get;set;}

    //public Post(){}
    public Post(string _category, string _image, string _title, string _views, string _link)
    {
        category = _category;
        imageThumbnail = _image;
        title = _title;
        numberOfViews = _views;
        link = _link;
    }
}