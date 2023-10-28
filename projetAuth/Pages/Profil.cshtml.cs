using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetAuth.Data;

namespace ProjetAuth.Pages
{
    [Authorize]
    public class ProfilModel : PageModel
    {
        private UserManager<MyIdentityUser> _userManager;
        private RoleManager<IdentityRole> _RoleManager;
        private ApplicationDbContext _context;
        public MyIdentityUser CurrentUser { get; set; }

        //var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
        public ProfilModel(UserManager<MyIdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _RoleManager = roleManager;
            _context = context;
        }
        public void OnGet()
        {
            SetUser();
        }
        public async Task<IActionResult> OnPostAsync(MyIdentityUser user)
        {
            SetUser();
            var isAdmin = HttpContext.Request.Form["isAdmin"].FirstOrDefault() == "on";

            var role = new IdentityRole("admin");
            await _RoleManager.CreateAsync(role);

            if (isAdmin)
            {
                await _userManager.AddToRoleAsync(CurrentUser, "admin");
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(CurrentUser, "admin");
            }
            CurrentUser.LastName = user.LastName;
            CurrentUser.FirstName = user.FirstName;

            _context.Users.Attach(CurrentUser).State = EntityState.Modified;


            _context.SaveChanges();

            return Page();
        }
        private void SetUser()
        {
            var userEmail = HttpContext.User.Identity.Name;
            var currentUser = _userManager.Users.FirstOrDefault(x => x.Email == userEmail);
            CurrentUser = currentUser;
        }
    }
}
