using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedKernel.Application.Cqrs.Commands;
using SharedKernel.Application.Cqrs.Queries;
using Yuxi.Andres.Test.Application.Commands.LocationCommands;
using Yuxi.Andres.Test.Application.Queries.LocationQueries;
using Yuxi.Andres.Test.Domain.Aggregates.Location;

namespace Yuxi.Andres.Test.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly ISender sender;
        private readonly IQueryBus queries;

        public LocationController(ISender sender, IQueryBus queries)
        {
            this.sender = sender;
            this.queries = queries;
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetLocationSummary))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(LocationAggregate), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetLocationSummary(Guid id, CancellationToken cancellationToken)
        {
            var location = await queries.Ask(new GetLocationQuery(id), cancellationToken);

            return location is not null
                ? Ok(location)
                : NotFound();
        }

        [HttpPost("add")]
        [ActionName(nameof(AddLocation))]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddLocation(AddLocationCommand command, CancellationToken cancellationToken)
        {
            Guid id = await sender.SendAsync(command, cancellationToken);

            return Ok(id);
        }

        [HttpGet("all/{offset:int}:{limit:int}")]
        [ActionName(nameof(GetLocations))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLocations(int offset, int limit, CancellationToken cancellationToken)
        {
            var locations = await queries.Ask(new GetAllLocationsQuery(offset, limit), cancellationToken);

            return Ok(locations);
        }

        [HttpGet("get-by-date/{offset:int}:{limit:int}:{openDate:datetime}:{closeDate:datetime}")]
        [ActionName(nameof(GetLocationsByDate))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLocationsByDate(int offset, int limit, DateTime openDate, DateTime closeDate, CancellationToken cancellationToken)
        {
            var locations = await queries.Ask(new GetLocationsByDateQuery(offset, limit, openDate, closeDate), cancellationToken);

            return Ok(locations);
        }

        [HttpGet("availables-by-hour/{offset:int}:{limit:int}")]
        [ActionName(nameof(GetLocationsByHour))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLocationsByHour(int offset, int limit, CancellationToken cancellationToken)
        {
            var locations = await queries.Ask(new GetLocationsAvailablesQuery(offset, limit), cancellationToken);

            return Ok(locations);
        }

        [HttpPost("add/file")]
        [ActionName(nameof(ImportLocations))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ImportLocations([FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0)
            {
                return BadRequest("The file was not uploaded or is empty");
            }

            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var locations = csv.GetRecords<LocationAggregate>();

            List<Guid> newLocations = new List<Guid>();

            try
            {
                foreach (LocationAggregate location in locations)
                {
                    var command = new AddLocationCommand(location.Name, location.Address, location.OpenDate, location.CloseDate);
                    Guid createdLocation = await sender.SendAsync(command, cancellationToken);
                    newLocations.Add(createdLocation);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            return Ok(newLocations);
        }
    }
}

