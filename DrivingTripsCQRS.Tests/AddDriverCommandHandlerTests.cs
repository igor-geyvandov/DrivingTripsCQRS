using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using DrivingTripsCQRS.Commands;
using DrivingTripsCQRS.Commands.Handlers;
using DrivingTripsCQRS.Domain.Driver;
using DrivingTripsCQRS.Events;
using DrivingTripsCQRS.Exceptions;
using System;
using System.IO;
using System.Threading;
using Xunit;

namespace DrivingTripsCQRS.Tests
{
    public class AddDriverCommandHandlerTests
    {
        [Fact]
        public async void AddDriverHandler_Returns_False_When_CommandInvalid()
        {
            //Arrange
            var mediatorMock = new Mock<IMediator>();
            var driverRepoMock = new Mock<IDriverRepository>();
            var command = new AddDriverCommand() { Name = "" };
            var handler = new AddDriverCommandHandler(driverRepoMock.Object, mediatorMock.Object);

            //Act 
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async void AddDriverHandler_Returns_True_When_CommandValidAndDriverIsNotInStore()
        {
            //Arrange
            var mediatorMock = new Mock<IMediator>();

            var driverRepoMock = new Mock<IDriverRepository>();
            Driver driver = null;
            driverRepoMock.Setup(mock => mock.AddDriver(It.IsAny<Driver>())).Verifiable();
            driverRepoMock.Setup(mock => mock.GetDriverByName(It.IsAny<string>())).Returns(driver);

            var command = new AddDriverCommand() { Name = "Kumi" };
            var handler = new AddDriverCommandHandler(driverRepoMock.Object, mediatorMock.Object);

            //Act 
            var result = await handler.Handle(command, new CancellationToken());

            //Assert that hanlder returns True and DriverAddedEvent is published once.
            Assert.True(result);
            mediatorMock.Verify(mock => mock.Publish(It.IsAny<DriverAddedEvent>(), new CancellationToken()), Times.Once());
        }

        [Fact]
        public async void AddDriverHandler_Returns_False_When_CommandValidAndDriverIsAlreadyInStore()
        {
            //Arrange
            var mediatorMock = new Mock<IMediator>();

            var driverRepoMock = new Mock<IDriverRepository>();
            Driver driver = new Driver() { Name = "Kumi" };
            driverRepoMock.Setup(mock => mock.AddDriver(It.IsAny<Driver>())).Verifiable();
            driverRepoMock.Setup(mock => mock.GetDriverByName(It.IsAny<string>())).Returns(driver);

            var command = new AddDriverCommand() { Name = "Kumi" };
            var handler = new AddDriverCommandHandler(driverRepoMock.Object, mediatorMock.Object);

            //Act 
            var result = await handler.Handle(command, new CancellationToken());

            //Assert that hanlder returns False and DriverAddedEvent is not published.
            Assert.False(result);
            mediatorMock.Verify(mock => mock.Publish(It.IsAny<DriverAddedEvent>(), new CancellationToken()), Times.Never());
        }
    }
}
