using PatientManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientManagementSoftware.DAL;

namespace PatientManagementSoftware.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        private bool AuthenticateUser(string username, string password)
        {
            // Call the stored procedure to authenticate user
            DataAccessLayer dal = new DataAccessLayer();
           
                string query = "SPLoginUser";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", password)
                };

                DataTable result = dal.ExecuteStoredProcedure(query, parameters);
                if (result.Rows.Count > 0)
                {
                return Convert.ToBoolean(result.Rows[0][0]);
                }
                return false;
        }
        // GET: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Call stored procedure to authenticate user
                bool isAuthenticated = AuthenticateUser(model.Username, model.Password);
                if (isAuthenticated)
                {
                    return RedirectToAction("Index", "Dashboard");
                    // Redirect to appropriate page based on user type
                    //if (model.SelectedType == "Admin")
                    //{
                    //    return RedirectToAction("AdminDashboard", "Dashboard");
                    //}
                    //else if (model.SelectedType == "Doctor")
                    //{
                    //    return RedirectToAction("DoctorDashboard", "Dashboard");
                    //}
                    // Add other types as needed

                    // If user type is not recognized, redirect to login page
                   // return RedirectToAction("Login");
                }
                else
                {
                    // If authentication fails, return to login page with error
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }

            }
            // If login fails or ModelState is not valid, return to login page with errors
            model.Types = GetTypesFromDatabase();
            return View(model);
        }
            private List<string> GetTypesFromDatabase()
        {
            // Method to fetch types from the database
            // Replace this with your actual implementation
            return new List<string> { "Doctor", "Staff", "Admin", "Patient" };
        }

        public ActionResult SignIn()
        {
            return View();
        }
    }
}