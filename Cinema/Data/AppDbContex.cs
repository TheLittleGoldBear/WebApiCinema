using Cinema.Model;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data
{
    public class AppDbContex : DbContext
    {
        public AppDbContex(DbContextOptions<AppDbContex> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmRoom>()
                .HasKey(x => new
                {
                    x.FilmId,
                    x.RoomId
                });

            modelBuilder.Entity<FilmRoom>()
                .HasOne(f => f.Film)
                .WithMany(fr => fr.FilmRooms)
                .HasForeignKey(f => f.FilmId);

            modelBuilder.Entity<FilmRoom>()
                .HasOne(r => r.Room)
                .WithMany(fr => fr.FilmRooms)
                .HasForeignKey(r => r.RoomId);

            modelBuilder.Entity<Film>()
                .HasOne(f => f.Director)
                .WithMany(d => d.Films)
                .HasForeignKey(f => f.DirectorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<FilmRoom> FilmRooms { get; set; }
    }
}
