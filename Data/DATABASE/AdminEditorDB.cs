using MySqlConnector;
using Dapper;
namespace BlazorBlocket.Data;

public class AdminEditorDB 
{
          public int UpdateAdminEmail(Admin admin, string adminEmail)
    {
        int rows = 0;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string? query = "UPDATE admins SET email = @adminEmail WHERE id = @id";
            rows = connection.ExecuteScalar<int>(query, param: new { @adminEmail = adminEmail, @id = admin.Id });
        }
        return rows;
    }

        public int UpdateAdminName(Admin admin, string name)
    {
        int rows = 0;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string? query = "UPDATE admins SET admin_name = @name WHERE id = @id";
            rows = connection.ExecuteScalar<int>(query, param: new { @admin_name = name, @id = admin });
        }
        return rows;
    }
}