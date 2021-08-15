using System.Data.Entity;

namespace AssetManager.Models
{
    public class DataContext : DbContext
    {
        static DataContext()
        {
            Database.SetInitializer(new DataInitializer());
        }
        
        public DbSet<AssetAnalytic> AssetAnalytics { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}