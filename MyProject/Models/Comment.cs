using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class Comment
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Pole zawartość jest wymagane.")]
        [Display(Name = "Zawartość")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public DateTime CommentDate { get; set; }
        public int RecipeID { get; set; }
        public int ProfileID { get; set; }
        

        public virtual Recipe Recipe { get; set; }
        public virtual Profile Profile{ get; set; }

    }
}