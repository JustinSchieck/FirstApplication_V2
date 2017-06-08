namespace FirstApplication_V2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<GameGenre> GameGenres { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(e => e.GameGenres)
                .WithRequired(e => e.Game)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.GameGenres)
                .WithRequired(e => e.Genre)
                .WillCascadeOnDelete(false);
        }
    }
}
