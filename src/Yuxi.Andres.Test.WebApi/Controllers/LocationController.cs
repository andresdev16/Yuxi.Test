﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedKernel.Application.Cqrs.Commands;
using SharedKernel.Application.Cqrs.Queries;
using Yuxi.Andres.Test.Application.Commands.LocationCommands;
using Yuxi.Andres.Test.Application.Queries.LocationQueries;
using Yuxi.Andres.Test.Domain.Aggregates.Location;
using Yuxi.Andres.Test.WebApi.Dtos;

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

        [HttpGet("all")]
        [ActionName(nameof(GetLocations))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLocations([FromRoute] int offset, [FromRoute] int limit, CancellationToken cancellationToken)
        {
            var locations = await queries.Ask(new GetAllLocationsQuery(offset, limit), cancellationToken);

            return Ok(JsonConvert.SerializeObject(locations));
        }

        [HttpGet("get-by-date")]
        [ActionName(nameof(GetLocationsByDate))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLocationsByDate([FromRoute] int offset, [FromRoute] int limit, [FromBody] GetByDateBody body, CancellationToken cancellationToken)
        {
            var locations = await queries.Ask(new GetLocationsByDateQuery(offset, limit, body.openDate, body.closeDate), cancellationToken);

            return Ok(JsonConvert.SerializeObject(locations));
        }
    }
}

