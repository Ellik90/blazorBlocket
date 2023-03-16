using MySqlConnector;
using Dapper;
namespace BlazorBlocket.Data;

public class AddvertiseDb 
{
    //Klass för att hålla funktioner för annonserna
    List<Advertise> ads = new(); //Ska hålla annonserna
    public int CreateAd(Advertise advertise)
    {
        List<Advertise> CreateAd = new();

        int id = 0;

        using (MySqlConnection con = new MySqlConnection("Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "INSERT INTO advertise(rubric,description,price,municipality,county,postal_number,user_id)VALUES(@rubric,@description,@price,@county,@municipality,@postalNumber, @userid); SELECT LAST_INSERT_ID();";

            id = con.ExecuteScalar<int>(query, param: advertise);
        }
        if (id >= 1)
        {
            Console.WriteLine("Registrerad.");
        }
        else
        {
            Console.WriteLine("Något gick fel.");
        }
        return id;

    }

    public void RemoveAd(int id)
    {
        int rowsEffected = 0;

        using (MySqlConnection con = new MySqlConnection("Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "DELETE FROM advertise WHERE id = @id;";

            rowsEffected = con.ExecuteScalar<int>(query, new { @id = id });
        }
        if (rowsEffected >= 1)
        {
            Console.WriteLine("Advertise removed");
        }
    }
    public List<Advertise> ShowAllAds()

    {
        List<Advertise> allAds = new();
        // id

        using (MySqlConnection con = new MySqlConnection("Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "SELECT id, rubric,description,price,municipality,county, is_checked AS 'isChecked' FROM advertise";

            allAds = con.Query<Advertise>(query).ToList();
        }

        return allAds;
    }
    public Advertise ShowAd(int id)
    {
        Advertise advertise = new();

        using (MySqlConnection con = new MySqlConnection("Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "SELECT rubric,description,price,municipality,county,user_id AS 'userID' FROM advertise WHERE id = @id;";

            advertise = con.QuerySingle<Advertise>(query, new { @id = id });
        }
        return advertise;
        //all info, även userid 

    }
    public void CheckAds(int id)

    {

        using (MySqlConnection con = new MySqlConnection("Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "UPDATE advertise SET is_checked = true WHERE id = @id;";

            int rows = con.ExecuteScalar<int>(query, new { @id = id });
        }


    }
    public List<Advertise> ShowMyads(int id)
    {
        List<Advertise> userAds = ads;

        using (MySqlConnection con = new MySqlConnection("Server=localhost;Database=Blocket_clone;Uid=root;Pwd=;"))
        {

            string query = "SELECT id, rubric,description,price,municipality,county, is_checked AS 'isChecked' FROM advertise WHERE user_id = @id;";

            userAds = con.Query<Advertise>(query, new { @id = id }).ToList();

        }
        return userAds;
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

        }
        return id;
    }
    public void AdOverview(Advertise advertise)
    {

    }

}