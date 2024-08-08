using System.ComponentModel.DataAnnotations;

namespace Company.Models;

public class Employee
{   
    [Required]
    public Guid EmployeeId { get; set;}
    public string Name {get; set; } = string.Empty;
    public string EmailId { get; set;} = string.Empty;

    [Required]
    public Guid GovernmentDirectoryId {get; set;}    

    public BackgroundCheck? BackgroundCheck {get; set;}
}
