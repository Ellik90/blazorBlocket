namespace BlazorBlocket.Data;
public class AdminService
{
    AdminEditorDB _adminEditorDB;
    UserDB _userDB;
    UserEditorDB _userEditorDB;
    AddvertiseDb _advertiseDB;
    AdminDB _adminDB;
    AdminExistsDB _adminExistsDB;

    public AdminService(UserDB userDB, UserEditorDB userEditorDB, AdminDB adminDB, AdminEditorDB adminEditorDB, AddvertiseDb advertiseDb, AdminExistsDB adminExistsDB)
    {
        _userDB = userDB;
        _userEditorDB = userEditorDB;
        _adminDB = adminDB;
        _advertiseDB = advertiseDb;
        _adminEditorDB = adminEditorDB;
        _adminExistsDB = adminExistsDB;
    }

    public bool UpdateEmail(Admin admin, string adminEmail)
    {
        int rows = 0;
        _adminEditorDB.UpdateAdminEmail(admin, adminEmail);
        if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Admin GetTheAdmin(Admin admin)
    {
        List<Admin> admins = _adminDB.GetAdmins(admin);

        foreach (Admin item in admins)
        {
            if (item.Id == admin.Id)
            {
                return item;
            }
        }
        return admin;
    }

    public bool MakeAdmin(Admin admin)
    {
        int rows = 0;
        rows = _adminDB.CreateAdmin(admin);
        if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool DeleteAdmin(Admin admin)
    {
        int rows = 0;
        _adminDB.DeleteAdmin(admin);
        if (rows > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // public bool CheckAdminNameExists(string name)
    // {
    //     bool rows = true;
    //     _adminHandeler.AdminNameExists(name); //name admin  // Används ej
    //     if ( _adminHandeler.AdminNameExists(name) == true)
    //     {
    //         rows = true;
    //     }
    //    return rows;
    // }
    public bool CheckAdminEmailExists(string Email)
    {
        bool rows = false;

        if (_adminExistsDB.AdminEmailExists(Email) == true)
        {
            return true;
        }
        return rows;
    }

    public List<Advertise> GetNonCheckAds()
    {
        List<Advertise> allAdvertises = _advertiseDB.ShowAllAds();
        List<Advertise> nonCheckedAds = new();
        foreach (Advertise item in allAdvertises)
        {
            if (item.isChecked == false)
            {
                nonCheckedAds.Add(item);
            }
        }
        return nonCheckedAds;
    }
    // LÄGG IN METODER
    // updateadminname

}