using Moq;
using EmployeeManagementAPI.Controllers;
using EmployeeManagementAPI.Services.Interfaz;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Tests
{
    public class EmployeesControllerTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly EmployeesController _controller;
        public EmployeesControllerTests() 
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _controller = new EmployeesController(_mockEmployeeService.Object);
        }

        #region TestCreateEmployee
        [Fact]
        public async Task CreateEmployee_ReturnsCreatedAtActionResult_WhenEmployeeIsValid()
        {
            // Arrange
            var newEmployee = new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "1234567890",
                HireDate = DateTime.Now
            };

            _mockEmployeeService.Setup(service => service.AddEmployee(It.IsAny<Employee>()))
                                .ReturnsAsync(newEmployee);

            // Act
            var result = await _controller.CreateEmployee(newEmployee);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Employee>(actionResult.Value);
            Assert.Equal(newEmployee.EmployeeId, returnValue.EmployeeId);
        }

        [Fact]
        public async Task CreateEmployee_ReturnsBadRequest_WhenEmployeeAlreadyExists()
        {
            // Arrange
            var newEmployee = new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "1234567890",
                HireDate = DateTime.Now
            };

            _mockEmployeeService.Setup(service => service.AddEmployee(It.IsAny<Employee>()))
                                .ThrowsAsync(new InvalidOperationException("An employee with the same email already exists."));

            // Act
            var result = await _controller.CreateEmployee(newEmployee);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);

            // Verifica que el valor devuelto es del tipo ErrorResponse
            var returnValue = Assert.IsType<ErrorResponse>(actionResult.Value);
            Assert.Equal("An employee with the same email already exists.", returnValue.Message);
        }
        #endregion

        #region TestUpdateEmployee
        [Fact]
        public async Task UpdateEmployee_ReturnsNoContent_WhenEmployeeIsUpdated()
        {
            // Arrange
            var updatedEmployee = new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "1234567890",
                HireDate = DateTime.Now
            };

            _mockEmployeeService.Setup(service => service.UpdateEmployee(It.IsAny<Employee>()))
                                .ReturnsAsync(updatedEmployee);

            // Act
            var result = await _controller.UpdateEmployee(updatedEmployee.EmployeeId, updatedEmployee);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateEmployee_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var updatedEmployee = new Employee
            {
                EmployeeId = 2,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "1234567890",
                HireDate = DateTime.Now
            };

            // Act
            var result = await _controller.UpdateEmployee(1, updatedEmployee);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateEmployee_ReturnsNotFound_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var updatedEmployee = new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                PhoneNumber = "1234567890",
                HireDate = DateTime.Now
            };

            _mockEmployeeService.Setup(service => service.UpdateEmployee(It.IsAny<Employee>()))
                                .ReturnsAsync((Employee)null);

            // Act
            var result = await _controller.UpdateEmployee(updatedEmployee.EmployeeId, updatedEmployee);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion
    }
}
