using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NuGet.Frameworks;
using Oblig_1_ITPE3200.Controllers;
using Oblig_1_ITPE3200.DAL;
using Oblig_1_ITPE3200.DTOs;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace ServerTests
{
    [ExcludeFromCodeCoverage]
    public class ControllerTests
    {

        private const string _loggedOn = "loggedOn";
        private const string _notLoggedOn = "";

        private readonly Mock<IObligRepository> mockRep = new Mock<IObligRepository>();
        private readonly Mock<ILogger<ObligController>> mockLog = new Mock<ILogger<ObligController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task GetDisease_RepoReturnsDisease()
        {
            // Arrange
            var disease = mockDiseaseDTO();

            mockRep
                .Setup(k => k.GetDisease(It.IsAny<int>()))
                .ReturnsAsync(disease);

            var controller = new ObligController(mockRep.Object, mockLog.Object);
            var expected = new OkObjectResult(disease);

            // Act
            var actual = await controller.GetDisease(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal((DiseaseDTO)expected.Value, (DiseaseDTO)actual.Value);
        }

        [Fact]
        public async Task GetDisease_RepoReturnsNull()
        {
            // Arrange
            mockRep
                .Setup(k => k.GetDisease(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            
            var controller = new ObligController(mockRep.Object, mockLog.Object);
            var expected = new NotFoundObjectResult("Disease was not found");

            // Act
            var actual = await controller.GetDisease(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task GetDisease_InvalidInputs()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            controller.ModelState.AddModelError("Id", "Feil i inputvalidering på server");

            var expected = new BadRequestObjectResult("Feil i inputvalidering");

            // Act
            var actual = await controller.GetDisease(It.IsAny<int>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task GetAllDiseases_RepoReturnsList()
        {
            // Arrange
            var diseaseList = mockDiseaseDTOList();

            mockRep
                .Setup(k => k.GetAllDiseases(It.IsAny<string>()))
                .ReturnsAsync(diseaseList);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new OkObjectResult(diseaseList);

            // Act
            var actual = await controller.GetAllDiseases(It.IsAny<string>()) as OkObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal((List<DiseaseDTO>)expected.Value, (List<DiseaseDTO>)actual.Value);
        }

        [Fact]
        public async Task GetAllDiseases_RepoReturnsNull()
        {
            // Arrange
            mockRep
                .Setup(k => k.GetAllDiseases(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new ObjectResult("Couldn't get disease list")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // Act
            var actual = await controller.GetAllDiseases(It.IsAny<string>()) as ObjectResult;

            // Assert
            Assert.Equal(expected.Value, actual.Value);
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }

        [Fact]
        public async Task GetAllDiseases_NotLoggedIn()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _notLoggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var actual = await controller.GetAllDiseases(It.IsAny<string>());

            // Assert
            Assert.IsType<UnauthorizedResult>(actual);
        }

        [Fact]
        public async Task GetAllDiseases_InvalidInput()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            controller.ModelState.AddModelError("searchString", "Feil i inputvalidering på server");

            var expected = new BadRequestObjectResult("Feil i inputvalidering");

            // Act
            var actual = await controller.GetAllDiseases(It.IsAny<string>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task CreateDisease_RepoReturnsDisease()
        {
            // Arrange
            var createdDisease = mockDiseaseDTO();

            mockRep
                .Setup(k => k.CreateDisease(It.IsAny<Disease>()))
                .ReturnsAsync(createdDisease);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new CreatedAtActionResult(
                nameof(controller.CreateDisease),
                nameof(controller),
                new { id = createdDisease.Id },
                createdDisease);

            // Act
            var actual = await controller.CreateDisease(It.IsAny<Disease>()) as CreatedAtActionResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal((DiseaseDTO)expected.Value, (DiseaseDTO)actual.Value);
            Assert.Equal(expected.RouteValues, actual.RouteValues);
        }

        [Fact]
        public async Task CreateDisease_RepoReturnsNull()
        {
            // Arrange
            mockRep
                .Setup(k => k.CreateDisease(It.IsAny<Disease>()))
                .ReturnsAsync(() => null);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new ObjectResult("Couldn't create disease")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // Act
            var actual = await controller.CreateDisease(It.IsAny<Disease>()) as ObjectResult;

            // Assert
            Assert.Equal(expected.Value, actual.Value);
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }

        [Fact]
        public async Task CreateDisease_NotLoggedIn()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _notLoggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var actual = await controller.CreateDisease(It.IsAny<Disease>()) as UnauthorizedResult;

            // Assert
            Assert.IsType<UnauthorizedResult>(actual);
        }

        [Fact]
        public async Task CreateDisease_InvalidInput()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            controller.ModelState.AddModelError("Name", "Feil i inputvalidering på server");

            var expected = new BadRequestObjectResult("Feil i inputvalidering");

            // Act
            var actual = await controller.CreateDisease(It.IsAny<Disease>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task UpdateDisease_RepoReturnsTrue()
        {
            // Arrange
            mockRep
                .Setup(k => k.UpdateDisease(It.IsAny<Disease>()))
                .ReturnsAsync(true);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var actual = await controller.UpdateDisease(It.IsAny<Disease>());

            // Assert
            Assert.IsType<OkResult>(actual);
        }

        [Fact]
        public async Task UpdateDisease_RepoReturnsFalse()
        {
            // Arrange
            mockRep
                .Setup(k => k.UpdateDisease(It.IsAny<Disease>()))
                .ReturnsAsync(false);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new ObjectResult("Couldn't update disease")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // Act
            var actual = await controller.UpdateDisease(It.IsAny<Disease>()) as ObjectResult;

            // Assert
            Assert.Equal(expected.Value, actual.Value);
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }

        [Fact]
        public async Task UpdateDisease_NotLoggedIn()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _notLoggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var actual = await controller.UpdateDisease(It.IsAny<Disease>()) as UnauthorizedResult;

            // Assert
            Assert.IsType<UnauthorizedResult>(actual);
        }

        [Fact]
        public async Task UpdateDisease_InvalidInput()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            controller.ModelState.AddModelError("Name", "Feil i inputvalidering på server");

            var expected = new BadRequestObjectResult("Feil i inputvalidering");

            // Act
            var actual = await controller.UpdateDisease(It.IsAny<Disease>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task DeleteDisease_RepoReturnsTrue()
        {
            // Arrange
            mockRep
                .Setup(k => k.DeleteDisease(It.IsAny<int>()))
                .ReturnsAsync(true);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var actual = await controller.DeleteDisease(It.IsAny<int>());

            // Assert
            Assert.IsType<OkResult>(actual);
        }

        [Fact]
        public async Task DeleteDisease_RepoReturnsFalse()
        {
            // Arrange
            mockRep
                .Setup(k => k.DeleteDisease(It.IsAny<int>()))
                .ReturnsAsync(false);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new ObjectResult("Couldn't delete disease")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // Act
            var actual = await controller.DeleteDisease(It.IsAny<int>()) as ObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task DeleteDisease_InvalidInput()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            controller.ModelState.AddModelError("Id", "Feil i inputvalidering på server");

            var expected = new BadRequestObjectResult("Feil i inputvalidering");

            // Act
            var actual = await controller.DeleteDisease(It.IsAny<int>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task DeleteDisease_NotLoggedOn()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _notLoggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var actual = await controller.DeleteDisease(It.IsAny<int>());

            // Assert
            Assert.IsType<UnauthorizedResult>(actual);
        }

        [Fact]
        public async Task GetAllSymptoms_RepoReturnsList()
        {
            // Arrange
            var symptomList = mockSymptomDTOList();

            mockRep
                .Setup(k => k.GetAllSymptoms())
                .ReturnsAsync(symptomList);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            var expected = new OkObjectResult(symptomList);

            // Act
            var actual = await controller.GetAllSymptoms() as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(actual);
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal((List<SymptomDTO>)expected.Value, (List<SymptomDTO>)actual.Value);
        }

        [Fact]
        public async Task GetAllSymptoms_RepoReturnsNull()
        {
            // Arrange
            mockRep
                .Setup(k => k.GetAllSymptoms())
                .ReturnsAsync(() => null);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            var expected = new ObjectResult("Couldn't get symptoms list")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // Act
            var actual = await controller.GetAllSymptoms() as ObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task GetSymptomsTable_RepoReturnsSymptomsTable()
        {
            // Arrange
            var symptomsTable = new SymptomsTable(new SymptomsTableOptions(), mockSymptomDTOList());

            mockRep
                .Setup(k => k.GetSymptomsTable(It.IsAny<SymptomsTableOptions>()))
                .ReturnsAsync(symptomsTable);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            var expected = new OkObjectResult(symptomsTable);

            // Act
            var actual = await controller.GetSymptomsTable(It.IsAny<SymptomsTableOptions>()) as OkObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal((SymptomsTable)actual.Value, (SymptomsTable)expected.Value);
        }

        [Fact]
        public async Task GetSymptomsTable_RepoReturnsNull()
        {
            // Arrange
            mockRep
                .Setup(k => k.GetSymptomsTable(It.IsAny<SymptomsTableOptions>()))
                .ReturnsAsync(() => null);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            var expected = new ObjectResult("Couldn't get symptoms table")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // Act
            var actual = await controller.GetSymptomsTable(It.IsAny<SymptomsTableOptions>()) as ObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task GetSymptomsTable_InvalidInput()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            controller.ModelState.AddModelError("searchString", "Feil i inputvalidering på server");

            var expected = new BadRequestObjectResult("Feil i inputvalidering");

            // Act
            var actual = await controller.GetSymptomsTable(It.IsAny<SymptomsTableOptions>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task GetRelatedSymptoms_RepoReturnsList()
        {
            // Arrange
            var symptoms = mockSymptomDTOList();

            mockRep
                .Setup(k => k.GetRelatedSymptoms(It.IsAny<int>()))
                .ReturnsAsync(symptoms);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            var expected = new OkObjectResult(symptoms);

            // Act
            var actual = await controller.GetRelatedSymptoms(It.IsAny<int>()) as OkObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal((List<SymptomDTO>)expected.Value, (List<SymptomDTO>)actual.Value);
        }

        [Fact]
        public async Task GetRelatedSymptoms_RepoReturnsNull()
        {
            // Arrange
            mockRep
                .Setup(k => k.GetRelatedSymptoms(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            var expected = new ObjectResult("Couldn't get related symptoms")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // Act
            var actual = await controller.GetRelatedSymptoms(It.IsAny<int>()) as ObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task GetRelatedSymptoms_InvalidInput()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            controller.ModelState.AddModelError("id", "Feil i inputvalidering på server");

            var expected = new BadRequestObjectResult("Feil i inputvalidering");

            // Act
            var actual = await controller.GetRelatedSymptoms(It.IsAny<int>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task SearchDiseases_RepoReturnsList()
        {
            // Arrange
            var diseases = mockDiseaseDTOList();

            mockRep
                .Setup(k => k.SearchDiseases(It.IsAny<List<Symptom>>()))
                .ReturnsAsync(diseases);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            var expected = new OkObjectResult(diseases);

            // Act
            var actual = await controller.SearchDiseases(It.IsAny<List<Symptom>>()) as OkObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal((List<DiseaseDTO>)expected.Value, (List<DiseaseDTO>)expected.Value);
        }

        [Fact]
        public async Task SearchDiseases_RepoReturnsNull()
        {
            // Arrange
            mockRep
                .Setup(k => k.SearchDiseases(It.IsAny<List<Symptom>>()))
                .ReturnsAsync(() => null);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            var expected = new ObjectResult("Couldn't get disease list")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // Act
            var actual = await controller.SearchDiseases(It.IsAny<List<Symptom>>()) as ObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task SearchDiseases_InvalidInput()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            controller.ModelState.AddModelError("id", "Feil i inputvalidering på server");

            var expected = new BadRequestObjectResult("Feil i inputvalidering");

            // Act
            var actual = await controller.SearchDiseases(It.IsAny<List<Symptom>>()) as BadRequestObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task LogIn_RepoReturnsTrue()
        {
            // Arrange
            mockRep
                .Setup(k => k.LogIn(It.IsAny<UserDTO>()))
                .ReturnsAsync(true);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new OkObjectResult(true);

            // Act
            var actual = await controller.LogIn(It.IsAny<UserDTO>()) as OkObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task LogIn_RepoReturnsFalse()
        {
            // Arrange
            var user = new UserDTO
            {
                Username = "foo"
            };

            mockRep
                .Setup(k => k.LogIn(It.IsAny<UserDTO>()))
                .ReturnsAsync(() => false);

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _notLoggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new OkObjectResult(false);

            // Act
            var actual = await controller.LogIn(user) as OkObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task LogIn_InvalidInput()
        {
            // Arrange
            var user = new UserDTO
            {
                Username = "foo"
            };

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            controller.ModelState.AddModelError("username", "Feil i inputvaliddering på server");

            var expected = new BadRequestObjectResult("Feil i inputvalidering");

            // Act
            var actual = await controller.LogIn(user) as BadRequestObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task LogIn_ExceptionThrown()
        {
            // Arrange
            mockRep
                .Setup(k => k.LogIn(It.IsAny<UserDTO>()))
                .ThrowsAsync(new Exception());

            var controller = new ObligController(mockRep.Object, mockLog.Object);

            var expected = new ObjectResult("Couldn't verify log in information")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // Act
            var actual = await controller.LogIn(It.IsAny<UserDTO>()) as ObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task LogOut()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new OkObjectResult(true);

            // Act
            var actual = await controller.LogOut() as OkObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task LogOut_ThrowsException()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Throws(new Exception());
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new ObjectResult("Something went wrong when logging out")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // Act
            var actual = await controller.LogOut() as ObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task IsLoggedIn_True()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _loggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new OkObjectResult(true);

            // Act
            var actual = await controller.IsLoggedIn() as OkObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task IsLoggedIn_False()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _notLoggedOn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new OkObjectResult(false);

            // Act
            var actual = await controller.IsLoggedIn() as OkObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public async Task IsLoggedIn_ThrowsException()
        {
            // Arrange
            var controller = new ObligController(mockRep.Object, mockLog.Object);

            mockSession[_loggedOn] = _notLoggedOn;
            mockHttpContext.Setup(s => s.Session).Throws(new Exception());
            controller.ControllerContext.HttpContext = mockHttpContext.Object;

            var expected = new ObjectResult("Couldn't check login status")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            // Act
            var actual = await controller.IsLoggedIn() as ObjectResult;

            // Assert
            Assert.Equal(expected.StatusCode, actual.StatusCode);
            Assert.Equal(expected.Value, actual.Value);
        }

        private DiseaseDTO mockDiseaseDTO()
        {
            return new DiseaseDTO
            {
                Id = 1,
                Name = "Foo",
                Description = "Bar",
                Symptoms = new string[] { "foo", "bar" }
            };
        }

        private List<DiseaseDTO> mockDiseaseDTOList()
        {
            return new List<DiseaseDTO> {
                new DiseaseDTO
                {
                    Id = 1,
                    Name = "Foo",
                    Description = "Bar",
                    Symptoms = new string[] { "foo", "bar" }
                },
                new DiseaseDTO
                {
                    Id = 2,
                    Name = "Foo",
                    Description = "Bar",
                    Symptoms = new string[] { "foo", "bar" }
                }
            };
        }

        private List<SymptomDTO> mockSymptomDTOList()
        {
            return new List<SymptomDTO>
            {
                new SymptomDTO
                {
                    Id = 1,
                    Name = "Foo"
                },
                new SymptomDTO
                {
                    Id = 2,
                    Name = "Bar"
                }
            };
        }
    }
}
