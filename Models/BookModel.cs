using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCReview.Models
{
    public class BookModel
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime PublishYear { get; set; }
    }
}
