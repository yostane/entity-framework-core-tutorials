﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace efcore_multithreading {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Entity Framework Core with multithreading!");
            Setup ();

            Console.WriteLine ("Addng games");

            Parallel.For (0, 30, i => {
                using (var context = new VideoGamesDatabaseContext ()) {
                    context.VideoGames.Add (new VideoGame {
                        Title = $"Game {i}",
                            Platform = "multi",
                            ReleaseYear = 2017
                    });
                    context.SaveChanges ();
                }
            });

            Console.WriteLine ("Games");
            using (var context = new VideoGamesDatabaseContext ()) {
                foreach (var videoGame in context.VideoGames) {
                    Console.WriteLine ($"Title: {videoGame.Title}");
                }
            }
        }

        static void Setup () {
            using (var context = new VideoGamesDatabaseContext ()) {

                context.Database.EnsureDeleted ();
                context.Database.EnsureCreated ();
            }
        }
    }
}