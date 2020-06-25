using DoctorsOffice.Data;
using DoctorsOffice.DbContexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Configuration;


[assembly: OwinStartupAttribute(typeof(DoctorsOffice.Startup))]
namespace DoctorsOffice
{
    public partial class Startup
    {
        
        private void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }
            if(!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            var user = new ApplicationUser();
            user.UserName = "Admin";
            user.Email = "admin@admin.com";

            string userPWD = "Admin#1";

            var adminUser = UserManager.Create(user, userPWD);

            //Add default User to Role Admin    
            if (adminUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, "Admin");
            }
        }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }
    }
}
