using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Display(Name = "Kategoria")]
        [Required(ErrorMessage = "Pole 'Kategoria' musi składać się z conajmnij 2 znaków.")]
        public string Name { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
       

    }
}