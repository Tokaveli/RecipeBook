using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class DayType
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

    }
}