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
}