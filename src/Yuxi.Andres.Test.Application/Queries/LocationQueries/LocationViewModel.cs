using System;
namespace Yuxi.Andres.Test.Application.Queries.LocationQueries
{
    public record LocationSummary(string name, string address, DateTimeOffset openDate, DateTimeOffset closeDate) { }
}

