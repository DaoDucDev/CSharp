using MySql.Data.MySqlClient;

public class DbHelper
{
    private static MySqlConnection connection;

    public static MySqlConnection GetConnection()
    {
        if (connection == null)
        {
            connection = new MySqlConnection
            {
                ConnectionString = @"server=localhost;user id=root;
<<<<<<< HEAD
                    port=3306;password=123456;database=girls"
=======
                    port=3306;password=261992;database=girls"
>>>>>>> 4a6063374730cf456904dc82fb50d9f8833d1d03
            };
        }

        return connection;
    }

    public static MySqlConnection OpenConnection()
    {
        if (connection == null)
        {
            GetConnection();
        }
        connection.Open();
        return connection;
    }

    public static void CloseConnection()
    {
        if (connection != null)
        {
            connection.Close();
        }
    }

    public static MySqlDataReader ExecQuery(string query)
    {
        MySqlCommand command = new MySqlCommand(query, connection);
        return command.ExecuteReader();
    }

    public static int ExecNonQuery(string query)
    {
        MySqlCommand command = new MySqlCommand(query, connection);
        return command.ExecuteNonQuery();
    }
}