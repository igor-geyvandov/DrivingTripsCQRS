using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using DrivingTripsCQRS.Commands;
using System.Threading;
using System.Threading.Tasks;
using DrivingTripsCQRS.Domain.Driver;
using DrivingTripsCQRS.Domain.Trip;
using DrivingTripsCQRS.Domain.DriverReport;
using DrivingTripsCQRS.Queries.Dtos;
using System.Linq;

namespace DrivingTripsCQRS.Queries.Handlers
{
    /// <summary>
    /// Hanldes GetReportOfAllDriversQuery by returning the latest DriverReport for each Driver.
    /// </summary>
    public class GetReportOfAllDriversQueryHandler : IRequestHandler<GetReportOfAllDriversQuery, GetReportOfAllDriversDto>
    {
        private readonly IDriverReportRepository _driverReportRepository;
        public GetReportOfAllDriversQueryHandler(IDriverReportRepository driverReportRepository, ITripRepository tripRepository)
        {
            _driverReportRepository = driverReportRepository ?? throw new ArgumentNullException(nameof(driverReportRepository));
        }

        public Task<GetReportOfAllDriversDto> Handle(GetReportOfAllDriversQuery query, CancellationToken cancellationToken)
        {
            var driverReports = _driverReportRepository.GetLatestReportOfAllDrivers().OrderByDescending(dr => dr.TotalMiles);
            var driverReportsDtos = driverReports.Select(dr =>
            {
                return new DriverReportDto
                {
                    Name = dr.Name,
                    TotalMiles = dr.TotalMiles,
                    AverageSpeed = dr.AverageSpeed
                };
            });

            return Task.FromResult(new GetReportOfAllDriversDto
            {
                DriverReports = driverReportsDtos.OrderByDescending(dr => dr.TotalMiles)
            });
        }
    }
}
