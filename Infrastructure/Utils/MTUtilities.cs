using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils
{
    internal static class MTUtilities
    {
        internal static string RemoveTagValueFirstLine(string tagValue)
        {         
            string[] result = tagValue.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (result.Length > 1)
            {
                return String.Join(Environment.NewLine, result, 1, result.Length - 1);
            }
            else
            {
                return String.Join(Environment.NewLine, result);
            }

        }

        internal static string SplitTagValueFirstLine(string tagValue)
        {
            string[] result = tagValue.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (result.Length > 1)
            {
                return String.Join(Environment.NewLine, result, 1, result.Length - 1);
            }
            else
            {
                return String.Join(Environment.NewLine, result);
            }

        }
    }
}
