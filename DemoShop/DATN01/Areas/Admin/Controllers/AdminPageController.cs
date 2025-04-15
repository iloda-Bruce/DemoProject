using DATN01.Data;
using DATN01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DATN01.Areas.Admin.Controllers
{
    
    public class AdminPageController : Controller
    {
        private readonly string connectionString = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLBH2;Integrated Security=True;Trust Server Certificate=True";
        [SessionFilter("admin")]
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("EID");
            string? role = HttpContext.Session.GetString("ARole");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select * from tblE01 where EID ='" + userId + "'";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            EmployeeModel employee = new EmployeeModel();
                            employee.EID = int.Parse(dt.Rows[0]["EID"].ToString());
                            employee.EName = dt.Rows[0]["EName"].ToString();
                            employee.EAddress = dt.Rows[0]["EAddress"].ToString();
                            employee.EPhone = dt.Rows[0]["EPhone"].ToString(); ;
                            employee.EDOB = DateTime.Parse(dt.Rows[0]["EDOB"].ToString() == "" ? DateTime.Now.ToString() : dt.Rows[0]["EDOB"].ToString());


                            ViewBag.Username = employee;
                            return View();
                        }
                        else
                        {
                            return RedirectToAction("Index","Login",null);
                        }
                    }
                }
            }
        }
        [HttpGet]
        public IActionResult GetData()
        {
            List<object> data = new List<object>();
            List<string> lable = new List<string>();

            List<object> data1 = new List<object>();
            List<string> lable1 = new List<string>();

            List<int> total = new List<int>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select * from tblO01 ";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach(DataRow row in dt.Rows)
                            {
                                lable.Add(DateTime.Parse(row["DateList"].ToString()).ToString("dd"));
                                total.Add(int.Parse(row["Total"].ToString() == ""?"0": row["Total"].ToString()));
                            }
                            data.Add(lable);
                            data.Add(total);
                            return Json(data);
                        }
                        else
                        {
                            return Json(new {code=401,mess = "fail"});
                        }
                    }
                }
            }
        }

        [HttpGet]
        public IActionResult GetData01()
        {
            List<object> data = new List<object>();
            List<string> lable = new List<string>();
            List<int> total = new List<int>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select * from tblSP01 ";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                lable.Add(row["PName"].ToString());
                                total.Add(int.Parse(row["Total"].ToString() == "" ? "0" : row["Total"].ToString()));
                            }
                            data.Add(lable);
                            data.Add(total);
                            return Json(data);
                        }
                        else
                        {
                            return Json(new { code = 401, mess = "fail" });
                        }
                    }
                }
            }
        }
    }

}
