using BTGIn_back.Entitites.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTGIn_backTest.Utils
{
    public static class MockRequestGenerator
    {
        public static FundInscriptionRequest GetFundInscriptionRequest(bool alterFundName = false, bool alterInscriptionCapital = false)
        {
            FundInscriptionRequest fundInscriptionRequest = new()
            {
                ClientName = "uno",
                ClientIdentification = 123,
                FundName = alterFundName ? "xx" : "FDO-ACCIONES",
                InscriptionCapital = alterInscriptionCapital ? 10 :325000
            };

            return fundInscriptionRequest;
        }
    }
}
