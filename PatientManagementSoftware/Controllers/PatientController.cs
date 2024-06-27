using PatientManagementSoftware.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using PatientManagementSoftware.Models;

namespace PatientManagementSoftware.Controllers
{
    public class PatientController : Controller
    {

        // GET: Patient

        DataAccessLayer dal;


        public ActionResult Index()
        {
            dal = new DataAccessLayer();

            string query = "ManagePatientsDML";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter
                ("@Action","select")
            };

            DataTable dataTable = dal.ExecuteStoredProcedure(query, parameters);

            List<PatientViewModel> patientList = new List<PatientViewModel>();

            foreach (DataRow row in dataTable.Rows)
            {
                PatientViewModel patientViewModel = new PatientViewModel()
                {
                    PatientID = Convert.ToInt32(row["PatientID"]),
                    Name = row["Name"].ToString(),
                    DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
                    Address = row["Address"].ToString(),
                    ContactNumber = row["ContactNumber"].ToString(),
                    Gender = row["Gender"].ToString()
                };
                patientList.Add(patientViewModel);
            }
            ViewBag.list = patientList;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePatient(PatientViewModel model)
        {
            {
                dal = new DataAccessLayer();

                string query = "ManagePatientsDML";

                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@Action","insert"),
                new SqlParameter("@Name",model.Name),
                new SqlParameter("@DateOfBirth",model.DateOfBirth),
                new SqlParameter("@Address",model.Address),
                new SqlParameter("@ContactNumber",model.ContactNumber),
                new SqlParameter("@Gender",model.Gender)
                };

                DataTable dataTable = dal.ExecuteStoredProcedure(query, parameters);

                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public ActionResult Edit(int id, PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                dal = new DataAccessLayer();

                string query = "GetDataByID";

                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                new SqlParameter( "@ChooseTable", "patient"),
                new SqlParameter ("@PatientID",id)
                };

                DataTable getDoctor = dal.ExecuteStoredProcedure(query, sqlParameter);

                foreach (DataRow row in getDoctor.Rows)
                {
                    model = new PatientViewModel();
                    {
                        model.PatientID = Convert.ToInt32(row["PatientID"]);
                        model.Name = row["Name"].ToString();
                        model.DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]).Date;
                        model.Address = row["Address"].ToString();
                        model.ContactNumber = row["ContactNumber"].ToString();
                        model.Gender = row["Gender"].ToString();
                    }
                } 
            }
            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePatient(PatientViewModel model)
        {
           
                dal = new DataAccessLayer();

                string query = "ManagePatientsDML";

                SqlParameter[] parameter = new SqlParameter[]
                {
                new SqlParameter("@Action","update"),
                new SqlParameter("@PatientID",model.PatientID),
                new SqlParameter("@Name",model.Name),
                new SqlParameter("@DateOfBirth",model.DateOfBirth),
                new SqlParameter("@Address",model.Address),
                new SqlParameter("@ContactNumber",model.ContactNumber),
                new SqlParameter("@Gender",model.Gender)
                };

                DataTable patient = dal.ExecuteStoredProcedure(query, parameter);

                return RedirectToAction("Index");

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            dal = new DataAccessLayer();

            string query = "ManagePatientsDML";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter ("@Action","delete"),
                new SqlParameter ("@PatientID",id)
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