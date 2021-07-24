using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace Infrastructure
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Category>().HasNoKey();
            //modelBuilder.Entity<Sales_H>().HasNoKey();
            //modelBuilder.Entity<Sales_B>().HasNoKey();
            //    base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Cat_No);

            });
            modelBuilder.Entity<Sales_H>(entity =>
            {
                entity.HasKey(x => x.Order_No);
                modelBuilder.Entity<Sales_H>().Property(u => u.Bills_No).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);



            });
            modelBuilder.Entity<Sales_B>(entity =>
            {
                entity.HasKey(x => x.keys);

            });
            modelBuilder.Entity<Offers>(entity =>
            {
                entity.HasKey(x => x.Offers_No);

            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.Items_No);

            });
            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(x => x.User_No);

            });
            modelBuilder.Entity<PromoCode>(entity =>
            {
                entity.HasKey(x => x.ID);

            });
            modelBuilder.Entity<Policies>(entity =>
            {
                entity.HasKey(x => x.ID);

            });


            //ignore ForeignKey
            //modelBuilder.Entity<Product>(EntityTypeBuilder =>
            //{
            //    EntityTypeBuilder.Ignore(x => x.ID_Offer);

            //});
            //////ignore ForeignKey
            //modelBuilder.Entity<Product>(EntityTypeBuilder =>
            //{
            //    EntityTypeBuilder.Ignore(x => x.ID_CAT);

            //});


        }

        public DbSet<Sales_H> Sales_Invoices_H { get; set; }
        public DbSet<Sales_B> Sales_Invoices_B { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Offers> Offers { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Registration> Registration { get; set; }
        public DbSet<PromoCode> PromoCode { get; set; }
        public DbSet<Policies> Policies { get; set; }



    }
}
