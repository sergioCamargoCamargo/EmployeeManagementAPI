using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaz;
using EmployeeManagementAPI.Services.Interfaz;

namespace EmployeeManagementAPI.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            // Validar si ya existe un empleado con el mismo correo electrónico
            var existingEmployee = await _employeeRepository.GetEmployeeByEmail(employee.Email);
            if (existingEmployee != null)
            {
                throw new InvalidOperationException("An employee with the same email already exists.");
            }

            return await _employeeRepository.AddEmployee(employee);
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            return await _employeeRepository.UpdateEmployee(employee);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeRepository.GetAllEmployees();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetEmployeeById(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesHiredAfter(DateTime hireDate)
        {
            return await _employeeRepository.GetEmployeesHiredAfter(hireDate);
        }

        public async Task DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployee(id);
        }
    }
}
