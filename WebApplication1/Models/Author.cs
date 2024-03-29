﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? YearOfBirth { get; set; }
        public string CountryOfBirth { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
