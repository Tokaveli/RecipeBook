using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MyProject.Models;

namespace MyProject.ViewModels
{
    public class CategoryRecipesGroup
    {
        public Category categories { get; set; }

        public int RecipesCount { get; set; }
    }
}



