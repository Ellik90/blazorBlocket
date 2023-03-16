namespace BlazorBlocket.Data;
public class AdminMessageService
{
    AdminMessageDB _adminMessageDB;
    MessageDB _messageDB;
    public AdminMessageService(AdminMessageDB adminMessageDB, MessageDB messageDB)
    {
        _adminMessageDB = adminMessageDB;
        _messageDB = messageDB;
    }
    public int GetSender(int messageId)
    {
        return _adminMessageDB.AdminGetSenderId(messageId);
    }
    public List<Message> GetUsersMessages(Admin admin)
    {
        return _adminMessageDB.GetUsersMessages(admin);
    }
    public void MessageUser(Message message, int messageId)
    {
        _adminMessageDB.CreateMessage(message, messageId);
    }
}