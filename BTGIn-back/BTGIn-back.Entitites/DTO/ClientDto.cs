namespace BTGIn_back.Entitites.DTO
{
    public class ClientDto
    {
        public required string Name { get; set; }
        public required int Identification { get; set; }
        public required double Cash { get; set; }
        public int CountRegisteredFunds { get; set; }
    }
}
