using System.Net.Mail;
namespace BlazorBlocket.Data;
public class Identifier 
{
    SmtpClient _smtpClient;
    readonly DateTime Date;
    public Identifier()
    {
        Date = DateTime.Now;
    }
    public SmtpClient SmtpClient
    {
        get { return _smtpClient; }
        set { _smtpClient = value; }
    }
    public bool ValidateEmail(string emailAdress)
    {
        bool isValid = true;
        try
        {
            // om mailadressen som kommer in känns igen som en mailadress så returneras true
            MailAddress mailAdress = new MailAddress(emailAdress);
        }
        catch
        {
            //annars returneras falsk, den är då inte validerad som riktig mailadress
            isValid = false;
        }
        return isValid;
    }
    public SmtpClient SetUpSmtpClient()
    {
        //PORT 587 BLOCKED I BORAS STAD INTERNET? KOLLA OM ANVÄNDA ANNAN PORT??
        SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com");
        smtpClient.Port = 587;// port 587 för utgående epost 
        smtpClient.Host = "smtp-mail.outlook.com";
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.EnableSsl = true;
        smtpClient.Credentials = new System.Net.NetworkCredential("testing_sendpwd_123@outlook.com", "Testing123321");
        return SmtpClient = smtpClient;
    }
    public int SendCodeViaEmail(string mail)
    {
        SmtpClient = SetUpSmtpClient();
        int code = GenerateUniqueCode();
        var message = new MailMessage()
        {
            From = new MailAddress("testing_sendpwd_123@outlook.com"),
            Subject = $"Validation Email. Blocket-Klon.Com",
            Body = $"Here is your unique code to login at Blocket-Klon.com: {code}"
        };
        message.To.Add(mail);
        SmtpClient.Send(message);
        return code;
    }
    public bool ValidateSocialSecurityNumber(string socialSecurityNumber)
    {
        bool isValid = false;
        //måste ha 12 siffror
        int socialSecurityNumberCount = 12;
        if (socialSecurityNumber.Count() == socialSecurityNumberCount && socialSecurityNumber.All(char.IsDigit) == true)
        {
            isValid = true;
        }
        return isValid;
    }
    public bool CheckIfUserExists(UserExistsDB userExistsDB, User user)
    {
        int id = userExistsDB.UserLogInExists(user);
        if (id == 0)
        {
            return false;
        }
        else
        {
            return true;   // den ska flyttas
        }
    }
    public int GenerateUniqueCode()
    {
        bool isCodeInt = false;

        int codePartOne = Date.Hour;
        int codePartTwo = Date.Minute;
        int codePartThree = Date.Millisecond;
        string codeString = codePartOne.ToString() + codePartTwo.ToString() + codePartThree.ToString();
        int code = 0;
        isCodeInt = int.TryParse(codeString, out code);
        if(!isCodeInt)
        {
            codeString.Replace(((char)codePartOne), '0');
        }
        return code;
    }
}