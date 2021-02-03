using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DrivingTripsCQRS.Commands;
using DrivingTripsCQRS.Domain.DriverReport;
using DrivingTripsCQRS.Exceptions;
using DrivingTripsCQRS.Queries;
using DrivingTripsCQRS.Queries.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingTripsCQRS
{
    public class App
    {
        private readonly AppSettings _appSettings;
        private readonly IMediator _mediator;
        private readonly ILogger<App> _logger;

        public App(IOptions<AppSettings> appSettings, IMediator mediator, ILogger<App> logger)
        {
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Run(string[] args)
        {
            _logger.LogInformation("Start app");

            if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                throw new ArgumentException("App args do not contain path to file with commands");
            }            

            List<AddDriverCommand> addDriverCommands;
            List<AddTripCommand> addTripCommands;
            ReadCommandsFromFile(args[0], out addDriverCommands, out addTripCommands);   
            
            if (!addDriverCommands.Any() && !addDriverCommands.Any())
            {
                throw new MissingCommandsException(args[0]);
            }

            foreach(var cmd in addDriverCommands)
            {
                var driverAdded = await _mediator.Send(cmd);
                _logger.LogInformation(string.Format("Driver with Name '{0}' {1}", cmd.Name, driverAdded ? "added" : "not added"));
            }
            foreach (var cmd in addTripCommands)
            {
                var tripAdded = await _mediator.Send(cmd);
                _logger.LogInformation(string.Format("Trip for Driver with Name '{0}' {1}", cmd.DriverName, tripAdded ? "added" : "not added"));
            }
            var getReportOfAllDriversDto = await _mediator.Send(new GetReportOfAllDriversQuery());
            WriteDriverReportToFile(getReportOfAllDriversDto);
            _logger.LogInformation("End app");
        }

        private void ReadCommandsFromFile(string commandfilePath, out List<AddDriverCommand> addDriverCommands, out List<AddTripCommand> addTripCommands)
        {
            if (!File.Exists(commandfilePath))
            {
                throw new FileNotFoundException(string.Format("Command file '{0}' not found", commandfilePath));
            }
            
            addDriverCommands = new List<AddDriverCommand>();
            addTripCommands = new List<AddTripCommand>();
            var commandsLines = File.ReadAllLines(commandfilePath);
            foreach (var line in commandsLines)
            {
                if (!string.IsNullOrEmpty(line) && !string.IsNullOrWhiteSpace(line))
                {
                    var commandParams = line.Split(" ");
                    if (commandParams.Length == 2 && commandParams[0] == "Driver")
                    {
                        addDriverCommands.Add(new AddDriverCommand  {
                            Name = commandParams[1] 
                        });
                    }
                    else if (commandParams.Length == 5 && commandParams[0] == "Trip")
                    {
                        addTripCommands.Add(new AddTripCommand { 
                            DriverName = commandParams[1], 
                            StartTime = TimeSpan.Parse(commandParams[2]), 
                            StopTime = TimeSpan.Parse(commandParams[3]),
                            MilesDriven = double.Parse(commandParams[4]) 
                        });
                    }
                }
            }
        }

        private void WriteDriverReportToFile(GetReportOfAllDriversDto getReportOfAllDriversDto)
        {
            var reportFilePath = _appSettings.ReportOutputFile;
            var sb = new StringBuilder();
            foreach(var report in getReportOfAllDriversDto.DriverReports)
            {
                sb.Append(string.Format("{0}: {1} miles ", report.Name, report.TotalMiles, report.AverageSpeed));
                if (report.AverageSpeed > 0)
                {
                    sb.Append(string.Format("{0} @ mph", report.AverageSpeed));                    
                }
                sb.Append(Environment.NewLine);
            }
            File.WriteAllText(reportFilePath, sb.ToString());            
        }
    }
}
