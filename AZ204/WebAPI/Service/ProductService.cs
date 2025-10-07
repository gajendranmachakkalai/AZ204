using System.Data;
using System.Data.SqlClient;
using WebAPI.Model;

namespace WebAPI.Service
{
    public class ProductService : IProductService
    {
        private string sqlconnection;
        public ProductService()
        {
            sqlconnection = "Server=tcp:appdbserver4215.database.windows.net,1433;Initial Catalog=appdb4215;Persist Security Info=False;" +
                "User ID=sqladmin;Password=Dharani@4215;" +
                "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection con = new SqlConnection(sqlconnection))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM dbo.Product", con);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                foreach (DataRow product in dt.Rows)
                {
                    products.Add(new Product()
                    {
                        Id = Convert.ToInt32(product["Id"].ToString()),
                        Name = product["Name"].ToString(),
                        Price = Convert.ToInt32(product["Price"].ToString())
                    });
                }
            }
            return products;
        }
    }
}
