using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Exceptions
{
    public class ISOSchemaNotFoundException: Exception
    {
        public string MissingVersion { get; set; }
        public string AdditionalDetails { get; set; }
        
        public ISOSchemaNotFoundException(string missingVersion, string additionalDetails)
        {
            MissingVersion = missingVersion;
            AdditionalDetails = additionalDetails;
        }
    }
}
