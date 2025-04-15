using DATN01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace DATN01.Data
{
    public class SessionFilter : ActionFilterAttribute
    {
        //private readonly AccountModel _accountModel;
        private readonly string _requiredRole;
        public SessionFilter(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("ARole");
            var username = context.HttpContext.Session.GetString("AName");

            if (string.IsNullOrEmpty(username))
            {
                // Người dùng chưa đăng nhập
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            if (!string.IsNullOrEmpty(_requiredRole) && role != _requiredRole)
            {
                // Người dùng đã đăng nhập nhưng không có quyền (role không khớp)
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                return;
            }

            base.OnActionExecuting(context);
        }

        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    var sessionObj = context.HttpContext.Session.GetString("ARole");
        //    if (String.IsNullOrEmpty(sessionObj))
        //    {
        //        context.Result = new RedirectToActionResult("Index", "Login",null);
        //        return;
        //    }
        //    var loginDetail = JsonConvert.DeserializeObject<AccountModel>(sessionObj);
        //    if(loginDetail.ARole != Arole)
        //    {
        //        context.Result = new RedirectToActionResult("Error", "Share", null);
        //        return;
        //    }
        //    //throw new NotImplementedException();
        //}
    }
}
