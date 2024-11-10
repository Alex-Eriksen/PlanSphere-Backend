using System.Text.RegularExpressions;
using PlanSphere.Core.Constants;

namespace PlanSphere.Core.Utilities.Builder;

public class PathBuilder
{
    private string path;

    public PathBuilder AddOrganisationDirectory(ulong organisationId)
    {
        path = $"{FileStorageConstants.OrganisationDirectory}/{organisationId}";
        return this;
    }
    
    public PathBuilder AddCompanyDirectory(ulong companyId)
    {
        path = $"{FileStorageConstants.CompanyDirectory}/{companyId}";
        return this;
    }
    
    public PathBuilder AddDepartmentDirectory(ulong departmentId)
    {
        path = $"{FileStorageConstants.DepartmentDirectory}/{departmentId}";
        return this;
    }
    
    public PathBuilder AddTeamDirectory(ulong teamId)
    {
        path = $"{FileStorageConstants.TeamDirectory}/{teamId}";
        return this;
    }
    
    public PathBuilder AddUserDirectory(string userId)
    {
        path = $"{path}/{FileStorageConstants.UserDirectory}/{userId}";
        return this;
    }

    public PathBuilder AddProfilePicture(string fileName)
    {
        string cleanedFileName = Regex.Replace(fileName, RegularExpressions.ValidFileName, "_");
        path = $"{path}/{FileStorageConstants.ProfilePictureDirectory}/{cleanedFileName}";
        return this;
    }
    
    public PathBuilder AddLogo(string fileName)
    {
        string cleanedFileName = Regex.Replace(fileName, RegularExpressions.ValidFileName, "_");
        path = $"{path}/{FileStorageConstants.LogoPath}/{cleanedFileName}";
        return this;
    }
    
    public string GeneratePath() => path;
}
