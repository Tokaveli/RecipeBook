using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MyProject.DAL
{
    public class EBookContext : DbContext

    {
        public EBookContext() : base("DefaultConnection")
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<DayType> DayTypes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RecipeLike> RecipeLikes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }


    }
}