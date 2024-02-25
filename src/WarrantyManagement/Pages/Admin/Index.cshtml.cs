using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarrantyManagement.Models.Auth;
using WarrantyManagement.Models.Repo;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Authorize(Roles = UserRoles.Admin)]
    public class IndexModel : PageModel
    {
        private readonly warrantyContext _warrantyCtx;
        public IndexModel(warrantyContext warrantyCtx){
            _warrantyCtx = warrantyCtx;
        }
        public int TotalEntries {get; set;}
        public async Task<PageResult> OnGetAsync(){
            var entries = await (from we in _warrantyCtx.WarrantyEntries
                                  join s in _warrantyCtx.WarrantyStatuses on we.Id equals s.EntryId
                                  where s.StatusId == 1
                                  select new WarrantyEntryDTO(){
                                    StatusId = s.StatusId,
                                    EntryId = we.Id
                                  }).ToListAsync();
            TotalEntries = entries.Count();
            return Page();
        }
    }
    public class WarrantyEntryDTO{
        public int StatusId {get; set;}
        public int EntryId {get; set;}
    }
}
