using PatientManagementSoftware.DAL;
using PatientManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementSoftware.Controllers
{
    public class MedicalRecordsController : Controller
    {
        // GET: MedicalRecords

        DataAccessLayer dal;
        public ActionResult Index()
        {
            //Form deta
            AppointmentController ap = new AppointmentController();

            ViewBag.patientlist = ap.PatientDDL();
            ViewBag.doctorslist = ap.DoctorDDL();


            //Table data
            dal = new DataAccessLayer();
            List<MedicalRecordsViewModel> MedicalRecordsList = new List<MedicalRecordsViewModel>();

            string query = "SPMedicalRecordsDML";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Action","Select")
            };

            DataTable dt = dal.ExecuteStoredProcedure(query, sqlParameters);


            foreach (DataRow dr in dt.Rows)
            {
                MedicalRecordsViewModel medicalRecord = new MedicalRecordsViewModel()
                {
                    Id = Convert.ToInt32(dr["RecordID"]),
                    PatientNAme = dr["PatientName"].ToString(),
                    DoctorName = dr["DoctorName"].ToString(),
                    Diagnosis = dr["Diagnosis"].ToString(),
                    Treatment = dr["Treatment"].ToString(),
                    Medications = dr["Medications"].ToString()
                };
                MedicalRecordsList.Add(medicalRecord);
            }
            ViewBag.list = MedicalRecordsList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMedicalRecord(MedicalRecordsViewModel model)
        {
            dal = new DataAccessLayer();

            // Prepare parameters for the stored procedure
            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@PatientID", model.PatientID),
             new SqlParameter("@DoctorID", model.DoctorID),
             new SqlParameter("@Diagnosis", model.Diagnosis),
             new SqlParameter("@Treatment", model.Treatment),
             new SqlParameter("@Medications", model.Medications),
              new SqlParameter("@Action", "Insert")
            };

            // Call the ExecuteStoredProcedure method with the insert action
            DataTable result = dal.ExecuteStoredProcedure("SPMedicalRecordsDML", parameters);

            // You can handle the result here if needed

            // Redirect to the Index action
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id, MedicalRecordsViewModel model)
        {
            AppointmentController ap = new AppointmentController();

            ViewBag.patientlist = ap.PatientDDL();
            ViewBag.doctorslist = ap.DoctorDDL();
            dal = new DataAccessLayer();

            string query = "GetDataByID";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter( "@ChooseTable", "records"),
                new SqlParameter ("@RecordId",id)
            };

            DataTable getMedicalRecords = dal.ExecuteStoredProcedure(query, sqlParameter);

            foreach (DataRow row in getMedicalRecords.Rows)
            {
                model = new MedicalRecordsViewModel();
                {
                    model.Id = Convert.ToInt32(row["RecordID"]);
                    model.PatientID = Convert.ToInt32(row["PatientID"]);
                    model.DoctorID = Convert.ToInt32(row["DoctorID"]);
                    model.Diagnosis = row["Diagnosis"].ToString();
                    model.Treatment = row["Treatment"].ToString();
                    model.Medications = row["Medications"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateMedicalRecord(MedicalRecordsViewModel model)
        {
            dal = new DataAccessLayer();

            string query = "SPMedicalRecordsDML";

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@Action","update"),
                new SqlParameter("@RecordID",model.Id),
                new SqlParameter("@PatientID",model.PatientID),
                new SqlParameter("@DoctorID",model.DoctorID),
                new SqlParameter("@Diagnosis",model.Diagnosis),
                new SqlParameter("@Treatment",model.Treatment),
                new SqlParameter("@Medications",model.Medications)
            };

            DataTable doctor = dal.ExecuteStoredProcedure(query, parameter);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            dal = new DataAccessLayer();

            string query = "SPMedicalRecordsDML";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter ("@Action","Delete"),
                new SqlParameter ("@RecordID",id)
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