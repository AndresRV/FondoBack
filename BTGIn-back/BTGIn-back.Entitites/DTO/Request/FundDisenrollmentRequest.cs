namespace BTGIn_back.Entitites.DTO.Request
{
    public class FundDisenrollmentRequest
    {
        public required int ClientIdentification { get; set; }
        public required string FundName { get; set; }
    }
}
