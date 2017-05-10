using Microsoft.EntityFrameworkCore;

namespace efcore_tuto_1
{
    /// <summary>
    /// This class handles the sqlite database
    /// </summary>
    public class VideoGamesDatabaseContext : DbContext
    {
        /// <summary>
        /// This property allows to manipoulate the video games table
        /// </summary>
        public DbSet<VideoGame> VideoGames {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // Specify the path of the database here
            optionsBuilder.UseSqlite("Filename=./video_games.sqlite");
        }
    }
}