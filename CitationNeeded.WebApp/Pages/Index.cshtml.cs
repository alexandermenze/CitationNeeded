using CitationNeeded.Database.Database;
using CitationNeeded.Domain.Exceptions;
using CitationNeeded.Domain.Interfaces;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [BindProperty]
        [Required]
        public string CreateBookName { get; set; }

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
                    .ThenInclude(b => b.Author)
                    .Include(b => b.CitationGroups)
                    .ThenInclude(c => c.Citations)
                    .OrderBy(b => b.Name)
                    .ToListAsync();
            }
            catch (IdentityException ex)
            {
                // Todo
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCreateBookAsync()
        {
            _citationContext.CitationBooks.Add(
                new CitationBook { Name = CreateBookName });

            await _citationContext.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetCitationBookPartial(string citationBookId)
        {
            var citationBook = await _citationContext.CitationBooks
                .Include(b => b.CitationGroups)
                .ThenInclude(b => b.Author)
                .Include(b => b.CitationGroups)
                .ThenInclude(c => c.Citations)
                .SingleOrDefaultAsync(b => b.Id == citationBookId);

            if (citationBook == null)
                return Partial("_EmptyPartial");

            var result = new PartialViewResult
            {
                ViewName = "_CitationBookPartial",
                ViewData = new ViewDataDictionary(ViewData)
            };
            result.ViewData["CitationBook"] = citationBook;

            return result;
        }

        public async Task<IActionResult> OnPostCreateCitationAsync(List<Citation> citationGroup, string citationBookId)
        {
            if (citationGroup == null || citationGroup.Count() == 0)
                return Redirect($"/Index?citationBookId={citationBookId}");

            var citationBook = await _citationContext
                .CitationBooks
                .Include(b => b.CitationGroups)
                .SingleOrDefaultAsync(b => b.Id == citationBookId);

            if (citationBook == null)
                return Redirect($"/Index?citationBookId={citationBookId}");

            var accountId = _identityService.GetIdentity().Id;

            var account = await _citationContext.Accounts.SingleOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
                return Redirect($"/Index?citationBookId={citationBookId}");

            citationBook.CitationGroups.Add(
                new CitationGroup
                {
                    Author = account,
                    Created = DateTime.Now,
                    Citations = citationGroup
                });

            await _citationContext.SaveChangesAsync();

            return Redirect($"/Index?citationBookId={citationBookId}");
        }

        [NonHandler]
        public DateTime GetLatestDate(CitationBook book)
        {
            if (book.CitationGroups == null || book.CitationGroups.Count() == 0)
                return DateTime.Now;

            return book.CitationGroups.Max(c => c.Created);
        }
    }
}