using System;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Cqrs.Queries;
using SharedKernel.Infrastructure.Data.EntityFrameworkCore.Queries;
using Yuxi.Andres.Test.Application.Queries.LocationQueries;
using Yuxi.Andres.Test.Domain.Aggregates.Location;

namespace Yuxi.Andres.Test.Infrastructure.Persistence.Queries
{
	internal class GetAllLocationHandler : IQueryRequestHandler<GetAllLocationsQuery, IEnumerable<Location>>
	{
        private readonly EntityFrameworkCoreQueryProvider<TestContext> queryProvider;

		public GetAllLocationHandler(EntityFrameworkCoreQueryProvider<TestContext> queryProvider)
		{
            this.queryProvider = queryProvider;
		}

        public async Task<IEnumerable<Location>> Handle(GetAllLocationsQuery query, CancellationToken cancellationToken)
        {
            return await queryProvider.GetQuery<Location>().Skip(query.offset).Take(query.limit).ToListAsync(cancellationToken);
        }
    }
}

