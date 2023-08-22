using GeoCodingService.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GeoCodingService
{
    public class EFDBContext : DbContext
    {
        public EFDBContext()
            : base("name=KIRIN")
        {
            // Database.Log = Console.WriteLine;
        }

        public DbSet<SchoolEntity> schoolEntities { get; set; }
        public DbSet<StudentEntity> studentEntities { get; set; }
        public DbSet<CountryEntity> countryEntities { get; set; }
        public DbSet<PlacesEntity> placesEntities { get; set; }
        public DbSet<NearestPlacesEntity> nearestPlacesEntities { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
