using ApplicationUserAuthSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;
using System.Data.Entity;
using System.Net;

namespace ApplicationUserAuthSystem.Controllers
{
    public class UserController : Controller
    {

        private UserRegistrationDBEntities _dbContext = new UserRegistrationDBEntities();

        //GET: Register
        public ActionResult Register()
        {
            return View();
        }

        //POST: Resgister
        [HttpPost]
        public ActionResult Register(ApplicationUser _user)
        {

            if (ModelState.IsValid)
            {
                using (UserRegistrationDBEntities _db = new UserRegistrationDBEntities())
                {
                    var userCheck = _db.ApplicationUsers.FirstOrDefault(x => x.EmailID == _user.EmailID);
                    ApplicationUser userData = new ApplicationUser();
                    if (userCheck == null)
                    {
                        _user.Password = GetHash.GetHashForString(_user.Password);
                        _dbContext.Configuration.ValidateOnSaveEnabled = false;
                        var storeData = new ApplicationUser()
                        {
                            UserFirstName = _user.UserFirstName,
                            UserFamilyName = _user.UserFamilyName,
                            EmailID = _user.EmailID,
                            Address1 = _user.Address1,
                            Address2 = _user.Address2,
                            Address3 = _user.Address3,
                            Password = _user.Password,
                            City = _user.City,
                            State = _user.State,
                            ZipCode = _user.ZipCode
                        };
                        _dbContext.ApplicationUsers.Add(storeData);
                        _dbContext.SaveChanges();
                        return RedirectToAction("ViewUserInformation", new RouteValueDictionary(
                                                                new { controller = "User", action = "UserData", Id = storeData.ApplicationUserId}));
                    }
                    else
                    {
                        ViewBag.error = "Email already exists";
                        return View();
                    }
                }
            }
            return View();
        }

        public ActionResult ViewUserInformation(RouteValueDictionary ReturnUrl)
        {
            UserInformationViewEdit userData = new UserInformationViewEdit();

            using (UserRegistrationDBEntities _db = new UserRegistrationDBEntities())
            {
                var userId = Convert.ToInt32(ReturnUrl["Id"]);
                var queryResult = _db.ApplicationUsers.FirstOrDefault(a => a.ApplicationUserId == userId);
                userData.UserId = userId;
                userData.UserFirstName = queryResult.UserFirstName;
                userData.UserFamilyName = queryResult.UserFamilyName;
                userData.EmailID = queryResult.EmailID;
                userData.Address1 = queryResult.Address1;
                userData.Address2 = queryResult.Address2;
                userData.Address3 = queryResult.Address3;
                userData.City = queryResult.City;
                userData.State = queryResult.State;
                userData.ZipCode = queryResult.ZipCode;
            }
            return View(userData);
        }

        //POST: Resgister
        [HttpPost]
        public ActionResult ProcessEditRequest(int? ID,UserInformationViewEdit _user, string save, string edit)
        {
            if (!string.IsNullOrEmpty(edit))
            {
                return View("~/Views/User/EditUserInformation.cshtml", _user);
            }

            if (!string.IsNullOrEmpty(save))
            {
                if (_user != null && ModelState.IsValid)
                {
                    ApplicationUser userData = new ApplicationUser();

                    using (UserRegistrationDBEntities _db = new UserRegistrationDBEntities())
                    {
                        var queryResult = _db.ApplicationUsers.FirstOrDefault(a => a.ApplicationUserId == ID);
                        queryResult.UserFirstName = _user.UserFirstName;
                        queryResult.UserFamilyName = _user.UserFamilyName;
                        queryResult.Address1 = _user.Address1;
                        queryResult.Address2 = _user.Address2;
                        queryResult.Address3 = _user.Address3;
                        queryResult.City = _user.City;
                        queryResult.State = _user.State;
                        queryResult.ZipCode = _user.ZipCode;
                        _db.SaveChanges();
                    }
                    return View("~/Views/User/ViewUserInformation.cshtml", _user);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return View("~/Views/User/ViewUserInformation.cshtml", _user);
        }


        //LogOut
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn");
        }

    }
}