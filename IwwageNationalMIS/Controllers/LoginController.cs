using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IwwageNationalMIS.Services.IServices;
using System.Data;
using IwwageNationalMIS.Model;

namespace IwwageNationalMIS.Web.Controllers
{
    public class LoginController : Controller
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ILoginService loginService;
        private readonly IMenuService menuService;

        public LoginController(ILoginService loginService, IMenuService menuService)
        {
            this.loginService = loginService;
            this.menuService = menuService;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ValidateLogin validateLogin)
        {
            log.Info("LoginController/Index");
            if (ModelState.IsValid)
            {
                ViewBag.BrowserTestMsg = "";
                bool isAuthenticated = false;
                UserDetail uDetail = new UserDetail();
                string currentPageUrl = Request.Url.Host.Split('.')[0].ToString().ToLower();
                log.Info("currentPageUrl - " + currentPageUrl);

                isAuthenticated = loginService.AuthenticateUser(validateLogin, out uDetail);
                if (isAuthenticated)
                {
                    Session["User"] = uDetail;
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.LoginMessage = "Invalid credential!";
                    return View(validateLogin);
                }
            }
            else
            {               
                return View(validateLogin);
            }

        }
        public ActionResult LogOut()
        {
            UserDetail userDetail = (UserDetail)Session["User"];

            if (userDetail != null)
            {
                System.Web.Security.FormsAuthentication.SignOut();
                Session.Abandon();
            }

            log.Info("LoginController/LogOff");
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult _Menu()
        {
            List<UserMenu> userMenu = new List<UserMenu>();
            UserDetail uDetail = new UserDetail();
            if (Session["User"] != null)
            {
                uDetail = (UserDetail)Session["User"];
            }
            userMenu = menuService.GetMenuByUser(uDetail.userId);
            return PartialView(userMenu);
        }
    }
}