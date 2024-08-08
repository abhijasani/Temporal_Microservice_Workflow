using Company.DTOs;
using Company.Models;

namespace Company.Services
{
    public class EmployeeService
    {
        private readonly List<Employee> _employees = [];

        public EmployeeService()
        {
            // Optional: Initialize with some sample data
            _employees =
            [
                // tru cictizen
                new Employee
                {
                    EmployeeId = new Guid("e71527b7-01e1-48b2-8f49-7b1ebe60afea"),
                    Name = "John Doe",
                    EmailId = "john.doe@example.com",
                    GovernmentDirectoryId = new Guid("d51bc0a8-b36c-4ef9-a773-501aa313588b"),
                    BackgroundCheck = null
                },

                // criminal 
                new Employee
                {
                    EmployeeId = new Guid("4a233c05-548e-4fec-8fbe-98732d235161"),
                    Name = "John How",
                    EmailId = "john.how@example.com",
                    GovernmentDirectoryId = new Guid("193cced6-2e02-47bc-a62e-4fc534a7bffc"),
                    BackgroundCheck = null
                },

                //Wrong SSN
                new Employee
                {
                    EmployeeId = new Guid("e60a54da-d1ad-40f4-8d75-cc82aaf811ea"),
                    Name = "Jane Smith",
                    EmailId = "jane.smith@example.com",
                    GovernmentDirectoryId = new Guid("2e6bb569-ef0b-4f33-9e6c-3418d7b787c9"),
                    BackgroundCheck = null
                },

                //Wrong GovertID
                new Employee
                {
                    EmployeeId = new Guid("6a688647-3c5d-47df-b84b-6af584d1bfac"),
                    Name = "Charlie Brown",
                    EmailId = "alice.johnson@example.com",
                    GovernmentDirectoryId = new Guid("4a3987b4-dec5-4163-aae4-1de47ca2587b"),
                    BackgroundCheck = null
                }
            ];
        }

        // Create
        public Guid AddEmployee(EmployeeDTO employeeDTO, Guid governmentDirectoryId)
        {
            Employee employee = new()
            {
                EmployeeId = Guid.NewGuid(),
                GovernmentDirectoryId = governmentDirectoryId,
                EmailId = employeeDTO.EmailId,
                Name = employeeDTO.Name,
                BackgroundCheck = employeeDTO.BackgroundCheck
            };

            _employees.Add(employee);

            return employee.EmployeeId;
        }

        // Read
        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee? GetEmployeeById(Guid employeeId)
        {
            return _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        // Update
        public bool UpdateEmployee(Guid employeeId, EmployeeDTO updatedEmployee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (existingEmployee == null) return false;

            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.EmailId = updatedEmployee.EmailId;
            existingEmployee.BackgroundCheck = updatedEmployee.BackgroundCheck;
            return true;
        }

        // Delete
        public bool DeleteEmployee(Guid employeeId)
        {
            var employee = _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null) return false;

            _employees.Remove(employee);
            return true;
        }
    }
}
