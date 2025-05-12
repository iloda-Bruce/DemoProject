using DATN01.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace DATN01.Areas.Repository
{
    public class ProductService : IProductServices
    {
        private readonly string connectionString = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLBH2;Integrated Security=True;Trust Server Certificate=True";

        public IActionResult GetProducts()
        {
            //return new JsonResult(new { code = 400, message = "fail" });
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select * from tblP01 left join tblST01 on tblP01.PID = tblST01.PID";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            List<ProductsViewModel> products = new List<ProductsViewModel>();
                            foreach (DataRow row in dt.Rows)
                            {
                                ProductsViewModel product = new ProductsViewModel();
                                product.PID = int.Parse(row["PID"].ToString());
                                product.PGroup = row["PGroup"].ToString();
                                product.PClass = row["PClass"].ToString();
                                product.PCode = row["PCode"].ToString();
                                product.PName = row["PName"].ToString();
                                product.PLimit = int.Parse(row["PLimit"].ToString());
                                product.PCapital = decimal.Parse(row["PCapital"].ToString());
                                product.PSell = decimal.Parse(row["PSell"].ToString());
                                product.PWeight = decimal.Parse(row["PWeight"].ToString());
                                product.Notes = row["Notes"].ToString();
                                product.isSell = row["isSell"].ToString().ToLower() == "true" ? true : false;
                                product.isActive = row["isActive"].ToString().ToLower() == "true" ? true : false;
                                product.PReserves = int.Parse(row["PReserves"].ToString() == "" ? "0" : row["PReserves"].ToString());

                                products.Add(product);
                            }
                            return new JsonResult(products);
                        }
                        else
                        {
                            return new JsonResult(new { code = 400, message = "fail" });
                        }
                    }
                }
            }
        }
    }
}
