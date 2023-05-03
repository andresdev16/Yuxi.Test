using System;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Cqrs.Queries;
using SharedKernel.Infrastructure.Data.EntityFrameworkCore.Queries;
using Yuxi.Andres.Test.Application.Queries.LocationQueries;
using Yuxi.Andres.Test.Domain.Aggregates.Location;

namespace Yuxi.Andres.Test.Infrastructure.Persistence.Queries
{
	internal class GetLocationHandler : IQueryRequestHandler<GetLocationQuery, LocationAggregate>
	{
		private readonly EntityFrameworkCoreQueryProvider<TestContext> queryProvider;

		public GetLocationHandler(EntityFrameworkCoreQueryProvider<TestContext> queryProvider)
		{
			this.queryProvider = queryProvider;
		}

        public async Task<LocationAggregate?> Handle(GetLocationQuery query, CancellationToken cancellationToken)
        {
			return await queryProvider.GetQuery<LocationAggregate>().Where(l => l.Id == query.id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}

