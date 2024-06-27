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
    public class InvoicingController : Controller
    {
        // GET: Invoicing
        DataAccessLayer dal = new DataAccessLayer();
        AppointmentController appointmentController = new AppointmentController();


        public ActionResult Index()
        {
            dal = new DataAccessLayer();
            List<BillingViewModel> BillingList = new List<BillingViewModel>();

            //Geting Dropdownlist Data

            ViewBag.PatientList = appointmentController.PatientDDL();

            string query = "ManageInvoiceDML";

            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@Action", "select")
            };

            DataTable dt = dal.ExecuteStoredProcedure(query, parameters);


            foreach (DataRow dr in dt.Rows)
            {
                BillingViewModel model = new BillingViewModel()
                {
                    BillID = Convert.ToInt32(dr["BillID"]),
                    PatientID = Convert.ToInt32(dr["PatientID"]),
                    PatientName = dr["PatientName"].ToString(),
                    BillDate = Convert.ToDateTime(dr["BillDate"]),
                    Amount = Convert.ToDecimal(dr["Amount"].ToString()),
                    PaymentStatus = dr["PaymentStatus"].ToString()
                };
                BillingList.Add(model);
            }
            ViewBag.list = BillingList;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInvoice(BillingViewModel model)
        {
            if (ModelState.IsValid)
            {
                dal = new DataAccessLayer();

                ViewBag.PatientList = appointmentController.PatientDDL();

                string query = "ManageInvoiceDML";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Action","insert"),
                    new SqlParameter("@PatientID",model.PatientID),
                    new SqlParameter("@BillDate",model.BillDate),
                    new SqlParameter("@Amount",model.Amount),
                    new SqlParameter("@PaymentStatus",model.PaymentStatus)
                };

                DataTable dt = dal.ExecuteStoredProcedure(query, parameters);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            dal = new DataAccessLayer();
            BillingViewModel model = new BillingViewModel();

            string query = "GetDataByID";

            ViewBag.PatientList = appointmentController.PatientDDL();

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter( "@ChooseTable", "billing"),
                new SqlParameter ("@BillingID",id)
            };

            DataTable getDoctor = dal.ExecuteStoredProcedure(query, sqlParameter);

            foreach (DataRow row in getDoctor.Rows)
            {
                model = new BillingViewModel();
                {
                    model.BillID = Convert.ToInt32(row["BillId"]);
                    model.PatientID = Convert.ToInt32(row["PatientID"]);
                    model.BillDate = Convert.ToDateTime(row["BillDate"]);
                    model.Amount = Convert.ToDecimal(row["Amount"]);
                    model.PaymentStatus = row["PaymentStatus"].ToString();
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateInvoice(BillingViewModel model)
        {

            dal = new DataAccessLayer();

            string query = "ManageInvoiceDML";

            SqlParameter[] parameter = new SqlParameter[]
            {
            new SqlParameter("@Action","update"),
            new SqlParameter("@BillID",model.BillID),
            new SqlParameter("@PatientID",model.PatientID),
            new SqlParameter("@BillDate", model.BillDate),
            new SqlParameter("@Amount",model.Amount),
            new SqlParameter("@PaymentStatus",model.PaymentStatus)
            };

            DataTable doctor = dal.ExecuteStoredProcedure(query, parameter);

            return RedirectToAction("Index");

        }

        public ActionResult PrintInvoice(int billId)
        {
            dal = new DataAccessLayer();

            BillingViewModel model = new BillingViewModel();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@BillID", billId)
            };

            DataTable patientdt = dal.ExecuteStoredProcedure("GetInvoiceByPatient", parameters);

            foreach (DataRow dr in patientdt.Rows)
            {
                model.BillID = Convert.ToInt32(dr["BillID"]);
                model.BillDate = Convert.ToDateTime(dr["BillDate"].ToString());
                model.Amount = Convert.ToDecimal(dr["Amount"].ToString());
                model.PatientID = Convert.ToInt32(dr["PatientID"].ToString());
                model.PatientName = dr["PatientName"].ToString();
                model.PaymentStatus = dr["PaymentStatus"].ToString();
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            dal = new DataAccessLayer();

            string query = "ManageInvoiceDML";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter ("@Action","delete"),
                new SqlParameter ("@BillID",id)
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