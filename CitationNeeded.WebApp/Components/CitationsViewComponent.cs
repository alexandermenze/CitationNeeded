using CitationNeeded.Database.Database;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Components
{
    public class CitationsViewComponent : ViewComponent
    {
        private readonly CitationContext _citationContext;

        public CitationBook CitationBook { get; set; }

        public CitationsViewComponent(CitationContext citationContext)
        {
            _citationContext = citationContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(string citationBookId)
        {
            CitationBook = await _citationContext
                .CitationBooks
                .SingleAsync(
                    c => string.CompareOrdinal(c.Id, citationBookId) == 0).ConfigureAwait(false);

            return View(CitationBook);
        }
    }
}
