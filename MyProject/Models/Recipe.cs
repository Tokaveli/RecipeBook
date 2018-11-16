using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class Recipe
    {
        public int ID { get; set; }

        [Required( ErrorMessage = "Nazwa musi składać się z conajmnij 1 znaku.")]
        [StringLength(50, ErrorMessage = "Nazwa nie może zawierać więcej niż 50 znaków.")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole 'Opis' jest wymagane.")]
        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole 'Składniki' jest wymagane.")]
        [Display(Name = "Składniki")]
        [DataType(DataType.MultilineText)]
        public string Components { get; set; }

        public int CategoryID { get; set; }

        public int TypeID { get; set; }
        public int ProfileID { get; set; }

        [Display(Name = "Obraz")]
        public string Picture { get; set; }


        [Display(Name = "Ocena")]
        public virtual ICollection<Rating> Ratings { get; set; }

        [Display(Name = "Komentarze")]
        public virtual ICollection<Comment> Comments { get; set; }

        [Display(Name = "Pora dnia")]
        public virtual ICollection<DayType> DayTypes { get; set; }
  
        public virtual Category Categories { get; set; }
        public virtual Type Types { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual ICollection<RecipeLike> Likes { get; set; }
    }
}