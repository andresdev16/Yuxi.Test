using System;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Cqrs.Queries;
using SharedKernel.Infrastructure.Data.EntityFrameworkCore.Queries;
using Yuxi.Andres.Test.Application.Queries.LocationQueries;
using Yuxi.Andres.Test.Domain.Aggregates.Location;

namespace Yuxi.Andres.Test.Infrastructure.Persistence.Queries.Location
{
	public class GetLocationAvailablesHandler : IQueryRequestHandler<GetLocationsAvailablesQuery, IEnumerable<LocationAggregate>>
	{
		private readonly EntityFrameworkCoreQueryProvider<TestContext> queryProvider;

		public GetLocationAvailablesHandler(EntityFrameworkCoreQueryProvider<TestContext> queryProvider)
		{
			this.queryProvider = queryProvider;
		}

        public async Task<IEnumerable<LocationAggregate>> Handle(GetLocationsAvailablesQuery query, CancellationToken cancellationToken)
        {
			return await queryProvider.GetQuery<LocationAggregate>().Where(l => l.OpenDate.Hour >= 10 && l.CloseDate.Hour <= 13).ToListAsync();
        }
    }
}

