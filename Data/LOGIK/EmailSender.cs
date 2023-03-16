using System.Net.Mail;
namespace BlazorBlocket.Data;
public class EmailSender 
{
    SmtpClient _smtpClient;
    readonly DateTime Date;
    public EmailSender()
    {
        Date = DateTime.Now;
    }
    public SmtpClient SmtpClient
    {
        get { return _smtpClient; }
        set { _smtpClient = value; }
    }
    public SmtpClient SetUpSmtpClient()
    {
        //PORT 587 BLOCKED I BORAS STAD INTERNET? KOLLA OM ANVÄNDA ANNAN PORT??
        SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com");
        smtpClient.Port = 587;// port 587 för utgående epost 
        smtpClient.Host = "smtp-mail.outlook.com";
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("blocket_clone_project@outlook.com", "Testing123321");
        return SmtpClient = smtpClient;
    }
    public int SendCodeViaEmail(string email)
    {
        SmtpClient = SetUpSmtpClient();
        int code = GenerateUniqueCode();
        var message = new MailMessage()
        {
            From = new MailAddress("blocket_clone_project@outlook.com"),
            Subject = $"Validation Email. Blocket-Klon.Com",
            Body = $"Here is your unique code to login at Blocket-Klon.com: {code}"
        };
        message.To.Add(email);
        SmtpClient.Send(message);
        return code;
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
        if (!isCodeInt)
        {
            codeString.Replace(((char)codePartOne), '0');
        }
        return code;
    }
}