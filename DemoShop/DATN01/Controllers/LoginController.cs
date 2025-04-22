using DATN01.Areas.Admin.Models;
using DATN01.Models;
using DATN01.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Data;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DATN01.Controllers
{
    public class LoginController : Controller
    {
        private readonly string connectionString = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLBH2;Integrated Security=True;Trust Server Certificate=True";
        private readonly ILogin logins;
        public LoginController(ILogin login)
        {
            this.logins = login;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginFunction([FromBody] LoginModel logModel) {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select * from tblA01 where AName ='" +logModel.AName +"' and APass ='"+logModel.APass+"'";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            AccountModel account = new AccountModel();
                            account.AID = int.Parse(dt.Rows[0]["AID"].ToString());
                            account.AName = dt.Rows[0]["AName"].ToString();
                            account.APass = dt.Rows[0]["APass"].ToString();
                            account.EID = int.Parse(dt.Rows[0]["EID"].ToString()==""?"0":dt.Rows[0]["EID"].ToString());
                            account.CID = int.Parse(dt.Rows[0]["CID"].ToString() == "" ? "0" : dt.Rows[0]["CID"].ToString());
                            account.ARole = dt.Rows[0]["ARole"].ToString();

                            HttpContext.Session.SetInt32("AID", account.AID);
                            HttpContext.Session.SetString("AName", account.AName);
                            HttpContext.Session.SetString("APass", account.APass);
                            HttpContext.Session.SetInt32("EID", int.Parse(account.EID.ToString() == "" ? "0" :account.EID.ToString()));
                            HttpContext.Session.SetInt32("CID", int.Parse(account.CID.ToString() == "" ? "0" : account.CID.ToString()));
                            HttpContext.Session.SetString("ARole", account.ARole);

                            return new JsonResult(new { code = 200, message = "Success", res = account });
                        }
                        else
                        {
                            return new JsonResult(new { code = 400, message = "undefine account"});
                        }
                    }
                }
            } 
        }
        public IActionResult RegisterPage() {
            return View();
        }
        [HttpPost]
        public IActionResult CheckAccount(string AName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select * from tblA01 where AName ='" + AName + "'";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            return new JsonResult(new { code = 200, message = "Existed Account"});
                        }
                        else
                        {
                            return new JsonResult(new { code = 201, message = "UnAvailable Account" });
                        }
                    }
                }
            }
        }
        [HttpPost]
        public IActionResult CreateAccount([FromBody] RegisterModel registerModel) 
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_createEmployee";
                    cmd.Parameters.AddWithValue("@EName", registerModel.Employee.EName);
                    cmd.Parameters.AddWithValue("@EAddress", registerModel.Employee.EAddress);
                    cmd.Parameters.AddWithValue("@EPhone", registerModel.Employee.EPhone);
                    cmd.Parameters.AddWithValue("@EDOB", registerModel.Employee.EDOB);
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        using (SqlCommand cmd1 = new SqlCommand())
                        {
                            cmd1.Connection = con;
                            cmd1.CommandType = CommandType.Text;
                            cmd1.CommandText = "select * from tblE01 where EID = (select max(EID) from tblE01)";
                            
                            using (SqlDataReader reader = cmd1.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    registerModel.Account.EID = int.Parse(String.IsNullOrEmpty(reader[0].ToString())? "0": reader[0].ToString());
                                }
                            }
                        }
                        using (SqlCommand cmd2 = new SqlCommand())
                        {
                            cmd2.Connection = con;
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.CommandText = "sp_createAccount";
                            cmd2.Parameters.AddWithValue("@AName", registerModel.Account.AName);
                            cmd2.Parameters.AddWithValue("@APass", registerModel.Account.APass);
                            cmd2.Parameters.AddWithValue("@EID", registerModel.Account.EID);
                            cmd2.Parameters.AddWithValue("@CID", registerModel.Account.CID);
                            cmd2.Parameters.AddWithValue("@ARole", registerModel.Account.ARole);
                            int res1 = cmd2.ExecuteNonQuery();
                            if(res1 > 0)
                            {
                                return Json(new { code = 200, message = "Tạo tài khoản thành công" });
                            }
                            else
                            {
                                return Json(new { code = 401, message = "Tạo tài khoản thất bại" });
                            }
                            
                        }
                    }
                    else
                    {
                        return Json(new { code = 400, message = "Tạo người dùng thất bại" });
                    }
                }
            }
            //return View();
        }
    }
}
