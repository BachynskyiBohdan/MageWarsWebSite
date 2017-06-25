using System.Data.Entity;

namespace MageWarsWebSite.Domain.Entities
{
    // ReSharper disable once InconsistentNaming
    public class EFDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public DbSet<School> Schools{ get; set; }

        public DbSet<CardType> CardTypes { get; set; }

        public DbSet<SubType> SubTypes { get; set; }

        public DbSet<Desk> Desks { get; set; }

        public DbSet<Mage> Mages { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<AspNetUser> Users { get; set; }

        public EFDbContext(): base("MageWarsDb") {}
    }
}
