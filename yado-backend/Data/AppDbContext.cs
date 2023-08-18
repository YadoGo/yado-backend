using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using yado_backend.Models;

namespace yado_backend.Data
{
    public class AppDbContext : DbContext
    {

        private readonly IConfiguration configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Population> Populations { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Parameter> Parameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
                .HasMany(role => role.Users)
                .WithOne(user => user.Role)
                .HasForeignKey(user => user.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<State>()
                  .HasOne(state => state.Country)
                  .WithMany(country => country.States)
                  .HasForeignKey(state => state.CountryId)
                  .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Population>()
                .HasOne(population => population.State)
                .WithMany(state => state.Populations)
                .HasForeignKey(population => population.StateId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Hotel>()
                .HasOne(hotel => hotel.Population)
                .WithMany(population => population.Hotels)
                .HasForeignKey(hotel => hotel.PopulationId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Owner>()
                .HasOne(owner => owner.User)
                .WithMany(user => user.OwnedHotels)
                .HasForeignKey(owner => owner.UserUuid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Owner>()
                .HasOne(owner => owner.Hotel)
                .WithMany(hotel => hotel.Owners)
                .HasForeignKey(owner => owner.HotelUuid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasKey(favorite => new { favorite.UserUuid, favorite.HotelUuid });

            modelBuilder.Entity<Favorite>()
                .HasOne(favorite => favorite.User)
                .WithMany(user => user.Favorites)
                .HasForeignKey(favorite => favorite.UserUuid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne(favorite => favorite.Hotel)
                .WithMany(hotel => hotel.Favorites)
                .HasForeignKey(favorite => favorite.HotelUuid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasKey(review => new { review.ID });

            modelBuilder.Entity<Review>()
                .HasOne(review => review.User)
                .WithMany(user => user.Reviews)
                .HasForeignKey(review => review.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(review => review.Hotel)
                .WithMany(hotel => hotel.Reviews)
                .HasForeignKey(review => review.HotelUuid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Image>()
                .HasKey(image => new { image.ID });

            modelBuilder.Entity<Image>()
                .HasOne(image => image.Hotel)
                .WithMany(hotel => hotel.Images)
                .HasForeignKey(image => image.HotelUuid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Site>()
                .HasKey(site => new { site.ID });

            modelBuilder.Entity<Site>()
                .HasOne(site => site.Hotel)
                .WithMany(hotel => hotel.Sites)
                .HasForeignKey(site => site.HotelUuid)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Site>()
                .HasOne(site => site.Company)
                .WithMany(company => company.Sites)
                .HasForeignKey(site => site.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Parameter>()
                .HasKey(parameter => parameter.HotelUuid);

            modelBuilder.Entity<Parameter>()
                .HasOne(parameter => parameter.Hotel)
                .WithOne(hotel => hotel.Parameters)
                .HasForeignKey<Parameter>(parameter => parameter.HotelUuid)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configuration.GetConnectionString("MySqlConnection");
            var serverVersion = new MySqlServerVersion(new Version(10, 11, 2));

            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
}

