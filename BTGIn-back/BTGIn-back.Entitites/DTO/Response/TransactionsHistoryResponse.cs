namespace BTGIn_back.Entitites.DTO.Response
{
    public class TransactionsHistoryResponse
    {
        public required string Type { get; set; }
        public required double Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsAcepted { get; set; }
        public required double ClientCash { get; set; }
        public required string FundName { get; set; }
        public required string FundCategory { get; set; }
    }
}
