using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
//using Microsoft.AspNet.Identity;
//using System.Data.Entity;

namespace Lisa.BookStoreLisa.web.Models
{
  
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public int Age{get;set;}//Lisa
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    //public class ApplicationRole : IdentityRole
    //{
    //    public ApplicationRole()
    //    {

    //    }
    //    public ApplicationRole(string name) : base(name)
    //    {

    //    }

    //}
    public class ApplicationDbInit:DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //创建管理对象
            //var roleManager= new RoleManager<IdentityRole>(
            //    new RoleStore<IdentityRole>(context));

            // var roleManager2 = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            //创建角色
            //roleManager.Create(new IdentityRole("Admin"));

            context.Roles.Add(new IdentityRole("Admin"));
            context.SaveChanges();
            ////创建用户管理对象
            var userManager = HttpContext.Current.GetOwinContext(
                ).GetUserManager<ApplicationUserManager>();
            //实例化一个管理员用户对象
            var user = new ApplicationUser {
                UserName = "Admin@123.com",
                Email = "Admin@123.com"
            };
            //添加管理员用户对象
            var result =  userManager.Create(user, "Admin@123");
         //将管理员对象设置到管理员角色
            userManager.AddToRole(user.Id, "Admin");

            base.Seed(context);

        }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new ApplicationDbInit());//设置初始化器
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}