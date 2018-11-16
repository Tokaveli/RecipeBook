using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class Type
    {
       
        public int ID { get; set; }

        [Display(Name = "Pora dnia")]
        [Required(ErrorMessage = "Pole 'Pora dnia' musi składać się z conajmnij 2 znaków.")]
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}