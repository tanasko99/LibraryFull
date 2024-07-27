using FullLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FullLibrary.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }

    }
}
