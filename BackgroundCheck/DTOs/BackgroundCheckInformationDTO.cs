using System;

namespace BackgroundCheck.DTOs;

public class BackgroundCheckInformationDTO
{
    public bool CriminalRecordCheck {get; set;}
    public bool TrafficViolation {get; set;}
    public bool CivilOffences {get; set;}
    public bool? IsVerified {get; set;}
}
