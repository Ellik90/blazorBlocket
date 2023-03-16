using System.Net.Mail;
namespace BlazorBlocket.Data;
public class LogInService 
{
    UserExistsDB _userExistsDB;
    AdminDB _adminDB;
    Validator _validator;
    EmailSender _emailSender;
    AdminExistsDB _adminExistsDB;

    public LogInService(AdminDB adminDB, Validator validator, EmailSender emailSender, UserExistsDB userExistsDB, AdminExistsDB adminExistsDB)
    {
        _userExistsDB = userExistsDB;
        _adminDB = adminDB;
        _validator = validator;
        _emailSender = emailSender;
        _adminExistsDB = adminExistsDB;
    }
    User user = new();
    public User MakeNewLogIn(User user)
    {
        //här anropar jag metoden och skickar in mailadressen som kommer in, denna metoden returnerar antingen true eller false 
        if (_validator.ValidateEmail(user.Email) == true)
        {
            Console.WriteLine("Valid email");
            try
            {
                user.Password = _emailSender.SendCodeViaEmail(user.Email);
                Console.WriteLine("Code sent to your mail. Please check junkmail if mail not found.");
            }
            catch(SmtpFailedRecipientException)
            {
                Console.WriteLine("Something went wrong. Please contact mail-service.");
            }

        }
        else
        {
            Console.WriteLine("Unvalid email");
        }
        return user;
    }
    public Admin MakeNewLogIn(Admin admin)
    {
        //här anropar jag metoden och skickar in mailadressen som kommer in, denna metoden returnerar antingen true eller false 
        if (_validator.ValidateEmail(admin.Email) == true)
        {
            Console.WriteLine("Valid email");
            admin.PassWord = _emailSender.SendCodeViaEmail(admin.Email);
            Console.WriteLine("Code sent to your mail. Please check junkmail if mail not found.");
        }
        else
        {
            Console.WriteLine("Unvalid email");
        }
        return admin;
    }
    public User UserLogIn(User user)
    {
        if (_validator.ValidateEmail(user.Email) == true)
        {
            Console.WriteLine("Valid email");
        }
        else
        {
            Console.WriteLine("Unvalid email");
        }
        return user;
    }
    public Admin AdminLogIn(Admin admin)
    {
        if (_validator.ValidateEmail(admin.Email) == true)
        {
            Console.WriteLine("Valid email");
        }
        else
        {
            Console.WriteLine("Unvalid email");
        }
        return admin;
    }
    public User SendNewCode(User user)
    {
        user.Password = _emailSender.SendCodeViaEmail(user.Email);
        return user;
    }
    public int UserLogInIsValid(User user)
    {
        return _userExistsDB.UserLogInExists(user);
    }
    public int AdminLogInIsValid(Admin admin)
    {
        return _adminExistsDB.AdminLogInExists(admin);
    }

}