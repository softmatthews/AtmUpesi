using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils
{
    internal static class Utilities
    {
        internal static string ExtractGeneralVersion(string version)
        {
            string[] versionArray = version.Split(":");
            string[] messageArray = versionArray[^1].Split(".");
            return $"{messageArray?[0]?.ToUpper()}{messageArray?[1]?.ToUpper()}";
        }

        internal static int IndexOfNth(this string input,
                             string value, int startIndex, int nth)
        {
            if (nth < 1)
                throw new NotSupportedException("Param 'nth' must be greater than 0!");
            if (nth == 1)
                return input.IndexOf(value, startIndex);
            var idx = input.IndexOf(value, startIndex);
            if (idx == -1)
                return -1;
            return input.IndexOfNth(value, idx + 1, --nth);
        }

        internal static bool IsISOTransaction(string transactionType)
        {
            return EISOTransactions.TryParse(transactionType, out EISOTransactions _);
        }

        internal static bool IsMTTransaction(string transactionType)
        {
            return EMTTransactions.TryParse(transactionType, out EMTTransactions _);
        
        }

    }
}
