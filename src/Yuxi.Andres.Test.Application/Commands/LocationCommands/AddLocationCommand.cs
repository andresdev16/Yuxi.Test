using System;
using Microsoft.Extensions.Logging;
using SharedKernel.Application.Cqrs.Commands;
using SharedKernel.Application.Cqrs.Commands.Handlers;
using Yuxi.Andres.Test.Domain.Aggregates.Location;

namespace Yuxi.Andres.Test.Application.Commands.LocationCommands
{
    public record AddLocationCommand(string name, string address, DateTime openDate, DateTime closeDate) : IRequest<Guid>;

    public class AddLocationCommandHandler : IRequestHandler<AddLocationCommand, Guid>
    {
        private readonly ILocationRepository locationRepository;
        private readonly ILogger<AddLocationCommandHandler> logger;

        public AddLocationCommandHandler(ILocationRepository locationRepository, ILogger<AddLocationCommandHandler> logger)
        {
            this.locationRepository = locationRepository;
            this.logger = logger;
        }

        public async Task<Guid> HandleAsync(AddLocationCommand request, CancellationToken cancellationToken)
        {
            var location = LocationAggregate.Create(request.name, request.address, request.openDate, request.closeDate);

            locationRepository.Add(location);

            logger.LogInformation("Creating new Location ({@Location})", location);

            await locationRepository.SaveChangesAsync(cancellationToken);

            return location.Id;
        }
    }
}

