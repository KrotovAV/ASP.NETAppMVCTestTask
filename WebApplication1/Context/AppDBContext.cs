using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;
using WebApplication1.Entities.Enums;

namespace WebApplication1.Context
{
    //    dotnet ef migrations add InitialMigration 
    //    dotnet ef database update

    //public class AppDBContext : DbContext
        public class AppDBContext : IdentityDbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public AppDBContext()
        {

        }
        public AppDBContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

            optionsBuilder
                .UseLazyLoadingProxies()
                    .UseSqlServer(config.GetConnectionString("Connection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //-----
            base.OnModelCreating(modelBuilder);
            //-----

            //modelBuilder.Entity<Contact>().HasOne(u => u.Category).WithMany(c => c.Contacts).HasForeignKey(u => u.CategoryId);
            modelBuilder.Entity<Contact>().HasOne(u => u.Category).WithMany(c => c.Contacts).HasForeignKey(u => u.CategoryId).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Category>().HasData(
                new Category[]{
                    new Category { Id=1, CategoryName="Category1"},
                    new Category { Id=2, CategoryName="Category2"},
                    new Category { Id=3, CategoryName="Category3"}
                }
            );

            modelBuilder.Entity<Contact>().HasData(
                new Contact[]{
                    new Contact { Id=1, FirstName= "name1", LastName="last1", Email="e1@e.e", Mobile="1234567891", BirthDate = DateTime.Parse("2011-01-21"), PhotoPath="01.jpg", CategoryId=1, PriorityType =PriorityType.high},
                    new Contact { Id=2, FirstName= "name2", LastName="last2", Email="e2@e.e", Mobile="1234567892",BirthDate = DateTime.Parse("2010-02-22"), PhotoPath="02.jpg", CategoryId=1, PriorityType =PriorityType.medium, UnDeleteAble=true},
                    new Contact { Id=3, FirstName= "name3", LastName="last3", Email="e3@e.e", Mobile="1234567893", BirthDate = DateTime.Parse("2012-03-23"), PhotoPath="03.jpg", CategoryId=2, PriorityType =PriorityType.low},
                    new Contact { Id=4, FirstName= "name4", LastName="last4", Email="e4@e.e", Mobile="1234567894",BirthDate = DateTime.Parse("2013-04-24"), PhotoPath="04.jpg", CategoryId=2},
                    new Contact { Id=5, FirstName= "name5", LastName="last5", Email="e5@e.e", Mobile="1234567895", PhotoPath="05.jpg", CategoryId=3}
                }
            );
        }
    }
}
