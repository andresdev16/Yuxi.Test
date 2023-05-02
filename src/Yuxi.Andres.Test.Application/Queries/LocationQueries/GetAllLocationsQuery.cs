using System;
using SharedKernel.Application.Cqrs.Queries;
using Yuxi.Andres.Test.Domain.Aggregates.Location;

namespace Yuxi.Andres.Test.Application.Queries.LocationQueries
{
	public record GetAllLocationsQuery(int offset, int limit) : IQueryRequest<IEnumerable<Location>>;
}