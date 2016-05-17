using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class DBManage
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);

        public  DataTable SelectData(string str)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ee)
            {
                return dt;
            }              
        }
        public int IUD_Data(string str)
        {
            int result = 0;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(str, con);
                result = cmd.ExecuteNonQuery();
                con.Close();
                return result;
            }
            catch (Exception ee)
            {
                return result;
            }
        }
    }
}
