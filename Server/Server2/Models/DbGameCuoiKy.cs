namespace Server2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbGameCuoiKy : DbContext
    {
        public DbGameCuoiKy()
            : base("name=DbGameCuoiKy")
        {
        }

        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<score> scores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.updated_at)
                .HasPrecision(0);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.scores)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<score>()
                .Property(e => e.score1)
                .IsUnicode(false);
        }
    }
}
