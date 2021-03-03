using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IwwageNationalMIS.Model
{
    public class ValidateLogin
    {
        [Display(Name = "Login ID")]
        [Required(ErrorMessage = "Enter login id")]
        public string userName { set; get; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter password")]
        public string password { set; get; }
    }
}
