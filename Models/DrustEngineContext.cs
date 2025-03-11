using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VBEngine.Models;

public partial class DrustEngineContext : DbContext
{
    public DrustEngineContext()
    {
    }

    public DrustEngineContext(DbContextOptions<DrustEngineContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<OfferStatus> OfferStatuses { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<ProviderService> ProviderServices { get; set; }

    public virtual DbSet<QoS> Qos { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("ConnectionString"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("order_status_type", new[] { "Pending", "Prepareing", "Delivering", "Completed" });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.OfferId).HasName("Offer_pkey");

            entity.ToTable("Offer");

            entity.Property(e => e.OfferId).ValueGeneratedNever();
            entity.Property(e => e.RequestedDate).HasColumnType("time with time zone");
            entity.Property(e => e.TotalPrice).HasColumnType("money");

            entity.HasOne(d => d.OfferStatus).WithMany(p => p.Offers)
                .HasForeignKey(d => d.OfferStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OfferStatusKey");
        });

        modelBuilder.Entity<OfferStatus>(entity =>
        {
            entity.HasKey(e => e.OfferStatusId).HasName("OfferStatus_pkey");

            entity.ToTable("OfferStatus");

            entity.Property(e => e.OfferStatusId).ValueGeneratedNever();
            entity.Property(e => e.OfferStatus1).HasColumnName("OfferStatus");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("Order_pkey");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.DeliveredDate).HasColumnType("time with time zone");
            entity.Property(e => e.RequestedDate).HasColumnType("time with time zone");
            entity.Property(e => e.TotalPrice).HasColumnType("money");

            entity.HasOne(d => d.OrderStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatus)
                .HasConstraintName("Order_OrderStatus");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("OrderStatus_pkey");

            entity.ToTable("OrderStatus");

            entity.Property(e => e.OrderStatusId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("providers_pkey");

            entity.ToTable("Provider");

            entity.Property(e => e.ProviderId).HasDefaultValueSql("nextval('providers_providerid_seq'::regclass)");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.PhoneNo).HasMaxLength(50);
        });

        modelBuilder.Entity<ProviderService>(entity =>
        {
            entity.HasKey(e => new { e.ProviderId, e.ServiceId }).HasName("ProviderId");

            entity.ToTable("ProviderService");
        });

        modelBuilder.Entity<QoS>(entity =>
        {
            entity.HasKey(e => e.QoSid).HasName("qos_pkey");

            entity.ToTable("QoS");

            entity.Property(e => e.QoSid)
                .HasDefaultValueSql("nextval('qos_qosid_seq'::regclass)")
                .HasColumnName("QoSId");

            entity.HasOne(d => d.Provider).WithMany(p => p.Qos)
                .HasForeignKey(d => d.ProviderId)
                .HasConstraintName("qos_providerid_fkey");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("requests_pkey");

            entity.ToTable("Request");

            entity.Property(e => e.RequestId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.RequsetDetail).HasColumnType("json");
            entity.Property(e => e.RequsetServices).HasColumnType("json");

            entity.HasOne(d => d.RequsetStatusNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RequsetStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Request_RequestStatus");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.RequsetStatusId).HasName("RequestStatus_pkey");

            entity.ToTable("RequestStatus");

            entity.Property(e => e.RequsetStatusId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("services_pkey");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasDefaultValueSql("nextval('services_serviceid_seq'::regclass)");
            entity.Property(e => e.ServiceName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
