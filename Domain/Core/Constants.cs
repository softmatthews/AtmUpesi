using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    /// <summary>
    /// Encapsulates some fields that should be handled specially due to format considerations
    /// </summary>
    public class Constants
    {
        #region MT Related
        // Validations
        public static readonly IReadOnlySet<string> XCharSet = new HashSet<string>
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            "/", "-", "?", ":", "(", ")", ".", ",", "`", "+", " ", "\r\n"
        };
        public static readonly IReadOnlySet<string> YCharSet = new HashSet<string>
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            " ", ".", ",", "-", "(", ")", "/", "=", "`", "+", ":", "?",
            // Incompatible with international telex
            "!", "\"", "%", "&", "*", ";", "<", ">"
        };
        public static readonly IReadOnlySet<string> ZCharSet = new HashSet<string>
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ".", ",", "-", "(", ")", "/", "=", "'", "+", ":", "?", "@", "#", "\r\n", " ", "{"


        };
        public static readonly IReadOnlySet<string> ValidationCharSet = new HashSet<string>
        {
            "-", "!", "*", "n", "a", "x", "y", "z", "c", "h", "e", "A", "B"
        };
        public static readonly IReadOnlySet<string> RawExceptions = new HashSet<string>()
        {
            "32A", "32B",
        };
         public static readonly IReadOnlySet<string> BoxExceptions = new HashSet<string>()
        {
            "32B","33B","32G","33E","32H","34E","60F","62F","64",
        };
        public static readonly IReadOnlySet<string> SAAExceptions = new HashSet<string>()
        {
            "32A"
        };
        // https://www.prowidesoftware.com/resources/SWIFT-JSON
        // Source of truth for common headers
        public static readonly IReadOnlySet<string> Block1Keys = new HashSet<string>
        {
            "AppID", "ServiceID"
        };
        #endregion

    }
}
