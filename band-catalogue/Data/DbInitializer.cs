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
                // Apply migrations if not already applied
                context.Database.Migrate(); 

                // Look for any Bands to prevent duplicate seeding
                if (context.Bands.Any())
                {
                    Console.WriteLine("Database has already been seeded.");
                    return;
                }

                Console.WriteLine("Seeding database with initial data...");

                var bands = new Band[]
                {
                    new Band{BandName="The Beatles", Genre="Rock", Country="UK", FormedYear=1960},
                    new Band{BandName="Led Zeppelin", Genre="Rock", Country="UK", FormedYear=1968},
                    new Band{BandName="The Rolling Stones", Genre="Rock", Country="UK", FormedYear=1962},
                    new Band{BandName="Pink Floyd", Genre="Rock", Country="UK", FormedYear=1965},
                    new Band{BandName="Queen", Genre="Rock", Country="UK", FormedYear=1970},
                    new Band{BandName="The Who", Genre="Rock", Country="UK", FormedYear=1964},
                    new Band{BandName="The Doors", Genre="Rock", Country="USA", FormedYear=1965},
                    new Band{BandName="Iron Maiden", Genre="Metal", Country="UK", FormedYear=1975},
                    new Band{BandName="Black Sabbath", Genre="Metal", Country="UK", FormedYear=1968},
                    new Band{BandName="Metallica", Genre="Metal", Country="USA", FormedYear=1981}
                };

                context.Bands.AddRange(bands);
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
