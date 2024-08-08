using GovernmentDirectory.Models;

namespace GovernmentDirectory.Services;
public class EmployeeInformationService
{
    private readonly List<EmployeeInformation> _employeeInformations;

    public EmployeeInformationService()
    {
        _employeeInformations =
            [
                // good cicitzen
                new EmployeeInformation
                {
                    GovernmentDirectoryId = new Guid("d51bc0a8-b36c-4ef9-a773-501aa313588b"),
                    Name = "John Doe",
                    DateOfBirth = new DateTime(1985, 4, 12),
                    Address = "123 Main St, Springfield, USA",
                    SocialSecurityNumber = new Guid("1cde6fe1-ccdf-478c-a7b5-e1f7bb01756d")
                },

                // criminal
                new EmployeeInformation
                {
                    GovernmentDirectoryId = new Guid("193cced6-2e02-47bc-a62e-4fc534a7bffc"),
                    Name = "John How",
                    DateOfBirth = new DateTime(1985, 4, 12),
                    Address = "123343 Main St, Springfield, USA",
                    SocialSecurityNumber = new Guid("10be66ba-8463-4eda-ae5e-2004ab9f6024")
                },

                //Wrong SSN
                new EmployeeInformation
                {
                    GovernmentDirectoryId = new Guid("2e6bb569-ef0b-4f33-9e6c-3418d7b787c9"),
                    Name = "Jane Smith",
                    DateOfBirth = new DateTime(1990, 6, 24),
                    Address = "456 Oak St, Springfield, USA",
                    SocialSecurityNumber = new Guid("5616f683-0335-4923-be7c-ac9b6168f21f")
                },

                //Wrong GOvtID
                new EmployeeInformation
                {
                    GovernmentDirectoryId = new Guid("4a3987b4-dec5-4163-aae4-1de47ca2587e"),
                    Name = "Charlie Brown",
                    DateOfBirth = new DateTime(1982, 11, 15),
                    Address = "789 Pine St, Springfield, USA",
                    SocialSecurityNumber = new Guid("e80f5ad1-52f5-422c-ad9b-49cdd41291d5")
                },
                // Add more dummy entries as needed
            ];
    }

    public Guid GetEmployeeInformation(Guid GovernmentDirectoryId)
    {
        EmployeeInformation? employeeInformation = _employeeInformations.Find(employee => employee.GovernmentDirectoryId == GovernmentDirectoryId);

        if(employeeInformation == null)
        {
            return Guid.Empty;
        }

        return employeeInformation.SocialSecurityNumber;
    }
}
