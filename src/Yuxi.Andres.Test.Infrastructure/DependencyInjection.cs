using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Infrastructure;
using SharedKernel.Infrastructure.Cqrs.Commands;
using SharedKernel.Infrastructure.Cqrs.Queries;
using SharedKernel.Infrastructure.Data.Dapper;
using SharedKernel.Infrastructure.Data.EntityFrameworkCore;
using Yuxi.Andres.Test.Application.Commands.LocationCommands;
using Yuxi.Andres.Test.Application.Queries.LocationQueries;
using Yuxi.Andres.Test.Domain.Aggregates.Location;
using Yuxi.Andres.Test.Infrastructure.Persistence;
using Yuxi.Andres.Test.Infrastructure.Persistence.Repositories;

namespace Yuxi.Andres.Test.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration, string connectionString)
		{
			services.AddSharedKernel();
			services.AddInfrastructure(configuration, connectionString);
			services.AddApplication();

			return services;
		}

		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string connectionString)
		{
			return services
				.AddDapperSqlServer(configuration, connectionString)
				.AddEntityFrameworkCoreSqlServer<TestContext>(configuration, connectionString)
				.AddTransient<ILocationRepository, LocationRepository>();
		}

		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			return services
				.AddCommandsHandlers(typeof(AddLocationCommandHandler))
				.AddQueriesHandlers(typeof(TestContext));
		}
	}
}

