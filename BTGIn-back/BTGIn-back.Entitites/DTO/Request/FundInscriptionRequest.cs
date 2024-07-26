namespace BTGIn_back.Entitites.DTO.Request
{
    public class FundInscriptionRequest
    {
        public required string ClientName { get; set; }
        public required int ClientIdentification { get; set; }
        public required string FundName { get; set; }
        public required double InscriptionCapital { get; set; }
    }
}
