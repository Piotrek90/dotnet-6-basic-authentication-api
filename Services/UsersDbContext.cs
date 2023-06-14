using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Services
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users {  get; set; }
        public UsersDbContext()
        {
            //Database.EnsureCreatedAsync();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=data.db");
        }
    }
}