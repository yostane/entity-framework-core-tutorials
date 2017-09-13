﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace efcore_tuto_1
{

    class ExecutionResult
    {
        public double ClassicTime { get; set; }
        public double BatchTime { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return $"{ClassicTime:0.00}\t{BatchTime:0.00}\t{Title}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World Entity Framework Core!");

            using (var context = new VideoGamesDatabaseContext())
            {

                // Migrate the database
                context.Database.Migrate();

                //delete all previous entitiies
                foreach (var videoGame in context.VideoGames)
                {
                    context.Remove(videoGame);
                }
                context.SaveChanges();
            }

            var results = new List<ExecutionResult>();
            results.Add(MeasureDelete("All 500 entities", 500, vg => true));
            results.Add(MeasureDelete("500 entities, year > 250", 500, vg => vg.ReleaseYear > 250));
            results.Add(MeasureDelete("All 2000 entities", 2000, vg => true));
            results.Add(MeasureDelete("2000 entities, year > 1000", 2000, vg => vg.ReleaseYear > 1000));
            results.Add(MeasureDelete("All 10000 entities", 10000, vg => vg.ReleaseYear > 1000));
            results.Add(MeasureDelete("10000 entities, year > 5000", 10000, vg => vg.ReleaseYear > 5000));

            Console.WriteLine("Classic\tBatch\tTitle");
            Console.WriteLine(string.Join("\n", results));
        }


        static void FillDatabase(int count)
        {
            Console.WriteLine("start adding");
            using (var context = new VideoGamesDatabaseContext())
            {
                for (int i = 0; i < count; i++)
                {
                    var vg = new VideoGame
                    {
                        Platform = "PS4",
                        Title = $"Game {i}",
                        ReleaseYear = i
                    };
                    context.Add(vg);
                }
                context.SaveChanges();
            }
            Console.WriteLine("Finished adding");
        }
        static ExecutionResult MeasureDelete(string title, int count, Func<VideoGame, bool> predicate)
        {
            TimeSpan duration;
            DateTime start;
            var result = new ExecutionResult();
            result.Title = title;

            FillDatabase(count);

            Console.WriteLine("Start classic delete");
            start = DateTime.Now;
            using (var context = new VideoGamesDatabaseContext())
            {
                //delete all previous entitiies
                foreach (var videoGame in context.VideoGames.Where(predicate))
                {
                    context.Remove(videoGame);
                }
                context.SaveChanges();
            }
            duration = DateTime.Now - start;
            result.ClassicTime = duration.TotalMilliseconds;
            Console.WriteLine($"Finished classic delete time {duration.Ticks}");

            FillDatabase(count);

            Console.WriteLine("Starting batch delete");
            start = DateTime.Now;
            using (var context = new VideoGamesDatabaseContext())
            {
                var res = context.VideoGames.Where(vg => predicate(vg)).Delete();
                context.SaveChanges();
            }
            duration = DateTime.Now - start;
            result.BatchTime = duration.TotalMilliseconds;
            Console.WriteLine($"Finished batch delete time {duration.Ticks}");

            return result;
        }
    }
}