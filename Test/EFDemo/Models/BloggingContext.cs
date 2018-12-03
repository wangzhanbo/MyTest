using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDemo.Models
{
   public class BloggingContext:DbContext
    {

        DbSet<Blog> Blog { get; set; }

        DbSet<Post> Post { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=11.10.166.50;Initial Catalog=test03;User ID=sa;Password=@cppei12#"); 
        }
    }
}
