using System;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Infrastructure.Data.EntityFrameworkCore.DbContexts;

namespace Yuxi.Andres.Test.Infrastructure.Persistence
{
	public class TestContext : DbContextBase
	{
		public TestContext(DbContextOptions options, IValidatableObjectService validatableObjectService) :
			base(options, "YuxiTest", typeof(TestContext).Assembly, validatableObjectService, null)
		{

		}
	}
}

