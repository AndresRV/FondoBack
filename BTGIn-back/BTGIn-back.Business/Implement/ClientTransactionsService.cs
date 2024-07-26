using BTGIn_back.Business.Contracts;
using BTGIn_back.Business.Exceptions;
using BTGIn_back.Entitites;
using BTGIn_back.Entitites.DTO.Request;
using BTGIn_back.Entitites.DTO.Response;
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
        private const double INITIAL_FUNDS = 500000;
        private const string FUND_INSCRIPTION = "Inscripción";
        private const string FUND_CANCELLATION = "Cancelación";
        private const int BOGOTA_GMT = -5;

        public async Task FundInscription(FundInscriptionRequest fundInscriptionRequest)
        {
            Client client = await _clientRepository.GetByIdentificationAsync(fundInscriptionRequest.ClientIdentification);
            client ??= new()
                {
                    Cash = INITIAL_FUNDS,
                    Name = fundInscriptionRequest.ClientName,
                    Identification = fundInscriptionRequest.ClientIdentification,
                };
                        
            Fund fund = await _fundRepository.GetByNameAsync(fundInscriptionRequest.FundName);

            await ValidateFundNotRegistred(fundInscriptionRequest.FundName, fundInscriptionRequest.InscriptionCapital, client, fund);
            await ValidateSufficientCash(fundInscriptionRequest.InscriptionCapital, client, fund);

            client.Cash -= fundInscriptionRequest.InscriptionCapital;
            fund.InscriptionCapital = fundInscriptionRequest.InscriptionCapital;
            client.Funds.Add(fund);

            if (string.IsNullOrEmpty(client.Id))
                await _clientRepository.CreateAsync(client);
            else
                await _clientRepository.UpdateAsync(client.Id, client);

            await RecordTransactionAsync(FUND_INSCRIPTION, true, fundInscriptionRequest.InscriptionCapital, client, fund);
        }

        public async Task FundDisenrollment(FundDisenrollmentRequest fundDisenrollmentRequest)
        {
            Client client = await _clientRepository.GetByIdentificationAsync(fundDisenrollmentRequest.ClientIdentification)
                ?? throw new KeyNotFoundException("El cliente no está registrado");

            Fund fundToRemove = client.Funds?.Find(fund => fund.Name.Equals(fundDisenrollmentRequest.FundName))
                ?? throw new KeyNotFoundException("Actualmente no se encuentra inscrito en el fondo");

            double recoveredCapital = (double)fundToRemove.InscriptionCapital;
            client.Cash += recoveredCapital;
            client.Funds.Remove(fundToRemove);
            await _clientRepository.UpdateAsync(client.Id, client);
            await RecordTransactionAsync(FUND_CANCELLATION, true, recoveredCapital, client, fundToRemove);
        }

        public async Task<List<TransactionsHistoryResponse>> GetTransactionsHistory(int clientIdentification)
        {
            List<TransactionsHistoryResponse> transactionsHistoryResponse = [];

            List<Transaction> transactions = await _transactionRepository.GetTransactionsByClientIdentification(clientIdentification);
            transactions.ForEach(transaction => transactionsHistoryResponse.Add(
                new()
                {
                    Type = transaction.Type,
                    Amount = transaction.Amount,
                    Date = transaction.Date,
                    IsAcepted = transaction.IsAcepted,
                    ClientCash = transaction.Client.Cash,
                    FundName = transaction.Fund.Name,
                    FundCategory = transaction.Fund.Category
                })
            );

            return transactionsHistoryResponse;
        }

        #region private
        private async Task ValidateFundNotRegistred(string fundName, double inscriptionCapital, Client client, Fund fund)
        {
            if (client.Funds.Any(fund => fund.Name.Equals(fundName)))
            {
                if (!string.IsNullOrEmpty(client.Id))
                    await RecordTransactionAsync(FUND_INSCRIPTION, false, inscriptionCapital, client, fund);

                throw new FundAlreadyRegistredException("Actualmente se encuentra inscrito en el fondo");
            }
        }

        private async Task ValidateSufficientCash(double inscriptionCapital, Client client, Fund fund)
        {
            if (client.Cash - inscriptionCapital < 0 || fund.MinimumRegistrationAmount > inscriptionCapital)
            {
                if (!string.IsNullOrEmpty(client.Id))
                    await RecordTransactionAsync(FUND_INSCRIPTION, false, inscriptionCapital, client, fund);

                throw new InsufficientCashException("La cantidad a invertir no es correcta");
            }
        }

        private async Task RecordTransactionAsync(string type, bool isAcepted, double inscriptionCapital, Client client, Fund fund)
        {
            client.Funds = null;
            fund.InscriptionCapital = null;
            Transaction transaction = new()
            {
                Type = type,
                Amount = inscriptionCapital,
                Date = DateTime.UtcNow.AddHours(BOGOTA_GMT),
                IsAcepted = isAcepted,
                Client = client,
                Fund = fund
            };

            await _transactionRepository.CreateAsync(transaction);
        }
        #endregion
    }
}
