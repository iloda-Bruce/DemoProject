using DATN01.Areas.Admin.Models;
using DATN01.Data;
using DATN01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DATN01.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly string connectionString = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLBH2;Integrated Security=True;Trust Server Certificate=True";
        [SessionFilter("admin")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetCustomers() 
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select * from tblC01 join tblA01 on tblC01.CID = tblA01.CID";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            List<CustomerAccountModel> customerModels = new List<CustomerAccountModel>();
                            foreach (DataRow dr in dt.Rows) {
                                CustomerAccountModel customerAcc = new CustomerAccountModel();
                                customerAcc.CID = int.Parse(dr["CID"].ToString());
                                customerAcc.CName = dr["CName"].ToString();
                                customerAcc.CAddress = dr["CAddress"].ToString();
                                customerAcc.CPhone = dr["CPhone"].ToString(); ;
                                customerAcc.CDOB = DateTime.Parse(dr["CDOB"].ToString() == "" ? DateTime.Now.ToString() : dr["CDOB"].ToString());
                                customerAcc.AID = int.Parse(dr["AID"].ToString());
                                customerAcc.AName = dr["AName"].ToString();
                                customerAcc.APass = dr["APass"].ToString();
                                customerAcc.EID = int.Parse(string.IsNullOrEmpty(dr["EID"].ToString())?"0": dr["EID"].ToString());
                                customerAcc.ARole = dr["ARole"].ToString();
                                customerModels.Add(customerAcc);
                            }
                           
                            return Json(customerModels);
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
        public IActionResult GetEmployee(int CID) {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select * from tblC01 join tblA01 on tblC01.CID = tblA01.CID where tblC01.CID = '"+CID+"'";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            CustomerAccountModel model = new CustomerAccountModel();
                            while (dr.Read())
                            {
                                model.CID = int.Parse(dr.GetInt32("CID").ToString()==""?"0": dr.GetInt32("CID").ToString());
                                model.CName = dr.GetString("CName").ToString();
                                model.CAddress = dr.GetString("CAddress").ToString();
                                model.CPhone = dr.GetString("CPhone").ToString();
                                model.CDOB = DateTime.Parse(dr.GetDateTime("CDOB").ToString()==""?DateTime.Now.ToString(): dr.GetDateTime("CDOB").ToString());
                                model.AID = int.Parse(dr.GetInt32("AID").ToString()==""?"0": dr.GetInt32("AID").ToString());
                                model.AName = dr.GetString("AName").ToString();
                                model.APass = dr.GetString("APass").ToString();
                                model.EID = int.Parse(dr[8] == DBNull.Value ? "0":dr.GetInt32("EID").ToString());
                                model.ARole = dr.GetString("ARole").ToString();
                            }
                            return Json(model);
                        }
                        else
                        {
                            return Json(new { code = 400, message="Fail" });
                        }
                    }
                }
            }
        }
    }
}
