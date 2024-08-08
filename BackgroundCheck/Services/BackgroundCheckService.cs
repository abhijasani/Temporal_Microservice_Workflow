using BackgroundCheck.DTOs;
using BackgroundCheck.Models;

namespace BackgroundCheck.Services;

public class BackgroundCheckService
{
    private readonly List<BackgroundCheckInformation> _backgroundCheckInformations;

    public BackgroundCheckService()
    {
        _backgroundCheckInformations =
            [
                // pass
                new BackgroundCheckInformation
                {
                    BackgroundInformationId = new Guid("c398682d-b16b-4cc8-beed-ac7364a1b1ee"),
                    SocialSecurityNumber = new Guid("1cde6fe1-ccdf-478c-a7b5-e1f7bb01756d"),
                    CriminalRecordCheck = false,
                    TrafficViolation = false,
                    CivilOffences = false
                },

                // fail
                new BackgroundCheckInformation
                {
                    BackgroundInformationId = new Guid("e28cce4e-2959-4575-8d00-01380ef2627a"),
                    SocialSecurityNumber = new Guid("10be66ba-8463-4eda-ae5e-2004ab9f6024"),
                    CriminalRecordCheck = false,
                    TrafficViolation = true,
                    CivilOffences = false
                },

                // SSN false
                new BackgroundCheckInformation
                {
                    BackgroundInformationId = new Guid("18c82ad3-686d-49b2-9d48-1c8fdffa6265"),
                    SocialSecurityNumber = new Guid("5616f683-0335-4923-be7c-ac9b6168f21e"),
                    CriminalRecordCheck = true,
                    TrafficViolation = true,
                    CivilOffences = false
                }
            ];
    }

    //returns null if not verified else returns emnployeeinformationdetails
    public BackgroundCheckInformationDTO? VerifyEmployee(Guid SSN)
    {
        var employee = _backgroundCheckInformations.Find(employee => employee.SocialSecurityNumber == SSN);

        if(employee == null)
        {
            return null;
        }

        BackgroundCheckInformationDTO backgroundCheckInformationDTO = new();

        backgroundCheckInformationDTO.CivilOffences = employee.CivilOffences;
        backgroundCheckInformationDTO.TrafficViolation = employee.TrafficViolation;
        backgroundCheckInformationDTO.CriminalRecordCheck = employee.CriminalRecordCheck;

        if(!backgroundCheckInformationDTO.CivilOffences && !backgroundCheckInformationDTO.CivilOffences && !backgroundCheckInformationDTO.TrafficViolation)
        {
            backgroundCheckInformationDTO.IsVerified = true;
        }

        else
        {
            backgroundCheckInformationDTO.IsVerified = false;
        }

        return backgroundCheckInformationDTO;
    }

}
