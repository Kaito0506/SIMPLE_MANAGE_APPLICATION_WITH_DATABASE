using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    internal class clsDatabase
    {
        public static SqlConnection con;

        public static bool OpenConnect()
        {
            try
            {
                con = new SqlConnection("Data Source=KAITO;Initial Catalog=DVDLibrary;Integrated Security=True");
                con.Open();
                Console.WriteLine("Connect successfully");
                return true;
            }
            catch(Exception e) {
                Console.WriteLine(e);
                Console.WriteLine("Can not connect server");
                return false; 
            }
        }

        public static bool CloseConnect()
        {
            try
            {
                con.Close();
                return true;
            }
            catch(Exception)
            {
                return false;
            }

        }
        
    }
}
