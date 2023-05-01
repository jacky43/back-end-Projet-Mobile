
using Microsoft.EntityFrameworkCore;
using ProjetMobileAPI.Models;

namespace ProjetMobileAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Client> Clients => Set<Client>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Courier> Couriers => Set<Courier>();

    }
}
