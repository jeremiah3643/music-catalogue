using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using band_catalogue.Models;

namespace band_catalogue.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            try
            {
                Console.WriteLine("Dropping Database on Startup...");
                context.Database.EnsureDeleted(); // Will drop the database and apply any pending migrations
                Console.WriteLine("Applying Migrations on Startup...");
                context.Database.Migrate(); // Will create the database and apply any pending migrations

                Console.WriteLine("Seeding database with initial data...");

                var bands = new Band[]
                {
                    new Band { BandName = "The Beatles", Genre = "Rock", Country = "UK", FormedYear = 1960 },
                    new Band { BandName = "Led Zeppelin", Genre = "Rock", Country = "UK", FormedYear = 1968 },
                    new Band { BandName = "The Rolling Stones", Genre = "Rock", Country = "UK", FormedYear = 1962 },
                    new Band { BandName = "Pink Floyd", Genre = "Rock", Country = "UK", FormedYear = 1965 },
                    new Band { BandName = "Queen", Genre = "Rock", Country = "UK", FormedYear = 1970 },
                    new Band { BandName = "The Who", Genre = "Rock", Country = "UK", FormedYear = 1964 },
                    new Band { BandName = "The Doors", Genre = "Rock", Country = "USA", FormedYear = 1965 },
                    new Band { BandName = "Iron Maiden", Genre = "Metal", Country = "UK", FormedYear = 1975 },
                    new Band { BandName = "Black Sabbath", Genre = "Metal", Country = "UK", FormedYear = 1968 },
                    new Band { BandName = "Metallica", Genre = "Metal", Country = "USA", FormedYear = 1981 }
                };

                context.Bands.AddRange(bands);
                context.SaveChanges();

                var albums = new Album[]
                {
                    new Album { Title = "Abbey Road", ReleaseYear = 1969, Band = bands[0] },
                    new Album { Title = "Led Zeppelin IV", ReleaseYear = 1971, Band = bands[1] },
                    new Album { Title = "Sticky Fingers", ReleaseYear = 1971, Band = bands[2] },
                    new Album { Title = "The Dark Side of the Moon", ReleaseYear = 1973, Band = bands[3] },
                    new Album { Title = "A Night at the Opera", ReleaseYear = 1975, Band = bands[4] },
                    new Album { Title = "Who's Next", ReleaseYear = 1971, Band = bands[5] },
                    new Album { Title = "L.A. Woman", ReleaseYear = 1971, Band = bands[6] },
                    new Album { Title = "The Number of the Beast", ReleaseYear = 1982, Band = bands[7] },
                    new Album { Title = "Paranoid", ReleaseYear = 1970, Band = bands[8] },
                    new Album { Title = "Master of Puppets", ReleaseYear = 1986, Band = bands[9] }
                };

                context.Albums.AddRange(albums);
                context.SaveChanges();

                var songs = new Song[]
                {
                    new Song { Title = "Come Together", Duration = TimeSpan.FromMinutes(4.20), Album = albums[0] },
                    new Song { Title = "Something", Duration = TimeSpan.FromMinutes(3.03), Album = albums[0] },
                    new Song { Title = "Here Comes the Sun", Duration = TimeSpan.FromMinutes(3.05), Album = albums[0] },
                    new Song { Title = "Black Dog", Duration = TimeSpan.FromMinutes(4.54), Album = albums[1] },
                    new Song { Title = "Rock and Roll", Duration = TimeSpan.FromMinutes(3.40), Album = albums[1] },
                    new Song { Title = "Stairway to Heaven", Duration = TimeSpan.FromMinutes(8.02), Album = albums[1] },
                    new Song { Title = "Brown Sugar", Duration = TimeSpan.FromMinutes(3.49), Album = albums[2] },
                    new Song { Title = "Wild Horses", Duration = TimeSpan.FromMinutes(5.42), Album = albums[2] },
                    new Song { Title = "Can't You Hear Me Knocking", Duration = TimeSpan.FromMinutes(7.15), Album = albums[2] },
                    new Song { Title = "Bohemian Rhapsody", Duration = TimeSpan.FromMinutes(5.55), Album = albums[4] },
                    new Song { Title = "You're My Best Friend", Duration = TimeSpan.FromMinutes(2.52), Album = albums[4] },
                    new Song { Title = "Love of My Life", Duration = TimeSpan.FromMinutes(3.38), Album = albums[4] },
                    new Song { Title = "Battery", Duration = TimeSpan.FromMinutes(5.12), Album = albums[9] },
                    new Song { Title = "Master of Puppets", Duration = TimeSpan.FromMinutes(8.35), Album = albums[9] },
                    new Song { Title = "Welcome Home (Sanitarium)", Duration = TimeSpan.FromMinutes(6.27), Album = albums[9] }
                };

                context.Songs.AddRange(songs);
                context.SaveChanges();

                Console.WriteLine("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during database initialization: {ex.Message}");
            }
        }
    }
}
