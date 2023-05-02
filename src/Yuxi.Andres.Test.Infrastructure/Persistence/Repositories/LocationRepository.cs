using System;
using SharedKernel.Infrastructure.Data.EntityFrameworkCore.Repositories;
using Yuxi.Andres.Test.Domain.Aggregates.Location;

namespace Yuxi.Andres.Test.Infrastructure.Persistence.Repositories
{
	class LocationRepository : EntityFrameworkCoreRepositoryAsync<Location>, ILocationRepository
	{
		public LocationRepository(TestContext context) : base(context) { }
	}
}

