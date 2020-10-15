using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class CategoryDAL
{
    private string query;
    private MySqlDataReader reader;
    private MySqlConnection connection;

    public CategoryDAL()
    {
        if(connection == null)
        {
            connection = DbHelper.OpenConnection();
        }
        
    }

    public List<Category> GetAllCategories()
    {
        if (connection.State == System.Data.ConnectionState.Closed)
        {
            connection.Open();
        }

        query = @"select * from categories;";
        MySqlCommand command = new MySqlCommand(query, connection);
        reader = command.ExecuteReader();

        List<Category> listCategories = new List<Category>();

        if (reader != null)
        {
            listCategories = GetListCategories(command);
        }

        reader.Close();
        DbHelper.CloseConnection();
        return listCategories;

    }

    private List<Category> GetListCategories(MySqlCommand command)
    {
        List<Category> listCategories = new List<Category>();

        while (reader.Read())
        {
            listCategories.Add(GetCategoryInfo(reader));
        }
        reader.Close();
        connection.Close();

        return listCategories;
    }

    private Category GetCategoryInfo(MySqlDataReader reader)
    {
        Category cate = new Category();
        cate.Id = reader.GetInt32("id");
        cate.Title = reader.GetString("title");
        cate.Link = reader.GetString("link");
        cate.NumberOfPost = reader.GetInt32("number_of_post");
        return cate;
    }

    public bool AddCategories(List<Category> categories)
    {
        bool result = false;
        foreach (var item in categories)
        {
            MySqlCommand command = new MySqlCommand("", connection);
            string text = @"insert into categories(title, link, number_of_post)
                    values(@title, @link, @number_of_post)";

            command.CommandText = text;

            command.Parameters.AddWithValue("@title", item.Title);
            command.Parameters.AddWithValue("@link", item.Link);
            command.Parameters.AddWithValue("@number_of_post", item.NumberOfPost);
            command.ExecuteNonQuery();
            result = true;
        }

        connection.Close();
        return result;
    }

    public bool UpdateCategory(List<Category> categories)
    {
        bool result = false;
        foreach (var item in categories)
        {
            MySqlCommand command = new MySqlCommand("", connection);
            string text = @"update ignore categories
                            SET
                                number_of_post = @number_of_post
                            WHERE
                                id = @Id";

            command.CommandText = text;
            command.Parameters.AddWithValue("@number_of_post", item.NumberOfPost);
            command.Parameters.AddWithValue("@Id", item.Id);
            command.ExecuteNonQuery();
            result = true;
        }

        connection.Close();
        return result;
    }
}