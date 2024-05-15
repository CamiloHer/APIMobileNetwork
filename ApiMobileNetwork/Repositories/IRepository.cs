using ApiMobileNetwork.Entities;
using System.Data;
using System.Data.SqlClient;

namespace ApiMobileNetwork.Repositories
{
    public interface IRepository
    {
        Task<List<ItemSignal>> GetSignals();
        Task<SignalData> GetSignalData(Guid id);
        Task<bool> SaveSignalData(DataTable signal);
    }
}
