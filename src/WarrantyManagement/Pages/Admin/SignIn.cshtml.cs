using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class SignInModel : PageModel
    {
        public string Email {get; set;} = "";
        public string Password {get; set;} = "";
        public IActionResult OnGet()
        {
            if (HttpContext.User.IsInRole("Admin")){
                return new RedirectToPageResult("/admin/index");
            }
            return Page();
        }
    }
}
