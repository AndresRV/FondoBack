namespace BTGIn_back.Entitites.DTO
{
    public class TransactionHistoryDto
    {
        public required string Type { get; set; }
        public required double Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsAcepted { get; set; }
        public required string FundName { get; set; }
    }
}
