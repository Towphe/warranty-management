using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarrantyManagement.Models.Auth;

namespace MyApp.Namespace
{
    [Authorize(Roles = UserRoles.Admin)]
    public class IndexModel : PageModel
    {
        public void OnGet(){
        }
    }
}
