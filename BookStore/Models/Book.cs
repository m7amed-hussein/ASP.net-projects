using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        [Required]
        [StringLength(120,MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 20)]
        public string Title { get; set; }
        public Author author { get; set; }
        public int Id { get; set; }
        
        public string fileURL { get; set; }
        public Book()
        {
        }
    }
}
