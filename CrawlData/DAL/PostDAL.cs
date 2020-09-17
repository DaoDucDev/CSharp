using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class PostDAL
{
    private string query;
    private MySqlDataReader reader;
    private MySqlConnection connection;

    public PostDAL()
    {
        connection = DbHelper.OpenConnection();
    }

    public bool AddPostsIntoDatabase(List<Post> posts, int categoryId)
    {
        bool result = false;

        foreach (var item in posts)
        {
            MySqlCommand command = new MySqlCommand("", connection);
            string text = @"insert into Post(title, thumbnail, link, view_number, category_id, time_create)
                    values(@title, @thumbnail, @link, @view_number, @category_id, @time_create)";

            command.CommandText = text;

            command.Parameters.AddWithValue("@title", item.Title);
            command.Parameters.AddWithValue("@thumbnail", item.Thumbnail);
            command.Parameters.AddWithValue("@link", item.Link);
            command.Parameters.AddWithValue("@view_number", item.ViewNumber);
            command.Parameters.AddWithValue("@category_id", categoryId);
            command.Parameters.AddWithValue("@time_create", item.TimeCreate);
            command.ExecuteNonQuery();
            result = true;
            

        }
        connection.Close();
        return result;
    }
}