using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PASSForm_BPS.Models;
using System.Data;

namespace PASSForm_BPS.Controllers
{
    public class LoginController : Controller
    {
        private readonly PassDbContext _passDbContext;

        public LoginController(PassDbContext passDbContext)
        {
            _passDbContext = passDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        //first

        [HttpGet]
        public IActionResult Login()
        {

            if (HttpContext.Session.GetString("EmpIdbps") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

            //return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {

            if (ModelState.IsValid)
            {
                var user = _passDbContext.Users.FirstOrDefault(u => u.UserEmail == model.UserEmail);
                var userWithDesignation = _passDbContext.Users
    .Where(u => u.UserEmail == model.UserEmail)
    .Join(
        _passDbContext.Roles,
        user => user.RoleId,
        role => role.RoleId,
        (user, role) => new
        {
            User = user,
            RoleName = role.RoleName
        }
    )
    .FirstOrDefault();

                if (user != null)
                {
                    if (user.IsActive != false)
                    {
                        if (user.RoleId! == 1)
                        {

                            HttpContext.Session.SetString("EmpIdbps", userWithDesignation.User.EmpId.ToString());
                            HttpContext.Session.SetString("roleid", userWithDesignation.User.RoleId.ToString());
                           HttpContext.Session.SetString("uname", userWithDesignation.User.UserName.ToString());
                            HttpContext.Session.SetString("rolename", userWithDesignation.RoleName.ToString());


                            Response.Cookies.Append("roleid", userWithDesignation.User.RoleId.ToString(), new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(7), // Set the expiration date (optional)
                                Path = "/", // Set the cookie path (optional)
                                Domain = "192.168.10.30", // Set the cookie domain
                                Secure = true, // Make the cookie secure (optional)
                                HttpOnly = true // Make the cookie accessible only through HTTP (optional)
                            });


                            ViewBag.RoleId = "1";
                            //return View("_RolesBasePartialView");
                            // return RedirectToAction("PendingView", "ListView");

                            return RedirectToAction("ApprovedView", "ListView");

                        }
                        //if (user.RoleId! == 15)
                        //{

                        //    HttpContext.Session.SetString("EmpIdbps", user.EmpId.ToString());
                        //    HttpContext.Session.SetString("roleid", user.RoleId.ToString());
                        //    HttpContext.Session.SetString("uname", user.UserName.ToString());

                        //    Response.Cookies.Append("roleid", user.RoleId.ToString(), new CookieOptions
                        //    {
                        //        Expires = DateTime.Now.AddDays(7), // Set the expiration date (optional)
                        //        Path = "/", // Set the cookie path (optional)
                        //        Domain = "192.168.10.30", // Set the cookie domain
                        //        Secure = true, // Make the cookie secure (optional)
                        //        HttpOnly = true // Make the cookie accessible only through HTTP (optional)
                        //    });


                        //    ViewBag.RoleId = "1";

                        //    return RedirectToAction("ApprovedView", "ListView");

                        //}
                        //else if (user.RoleId! == 16)
                        //{
                        //    HttpContext.Session.SetString("EmpIdbps", user.EmpId.ToString());
                        //    HttpContext.Session.SetString("roleid", user.RoleId.ToString());
                        //    HttpContext.Session.SetString("uname", user.UserName.ToString());
                        //    Response.Cookies.Append("roleid", user.RoleId.ToString(), new CookieOptions
                        //    {
                        //        Expires = DateTime.Now.AddDays(7), // Set the expiration date (optional)
                        //        Path = "/", // Set the cookie path (optional)
                        //        Domain = "192.168.10.30", // Set the cookie domain
                        //        Secure = true, // Make the cookie secure (optional)
                        //        HttpOnly = true // Make the cookie accessible only through HTTP (optional)
                        //    });


                        //    ViewBag.RoleId = "2";

                        //    return RedirectToAction("PendingView", "ListView");

                        //}
                        else
                        {
                            HttpContext.Session.SetString("EmpIdbps", userWithDesignation.User.EmpId.ToString());
                            HttpContext.Session.SetString("roleid", userWithDesignation.User.RoleId.ToString());
                            HttpContext.Session.SetString("uname", userWithDesignation.User.UserName.ToString());
                            HttpContext.Session.SetString("rolename", userWithDesignation.RoleName.ToString());
                            Response.Cookies.Append("roleid", userWithDesignation.User.RoleId.ToString(), new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(7), // Set the expiration date (optional)
                                Path = "/", // Set the cookie path (optional)
                                Domain = "192.168.10.30", // Set the cookie domain
                                Secure = true, // Make the cookie secure (optional)
                                HttpOnly = true // Make the cookie accessible only through HTTP (optional)
                            });

                            ViewBag.RoleId = "2";
                            // return View("_RolesBasePartialView");
                            //return RedirectToAction("ApprovedView", "ListView");

                            return RedirectToAction("PendingView", "ListView");

                        }

                    }
                    else
                    {
                        ModelState.AddModelError("Invalid Login", "Invalid");
                        ViewBag.Message = "Login Failed";
                    }



                }



                return View("Login", model);
            }
            return View();
        }

        public IActionResult Logout()
        {
            var cookieName = "roleid";

            // Delete the cookie by setting its expiration date to a time in the past
            Response.Cookies.Delete(cookieName, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1),
                Path = "/"
                // Other options as needed
            });
            if (HttpContext.Session.GetString("EmpIdbps") != null)
            {
                HttpContext.Session.Remove("EmpIdbps");
                HttpContext.Session.Remove("uname");
                HttpContext.Session.Remove("roleid");
                return RedirectToAction("Login");
            }
            return View();
        }

    }
}
