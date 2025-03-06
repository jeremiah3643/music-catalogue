

namespace band_catalogue.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Bands.
            if (context.Bands.Any())
            {
                return;   // DB has been seeded
            }

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
            foreach (Band b in bands)
            {
                context.Bands.Add(b);
            }
            context.SaveChanges();
        }
    }
}