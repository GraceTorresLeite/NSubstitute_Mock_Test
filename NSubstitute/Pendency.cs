using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditConsult
{
    public class Pendency
    {
        public string CPF { get; set; }
        public string NamePerson { get; set; }
        public string NameClaimant { get; set; }
        public string DeclarationPendency { get; set; }
        public DateTime DatePendency { get; set; }
        public double ValuePendency { get; set; }
    }
}
