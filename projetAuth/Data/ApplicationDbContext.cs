using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjetAuth.Data
{
    // pour genere la migration
    //dotnet ef migrations add init --project .\projetAuth\

    // lancer les migration
    //dotnet ef database update --project Model
    public class MyIdentityUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<MyIdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}