using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    /// <summary>
    /// Application wide traffic type categorization
    /// </summary>
    public enum ETrafficType
    {
        INPUT,
        OUTPUT
    }

    /// <summary>
    /// Abstract view of common transaction details
    /// </summary>
    public enum ETransactionMessageDetails
    {
        Beneficiary,
        Beneficiary_Name,
        InstitutionBeneficiaryName,
        OrderingCustomer,
        OrderingInstitution,
        AccountWithInstitution,
        InstitutionAccountWithInstitution,
        Sender,
        Receiver,
        Customer_name,
        Senders_reference,
        InstrId,
        MessageReference,
        ServiceCode,
        Group,
        Traffic,
    }
}
