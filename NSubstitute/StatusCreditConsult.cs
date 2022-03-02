using System;

namespace CreditConsult
{
    public enum StatusCreditConsult
    {
        ParameterSendInvalid = -2,
        ErrorComunication = -1, 
        NoPendencies = 0,
        Defaulter = 1,
    }
}
