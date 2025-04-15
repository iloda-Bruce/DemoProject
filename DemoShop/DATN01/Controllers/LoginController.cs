using DATN01.Models;
using DATN01.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Data;
using System.Text.Json.Serialization;

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
    }
}
