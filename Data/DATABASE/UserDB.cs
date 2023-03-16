using MySqlConnector;
using Dapper;
namespace BlazorBlocket.Data;

public class UserDB 
{
    public List<User> GetUser()
    {
        List<User> users = new();

        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "SELECT id AS 'id', nick_name AS 'name', social_security_number AS 'socialsecuritynumber', description AS 'description', email AS 'email', pass_word AS 'password' FROM users;";
            users = connection.Query<User>(query).ToList();
            return users;
        }
    }
    public int GetUserIdFromAdvertise(int advertiseId)
    {
        // daniel ska ha getadvertise, sedan i service -> 
        //en metod som ger ut endast userns id på annonsen
        int id = 0;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "SELECT user_id FROM advertise WHERE id = @id";
            id = connection.ExecuteScalar<int>(query, new { @id = advertiseId });
            return id;
        }
    }

    public int CreateUser(User user)
    {
        int rows = 0;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "INSERT INTO users(nick_name,social_security_number,email,pass_word)VALUES(@name,@SocialSecurityNumber,@email,@passWord);";
            rows = connection.ExecuteScalar<int>(query, param: user);
        }
        return rows;
    }

    public int DeleteUser(User user)
    {
        int rows = 0;
        using (MySqlConnection connection = new MySqlConnection($"Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string? query = "DELETE FROM user_message WHERE from_user_id = @id OR to_user_id = @id; DELETE FROM users WHERE id = @id";
            rows = connection.ExecuteScalar<int>(query, new { @id = user.Id });//DET GÅR INTE ATT RADERA FÖR FOREIGN KEY, MESSAGE
        }
        return rows;
    }
}