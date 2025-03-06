using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using band_catalogue.Models;

namespace band_catalogue.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            try
            {
                Console.WriteLine("Applying database migrations...");

                // Ensure the database is created and migrations are applied
                context.Database.Migrate();

                Console.WriteLine("Checking if database seeding is needed...");

                // Check if data already exists
                if (context.Bands.Any())
                {
                    Console.WriteLine("Database already seeded. Skipping...");
                    return;
                }

                Console.WriteLine("Seeding database with initial data...");

                var bands = new Band[]
                {
                    new Band { BandId = 1, BandName = "The Beatles", Genre = "Rock", Country = "UK", FormedYear = 1960 },
                    new Band { BandId = 2, BandName = "Led Zeppelin", Genre = "Rock", Country = "UK", FormedYear = 1968 },
                    new Band { BandId = 3, BandName = "The Rolling Stones", Genre = "Rock", Country = "UK", FormedYear = 1962 },
                    new Band { BandId = 4, BandName = "Pink Floyd", Genre = "Rock", Country = "UK", FormedYear = 1965 },
                    new Band { BandId = 5, BandName = "Queen", Genre = "Rock", Country = "UK", FormedYear = 1970 },
                    new Band { BandId = 6, BandName = "The Who", Genre = "Rock", Country = "UK", FormedYear = 1964 },
                    new Band { BandId = 7, BandName = "The Doors", Genre = "Rock", Country = "USA", FormedYear = 1965 },
                    new Band { BandId = 8, BandName = "Iron Maiden", Genre = "Metal", Country = "UK", FormedYear = 1975 },
                    new Band { BandId = 9, BandName = "Black Sabbath", Genre = "Metal", Country = "UK", FormedYear = 1968 },
                    new Band { BandId = 10, BandName = "Metallica", Genre = "Metal", Country = "USA", FormedYear = 1981 }
                };

                var albums = new Album[]
                {
                    new Album { AlbumId = 1, Title = "Abbey Road", ReleaseYear = 1969, BandId = 1 },
                    new Album { AlbumId = 2, Title = "Led Zeppelin IV", ReleaseYear = 1971, BandId = 2 },
                    new Album { AlbumId = 3, Title = "Sticky Fingers", ReleaseYear = 1971, BandId = 3 },
                    new Album { AlbumId = 4, Title = "The Dark Side of the Moon", ReleaseYear = 1973, BandId = 4 },
                    new Album { AlbumId = 5, Title = "A Night at the Opera", ReleaseYear = 1975, BandId = 5 },
                    new Album { AlbumId = 6, Title = "Who's Next", ReleaseYear = 1971, BandId = 6 },
                    new Album { AlbumId = 7, Title = "L.A. Woman", ReleaseYear = 1971, BandId = 7 },
                    new Album { AlbumId = 8, Title = "The Number of the Beast", ReleaseYear = 1982, BandId = 8 },
                    new Album { AlbumId = 9, Title = "Paranoid", ReleaseYear = 1970, BandId = 9 },
                    new Album { AlbumId = 10, Title = "Master of Puppets", ReleaseYear = 1986, BandId = 10 }
                };

                var songs = new Song[]
                {
                    new Song { SongId = 1, Title = "Come Together", Duration = 4.20, AlbumId = 1 },
                    new Song { SongId = 2, Title = "Something", Duration = 3.03, AlbumId = 1 },
                    new Song { SongId = 3, Title = "Here Comes the Sun", Duration = 3.05, AlbumId = 1 },
                    new Song { SongId = 4, Title = "Black Dog", Duration = 4.54, AlbumId = 2 },
                    new Song { SongId = 5, Title = "Rock and Roll", Duration = 3.40, AlbumId = 2 },
                    new Song { SongId = 6, Title = "Stairway to Heaven", Duration = 8.02, AlbumId = 2 },
                    new Song { SongId = 7, Title = "Brown Sugar", Duration = 3.49, AlbumId = 3 },
                    new Song { SongId = 8, Title = "Wild Horses", Duration = 5.42, AlbumId = 3 },
                    new Song { SongId = 9, Title = "Can't You Hear Me Knocking", Duration = 7.15, AlbumId = 3 },
                    new Song { SongId = 10, Title = "Speak to Me", Duration = 1.30, AlbumId = 4 },
                    new Song { SongId = 11, Title = "Breathe", Duration = 2.43, AlbumId = 4 },
                    new Song { SongId = 12, Title = "On the Run", Duration = 3.35, AlbumId = 4 },
                    new Song { SongId = 13, Title = "Bohemian Rhapsody", Duration = 5.55, AlbumId = 5 },
                    new Song { SongId = 14, Title = "You're My Best Friend", Duration = 2.52, AlbumId = 5 },
                    new Song { SongId = 15, Title = "Love of My Life", Duration = 3.38, AlbumId = 5 },
                    new Song { SongId = 16, Title = "Baba O'Riley", Duration = 5.08, AlbumId = 6 },
                    new Song { SongId = 17, Title = "Behind Blue Eyes", Duration = 3.42, AlbumId = 6 },
                    new Song { SongId = 18, Title = "Won't Get Fooled Again", Duration = 8.32, AlbumId = 6 },
                    new Song { SongId = 19, Title = "Riders on the Storm", Duration = 7.15, AlbumId = 7 },
                    new Song { SongId = 20, Title = "Love Her Madly", Duration = 3.20, AlbumId = 7 },
                    new Song { SongId = 21, Title = "L.A. Woman", Duration = 7.49, AlbumId = 7 },
                    new Song { SongId = 22, Title = "The Number of The Beast", Duration = 4.50, AlbumId = 8 },
                    new Song { SongId = 23, Title = "Run to the Hills", Duration = 3.54, AlbumId = 8 },
                    new Song { SongId = 24, Title = "Hallowed Be Thy Name", Duration = 7.12, AlbumId = 8 },
                    new Song { SongId = 25, Title = "War Pigs", Duration = 7.57, AlbumId = 9 },
                    new Song { SongId = 26, Title = "Paranoid", Duration = 2.50, AlbumId = 9 },
                    new Song { SongId = 27, Title = "Planet Caravan", Duration = 4.34, AlbumId = 9 },
                    new Song { SongId = 28, Title = "Master of Puppets", Duration = 8.35, AlbumId = 10 },
                    new Song { SongId = 29, Title = "Battery", Duration = 5.12, AlbumId = 10 },
                    new Song { SongId = 30, Title = "Welcome Home (Sanitarium)", Duration = 6.27, AlbumId = 10 }
                };

                context.Bands.AddRange(bands);
                context.Albums.AddRange(albums);
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
