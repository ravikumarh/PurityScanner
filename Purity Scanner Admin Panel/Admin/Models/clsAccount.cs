using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.Data;

namespace Admin.Models
{

    public class clsAccount
    {
        DBManage DBobject = new DBManage();
        string user_name;
        string user_password;

        public string UserName
        {
            get { return user_name; }
            set { user_name = value; }
        }

        public string UserPassword
        {
            get { return user_password; }
            set { user_password = value; }
        }

        public int loginUser(clsAccount objUser)
        {
            string str = "";
            try
            {
                if (!string.IsNullOrEmpty(objUser.UserName) && !string.IsNullOrEmpty(objUser.UserPassword))
                {
                    str = "select * from UsersDetails where user_name='" + objUser.UserName.Trim() + "' and user_password='" + objUser.UserPassword.Trim() + "'";
                    DataTable dt = DBobject.SelectData(str);
                    if (dt.Rows.Count > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ee)
            {
                return 0;
            }
        }
    }
}