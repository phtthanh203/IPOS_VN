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
    public class Category_Access
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        public List<Category> GetAllTables()
        {
            List<Category> list = new List<Category>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select CategoryId, CategoryName from Category";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Category
                    {
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryName = Convert.ToString(reader["CategoryName"]),
                    });
                   
                }
            }

            return list;
        }
    }
}
    

