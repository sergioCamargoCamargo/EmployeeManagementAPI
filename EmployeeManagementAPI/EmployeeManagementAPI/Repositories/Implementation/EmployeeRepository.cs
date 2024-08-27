using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaz;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Repositories.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "AddEmployee";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                command.Parameters.Add(new SqlParameter("@Email", employee.Email));
                command.Parameters.Add(new SqlParameter("@PhoneNumber", employee.PhoneNumber));
                command.Parameters.Add(new SqlParameter("@HireDate", employee.HireDate));

                _appDbContext.Database.OpenConnection();
                var result = await command.ExecuteScalarAsync();
                employee.EmployeeId = Convert.ToInt32(result);
            }

            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "UpdateEmployee";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@EmployeeId", employee.EmployeeId));
                command.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                command.Parameters.Add(new SqlParameter("@Email", employee.Email));
                command.Parameters.Add(new SqlParameter("@PhoneNumber", employee.PhoneNumber));
                command.Parameters.Add(new SqlParameter("@HireDate", employee.HireDate));

                _appDbContext.Database.OpenConnection();
                await command.ExecuteNonQueryAsync();
            }

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = new List<Employee>();

            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GetAllEmployees";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                _appDbContext.Database.OpenConnection();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        var employee = new Employee
                        {
                            EmployeeId = result.GetInt32(0),
                            FirstName = result.GetString(1),
                            LastName = result.GetString(2),
                            Email = result.GetString(3),
                            PhoneNumber = result.GetString(4),
                            HireDate = result.GetDateTime(5)
                        };
                        employees.Add(employee);
                    }
                }
            }

            return employees;

        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            Employee employee = null;

            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GetEmployeeById";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@EmployeeId", id));

                _appDbContext.Database.OpenConnection();
                using (var result = await command.ExecuteReaderAsync())
                {
                    if (await result.ReadAsync())
                    {
                        employee = new Employee
                        {
                            EmployeeId = result.GetInt32(0),
                            FirstName = result.GetString(1),
                            LastName = result.GetString(2),
                            Email = result.GetString(3),
                            PhoneNumber = result.GetString(4),
                            HireDate = result.GetDateTime(5)
                        };
                    }
                }
            }

            return employee;

        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM Employees WHERE Email = @Email";
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add(new SqlParameter("@Email", email));

                _appDbContext.Database.OpenConnection();
                using (var result = await command.ExecuteReaderAsync())
                {
                    if (await result.ReadAsync())
                    {
                        return new Employee
                        {
                            EmployeeId = result.GetInt32(0),
                            FirstName = result.GetString(1),
                            LastName = result.GetString(2),
                            Email = result.GetString(3),
                            PhoneNumber = result.GetString(4),
                            HireDate = result.GetDateTime(5)
                        };
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesHiredAfter(DateTime hireDate)
        {
            var employees = new List<Employee>();

            using (var command = _appDbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GetEmployeesHiredAfter";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@HireDate", hireDate));

                _appDbContext.Database.OpenConnection();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        employees.Add(new Employee
                        {
                            EmployeeId = result.GetInt32(0),
                            FirstName = result.GetString(1),
                            LastName = result.GetString(2),
                            Email = result.GetString(3),
                            PhoneNumber = result.GetString(4),
                            HireDate = result.GetDateTime(5)
                        });
                    }
                }
            }

            return employees;
        }


        public async Task DeleteEmployee(int id)
        {
            using var command = _appDbContext.Database.GetDbConnection().CreateCommand();

            command.CommandText = "DeleteEmployee";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@EmployeeId", id));

            _appDbContext.Database.OpenConnection();
            await command.ExecuteNonQueryAsync();

        }
    }
}
