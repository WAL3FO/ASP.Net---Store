using CRT.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRT.Startup))]
namespace CRT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {  
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);                 

                var user = new ApplicationUser();
                user.UserName = "husydr";
                user.Email = "husy@softechers.com";

                string userPWD = "A@Z200711";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Manager role     
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);

            }

            // creating Creating Employee role     
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }
        }
    }
}
