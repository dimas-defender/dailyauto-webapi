using DailyAuto.ModelsDB;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DailyAuto.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public virtual DbSet<CarDB> Cars { get; set; }
        public virtual DbSet<UserDB> Users { get; set; }
        public virtual DbSet<OrderDB> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarDB>(entity =>
            {
                entity.ToTable("car_");

                entity.HasKey(e => e.Id)
                    .HasName("id_");

                entity.Property(e => e.Id)
                   .HasColumnName("id_");

                entity.Property(e => e.Model)
                    .HasColumnName("model_");

                entity.Property(e => e.IsAvailable)
                    .HasColumnName("is_available_");

                entity.Property(e => e.Price)
                    .HasColumnName("price_");

                entity.Property(e => e.Mileage)
                    .HasColumnName("mileage_");
            });

            modelBuilder.Entity<UserDB>(entity =>
            {
                entity.ToTable("user_");

                entity.HasKey(e => e.Id)
                    .HasName("id_");

                entity.Property(e => e.Id)
                   .HasColumnName("id_");

                entity.Property(e => e.Login)
                    .HasColumnName("login_");

                entity.Property(e => e.Password)
                    .HasColumnName("password_");

                entity.Property(e => e.License)
                    .HasColumnName("license_");
            });

            modelBuilder.Entity<OrderDB>(entity =>
            {
                entity.ToTable("order_");

                entity.HasKey(e => e.Id)
                    .HasName("id_");

                entity.Property(e => e.Id)
                   .HasColumnName("id_");

                entity.Property(e => e.TimeCreated)
                    .HasColumnName("created_");

                entity.Property(e => e.DurationHours)
                    .HasColumnName("duration_");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost_");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id_");

                entity.Property(e => e.CarId)
                    .IsRequired()
                    .HasColumnName("car_id_");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("order_user_id_fkey");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("order_car_id_fkey");
            });
        }
    }
}
