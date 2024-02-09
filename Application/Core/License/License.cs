using ChoETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.License
{
	public class LicenseInfo
	{
		public string LicenseKey { get; } = null!;
		public DateTime ExpirationDate { get; }
		public string[] AllowedATMS { get; } = Array.Empty<string>();
		public string[] AllowedPackages { get; } = Array.Empty<string>();
		public Dictionary<string, string[]> AllowedFeatures { get; } = new();
		public Dictionary<string, DateTime> AllowedFeatureUsage { get; } = new();

		public LicenseInfo
				(
						string licenseKey,
						DateTime expirationDate,						
						string[] allowedATMS,
						string[] allowedPackages,
						Dictionary<string, string[]> allowedFeatures,
						Dictionary<string, DateTime> allowedFeatureUsage
				)
		{
			LicenseKey = licenseKey;
			ExpirationDate = expirationDate;
			AllowedATMS = allowedATMS;
			AllowedPackages = allowedPackages;
			AllowedFeatures = allowedFeatures;
			AllowedFeatureUsage = allowedFeatureUsage;
		}
	}

	public class LicenseCache
	{
		private static readonly LicenseCache instance = new();
		private static readonly object lockObject = new();
		private LicenseInfo? licenseInfo;

		private LicenseCache()
		{
			licenseInfo = null;
		}

		public static LicenseCache Instance
		{
			get
			{
				return instance;
			}
		}

		public LicenseInfo? GetLicenseInfo()
		{
			lock (lockObject)
			{
				return licenseInfo;
			}
		}

		/// <summary>
		/// Temporary placeholder function
		/// Need to protect it from being seen from memory as well, encrypt and decrypt when needed
		/// TODO: Remove
		/// </summary>
		/// <param name="licenseFilePath"></param>
		public void LoadLicense(string licenseFilePath)
		{
			string[] mainPackages = { "WITHRAW", "DEPOSIT" };
            string? licenseKey = "UPESI";
            DateTime expirationDate = DateTime.Parse("2025-05/05");
			string[] allowedATMS = { "TWN", "NGONG" }
;
            string[] otherFeatures = {"KSHS","TZS"};

			Dictionary<string, string[]> mainFeatures = new();
            Dictionary<string, DateTime> featureUsage = new();
            mainFeatures.Add("WITHRAW", otherFeatures);
			featureUsage.Add("WITHRAW", expirationDate);
						
			licenseInfo = new LicenseInfo(
							licenseKey,
							expirationDate,
							allowedATMS,
							mainPackages,
							mainFeatures,
							featureUsage
);
			
		}

		/* Hook up encryption and decryption methodology here
		public void LoadLicense(string licenseFilePath, byte[] encryptionKey)
		{
				lock (lockObject)
				{
						using (Aes aes = Aes.Create())
						{
								aes.Key = encryptionKey;

								using (FileStream fileStream = new FileStream(licenseFilePath, FileMode.Open, FileAccess.Read))
								using (CryptoStream cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
								using (StreamReader reader = new StreamReader(cryptoStream))
								{
										string licenseKey = reader.ReadLine();
										DateTime expirationDate = DateTime.Parse(reader.ReadLine());
										string[] allowedFeatures = reader.ReadLine().Split(',');

										licenseInfo = new LicenseInfo
										{
												LicenseKey = licenseKey,
												ExpirationDate = expirationDate,
												AllowedFeatures = allowedFeatures
										};
								}
						}
				}
		}*/
	}
}
