using CitationNeeded.Database.Database;
using CitationNeeded.Domain.Exceptions;
using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

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

        public void OnGet()
        {
            try
            {
                Account = _identityService.GetIdentity();
                CitationBooks = _citationContext.CitationBooks.ToList();
            }
            catch (IdentityException ex)
            {
                // Todo
            }
        }
    }
}