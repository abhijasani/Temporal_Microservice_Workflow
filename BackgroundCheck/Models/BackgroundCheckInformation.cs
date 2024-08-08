namespace BackgroundCheck.Models;

public class BackgroundCheckInformation
{
    public Guid BackgroundInformationId {get; set;}
    public Guid SocialSecurityNumber {get; set;}

    public bool CriminalRecordCheck {get; set;}
    public bool TrafficViolation {get; set;}
    public bool CivilOffences {get; set;}

    public bool? isVerified {get; set;}

}
