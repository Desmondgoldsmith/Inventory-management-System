using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ShopriteInventoryApp
{
    class MotherOfAllConnections
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-B22ALD5;Initial Catalog=Shoprite;Integrated Security=True;");
         
        public void openConn()
        {
            if(conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        
        //close connection
        public void closeConn()
        {
            if(conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }         

        //ruturn connection
        public SqlConnection returnConn()
        {
            return conn;
        }
    }
}
