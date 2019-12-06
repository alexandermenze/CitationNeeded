using System.ComponentModel.DataAnnotations;

namespace CitationNeeded.Domain.Models
{
    public class CreateCitationBookModel
    {
        [Required]
        public string Name { get; set; }
    }
}
