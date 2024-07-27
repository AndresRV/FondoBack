using BTGIn_back.Entitites;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTGIn_backTest.Utils
{
    public static class MockResponsesGenerator
    {
        public static Client GetByIdentificationAsyncResult()
        {
            Client client = new()
            {
                Id = "qwer1234asdf",
                Name = "uno",
                Identification = 123,
                Cash = 500000,
                Funds = new() { new() { Name = "FDO-ACCIONES"} }
            };

            return client;
        }

        public static Fund GetByNameAsyncResult()
        {
            Fund fund = new()
            {
                MinimumRegistrationAmount = 20000
            };

            return fund;
        }
    }
}
