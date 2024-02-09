namespace Application.Interfaces.Security
{
    public interface ILicensingService
    {
        bool IsFeatureAvailable(string feature);
        bool IsPackageAvailable(string package);
        List<string> GetRelatedFeatures(string prefix);
        bool IsValidATM(string activeATM);
    }
}