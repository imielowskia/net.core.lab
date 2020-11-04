using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace CW4.Models
{
    public class CW4Context : DbContext
    {
        public CW4Context(DbContextOptions<CW4Context> options): base(options) { }
        
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
