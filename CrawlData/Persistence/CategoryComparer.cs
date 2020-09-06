using System.Collections.Generic;

public class CategoryComparer : IEqualityComparer<Category>
{
    public bool Equals(Category x, Category y)
    {
        if(x == null && y == null)
        {
            return true;
        }

        return x.NumberOfPost == y.NumberOfPost;
    }

    public int GetHashCode(Category obj)
    {
        return obj.NumberOfPost.GetHashCode();
    }
}