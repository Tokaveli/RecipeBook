using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyProject.DAL
{
    public class EBookInitializer : DropCreateDatabaseIfModelChanges<EBookContext>
    {

        protected override void Seed(EBookContext context)
        {

            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));

            roleManager.Create(new IdentityRole("Administrator"));

            var user = new ApplicationUser { UserName = "zoska@gmail.com"};
            string pass = "1Haslo.!";
            userManager.Create(user, pass);
            userManager.AddToRole(user.Id, "Administrator");


            userManager.Create(new ApplicationUser { UserName = "bogus@gmail.com" }, "1Haslo.!");
            userManager.Create(new ApplicationUser { UserName = "piotr@gmail.com" }, "1Haslo.!");




            var profiles = new List<Profile>
            {
                new Profile {FirstName="Bogdan", LastName="Linda", Age=50, IsMale=true, Login="bogus@gmail.com"},
                new Profile {FirstName="Zosia", LastName="Samosia", Age=25, IsMale=false, Login="zoska@gmail.com"},
                new Profile {FirstName="Piotr", LastName="Smialy", Age=23, IsMale=true, Login="piotr@gmail.com"}
            };

            profiles.ForEach(p => context.Profiles.Add(p));
            context.SaveChanges();


            var categories = new List<Category>
            {
                new Category {Name="Miesa"},
                new Category {Name="Warzywa"},
                new Category {Name="Ryz"},
                new Category {Name="Makarony"},
                new Category {Name="Jajka"},
                new Category {Name="Nabiał"}
            };

            categories.ForEach(p => context.Categories.Add(p));
            context.SaveChanges();

            var types = new List<Models.Type>
            {
                new Models.Type {Name="Sniadanie"},
                new Models.Type {Name="Obiad"},           
                new Models.Type {Name="Lunch"},
                new Models.Type {Name="Kolacja"}
            };

            types.ForEach(p => context.Types.Add(p));
            context.SaveChanges();


            var dayTypes = new List<DayType>
            {
                new DayType {Name="Sniadanie"},
                new DayType {Name="Obiad"},
                new DayType {Name="Lunch"},
                new DayType {Name="Kolacja"}
            };

            dayTypes.ForEach(p => context.DayTypes.Add(p));
            context.SaveChanges();


            var recipes = new List<Recipe>
                        {
                new Recipe {Name = "Spaghetti Napoli", Description = "Na patelni rozgrzać olej i podsmażyć cebulę z czosnkiem. Dodać mięso, rozdrobnić drewnianą łyżką i zrumienić. Osączyć z nadmiaru tłuszczu i dolać wino. Dusić przez 5 minut. Dodać paprykę, cukinię i zioła. Dusić przez kolejne 5 minut. Dodać pomidory, doprawić solą i pieprzem i dusić przez 10 minut. W dużej ilości osolonej wody ugotować makaron al dente. Odcedzić. Podawać z sosem, posypany parmezanem.", Components="3 łyżki oleju roślinnego 1 cebula, posiekana 3 ząbki czosnku, posiekane 500 g mielonej wołowiny 1 szklanka białego wytrawnego wina 1 czerwona papryka, pokrojona w kostkę 1 mała cukinia, pokrojona w kostkę 1/2 łyżeczki oregano" ,Picture="spa.jpeg", Categories = categories[3],Types = types[1], Profile=profiles[0],DayTypes = new List<DayType> {dayTypes[2]} },
                new Recipe {Name = "Nalesniki z dzemem", Description = "Wszystkie składniki na ciasto zmiksować na gładko. W miseczce rozdrobnić ser, dodać jogurt i cukier, i wymieszać na jednolitą masę. Na dobrze rozgrzaną patelnię nalać odrobinę oleju, nalać chochelkę ciasta i rozprowadzić ruchem kolistym. Smażyć przez 2 minuty na każdej stronie. Naleśniki smarować dżemem następnie zwijać w trójkaty.", Components="3 jajka, 150 g mąki pszennej, 125 ml mleka, 125 ml wody mineralnej, szczypta soli oraz 1 łyżka cukru" ,Picture="nalesniki.jpg", Categories = categories[4],Types = types[0],Profile=profiles[1] ,DayTypes = new List<DayType> {dayTypes[0]} },
                new Recipe {Name = "Kotlet schabowy", Description = "Kotlety dokładnie roztłuc tłuczkiem, odwracając co jakiś czas. Grubość kotletów zależy od indywidualnych upodobań, ale powinny mieć około 5 mm. Oprószyć mięso solą i pieprzem, odstawić. Jajko dobrze roztrzepać widelcem w głębokim talerzu. Do drugiego talerza wsypać mąkę, a do trzeciego bułkę tartą. Obtaczać kotlety porządnie w mące, następnie zanurzać w jajku i na koniec panierować w bułce tartej. Zawsze strząsać nadmiar panierki. W dużej patelni rozgrzać olej i kłaść kotlety na rozgrzany tłuszcz. Smażyć na złocisty kolor na niezbyt dużym ogniu. Usmażone kotlety odkładać w ciepłe miejsce, żeby nie wystygły przed podaniem.", Components="2 plastry schabu bez kości, 1 jajko, 1 łyżka mąki, 5 łyżek bułki tartej, sól i pieprz do smaku, olej do smażenia" ,Picture="schabowy.jpg", Categories = categories[0],Types = types[1],Profile=profiles[1], DayTypes = new List<DayType> {dayTypes[1]} },
                new Recipe {Name = "Kotlet mielony", Description = "Zmielić lub drobno posiekać kurczaka. Posiekać cebulę i pietruszkę. Je także zmielić i dodać do mięsa. Wbić jajko, dodać rozgnieciony czosnek, doprawić solą i pieprzem. Dobrze wymieszać. Dodać majonez. Nabierać po łyżce masy i obtaczać w mące. Smażyć na rozgrzanym oleju po 5-7 minut z każdej strony, aż kotlety się zrumienią.", Components="500 g piersi kurczaka, 1 jajko, 2-3 ząbki czosnku, 1 cebula, 1 duży pęczek pietruszki, 2 łyżki majonezu, sól i pieprz, mąka do panierowania",Picture="mielony.jpg", Categories = categories[0],Types = types[3],Profile=profiles[1], DayTypes = new List<DayType> {dayTypes[1], dayTypes[3] } },
                new Recipe {Name = "Twaróg na mase", Description = "Składniki dobrze wymieszać ze sobą w dużej misce, zjeść i patrzeć jak bicki rosną.", Components="500g twarogu, 400g jogurtu naturalnego, 30g siemienia lnianego, 12g otręb pszennych,50g cukru, 4g kakao, 1 banan, 1 jabłko" ,Picture="twarog.jpg", Categories = categories[5],Types = types[0],Profile=profiles[2] ,DayTypes = new List<DayType> {dayTypes[0]} }
            };

            recipes.ForEach(p => context.Recipes.Add(p));
            context.SaveChanges();

            var ratings = new List<Rating>
            {
                new Rating {Rate=1,Profile=profiles[1],Recipe=recipes[0]},
                new Rating {Rate=2,Profile=profiles[0],Recipe=recipes[0]},
                new Rating {Rate=4,Profile=profiles[0],Recipe=recipes[1]},
                new Rating {Rate=5,Profile=profiles[1],Recipe=recipes[2]},
                new Rating {Rate=3,Profile=profiles[0],Recipe=recipes[3]},
                new Rating {Rate=5,Profile=profiles[0],Recipe=recipes[4]}
            };

            ratings.ForEach(p => context.Ratings.Add(p));
            context.SaveChanges();


            var comments = new List<Comment>
            {
                new Comment {Content="Pyszne",CommentDate=DateTime.Parse("2001-09-01"),Profile=profiles[1],Recipe=recipes[0]},
                new Comment {Content="Smaczne",CommentDate=DateTime.Parse("2001-09-02"),Profile=profiles[1],Recipe=recipes[1]},
                new Comment {Content="Obrzydliwe, ale smaczne",CommentDate=DateTime.Parse("2001-09-02"),Profile=profiles[1],Recipe=recipes[2]},
                new Comment {Content="Zjadlem wieczorem :P",CommentDate=DateTime.Parse("2001-09-03"),Profile=profiles[0],Recipe=recipes[3]},
                new Comment {Content="Czuje jak mój bicek rośnie!",CommentDate=DateTime.Parse("2017-07-04"),Profile=profiles[1],Recipe=recipes[4]}

            };

            comments.ForEach(p => context.Comments.Add(p));
            context.SaveChanges();

            var recipeLikes = new List<RecipeLike>()
            {
                new RecipeLike { ProfileID = profiles[0].ID, Recipe = recipes[0] }
            };

            recipeLikes.ForEach(recipeLike => context.RecipeLikes.Add(recipeLike));

            context.SaveChanges();

 

        }

    }
}