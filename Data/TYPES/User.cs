namespace BlazorBlocket.Data;
public class User
{
    public List<Message> messages = new();
    public int Id { get; set; }
    public int Password { get; set; }
    public string? Name { get; set; }
    public string? SocialSecurityNumber { get; set; }
    public string? Email { get; set; }
    public readonly DateTime Openaccount;
    public User(string name, string SocialSecurityNumber, string Email, int password)
    {
        Openaccount = DateTime.Now;
    }
    public User()
    {

    }
}