namespace BlazorBlocket.Data;
public class Message
{
    public int ID { get; set; }
    public int IDFromUser { get; set; }
    public string NameFromUser { get; set; }
    public string NameToUser { get; set; }
    public int IDToUser { get; set; }
    public string Rubric { get; private set; }
    public string Content { get; private set; }
    public DateTime DateSent { get; set; }
    // public annons annons;
    public int CountMessagesFromUser { get; set; }
    public User user = new();
    public bool IsOld { get; private set; }   // om det gått 10 dagar så hamnar den i i gamla listan

    public Message(string rubric = "Empty title", string content = "Empty message", int idFromUser = 0, int idToUser = 0)
    {
        Rubric = rubric; // annonsrubriken?
        Content = content;
        IDFromUser = idFromUser;
        IDToUser = idToUser;
        DateSent = DateTime.Now;
    }
    public Message() { }

    public bool IsMessageOld()
    {
        if (DateTime.Now > DateSent.AddDays(14))
        {
            IsOld = true;
        }
        else
        {
            IsOld = false;
        }
        return IsOld;
    }

    public string MessagesToString()
    {
        return $"Message id [{ID}]: {DateSent.Date.ToShortDateString()} {Rubric} From {NameFromUser}\n\rMessages {CountMessagesFromUser}";
    }
    public string ConversationToString()
    {
        return $"{NameFromUser}\n\r{Rubric}\n\r{Content}\n\r";
    }

    public string AdminMessageString()
    {
        return $"Message ID:[{ID}] {Rubric}\n\r{Content}\n\r//{NameFromUser}";
    }

}