using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using dotenv.net;
namespace MusicLibrary
{

    public class MusicLibraryDb : DbContext
    {

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<ArtistDetails> ArtisstDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DotEnv.Load();
            var server = Environment.GetEnvironmentVariable("server");
            var database = Environment.GetEnvironmentVariable("database");
            var user = Environment.GetEnvironmentVariable("user");
            var password = Environment.GetEnvironmentVariable("password");
            optionsBuilder.UseSqlServer($"Server={server};Database={database};User Id={user};Password={password};TrustServerCertificate=True;");
        }
    }
}