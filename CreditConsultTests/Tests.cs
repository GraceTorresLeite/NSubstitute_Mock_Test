using CreditConsult;
using System;
using Xunit;
using NSubstitute;
using System.Collections.Generic;

namespace CreditConsultTests
{
    public class Tests
    {
        private const string CPF_INVALID = "123A";
        private const string CPF_ERROR = "76217486300";
        private const string CPF_NO_PENDENCY = "60487583752";
        private const string CPF_DEFAULTER = "82226651209";

        private IServiceCreditConsult mock;

        public Tests()
        {
            mock = Substitute.For<IServiceCreditConsult>();

            mock.ToConsultPendenciesForCPF(CPF_INVALID)
                .Returns((List<Pendency>)null);

            mock.ToConsultPendenciesForCPF(CPF_ERROR)
                .Returns(s => { throw new Exception("Error comunication"); });

            mock.ToConsultPendenciesForCPF(CPF_NO_PENDENCY)
                .Returns(new List<Pendency>());

            Pendency pendency = new Pendency();
            pendency.CPF = CPF_DEFAULTER;
            pendency.NamePerson = "Fulano";
            pendency.NameClaimant = "Casas Bahia";
            pendency.DeclarationPendency = "installment not paid";
            pendency.ValuePendency = 700;
            List<Pendency> pendencies = new List<Pendency>();
            pendencies.Add(pendency);

            mock.ToConsultPendenciesForCPF(CPF_DEFAULTER)
                .Returns(pendencies);
        }

        private StatusCreditConsult GetStatusAnalyzeCredit(string cpf) 
        {
            AnalyzeCredit analyze = new AnalyzeCredit(mock);
            return analyze.ToConsultSituationCPF(cpf);
        }

        [Fact]
        public void TestParameterInvalid() 
        {
            StatusCreditConsult status = GetStatusAnalyzeCredit(CPF_INVALID);
            Assert.Equal(StatusCreditConsult.ParameterSendInvalid , status);
        }

        [Fact]
        public void TestaErrorComunication()
        {
            StatusCreditConsult status = GetStatusAnalyzeCredit(CPF_ERROR);
            Assert.Equal(StatusCreditConsult.ErrorComunication, status);
        }

        [Fact]
        public void TestCPFNoPendencies()
        {
            StatusCreditConsult status = GetStatusAnalyzeCredit(CPF_NO_PENDENCY);
            Assert.Equal(StatusCreditConsult.NoPendencies, status);
        }

        [Fact]
        public void TestDefaulter()
        {
            StatusCreditConsult status = GetStatusAnalyzeCredit(CPF_DEFAULTER);
            Assert.Equal(StatusCreditConsult.Defaulter, status);
        }
    }
}
