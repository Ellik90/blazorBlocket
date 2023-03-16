using MySqlConnector;
using Dapper;
namespace BlazorBlocket.Data;
public class UserEditorDB 
{
      public int UpdateEmail(User user, string userEmail)
    {
        int rows = 0;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string? query = "UPDATE users SET email = @userEmail WHERE id = @id";
            rows = connection.ExecuteScalar<int>(query, param: new { @userEmail = userEmail, @id = user.Id });
        }
        return rows;
    }

    public int UpdateNickName(User user, string nickName)
    {
        int rows = 0;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string? query = "UPDATE users SET nick_name = @nickname WHERE id = @id";
            rows = connection.ExecuteScalar<int>(query, param: new { @nickname = nickName, @id = user.Id });
        }
        return rows;
    }
    public int UpDateDescription(User user, string updateDescription)
    {
        int rows = 0;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string? query = "UPDATE users SET description = @description WHERE id = @id";
            rows = connection.ExecuteScalar<int>(query, param: new { @description = updateDescription, @id = user.Id });
        }
        return rows;
    }

    public int UpDatePassword(User user, string passWord)
    {
        int rows = 0;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string? query = "UPDATE users SET pass_word = @password WHERE id = @id";
            rows = connection.ExecuteScalar<int>(query, param: new { @password = passWord, @id = user.Id });
        }
        return rows;
    }
}