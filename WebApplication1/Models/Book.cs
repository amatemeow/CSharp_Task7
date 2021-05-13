using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Book
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public int? AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
