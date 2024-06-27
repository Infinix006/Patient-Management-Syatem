using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientManagementSoftware.Models
{
    public class PatientViewModel
    {

        public int PatientID { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        public string Gender { get; set; }

    }
}