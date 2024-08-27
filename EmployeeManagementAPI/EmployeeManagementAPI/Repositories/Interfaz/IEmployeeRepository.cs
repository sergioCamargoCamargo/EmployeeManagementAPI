using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories.Interfaz
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> GetEmployeeByEmail(string name);
        Task<IEnumerable<Employee>> GetEmployeesHiredAfter(DateTime hireDate);
        Task DeleteEmployee(int id);
    }
}
