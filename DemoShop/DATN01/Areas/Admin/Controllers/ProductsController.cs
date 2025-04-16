using DATN01.Areas.Admin.Models;
using DATN01.Data;
using DATN01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                                product.PMark = row["PMark"].ToString();
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
        public IActionResult UpdateProduct([FromBody]ProductModel _product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    // $('#productCode').attr('data-product-id', data.pid);
                    //$("#productCode").val(data.pCode);
                    //$("#productName").val(data.pName);
                    //$("#productGroup").val(data.pGroup);
                    //$("#productClass").val(data.pClass);
                    //$("#price").val(data.pSell);
                    //$("#cost").val(data.pCapital);
                    //$("#quantity").val(data.pReserves);
                    //$("#weight").val(data.pWeight);
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "spUpdate_tblP01";
                    cmd.Parameters.AddWithValue("@PID", _product.PID);
                    cmd.Parameters.AddWithValue("@PCode", _product.PCode);
                    cmd.Parameters.AddWithValue("@PName", _product.PName);
                    cmd.Parameters.AddWithValue("@PMark", _product.PMark);
                    cmd.Parameters.AddWithValue("@PLimit", _product.PLimit);
                    cmd.Parameters.AddWithValue("@PGroup", _product.PGroup);
                    cmd.Parameters.AddWithValue("@PClass", _product.PClass);
                    cmd.Parameters.AddWithValue("@PSell", _product.PSell);
                    cmd.Parameters.AddWithValue("@PCapital", _product.PCapital);
                    cmd.Parameters.AddWithValue("@PWeight", _product.PWeight);
                    cmd.Parameters.AddWithValue("@Notes", _product.Notes);
                    cmd.Parameters.AddWithValue("@isSell", _product.isSell);
                    cmd.Parameters.AddWithValue("@isActive", _product.isActive);
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

        [HttpPost]
        public IActionResult DeleteProduct(int PID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Delete from tblP01 where tblP01.PID='" + PID + "'";
                    int res = cmd.ExecuteNonQuery();
                    if(res > 0)
                    {
                        return Json(new { code = 200, message = "Xóa sản phẩm thành công!" });
                    }
                    else
                    {
                        return Json(new { code = 401, message = "Xóa sản phẩm thất bại!" });
                    }
                    
                }
            }
        }
    }
}
