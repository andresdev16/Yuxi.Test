using System;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Cqrs.Queries;
using SharedKernel.Infrastructure.Data.EntityFrameworkCore.Queries;
using Yuxi.Andres.Test.Application.Queries.LocationQueries;
using Yuxi.Andres.Test.Domain.Aggregates.Location;

namespace Yuxi.Andres.Test.Infrastructure.Persistence.Queries.Location
{
	public class GetLocationsByDateHandler : IQueryRequestHandler<GetLocationsByDateQuery, IEnumerable<LocationAggregate>>
	{
		private readonly EntityFrameworkCoreQueryProvider<TestContext> queryProvider;

		public GetLocationsByDateHandler(EntityFrameworkCoreQueryProvider<TestContext> queryProvider)
		{
			this.queryProvider = queryProvider;
		}

        public async Task<IEnumerable<LocationAggregate>> Handle(GetLocationsByDateQuery query, CancellationToken cancellationToken)
        {
			return await queryProvider.GetQuery<LocationAggregate>()
				.Where(l => l.OpenDate >= query.openDate && l.CloseDate <= query.closeDate)
				.Skip(query.offset)
				.Take(query.limit)
				.ToListAsync();
        }
    }
}

