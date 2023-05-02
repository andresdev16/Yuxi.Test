using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Application.Cqrs.Commands;
using SharedKernel.Application.Cqrs.Queries;
using Yuxi.Andres.Test.Application.Commands.LocationCommands;
using Yuxi.Andres.Test.Application.Queries.LocationQueries;
using Yuxi.Andres.Test.Domain.Aggregates.Location;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        [ProducesResponseType(typeof(Location), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetLocationSummary(Guid id, CancellationToken cancellationToken)
        {
            var location = await queries.Ask(new GetLocationQuery(id), cancellationToken);

            return location is not null
                ? Ok(location)
                : NotFound();
        }

        [HttpPost("add")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddLocation(AddLocationCommand command, CancellationToken cancellationToken)
        {
            Guid id = await sender.SendAsync(command, cancellationToken);

            return Ok(id);
        }
    }
}

