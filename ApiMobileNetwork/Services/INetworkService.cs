using ApiMobileNetwork.Entities;

namespace ApiMobileNetwork.Services
{
    public interface INetworkService
    {
        Task<List<ItemSignal>> GetSignals();
        Task<SignalData> GetSignalData(Guid id);
        Task<bool> SaveSignalData(SignalData signal);
    }
}
