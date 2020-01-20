 namespace MJC_Blogs.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MJC_Blogs.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.Email == "markjcorum@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "markjcorum@gmail.com",
                    Email = "markjcorum@gmail.com",
                    FirstName = "Mark",
                    LastName = "Corum",
                    DisplayName = "MC Hammer"
                }, "Abc&123!");
            }
            var userId = userManager.FindByEmail("markjcorum@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.Email == "rando@email.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "rando@email.com",
                    Email = "Rando@email.com",
                    FirstName = "Rando",
                    LastName = "Mando",
                    DisplayName = "RandoMando"
                }, "Abc&123!");
            }
            userId = userManager.FindByEmail("rando@email.com").Id;
            userManager.AddToRole(userId, "Moderator");

            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.Email == "moderator@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "moderator@coderfoundry.com",
                    Email = "moderator@coderfoundry.com",
                    FirstName = "CF",
                    LastName = "Moderator",
                    DisplayName = "CFMOD"
                }, "Abc&123!");
            }
            userId = userManager.FindByEmail("moderator@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Moderator");
        }
    }
   }

