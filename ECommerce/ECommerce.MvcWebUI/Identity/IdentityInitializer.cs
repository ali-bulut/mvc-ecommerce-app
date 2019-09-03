using ECommerce.MvcWebUI.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ECommerce.MvcWebUI.Identity
{
    //Sadece Database yoksa oluşturur onun dışında modelde herhangi bir değişiklik olsa bile databasei değiştirmez
    public class IdentityInitializer :CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
        {
            if (!context.Roles.Any(i=>i.Name=="admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name="admin",Description= "administrator role" };

                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name = "user", Description = "user role" }; 

                manager.Create(role);
            }

            if (!context.Users.Any(i => i.UserName == "alibulut"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
                {
                    Name="Ali",
                    Surname="Bulut",
                    UserName="alibulut",
                    Email="alibulut@yahoo.com"
                };

                manager.Create(user,"alibulut");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");

            }

            if (!context.Users.Any(i => i.UserName == "gokhanbulut"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
                {
                    Name = "Gökhan",
                    Surname = "Bulut",
                    UserName = "gokhanbulut",
                    Email = "gokhanbulut@yahoo.com"
                };

                manager.Create(user, "gokhanbulut");
                manager.AddToRole(user.Id, "user");
            }

            base.Seed(context);
        }
    }
}