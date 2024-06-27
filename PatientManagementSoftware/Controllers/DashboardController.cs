using PatientManagementSoftware.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementSoftware.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            int totalPatients, totalDoctors, totalAppointments, totalMedicalRecords;
            decimal totalBilling;
            DataAccessLayer dal = new DataAccessLayer();
            // Call the stored procedure and get output values
            string query = "SPDashboardData";
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@totalPatients", SqlDbType.Int) { Direction = ParameterDirection.Output },
                 new SqlParameter("@totalDoctors", SqlDbType.Int) { Direction = ParameterDirection.Output },
                 new SqlParameter("@totalAppointments", SqlDbType.Int) { Direction = ParameterDirection.Output },
                 new SqlParameter("@totalMedicalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output },
                 new SqlParameter("@totalBilling", SqlDbType.Money) { Direction = ParameterDirection.Output }
            };

            DataTable result = dal.ExecuteStoredProcedure(query, parameters);

            // Populate ViewBag with retrieved data
            // Retrieve output values
            totalPatients = Convert.ToInt32(parameters[0].Value);
            totalDoctors = Convert.ToInt32(parameters[1].Value);
            totalAppointments = Convert.ToInt32(parameters[2].Value);
            totalMedicalRecords = Convert.ToInt32(parameters[3].Value);
            totalBilling = Convert.ToDecimal(parameters[4].Value);

            // Pass the retrieved data to the view
            ViewBag.TotalPatients = totalPatients;
            ViewBag.TotalDoctors = totalDoctors;
            ViewBag.TotalAppointments = totalAppointments;
            ViewBag.TotalMedicalRecords = totalMedicalRecords;
            ViewBag.TotalBilling = totalBilling.ToString("0.00");


            return View();
        }

    }

}
