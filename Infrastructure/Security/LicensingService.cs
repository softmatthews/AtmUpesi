using Application.Core.License;
using Application.Interfaces.Security;
using Newtonsoft.Json;

namespace Infrastructure.Security
{
	public class LicensingService : ILicensingService
	{
		private LicenseCache _licenseCache { get; }
		public LicensingService(LicenseCache licenseCache)
		{
			_licenseCache = licenseCache;
		}

		/// <summary>
		/// Check if feature exists and is not expired
		/// </summary>
		/// <param name="feature"></param>
		/// <returns></returns>
		public bool IsFeatureAvailable(string feature)
		{
			var licenseInfo = _licenseCache.GetLicenseInfo() ?? throw new Exception("No license information found.");

			return licenseInfo.AllowedFeatures.ContainsKey(feature) && licenseInfo.AllowedFeatureUsage[feature] > DateTime.Now;
		}

		/// <summary>
		/// Checks if package exists and whole license is not expired
		/// </summary>
		/// <param name="package"></param>
		/// <returns></returns>
		public bool IsPackageAvailable(string package)
		{
			var licenseInfo = _licenseCache.GetLicenseInfo() ?? throw new Exception("No license information found.");
			return licenseInfo.AllowedPackages.Contains(package) && licenseInfo.ExpirationDate > DateTime.Now;
		}

		public List<string> GetRelatedFeatures(string prefix)
		{
			var licenseInfo = _licenseCache.GetLicenseInfo() ?? throw new Exception("No license information found.");
			
			var features = licenseInfo.AllowedFeatures
				.Where(x => x.Key.StartsWith(prefix) && IsFeatureAvailable(x.Key))
				.Select(x => x.Key).ToList();

			return features;
		}
		
		public bool IsValidATM(string activeATM)
		{
			var licenseInfo = _licenseCache.GetLicenseInfo() ?? throw new Exception("No license information found.");

			if (activeATM == null || activeATM.Length < 7)
			{
				return false;
			}

			if (activeATM.Length > 8)
			{
				return licenseInfo.AllowedATMS.Any(x => x.Equals(activeATM));
			}

			return licenseInfo.AllowedATMS.Any(x => x[0..8].Equals(activeATM[0..8]));
		}
	}
}