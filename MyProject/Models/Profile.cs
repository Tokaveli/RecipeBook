using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class Profile
    {
        public int ID { get; set; }

        [Display(Name = "Login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Imie musi składać się z conajmnij 2 znaków.")]
        [Display(Name = "Imie")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nazwisko musi składać się z conajmnij 2 znaków.")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Pole wiek jest wymagane.")]
        [Display(Name = "Wiek")]
        public int Age { get; set; }
        [Display(Name = "Płeć")]
        public bool IsMale { get; set; }


    }

}