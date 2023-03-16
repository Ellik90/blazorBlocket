namespace BlazorBlocket.Data;
public class Admin
{
    public int Id { get; set; }
    public string? SocialSecurityNumber { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int PassWord { get; set; }
    public readonly DateTime Openaccount;

    public Admin(int id, string socialsecuritynumber, string name, string email, int passWord)
    {
        Openaccount = DateTime.Now;
    }
    public Admin()
    {

    }
}