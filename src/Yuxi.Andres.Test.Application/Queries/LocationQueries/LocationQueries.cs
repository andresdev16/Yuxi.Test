using System;
using SharedKernel.Application.Cqrs.Queries;
using Yuxi.Andres.Test.Domain.Aggregates.Location;

namespace Yuxi.Andres.Test.Application.Queries.LocationQueries
{
    public record GetLocationQuery(Guid id) : IQueryRequest<LocationAggregate>;
    public record GetAllLocationsQuery(int offset, int limit) : IQueryRequest<IEnumerable<LocationAggregate>>;
    public record GetLocationsByDateQuery(int offset, int limit, DateTime openDate, DateTime closeDate) : IQueryRequest<IEnumerable<LocationAggregate>>;
    public record GetLocationsAvailablesQuery(int offset, int limit): IQueryRequest<IEnumerable<LocationAggregate>>;
}

