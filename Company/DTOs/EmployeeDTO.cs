using Company.Models;

namespace Company.DTOs;

public class EmployeeDTO
{   
    public string Name {get; set; } = string.Empty;
    public string EmailId { get; set;} = string.Empty;
    public BackgroundCheck? BackgroundCheck {get; set;}
}
