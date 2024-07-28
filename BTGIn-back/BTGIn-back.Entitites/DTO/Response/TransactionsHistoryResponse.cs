namespace BTGIn_back.Entitites.DTO.Response
{
    public class TransactionsHistoryResponse
    {
        public required ClientDto Client { get; set; }
        public List<TransactionHistoryDto> TransactionHistory { get; set; } = [];
        public List<FundDto> FundsAvailable { get; set; } = [];
        public List<FundDto> RegisteredFunds { get; set; } = [];
    }
}
