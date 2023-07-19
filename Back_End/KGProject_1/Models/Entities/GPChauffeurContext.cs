using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace KGProject_1.Models.Entities
{
    public partial class GPChauffeurContext : DbContext
    {
        public GPChauffeurContext()
        {
        }

        public GPChauffeurContext(DbContextOptions<GPChauffeurContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ComAndRate> ComAndRates { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DetailInvoice> DetailInvoices { get; set; }
        public virtual DbSet<DiscountCode> DiscountCodes { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Img> Imgs { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Tservice> Tservices { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=ADMIN\\SQLEXPRESS;Initial Catalog=GPChauffeur;Integrated Security=True");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.AccountBalance)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CurrentLocation)
                          .IsRequired()
                          .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Remembertoken)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<ComAndRate>(entity =>
            {
                entity.ToTable("ComAndRate");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("created_at");

                entity.Property(e => e.Crid)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CRId");

                entity.Property(e => e.Describe)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Displaystatus)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("updated_at");

            });

            modelBuilder.Entity<DetailInvoice>(entity =>
            {
                entity.ToTable("DetailInvoice");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("created_at");

                entity.Property(e => e.IdInvoice)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IdService)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<DiscountCode>(entity =>
            {
                entity.ToTable("DiscountCode");

                entity.Property(e => e.Id)
                    .HasMaxLength(200)
                    .HasColumnName("id");

                entity.Property(e => e.DiscountCode1)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("DiscountCode");

                entity.Property(e => e.DiscountType)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("Driver");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Birthday)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("created_at");

                entity.Property(e => e.DriverLicense)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Rate)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Img>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.Tid)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("TId");

                entity.Property(e => e.Urlimg)
                    .IsRequired()
                    .HasMaxLength(2000);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("created_at");

                entity.Property(e => e.IdCustomer)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IdDriver)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.InvoiceStatus)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Paymentid)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Coordinates)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Describe)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Locationp1)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Locationp2)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Locationp3)
                    .IsRequired()
                    .HasMaxLength(2000);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(int.MaxValue);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("created_at");

                entity.Property(e => e.Tag)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("created_at");

                entity.Property(e => e.PaymentStatus)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PaymentType)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("updated_at");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Tservice>(entity =>
            {
                entity.ToTable("TServices");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("created_at");

                entity.Property(e => e.Describe)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Destination)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Discount)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IdVehicle)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PickUpPoint)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Distance)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PriceBDistance).HasColumnName("PriceBDistance");

                entity.Property(e => e.TransTime)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ServicePrice).HasColumnName("ServicePrice");

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(200);


                entity.Property(e => e.Cbrand)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("created_at");

                entity.Property(e => e.Describe)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.DlicenseRequired)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Illustration)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Sprice).HasColumnName("SPrice");

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("updated_at");

                entity.Property(e => e.VehiclesName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
