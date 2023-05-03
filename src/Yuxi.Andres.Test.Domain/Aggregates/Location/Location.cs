using System;
using SharedKernel.Domain.Aggregates;

namespace Yuxi.Andres.Test.Domain.Aggregates.Location
{
	public class LocationAggregate : AggregateRoot<Guid>
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public DateTimeOffset OpenDate { get; set; }
		public DateTimeOffset CloseDate { get; set; }

		private LocationAggregate(string name, string address, DateTimeOffset openDate, DateTimeOffset closeDate)
		{
			Name = name;
			Address = address;
			OpenDate = openDate;
			CloseDate = closeDate;
		}

		public static LocationAggregate Create(string name, string address, DateTimeOffset openDate, DateTimeOffset closeDate)
		{
			var location = new LocationAggregate(name, address, openDate, closeDate);

			return location;
		}
	}
}

