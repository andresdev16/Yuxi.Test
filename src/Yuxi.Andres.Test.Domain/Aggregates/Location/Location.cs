using System;
using SharedKernel.Domain.Aggregates;

namespace Yuxi.Andres.Test.Domain.Aggregates.Location
{
	public class LocationAggregate : AggregateRoot<Guid>
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public DateTime OpenDate { get; set; }
		public DateTime CloseDate { get; set; }

		private LocationAggregate(string name, string address, DateTime openDate, DateTime closeDate)
		{
			Name = name;
			Address = address;
			OpenDate = openDate;
			CloseDate = closeDate;
		}

		public static LocationAggregate Create(string name, string address, DateTime openDate, DateTime closeDate)
		{
			var location = new LocationAggregate(name, address, openDate, closeDate);

			return location;
		}
	}
}

