using BTGIn_back.Business.Contracts;
using BTGIn_back.Business.Exceptions;
using BTGIn_back.Entitites;
using BTGIn_back.Entitites.DTO.Request;
using BTGIn_back.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Net;

namespace BTGIn_back.Business.Implement
{
    public class ClientTransactionsService(
        IClientRepository _clientRepository,
        IFundRepository _fundRepository,
        ITransactionRepository _transactionRepository
    ) : IClientTransactionsService
    {
        private const string FUND_INSCRIPTION = "Inscripción";
        private const string FUND_CANCELLATION = "Cancelación";
        private const int BOGOTA_GMT = -5;

        public async Task FundInscription(FundInscriptionRequest fundInscriptionRequest)
        {
            Client client = await _clientRepository.GetByIdentificationAsync(fundInscriptionRequest.ClientIdentification);
            client ??= new()
                {
                    Cash = 500000,
                    Name = fundInscriptionRequest.ClientName,
                    Identification = fundInscriptionRequest.ClientIdentification,
                };
                        
            Fund fund = await _fundRepository.GetByNameAsync(fundInscriptionRequest.FundName);

            await ValidateFundNotRegistred(fundInscriptionRequest, client, fund);
            await ValidateSufficientCash(fundInscriptionRequest, client, fund);

            client.Cash -= fundInscriptionRequest.InscriptionCapital;
            fund.InscriptionCapital = fundInscriptionRequest.InscriptionCapital;
            client.Funds.Add(fund);

            if (string.IsNullOrEmpty(client.Id))
                await _clientRepository.CreateAsync(client);
            else
                await _clientRepository.UpdateAsync(client.Id, client);

            await RecordTransactionAsync(FUND_INSCRIPTION, true, fundInscriptionRequest, client, fund);
        }

        #region private
        private async Task ValidateFundNotRegistred(FundInscriptionRequest fundInscriptionRequest, Client client, Fund fund)
        {
            if (client.Funds.Any(fund => fund.Name.Equals(fundInscriptionRequest.FundName)))
            {
                if (!string.IsNullOrEmpty(client.Id))
                    await RecordTransactionAsync(FUND_INSCRIPTION, false, fundInscriptionRequest, client, fund);

                throw new FundAlreadyRegistredException("Actualmente se encuentra inscrito en el fondo");
            }
        }

        private async Task ValidateSufficientCash(FundInscriptionRequest fundInscriptionRequest, Client client, Fund fund)
        {
            if (client.Cash - fundInscriptionRequest.InscriptionCapital < 0 || fund.MinimumRegistrationAmount > fundInscriptionRequest.InscriptionCapital)
            {
                if (!string.IsNullOrEmpty(client.Id))
                    await RecordTransactionAsync(FUND_INSCRIPTION, false, fundInscriptionRequest, client, fund);

                throw new InsufficientCashException("La cantidad a invertir no es correcta");
            }
        }

        private async Task RecordTransactionAsync(string type, bool isAcepted, FundInscriptionRequest fundInscriptionRequest, Client client, Fund fund)
        {
            client.Funds = null;
            fund.InscriptionCapital = null;
            Transaction transaction = new()
            {
                Type = type,
                Amount = fundInscriptionRequest.InscriptionCapital,
                Date = DateTime.UtcNow.AddHours(BOGOTA_GMT),
                IsAcepted = isAcepted,
                Client = client,
                Fund = fund
            };

            await _transactionRepository.CreateAsync(transaction);
        }
        #endregion



        /*
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task CreateAsync(Client entity)
        {
            await _clientRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            //await _clientRepository.DeleteAsync(id);
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return null; //await _clientRepository.GetAllAsync();
        }

        public async Task<Client> GetAsync(string id)
        {
            return null;// await _clientRepository.GetAsync(id);
        }

        public async Task UpdateAsync(string id, Client entity)
        {
            await _clientRepository.UpdateAsync(id, entity);
        }         
         
         
         */
    }
}
