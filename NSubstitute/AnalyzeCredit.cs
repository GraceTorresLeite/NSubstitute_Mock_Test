using System.Collections.Generic;

namespace CreditConsult
{
    public class AnalyzeCredit
    {
        private IServiceCreditConsult _serviceCreditConsult;

        public AnalyzeCredit(IServiceCreditConsult serviceCreditConsult)
        {
            _serviceCreditConsult = serviceCreditConsult;
        }

        public StatusCreditConsult ToConsultSituationCPF(string cpf) 
        {
            try 
            {
                IList<Pendency> pendencies = _serviceCreditConsult.ToConsultPendenciesForCPF(cpf);

                if (pendencies == null)
                    return StatusCreditConsult.ParameterSendInvalid;
                else if (pendencies.Count == 0)
                    return StatusCreditConsult.NoPendencies;
                else
                    return StatusCreditConsult.Defaulter;
            }
            catch 
            {
                return StatusCreditConsult.ErrorComunication;
            }

        }
    }
}
