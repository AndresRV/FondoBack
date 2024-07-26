using BTGIn_back.Entitites;

namespace BTGIn_back.Repositories.Contracts
{
    public interface IFundRepository
    {
        Task<Fund> GetByNameAsync(string name);
    }
}
