using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_b3
{
    internal class Database
    {
        public static SqlConnection con;

        public static bool OpenConnect()
        {
            try
            {
                con = new SqlConnection("Data Source=KAITO;Initial Catalog=HOTEL_MANAGEMENT;Integrated Security=True");
                con.Open();
                Console.WriteLine("Connect successfully");
                return true;

            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Can not connect to server");
                return false;
            }
        }

        public static bool CloseConnect()
        {
            try
            {
                con.Close();
                return true;
            }catch { return false; }
        }
    }
}
