using CitationNeeded.Database.Database;
using CitationNeeded.Domain.Exceptions;
using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IIdentityService _identityService;
        private readonly CitationContext _citationContext;

        public Domain.ValueTypes.Account Account { get; set; }
        public IEnumerable<CitationBook> CitationBooks { get; set; }

        public IndexModel(IIdentityService identityService, CitationContext citationContext)
        {
            _identityService = identityService;
            _citationContext = citationContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Account = _identityService.GetIdentity();
                CitationBooks = await _citationContext
                    .CitationBooks
                    .Include(b => b.CitationGroups)
                    .ThenInclude(c => c.Citations)
                    .ToListAsync();
            }
            catch (IdentityException ex)
            {
                // Todo
            }

            return Page();
        }
    }
}