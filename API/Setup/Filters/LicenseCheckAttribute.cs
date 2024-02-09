namespace API.Setup.Filters
{
    public class LicenseCheckAttribute : Attribute
    {
        public string? Feature { get; set; }
        public string? Package { get; set; }

    }
}