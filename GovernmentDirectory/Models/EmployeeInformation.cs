using System.ComponentModel.DataAnnotations;

namespace GovernmentDirectory.Models;

public class EmployeeInformation
{
    [Required]
    public Guid GovernmentDirectoryId {get; set;}
    public string Name {get; set;} = string.Empty;
    public DateTime DateOfBirth {get; set;}
    public string Address {get; set;} = string.Empty;

    [Required]
    public Guid SocialSecurityNumber {get; set;}
}
