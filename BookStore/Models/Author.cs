using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Author
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public String FullName { get; set; }
        public Author()
        {
        }
    }
}
