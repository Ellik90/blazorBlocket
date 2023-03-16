using MySqlConnector;
using Dapper;
namespace BlazorBlocket.Data;
public class MessageDB 
{
    public MessageDB() { }
    public int CreateSendMessage(Message message)
    {
        int rows = 0;
        using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=blocket_clone;Uid=root;Pwd=;Allow User Variables=true;"))
        {
            string query = "START TRANSACTION;" +
            "INSERT INTO message (rubric, content) VALUES(@rubric, @content);" +
            "SET @message_id := LAST_INSERT_ID();" +
            "INSERT INTO user_message (from_user_id, to_user_id, message_id) VALUES(@idfromuser, @idtouser, @message_id);" +
            "SET @user_message_id := LAST_INSERT_ID();" +
            "INSERT INTO conversation_thread (user_id, user_message_id) VALUES(@idfromuser, @user_message_id);" +
            "INSERT INTO conversation_thread (user_id, user_message_id) VALUES(@idtouser, @user_message_id);" +
            "COMMIT; SELECT @user_message_id;";
            rows = connection.QuerySingle<int>(query, param: message);
        }
        return rows;
    }

    public int GetSenderId(int messageId)
    {
        int fromUserId = 0;
        using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "SELECT from_user_id FROM user_message where message_id = @messageid;";
            fromUserId = connection.QuerySingle<int>(query, new { @messageid = messageId });
        }
        return fromUserId;
    }
    public List<Message> GetMessageConversation(int messageId, int otherUserId, int myId)
    {
        List<Message> messages = new();
        using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "SELECT p.rubric, p.content, u1.nick_name as 'namefromuser' " +
            " FROM user_message um LEFT JOIN message p ON (um.message_id = p.id) LEFT JOIN users u1 " +
            " ON (u1.id = um.from_user_id) LEFT JOIN users u2 ON (u2.id = um.to_user_id) " +
            "  WHERE (u2.id = @otheruserid AND u1.id = @myid) OR (u2.id = @myid AND u1.id = @otheruserid) ORDER BY um.date_sent ASC;";
            messages = connection.Query<Message>(query, new { @messageid = messageId, @otheruserid = otherUserId, @myid = myId }).ToList();
        }
        return messages;
    }
    public int DeleteMessageConversation(int myId, int participantId)
    {
        int rows = 0;
        using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=blocket_clone;Uid=root;Pwd=;"))
        {
            string query = " DELETE ct FROM conversation_thread ct " +
            "INNER JOIN user_message um  ON (um.id = ct.user_message_id) " +
            "INNER JOIN users u1 ON (u1.id = um.from_user_id) " +
            "INNER JOIN users u2 ON (u2.id = um.to_user_id) " +
            "WHERE (u1.id = @participantid AND u2.id = @myid) " +
            "OR (u2.id = @participantid AND u1.id = @myid) " +
            "AND ct.user_id = @myid;";
            rows = connection.ExecuteScalar<int>(query, new { @myid = myId, @participantId = participantId });
        }
        return rows;
    }
    public List<Message> GetMessagesOverview(User user)
    {
        List<Message> allMessages = new();
        using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "SELECT p.id, p.rubric, u1.nick_name as 'namefromuser',u2.nick_name as 'touser', um.date_sent as 'datesent',COUNT(um.from_user_id) as 'countMessagesFromUser' " +
            "FROM user_message um " +
            " INNER JOIN message p ON um.message_id = p.id " +
            " INNER JOIN conversation_thread ct ON (ct.user_message_id = um.id) " +
            "INNER JOIN users u1 ON um.from_user_id = u1.id " +
            "INNER JOIN users u2 ON um.to_user_id = u2.id " +
            "WHERE u2.id = @id AND ct.user_id = @id " +
            "GROUP BY um.from_user_id " +
            "HAVING COUNT(from_user_id) >= 1 " +
            "ORDER BY um.date_sent ASC;";
            allMessages = connection.Query<Message>(query, param: user).ToList();
        }
        return allMessages;
    }
    public int CreateMessage(Message message)
    {
        int messageId = 0;
        using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "INSERT INTO message (rubric, content) VALUES(@rubric, @content);SELECT LAST_INSERT_ID();";
            messageId = connection.ExecuteScalar<int>(query, param: message);
        }
        return messageId;
    }
    public List<int> GetAdminId()
    {
        List<int> adminIds = new();
        using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "SELECT id FROM admins"; // senare tex: where role is ....
            adminIds = connection.Query<int>(query).ToList();
        }
        return adminIds;
    }
    public int SendMessageToAdmin(int userId, Message message, List<int> adminIds)
    {
        //vill ha att göra och skicka medd ska vara i transaktion, problemet är att det är ett okänt antal
        //admins idn som ska sättas in i admin_message! kan ju senare om jag utv. detta ha att den skickas
        // till admins som har en spcifik roll, tex kundhantering/kundservice? 
        int usermessageId = 0;
        foreach (int item in adminIds)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=blocket_clone;Uid=root;Pwd=;"))
            {
                string query = "INSERT INTO admin_message (user_id, admin_id, message_id) VALUES(@userId, @adminId, @messageId);";
                usermessageId = connection.ExecuteScalar<int>(query, new { @userId = message.IDFromUser, @adminId = item, @messageId = message.ID });
            }
        }
        return usermessageId;
    }
    public List<Message> GetMessagesFromAdmin(User user)
    {
        List<Message> adminMessages = new();
        using (MySqlConnection connection = new MySqlConnection("Server=localhost;Database=blocket_clone;Uid=root;Pwd=;"))
        {
            string query = "SELECT message_id as 'id', date_sent as 'datesent', rubric, content, nick_name as 'namefromuser' FROM admin_message " +
            "INNER JOIN message ON admin_message.message_id = message.id " +
            "INNER JOIN users ON admin_message.user_id = users.id WHERE user_id = @id ORDER BY date_sent DESC LIMIT 1;";
            adminMessages = connection.Query<Message>(query, param: user).ToList();
        }
        return adminMessages;
    }
}