using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class RecipeLike
    {

        public int ID { get; set; }

        public int RecipeID { get; set; }

        public int ProfileID { get; set; }

        public virtual Recipe Recipe { get; set; }

        public virtual Profile Profile { get; set; }
    }

}


