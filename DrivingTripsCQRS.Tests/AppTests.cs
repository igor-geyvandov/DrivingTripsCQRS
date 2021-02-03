using System;
using DrivingTripsCQRS;
using DrivingTripsCQRS.Exceptions;
using Xunit;
using Moq;
using Microsoft.Extensions.Options;
using MediatR;
using Microsoft.Extensions.Logging;
using System.IO;

namespace DrivingTripsCQRS.Tests
{
    public class AppTests
    {
        [Fact]
        public void App_Throws_ArgumentException_When_ArgsAreMissing()
        {
            //Arrange
            var appSettingsMock = new Mock<IOptions<AppSettings>>();
            appSettingsMock.Setup(mock => mock.Value).Returns(new AppSettings());

            var mediatorMock = new Mock<IMediator>();

            var loggerMock = new Mock<ILogger<App>>();            

            var app = new App(appSettingsMock.Object, mediatorMock.Object, loggerMock.Object);
            string[] args = { };

            //Act + Assert
            Assert.ThrowsAsync<ArgumentException>(() => app.Run(args));
        }

        [Fact]
        public void App_Throws_FileNotFoundException_When_CommandFilePathIsNotValid()
        {
            //Arrange
            var appSettingsMock = new Mock<IOptions<AppSettings>>();
            appSettingsMock.Setup(mock => mock.Value).Returns(new AppSettings());

            var mediatorMock = new Mock<IMediator>();

            var loggerMock = new Mock<ILogger<App>>();            

            var app = new App(appSettingsMock.Object, mediatorMock.Object, loggerMock.Object);
            string[] args = { @"C:\temp\INALIDFILENAME-RootDrivingTripsCommands.txt" };

            //Act + Assert
            Assert.ThrowsAsync<FileNotFoundException>(() => app.Run(args));
        }

        [Fact]
        public void App_Throws_Exception_When_NoValidCommansFoundInCommandFile()
        {
            //Arrange
            var appSettingsMock = new Mock<IOptions<AppSettings>>();
            appSettingsMock.Setup(mock => mock.Value).Returns(new AppSettings());

            var mediatorMock = new Mock<IMediator>();

            var loggerMock = new Mock<ILogger<App>>();            

            var app = new App(appSettingsMock.Object, mediatorMock.Object, loggerMock.Object);
            string[] args = { @"C:\temp\RootDrivingTripsCommands-Empty.txt" };

            //Act + Assert
            Assert.ThrowsAsync<MissingCommandsException>(() => app.Run(args));
        }
    }
}
