﻿using BookAPI.Models;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }
        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Publication> Publications { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }

    }


}
