using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatientManagementSoftware.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Name Field is Required")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string SelectedType { get; set; }
        public List<string> Types { get; set; }
        public bool RememberMe { get; set; }
    }
}