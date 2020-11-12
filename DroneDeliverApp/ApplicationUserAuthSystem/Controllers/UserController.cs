﻿using ApplicationUserAuthSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;
using System.Data.Entity;

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
            ApplicationUser userData = new ApplicationUser();

            using (UserRegistrationDBEntities _db = new UserRegistrationDBEntities())
            {
                var userId = Convert.ToInt32(ReturnUrl["Id"]);
                var queryResult = _db.ApplicationUsers.FirstOrDefault(a => a.ApplicationUserId == userId);
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
        public ActionResult Edit(ApplicationUser _editUser)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(_editUser).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("ViewUserInformation");
            }
            return View(_editUser);
        }


        //LogOut
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn");
        }

    }
}