using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using yado_backend.Models;

namespace yado_backend.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserRoleRequest> UserRoleRequests { get; set; }
        public DbSet<Country> Countries { get; set; }
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

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRoleRequest>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoleRequests)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRoleRequest>()
                .HasOne(ur => ur.RequestedRole)
                .WithMany()
                .HasForeignKey(ur => ur.RequestedRoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRoleRequest>()
                .HasOne(ur => ur.ApprovedByUser)
                .WithMany()
                .HasForeignKey(ur => ur.ApprovedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Population>()
                .HasOne(population => population.Country)
                .WithMany(country => country.Populations)
                .HasForeignKey(population => population.CountryCode)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hotel>()
                .HasOne(hotel => hotel.Population)
                .WithMany(population => population.Hotels)
                .HasForeignKey(hotel => hotel.PopulationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Owner>()
                .HasOne(owner => owner.User)
                .WithMany(user => user.OwnedHotels)
                .HasForeignKey(owner => owner.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Owner>()
                .HasOne(owner => owner.Hotel)
                .WithMany(hotel => hotel.Owners)
                .HasForeignKey(owner => owner.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasKey(favorite => new { favorite.UserId, favorite.HotelId });

            modelBuilder.Entity<Favorite>()
                .HasOne(favorite => favorite.User)
                .WithMany(user => user.Favorites)
                .HasForeignKey(favorite => favorite.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne(favorite => favorite.Hotel)
                .WithMany(hotel => hotel.Favorites)
                .HasForeignKey(favorite => favorite.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasKey(review => new { review.Id });

            modelBuilder.Entity<Review>()
                .HasOne(review => review.User)
                .WithMany(user => user.Reviews)
                .HasForeignKey(review => review.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(review => review.Hotel)
                .WithMany(hotel => hotel.Reviews)
                .HasForeignKey(review => review.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Image>()
                .HasKey(image => new { image.Id });

            modelBuilder.Entity<Image>()
                .HasOne(image => image.Hotel)
                .WithMany(hotel => hotel.Images)
                .HasForeignKey(image => image.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Site>()
                .HasKey(site => new { site.Id });

            modelBuilder.Entity<Site>()
                .HasOne(site => site.Hotel)
                .WithMany(hotel => hotel.Sites)
                .HasForeignKey(site => site.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Site>()
                .HasOne(site => site.Company)
                .WithMany(company => company.Sites)
                .HasForeignKey(site => site.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Parameter>()
                .HasKey(parameter => parameter.HotelId);

            modelBuilder.Entity<Parameter>()
                .HasOne(parameter => parameter.Hotel)
                .WithOne(hotel => hotel.Parameters)
                .HasForeignKey<Parameter>(parameter => parameter.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var server = Environment.GetEnvironmentVariable("MySql_Server");
            var user = Environment.GetEnvironmentVariable("MySql_User");
            var password = Environment.GetEnvironmentVariable("MySql_Password");
            var database = Environment.GetEnvironmentVariable("MySql_Database");

            var connectionString = $"server={server};user={user};password={password};database={database}";

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}

