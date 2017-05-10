using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using efcore_tuto_1;

namespace efcoretuto2.Migrations
{
    [DbContext(typeof(VideoGamesDatabaseContext))]
    partial class VideoGamesDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("efcore_tuto_1.VideoGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Platform");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("VideoGames");
                });
        }
    }
}
