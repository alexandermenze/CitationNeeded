using CitationNeeded.Database.Database;
using CitationNeeded.Domain.ValueTypes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CitationNeeded.WebApp.Pages
{
    public class TestAlexModel : PageModel
    {
        private readonly CitationContext _citationContext;

        public string Text { get; set; }
        public IEnumerable<CitationBook> Books { get; set; }

        public TestAlexModel(CitationContext citationContext)
        {
            _citationContext = citationContext;
        }

        public async Task OnGetAsync()
        {
            //var book = CreateBook();
            //_citationContext.CitationBooks.Add(book);
            //await _citationContext.SaveChangesAsync();
            //Text = $"{_citationContext.CitationBooks.Count()}";
            //Books = _citationContext.CitationBooks;
        }

        private CitationBook CreateBook()
        {
            var author = new User
            {
                Id = $"{Guid.NewGuid()}",
                FirstName = "Lars Arensmeier"
            };

            var citationPerson = new User
            {
                Id = $"{Guid.NewGuid()}",
                FirstName = "Cedric Strate"
            };

            return new CitationBook
            {
                Id = $"{Guid.NewGuid()}",
                Name = "ITFA20",
                CitationGroups = new List<CitationGroup>()
                {
                    new CitationGroup
                    {
                        Id = $"{Guid.NewGuid()}",
                        Author = author,
                        Created = DateTime.Now,
                        Citations = new List<Citation>
                        {
                            new Citation
                            {
                                Id = $"{Guid.NewGuid()}",
                                Speaker = citationPerson,
                                Text = "Ich bin dumm."
                            }
                        }
                    }
                }
            };
        }
    }
}