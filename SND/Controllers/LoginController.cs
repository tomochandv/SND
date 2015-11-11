using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SND.Dac;
using SND.Models;

namespace SND.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public RedirectResult Login(string id = "", string pwd = "")
        {
            string url = "/Login/Index";
            LoginModel model = new Dac_User().UserSelectOne(id, pwd);
            if (model != null && model.email == id)
            {
                FormsAuthentication.SetAuthCookie(model.name, true);
                url = "/Home/Index";
            }
            return Redirect(url);
        }
        public RedirectResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Login/index");
        }

        public JsonResult Proc(string id = "", string pwd = "")
        {
            bool result = false;
            LoginModel model = new Dac_User().UserSelectOne(id, pwd);
            if (model != null && model.email == id)
            {
                FormsAuthentication.SetAuthCookie(model.name, true);
                result = true;
            }
            return Json(result);
        }

    }
}
