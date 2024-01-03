using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameMasterEnterprise.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
                //options.UseMySql("server=mysql-banco-api.mysql.database.azure.com;initial catalog = IPET;uid=MysqlRoot;pwd=Mudar#123",
            options.UseMySql("server=localhost;port=3310;user=root;password=root;database=GameMasterEnterprise;",
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.0-mysql")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    } 
}
