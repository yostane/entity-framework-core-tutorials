using System;
using System.Linq;

namespace efcore_tuto_1 {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello World Entity Framework Core!");

            using (var context = new VideoGamesDatabaseContext ()) {

                //The line below clears and resets the databse.
                context.Database.EnsureDeleted();

                // Create the database if it does not exist
                context.Database.EnsureCreated ();

                // Add some video games. 
                //Note that the Id field is autoincremented by default
                context.VideoGames.Add (new VideoGame {
                    Title = "Persona 5",
                        Platform = "PS4"
                });
                var SG = new VideoGame ();
                SG.Title = "Steins's Gate";
                SG.Platform = "PSVita";
                context.VideoGames.Add (SG);
                //Commit changes by calling save changes
                context.SaveChanges ();

                // Fetch all video games
                Console.WriteLine ("Current database content");
                foreach (var videoGame in context.VideoGames.ToList ()) {
                    Console.WriteLine ($"{videoGame.Title} - {videoGame.Platform}");
                }

                // Fetch all PS4 games
                var ps4Games = from v in context.VideoGames where v.Platform == "PS4"
                select v;

                Console.WriteLine ("PS4 Games");
                foreach (var videoGame in ps4Games) {
                    Console.WriteLine ($"{videoGame.Title} - {videoGame.Platform}");
                }

                //delete ps4 games
                Console.WriteLine ("Deleting PS4 Games");
                context.VideoGames.RemoveRange (ps4Games);
                //Do not forget to commit changes by calling save changes
                context.SaveChanges ();
                Console.WriteLine ("Current database content");
                foreach (var videoGame in context.VideoGames) {
                    Console.WriteLine ($"{videoGame.Title} - {videoGame.Platform}");
                }
            }
        }
    }
}