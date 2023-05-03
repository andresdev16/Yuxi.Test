using System;
namespace Yuxi.Andres.Test.WebApi.Dtos
{
    public record GetByDateBody(DateTimeOffset openDate, DateTimeOffset closeDate);
}

