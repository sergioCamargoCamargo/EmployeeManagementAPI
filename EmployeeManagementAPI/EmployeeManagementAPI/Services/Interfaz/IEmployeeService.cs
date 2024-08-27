using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Services.Interfaz
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<IEnumerable<Employee>> GetEmployeesHiredAfter(DateTime hireDate);
        Task DeleteEmployee(int id);
    }
}
