using MySqlConnector;
using Dapper;
namespace BlazorBlocket.Data;
public class AdminExistsDB 
{
        public int AdminLogInExists(Admin admin)
    {
        int id = 0;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string? query = "SELECT * FROM admins WHERE email = @email AND pass_word = @password; SELECT LAST_INSERT_ID() ";
            id = connection.ExecuteScalar<int>(query, param: admin);
            return id;
        }
    }

       public bool AdminEmailExists(string Email)
    {
        bool rows = true;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string? query = "SELECT * FROM admins WHERE email = @email";
            rows = connection.ExecuteScalar<bool>(query, new { @email = Email });
        }
        return rows;
    }

        // public bool AdminNameExists(string name)
    // {
    //     //EGEN DB KLASS
    //     bool rows = true;
    //     using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
    //     {
    //         string? query = "SELECT * FROM admins WHERE admin_name = @name";
    //         rows = connection.ExecuteScalar<bool>(query, new { @name = name });   // denna används inte
    //     }
    //     return rows;
    // }


}