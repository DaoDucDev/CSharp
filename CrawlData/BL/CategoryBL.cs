using System.Collections.Generic;
public class CategoryBL
{
    private CategoryDAL categoryDAL;

    public CategoryBL()
    {
        categoryDAL = new CategoryDAL();
    }

    public List<Category> GetAllCategories()
    {
        return categoryDAL.GetAllCategories();
    }

    public bool AddCategories(List<Category> categories)
    {
        return categoryDAL.AddCategories(categories);
    }
}