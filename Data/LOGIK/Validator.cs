using System.Net.Mail;
namespace BlazorBlocket.Data;

public class Validator 
{
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

}