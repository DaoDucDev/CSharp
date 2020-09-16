using System.Collections.Generic;

public class PostBL
{
    private PostDAL postDAL;

    public PostBL()
    {
        postDAL = new PostDAL();
    }
    public bool AddPostsIntoDatabase(List<Post> posts, int categoryId)
    {
        return postDAL.AddPostsIntoDatabase(posts, categoryId);
    }
}