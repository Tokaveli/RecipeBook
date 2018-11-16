using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class Rating
    {
        public int ID { get; set; }

        [Display(Name = "Ocena")]
        public int Rate { get; set; }
        [Display(Name = "Nazwa przepisu")]
        public int RecipeID { get; set; }
        public int ProfileID { get; set; }

        [Display(Name = "Nazwa przepisu")]
        public virtual Recipe Recipe { get; set; }
        [Display(Name = "Login")]
        public virtual Profile Profile { get; set; }

    }
}