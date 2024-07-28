namespace BTGIn_back.Entitites.DTO
{
    public class FundDto
    {
        public required string Name { get; set; }
        public double MinimumRegistrationAmount { get; set; }
        public required string Category { get; set; }
        public double? InscriptionCapital { get; set; }
    }
}
