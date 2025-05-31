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
    public class ProductDetails_Access
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

        public List<ProductDetails> getAllTables()
        {
            List<ProductDetails> list = new List<ProductDetails>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "Select ProductId, ProductName,Price,ImagePath,CategoryId from Products";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new ProductDetails
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = Convert.ToString(reader["ProductName"]),
                        Price = Convert.ToInt32(reader["Price"]),
                        ImagePath = Convert.ToString(reader["ImagePath"]),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    });

                }
                
            }
            return list;
        }

    }
}
