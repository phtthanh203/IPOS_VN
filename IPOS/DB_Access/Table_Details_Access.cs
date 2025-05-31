using IPOS.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPOS.DB_Access
{
    public class Table_Details_Access
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

        public List<Table_Details> GetAllTables()
        {
            List<Table_Details> list = new List<Table_Details>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ID, nameTable, Status, IsTakeAway FROM Table_Details";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Table_Details
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        nameTable = reader["nameTable"].ToString(),
                        Status = reader["Status"].ToString(),
                        IsTakeAway = Convert.ToBoolean(reader["IsTakeAway"])
                    });
                }
                reader.Close();
            }
            return list;
        }
    }
}
