using MediatR;
using DrivingTripsCQRS.Domain.Driver;
using DrivingTripsCQRS.Queries.Dtos;
using System.Collections.Generic;

namespace DrivingTripsCQRS.Queries
{
    public class GetReportOfAllDriversQuery : IRequest<GetReportOfAllDriversDto>
    {
    }
}
