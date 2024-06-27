using PatientManagementSoftware.DAL;
using PatientManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace PatientManagementSoftware.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        DataAccessLayer dal;

        public List<DoctorsViewModel> DoctorsList()
        {
            dal = new DataAccessLayer();

            List<DoctorsViewModel> doctorsList = new List<DoctorsViewModel>();

            string query = "ManageDoctorsDML";
            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@Action", "select")
            };

            DataTable dataTable = dal.ExecuteStoredProcedure(query, parameters);

            foreach (DataRow data in dataTable.Rows)
            {
                DoctorsViewModel doctor = new DoctorsViewModel
                {
                    DoctorID = Convert.ToInt32(data["DoctorID"]),
                    Name = data["Name"].ToString(),
                    Specialization = data["Specialization"].ToString(),
                    ContactNumber = data["ContactNumber"].ToString(),
                    Availability = data["Availability"].ToString(),
                };
                doctorsList.Add(doctor);
            }

            return doctorsList;
        }


        public ActionResult Index()
        {
            ViewBag.list = DoctorsList();
            return View();
        }

        [HttpPost]
        public ActionResult SaveDoctor(DoctorsViewModel model)
        {
            if (ModelState.IsValid)
            {
                dal = new DataAccessLayer();

                string query = "ManageDoctorsDML";

                // Prepare parameters for the stored procedure
                SqlParameter[] parameters = new SqlParameter[]
                {
             new SqlParameter("@Name", model.Name),
             new SqlParameter("@Specialization", model.Specialization),
             new SqlParameter("@ContactNumber", model.ContactNumber),
             new SqlParameter("@Availability", model.Availability),
              new SqlParameter("@Action", "Insert")
                };

                // Call the ExecuteStoredProcedure method with the insert action
                DataTable result = dal.ExecuteStoredProcedure(query, parameters);

                // You can handle the result here if needed

                // Redirect to the Index action
                return RedirectToAction("Index");
            }

            // If the model state is not valid, return the view with validation errors
            return View(model);
        }


        public ActionResult Edit(int  id, DoctorsViewModel model)
        {
            ViewBag.list = DoctorsList();

            dal = new DataAccessLayer();

            string query = "GetDataByID";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter( "@ChooseTable", "doctor"),
                new SqlParameter ("@DoctorID",id)
            };

            DataTable getDoctor = dal.ExecuteStoredProcedure(query, sqlParameter);

            foreach (DataRow row in getDoctor.Rows)
            {
                model = new DoctorsViewModel();
                {
                    model.DoctorID = Convert.ToInt32(row["DoctorID"]);
                    model.Name = row["Name"].ToString();
                    model.Specialization = row["Specialization"].ToString();
                    model.ContactNumber = row["ContactNumber"].ToString();
                    model.Availability = row["Availability"].ToString();
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateDoctor(DoctorsViewModel model)
        {
             if (ModelState.IsValid)
             {
                dal = new DataAccessLayer();

                string query = "ManageDoctorsDML";

                SqlParameter[] parameter = new SqlParameter[]
                {
                new SqlParameter("@Action","update"),
                new SqlParameter("@DoctorID",model.DoctorID),
                new SqlParameter("@Name",model.Name),
                new SqlParameter("@Specialization",model.Specialization),
                new SqlParameter("@ContactNumber",model.ContactNumber),
                new SqlParameter("@Availability",model.Availability)
                };

                DataTable doctor = dal.ExecuteStoredProcedure(query, parameter);

                return RedirectToAction("Index");
             }

            return View(model);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            dal = new DataAccessLayer();

            string query = "ManageDoctorsDML";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter ("@Action","delete"),
                new SqlParameter ("@DoctorID",id)
            };

            DataTable delete = dal.ExecuteStoredProcedure(query, sqlParameter);

            if (delete == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

    }
}