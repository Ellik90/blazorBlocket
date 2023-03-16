using MySqlConnector;
using Dapper;
namespace BlazorBlocket.Data;
public class UserExistsDB 
{
        public bool NicknameExists(string nickname)
    {
        bool rows = true;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string? query = "SELECT * FROM users WHERE nick_name = @name";
            rows = connection.ExecuteScalar<bool>(query, new { @name = nickname });
        }
        return rows;
    }

     public int UserLogInExists(User user)
    {
        int id = 0;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string? query = "SELECT * FROM users WHERE email = @email AND pass_word = @password; SELECT LAST_INSERT_ID() ";
            id = connection.ExecuteScalar<int>(query, param: user);
            return id;
        }
    }

      public bool UserEmailExists(string email)
    {
        bool rows = true;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string? query = "SELECT * FROM users WHERE email = @email ";
            rows = connection.ExecuteScalar<bool>(query, new { @email = email });
        }
        return rows;
    }
}