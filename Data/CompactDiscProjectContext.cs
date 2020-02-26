using CompactDiscProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompactDiscProject.Data
{
    public class CompactDiscProjectContext : DbContext
    {
        public CompactDiscProjectContext(DbContextOptions<CompactDiscProjectContext> options) : base(options)
        {

        }

        public DbSet<CompactDisc> CompactDisc { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Renter> Renter { get; set; }
    }
}
