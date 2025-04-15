using DATN01.Areas.Admin.Models;
using DATN01.Data;
using DATN01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace DATN01.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly string connectionString = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLBH2;Integrated Security=True;Trust Server Certificate=True";
        [SessionFilter("admin")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select * from tblP01 join tblST01 on tblP01.PID = tblST01.PID";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            List<ProductsViewModel> products = new List<ProductsViewModel>();
                            foreach(DataRow row in dt.Rows)
                            {
                                ProductsViewModel product = new ProductsViewModel();
                                product.PID = int.Parse(row["PID"].ToString());
                                product.PGroup =row["PGroup"].ToString();
                                product.PClass = row["PClass"].ToString();
                                product.PCode = row["PCode"].ToString();
                                product.PName = row["PName"].ToString();
                                product.PLimit = int.Parse(row["PLimit"].ToString());
                                product.PCapital = decimal.Parse(row["PCapital"].ToString());
                                product.PSell = decimal.Parse(row["PSell"].ToString());
                                product.PWeight = decimal.Parse(row["PWeight"].ToString());
                                product.Notes = row["Notes"].ToString();
                                product.isSell = row["isSell"].ToString().ToLower() == "true"?true:false;
                                product.isActive = row["isActive"].ToString().ToLower() == "true" ? true : false;
                                product.PReserves = int.Parse(row["PReserves"].ToString()==""?"0": row["PReserves"].ToString());

                                products.Add(product);
                            }
                            return Json(products);
                        }
                        else
                        {
                            return Json(new {code=400, message="fail"});
                        }
                    }
                }
            }
        }

        [HttpPost]
        public IActionResult GetProduct(int PID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select * from tblP01 join tblST01 on tblP01.PID = tblST01.PID where tblP01.PID='"+ PID + "'";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ProductsViewModel product = new ProductsViewModel();
                            //List<ProductsViewModel> products = new List<ProductsViewModel>();
                            foreach (DataRow row in dt.Rows)
                            {
                                
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

                                //products.Add(product);
                            }
                            return Json(product);
                        }
                        else
                        {
                            return Json(new { code = 400, message = "fail" });
                        }
                    }
                }
            }
        }
        [HttpPost]
        public IActionResult UpdateProduct([FromBody] ProductModel _product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "spUpdate_tblP01";
                    cmd.Parameters.AddWithValue("", _product.PID);
                    int res = cmd.ExecuteNonQuery();
                    if(res > 0)
                    {
                        return Json(new { code = 200, message = "Success" });
                    }
                    else
                    {
                        return Json(new { code = 401, message = "Fail" });
                    }
                }
            }
        }
    }
}
