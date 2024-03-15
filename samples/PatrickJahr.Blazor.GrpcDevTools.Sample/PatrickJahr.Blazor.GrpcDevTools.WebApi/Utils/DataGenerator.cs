using PatrickJahr.Blazor.GrpcDevTools.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace PatrickJahr.Blazor.GrpcDevTools.WebApi.Utils;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ConferencesDbContext(
                   serviceProvider.GetRequiredService<DbContextOptions<ConferencesDbContext>>()))
        {
            if (context.Conferences.Any())
            {
                return;
            }

            context.Conferences.AddRange(
                new Conference
                {
                    ID = Guid.NewGuid(),
                    Title = "BASTA! Spring 2020",
                    City = "Frankfurt am Main",
                    Country = "Germany",
                    DateFrom = new DateTime(2020, 2, 24),
                    DateTo = new DateTime(2020, 2, 28)
                },
                new Conference
                {
                    ID = Guid.NewGuid(),
                    Title = "IJS 2020 London",
                    City = "London",
                    Country = "England",
                    DateFrom = new DateTime(2020, 4, 20),
                    DateTo = new DateTime(2020, 4, 22)
                },
                new Conference
                {
                    ID = Guid.NewGuid(),
                    Title = "Global Azure Bootcamp 2020",
                    City = "Hamburg",
                    Country = "Germany",
                    DateFrom = new DateTime(2020, 4, 25),
                    DateTo = new DateTime(2020, 4, 25)
                },
                new Conference
                {
                    ID = Guid.NewGuid(),
                    Title = "DevOpsCon 2020 Berlin",
                    City = "Berlin",
                    Country = "Germany",
                    DateFrom = new DateTime(2020, 6, 8),
                    DateTo = new DateTime(2020, 6, 11)
                },
                new Conference
                {
                    ID = Guid.NewGuid(),
                    Title = "BASTA! 2020",
                    City = "Mainz",
                    Country = "Germany",
                    DateFrom = new DateTime(2020, 9, 21),
                    DateTo = new DateTime(2020, 9, 25)
                },
                new Conference
                {
                    ID = Guid.NewGuid(),
                    Title = "IJS 2020 NYC",
                    City = "New York City",
                    Country = "USA",
                    DateFrom = new DateTime(2020, 9, 28),
                    DateTo = new DateTime(2020, 10, 1)
                });

            var moreConfs = new List<Conference>();

            for (var i = 0; i < 300; i++)
            {
                var conf = new Conference
                {
                    ID = Guid.NewGuid(),
                    Title = "Conf " + i,
                    City = "City " + i,
                    Country = "Germany",
                    DateFrom = new DateTime(2021, 1, 2),
                    DateTo = new DateTime(2021, 1, 3)
                };

                moreConfs.Add(conf);
            }

            context.Conferences.AddRange(moreConfs);


            context.SaveChanges();
        }
    }
}