using eCommerceApp.Hosts.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Hosts.Controllers
{
    public class LoginController : Controller
    {
        private readonly IManageAccount manageAccount;
        private LoginController(IManageAccount manageAccount)
        {
            this.manageAccount = manageAccount;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
