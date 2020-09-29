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
                    port=3306;password=DaoDuc@0979755154;database=girls"
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