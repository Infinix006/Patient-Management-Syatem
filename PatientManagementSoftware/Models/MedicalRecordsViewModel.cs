using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientManagementSoftware.Models
{
    public class MedicalRecordsViewModel
    {


        public int Id { get; set; }
        public int PatientID { get; set; }

        public string PatientNAme { get; set; }

        public int DoctorID { get; set; }

        public string DoctorName { get; set; }

        public string Diagnosis { get; set; }

        public string Treatment { get; set; }

        public string Medications { get; set; }

        public string TestResults { get; set; }

    }
}