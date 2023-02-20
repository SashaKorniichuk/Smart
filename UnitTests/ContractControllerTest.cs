using Application.EquipmentContracts.Commands.AddEquipmentContract;
using Application.EquipmentContracts.Queries.GetAllEquipmentContracts;
using NUnit.Framework;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;

namespace UnitTests
{
    [TestFixture]
    public class ContractControllerTests
    {
        private Mock<ISender>? _mockSender;
        private ContractController? _controller;

        [SetUp]
        public void Setup()
        {
            _mockSender = new Mock<ISender>();
            _controller = new ContractController(_mockSender.Object);
        }

        [Test]
        public async Task AllContracts_ReturnsOk_WhenResultIsSuccess()
        {
            // Arrange
            var expectedContracts = new List<EquipmentContractResponse>
            {
                new EquipmentContractResponse(Guid.NewGuid(), "test","test",3),
                new EquipmentContractResponse(Guid.NewGuid(), "test2","test2",30),

            };
            var result = Result.Success<List<EquipmentContractResponse>>(expectedContracts);
            _mockSender!.Setup(x => x.Send(It.IsAny<GetAllEquipmentContractsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

            // Act
            var actionResult = await _controller!.AllContracts(CancellationToken.None);
            var okObjectResult = actionResult as OkObjectResult;

            // Assert
            Assert.That(okObjectResult, Is.Not.Null);
            Assert.That(okObjectResult?.Value, Is.EqualTo(expectedContracts));
        }

        [Test]
        public async Task AllContracts_ReturnsBadRequest_WhenResultIsFailure()
        {
            // Arrange
            var error = new Error("Error", "Something went wrong.");
            var result = Result.Failure<List<EquipmentContractResponse>>(error);
            _mockSender!.Setup(x => x.Send(It.IsAny<GetAllEquipmentContractsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            // Act
            var actionResult = await _controller!.AllContracts(CancellationToken.None);
            var badRequestObjectResult = actionResult as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(badRequestObjectResult);
            Assert.That(badRequestObjectResult?.Value, Is.EqualTo(error));
        }

        [Test]
        public async Task AddContract_ReturnsOk_WhenResultIsSuccess()
        {
            // Arrange
            var request = new AddContractRequest(Guid.NewGuid(), Guid.NewGuid(), 3);
            var result = Result.Success(Unit.Value);
            _mockSender!.Setup(x => x.Send(It.IsAny<AddEquipmentContractCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            // Act
            var actionResult = await _controller!.AddContract(request, CancellationToken.None);
            var okResult = actionResult as OkResult;

            // Assert
            Assert.IsNotNull(okResult);
        }

        [Test]
        public async Task AddContract_ReturnsBadRequest_WhenResultIsFailure()
        {
            // Arrange
            var request = new AddContractRequest(Guid.NewGuid(), Guid.NewGuid(), 3);
            var error = new Error("Error", "Something went wrong.");
            var result = Result.Failure(error);
            _mockSender!.Setup(x => x.Send(It.IsAny<AddEquipmentContractCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            // Act
            var actionResult = await _controller!.AddContract(request, CancellationToken.None);
            var badRequestObjectResult = actionResult as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(badRequestObjectResult);

        }
    }
}