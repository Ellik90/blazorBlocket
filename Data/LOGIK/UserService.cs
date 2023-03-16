namespace BlazorBlocket.Data;
public class UserService 
{
    UserDB _userDB;
    UserEditorDB _userEditorDB;
    UserExistsDB _userExistsDB;
    //här i är funktioner mellan anv och db, tex makenewuser(string name, string email) eller makenewuser(User user)samt kontrollerare osv;
    public UserService(UserDB userDB, UserEditorDB userEditorDB, UserExistsDB userExistsDB)
    {
        _userDB = userDB;
        _userEditorDB = userEditorDB;
        _userExistsDB = userExistsDB;
    }
    public User GetTheUser(User user)
    {
       List<User> users = _userDB.GetUser();
       User getUser = new();

       foreach(User auser in users)
       {
        if(auser.Id == user.Id)
        {
            getUser = auser;
        }
       }
       return getUser;    
               
    }

    public bool GetUserIdToAD(int advertiseId)
    {
        // ADVERTISE SERVICE METOD SOM HÄMTAR ALLA ANNONS EGENSKAPER, SEN SKICKAR VIDARE USERID
        int rows = 0;
        _userDB.GetUserIdFromAdvertise(advertiseId);
        if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool MakeUser(User user)
    {
        int rows = 0;
        _userDB.CreateUser(user);
        if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckNickNameExists(string nickName)
    {
        bool rows = false;
        _userExistsDB.NicknameExists(nickName);
        if (_userExistsDB.NicknameExists(nickName) == true)
        {
           rows = true;
        }
       return rows;
    }
         public bool CheckUserEmailExists(string email)
    {
        bool rows = false;
        
        if (_userExistsDB.UserEmailExists(email) == true)
        {
            rows = true;
        }
     return rows;
    }
    public bool DeleteTheUser(User user)
    {
        int rows = 0;
        _userDB.DeleteUser(user);
        if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool DescriptionInput(User user, string updateDescription)
    {
        int rows = 0;
        _userEditorDB.UpDateDescription(user, updateDescription);
        if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
     public bool UpdateEmail(User user, string userEmail)
    {
        int rows = 0;
        _userEditorDB.UpdateEmail(user, userEmail);
          if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
      public bool UpdateNickname(User user, string updateNickname)
    {
        int rows = 0;
        _userEditorDB.UpdateNickName(user, updateNickname);
           if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool UpDateDescription(User user, string updateDescription)
    {
       int rows = 0;
       _userEditorDB.UpDateDescription(user, updateDescription);
          if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
     public bool UpDatePassword(User user, string passWord)
    {
       int rows = 0;
       _userEditorDB.UpDatePassword(user, passWord);
          if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
   
}