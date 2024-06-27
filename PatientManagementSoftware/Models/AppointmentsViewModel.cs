using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientManagementSoftware.Models
{
    public class AppointmentsViewModel
    {

        public int AppointmentID { get; set; }

        public int PatientID { get; set; }

        public string PatientName { get; set; }

        public int DoctorID { get; set; }

        public string DoctorName { get; set; }

        public DateTime AppointmentDateTime { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }




    }
}