﻿﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace efcore_tuto_1 {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello World Entity Framework Core!");

            using (var context = new VideoGamesDatabaseContext ()) {

                // Migrate the database
                context.Database.Migrate();

				// Verify that all previous data is kept
				Console.WriteLine("Current database content");
				foreach (var videoGame in context.VideoGames.ToList())
				{
                    Console.WriteLine($"{videoGame.Title} - {videoGame.Platform} - {videoGame.ReleaseYear}");
				}

                // Add a video game with its release year filled
                context.VideoGames.Add (new VideoGame {
                    Title = "Final Fantasy XV",
                    Platform = "PS4",
                    ReleaseYear = 2016
                });
                context.SaveChanges ();

                // Fetch all video games
                Console.WriteLine ("Current database content");
                foreach (var videoGame in context.VideoGames.ToList ()) {
                    Console.WriteLine ($"{videoGame.Title} - {videoGame.Platform} - {videoGame.ReleaseYear}");
                }
            }
        }
    }
}