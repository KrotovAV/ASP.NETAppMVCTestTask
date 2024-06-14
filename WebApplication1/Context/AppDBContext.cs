using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Context
{
    //    dotnet ef migrations add InitialMigration 
    //    dotnet ef database update
    public class AppDBContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public AppDBContext()
        {

        }
        public AppDBContext(DbContextOptions dbc) : base(dbc)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

            optionsBuilder
                //.UseLazyLoadingProxies()
                    .UseSqlServer(config.GetConnectionString("Connection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Contact>().HasOne(u => u.Category).WithMany(c => c.Contacts).HasForeignKey(u => u.CategoryId);
            
            modelBuilder.Entity<Category>().HasData(
                new Category[]{
                    new Category { Id=1, CategoryName="Category1"},
                    new Category { Id=2, CategoryName="Category2"},
                    new Category { Id=3, CategoryName="Category3"}
                }
            );

            modelBuilder.Entity<Contact>().HasData(
                new Contact[]{
                    new Contact { Id=1, FirstName= "name1", LastName="last1", Email="e1@e.e", Mobile="11", CategoryId=1},
                    new Contact { Id=2, FirstName= "name2", LastName="last2", Email="e2@e.e", Mobile="22", CategoryId=1},
                    new Contact { Id=3, FirstName= "name3", LastName="last3", Email="e3@e.e", Mobile="33", CategoryId=2},
                    new Contact { Id=4, FirstName= "name4", LastName="last4", Email="e4@e.e", Mobile="4444", CategoryId=2},
                    new Contact { Id=5, FirstName= "name5", LastName="last5", Email="e5@e.e", Mobile="55", CategoryId=3}
                }
            );
        }
    }
}
