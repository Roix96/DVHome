using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DVHome.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            
        }
        

        public DbSet<User> Users {get; set;} = null;
        public DbSet<ShopItem> Items {get; set;} = null;
    }
}