using System.Data.Entity;
using System.IO;

namespace OfficeEquipmentManager.LocalDB
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
            : base($@"data source = (localdb)\MSSQLLocalDB; AttachDbFilename={Directory.GetCurrentDirectory()
        }\OfficeEquipment1.mdf;integrated security = True; MultipleActiveResultSets=True;App=EntityFramework")
        {
        }
        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Barcode> Barcode { get; set; }
        public virtual DbSet<Booker> Booker { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentCategory> EquipmentCategory { get; set; }
        public virtual DbSet<EquipmentStatus> EquipmentStatus { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<Barcode>()
                .HasMany(e => e.Equipment)
                .WithRequired(e => e.Barcode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Booker>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<EquipmentCategory>()
                .HasMany(e => e.Equipment)
                .WithRequired(e => e.EquipmentCategory)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EquipmentStatus>()
                .HasMany(e => e.Equipment)
                .WithRequired(e => e.EquipmentStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Administrator)
                .WithRequired(e => e.User);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Booker)
                .WithRequired(e => e.User);
        }
    }
}
