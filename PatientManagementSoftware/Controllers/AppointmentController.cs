using PatientManagementSoftware.DAL;
using PatientManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PatientManagementSoftware.Controllers
{
    public class AppointmentController : Controller
    {

        DataAccessLayer dal = new DataAccessLayer();
        // GET: Appointment

        public List<DoctorsViewModel> DoctorDDL()
        {
            List<DoctorsViewModel> doctorList = new List<DoctorsViewModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@Action", "select")
            };

            DataTable doctordt = dal.ExecuteStoredProcedure("ManageDoctorsDML", parameters);

            foreach (DataRow dr in doctordt.Rows)
            {
                doctorList.Add(new DoctorsViewModel
                {
                    DoctorID = Convert.ToInt32(dr["DoctorID"]),
                    Name = dr["Name"].ToString(),
                });
            }
            return doctorList;
        }


        public List<PatientViewModel> PatientDDL()
        {
            List<PatientViewModel> patientList = new List<PatientViewModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@Action", "select")
            };

            DataTable patientdt = dal.ExecuteStoredProcedure("ManagePatientsDML", parameters);

            foreach (DataRow dr in patientdt.Rows)
            {
                patientList.Add(new PatientViewModel
                {
                    PatientID = Convert.ToInt32(dr["PatientID"]),
                    Name = dr["Name"].ToString(),
                });
            }
            return patientList;
        }

        public ActionResult Index()
        {
            dal = new DataAccessLayer();

            //Geting Dropdownlist Data
            ViewBag.DoctorList = DoctorDDL();

            ViewBag.PatientList = PatientDDL();

            string query = "ManageAppointmentsDML";

            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@Action", "select")
            };

            DataTable dt = dal.ExecuteStoredProcedure(query, parameters);

            List<AppointmentsViewModel> AppointmentList = new List<AppointmentsViewModel>();

            foreach (DataRow dr in dt.Rows)
            {
                AppointmentsViewModel model = new AppointmentsViewModel()
                {
                    AppointmentID = Convert.ToInt32(dr["AppointmentID"]),
                    PatientName = dr["PatientName"].ToString(),
                    DoctorName = dr["DoctorName"].ToString(),
                    AppointmentDateTime = Convert.ToDateTime(dr["AppointmentDateTime"]),
                    Reason = dr["Reason"].ToString(),
                    Status = dr["Status"].ToString()
                };
                AppointmentList.Add(model);
            }
            ViewBag.list = AppointmentList;
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAppointment(AppointmentsViewModel model)
        {
            if (ModelState.IsValid)
            {
                dal = new DataAccessLayer();

                string query = "ManageAppointmentsDML";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Action","insert"),
                    new SqlParameter("@AppointmentID",model.AppointmentID),
                    new SqlParameter("@PatientID",model.PatientID),
                    new SqlParameter("@DoctorID",model.DoctorID),
                    new SqlParameter("@AppointmentDateTime",model.AppointmentDateTime),
                    new SqlParameter("@Reason",model.Reason),
                    new SqlParameter("@Status",model.Status)
                };

                DataTable dt = dal.ExecuteStoredProcedure(query, parameters);
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id, AppointmentsViewModel model)
        {
            dal = new DataAccessLayer();

            string query = "GetDataByID";

            ViewBag.PatientList = PatientDDL();
            ViewBag.DoctorList = DoctorDDL();

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter( "@ChooseTable", "appointment"),
                new SqlParameter ("@AppointmentID",id)
            };

            DataTable getDoctor = dal.ExecuteStoredProcedure(query, sqlParameter);

            foreach (DataRow row in getDoctor.Rows)
            {
                model = new AppointmentsViewModel();
                {
                    model.AppointmentID = Convert.ToInt32(row["AppointmentID"]);
                    model.PatientID = Convert.ToInt32(row["PatientID"]);
                    model.DoctorID = Convert.ToInt32(row["DoctorID"]);
                    model.AppointmentDateTime = Convert.ToDateTime(row["AppointmentDateTime"]);
                    model.Reason = row["Reason"].ToString();
                    model.Status = row["Status"].ToString();
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateAppointment(AppointmentsViewModel model)
        {
            if (ModelState.IsValid)
            {
                dal = new DataAccessLayer();

                string query = "ManageAppointmentsDML";

                SqlParameter[] parameter = new SqlParameter[]
                {
                new SqlParameter("@Action","update"),
                new SqlParameter("@AppointmentID",model.AppointmentID),
                new SqlParameter("@PatientID",model.PatientID),
                new SqlParameter("@DoctorID",model.DoctorID),
                new SqlParameter("@AppointmentDateTime",model.AppointmentDateTime),
                new SqlParameter("@Reason",model.Reason),
                new SqlParameter("@Status",model.Status)
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

            string query = "ManageAppointmentsDML";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter ("@Action","delete"),
                new SqlParameter ("@AppointmentID",id)
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