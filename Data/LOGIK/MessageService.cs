namespace BlazorBlocket.Data;
public class MessageService
{
    MessageDB _messageDB;
    AdminMessageDB _adminMessageDB;
    List<Message> allMessages = new();
    List<Message> oneConversationMessages = new();
    Message message = new();
    public MessageService(MessageDB messageDB, AdminMessageDB adminMessageDB)
    {
        _messageDB = messageDB;
        _adminMessageDB = adminMessageDB;
    }
    public bool MakeMessage(Message message)
    {
        int rows = _messageDB.CreateSendMessage(message);
        if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public List<Message> ShowAllMessages(User user)
    {
        List<Message> messages = _messageDB.GetMessagesOverview(user); //_conversationHandler.GetAllMessagesOverlookTest(user);
        return messages;
    }
    public List<Message> ShowOneMessageConversation(int messageId, int participantId, int myId)
    {
        // den hittar meddelande med specifikt id
        List<Message> messages = _messageDB.GetMessageConversation(messageId, participantId, myId);//_conversationHandler.GetMessageConversationTEST(messageId, fromUserId, thisUserId);
        return messages;
    }
    public List<Message> ShowStructuredConversation(int messageId, int fromUserId, int thisUserId)
    {
        // DENNA SKA INNEHÅLLA FUNKTION, TAR IN ALLA MEDDELANDEN OCH STRUKTURERAR KONVERSATION HÄR I?
        List<Message> messages = _messageDB.GetMessageConversation(messageId, fromUserId, thisUserId);
        return messages;
    }
    public void DeleteConversation(int myid, int participantId)
    {
        _messageDB.DeleteMessageConversation(myid, participantId);
    }
    public int GetSender(int messageId)
    {
        return _messageDB.GetSenderId(messageId);
    }
    public bool MessageToAdmin(User user, Message message)
    {
        int newMessageId = _messageDB.CreateMessage(message);
        message.ID = newMessageId;
        allMessages.Add(message);
        List<int> adminIds = _messageDB.GetAdminId();
        int rows = _messageDB.SendMessageToAdmin(user.Id, message, adminIds);
        if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public List<Message> GetMessagesFromAdmin(User user)
    {
        List<Message> getMessages = _messageDB.GetMessagesFromAdmin(user);
        List<Message> messagesNotOld = new();
        foreach (Message item in getMessages)
        {
            if (DateTime.Now < item.DateSent.AddDays(7))
            {
                messagesNotOld.Add(item);
            }
        }
        return messagesNotOld;
    }
}