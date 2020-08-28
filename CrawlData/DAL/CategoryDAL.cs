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
        connection = DbHelper.OpenConnection();
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

        connection.Close();

        return listCategories;
    }

    private Category GetCategoryInfo(MySqlDataReader reader)
    {
        Category cate = new Category();
        cate.Id = reader.GetInt32("id");
        cate.Title = reader.GetString("title");
        cate.Link = reader.GetString("link");
        return cate;
    }
}