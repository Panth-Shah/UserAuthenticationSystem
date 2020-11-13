using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationUserAuthSystem.Models;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Web.Routing;

namespace ApplicationUserAuthSystem.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HandleIndexRequest(string request)
        {
            switch(request)
            {
                case ("LogIn"):
                    return RedirectToAction("LogIn");
                case ("Register"):
                    return RedirectToAction("Register", "User");
            }
            return RedirectToAction("Index");
        }
        //GET: LogIn
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }


        //POST: UserLogIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (UserRegistrationDBEntities _db = new UserRegistrationDBEntities())
            {
                var data = _db.ApplicationUsers.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
                if (data != null)
                {
                    if (string.Compare(GetHash.GetHashForString(login.Password), data.Password) == 0)
                    {
                        var ticket = new FormsAuthenticationTicket(login.EmailID, true, 10);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(10);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                        Session["FullName"] = data.UserFirstName + " " + data.UserFamilyName;
                        Session["Email"] = data.EmailID;
                        Session["idUser"] = data.ApplicationUserId;

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("ViewUserInformation", new RouteValueDictionary(
                                        new { controller = "User", action = "UserData", Id = data.ApplicationUserId}));
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }
    }
}