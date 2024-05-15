using ApiMobileNetwork.Entities;
using ApiMobileNetwork.Repositories;
using System.Data;

namespace ApiMobileNetwork.Services
{
    public class NetworkService : INetworkService
    {
        private readonly IRepository _repository;
        public NetworkService(IRepository repository) { _repository = repository; }
        public async Task<SignalData> GetSignalData(Guid id)
        {
            return await _repository.GetSignalData(id);
        }

        public async Task<List<ItemSignal>> GetSignals()
        {
            return await _repository.GetSignals();
        }

        public async Task<bool> SaveSignalData(SignalData signal)
        {
            DataTable item = new DataTable();
            item.Columns.Add("Id");
            item.Columns.Add("Longitude", typeof(Decimal));
            item.Columns.Add("Latitude", typeof(Decimal));
            item.Columns.Add("Level", typeof(Int32));
            item.Columns.Add("Dbm", typeof(Int32));
            item.Columns.Add("Rsrp", typeof(Int32));
            item.Columns.Add("Rsrq", typeof(Int32));
            item.Columns.Add("Rssi", typeof(Int32));
            item.Columns.Add("TransferSpeed", typeof(Decimal));
            item.Columns.Add("Latency", typeof(Int32));
            item.Columns.Add("Operator", typeof(Int32));
            item.Columns.Add("CaptureDate", typeof(DateTime));
            item.Columns.Add("Rssnr", typeof(Int32));
            item.Columns.Add("ecio", typeof(Int32));
            item.Columns.Add("evdoSnr", typeof(Int32));

            item.NewRow();
            item.Rows.Add(signal.Id,
                signal.Longitude,
                signal.Latitude,
                Convert.ToInt32(signal.Level),
                signal.Dbm,
                signal.Rsrp,
                signal.Rsrq,
                signal.Rssi,
                signal.SpeedTest,
                signal.Latency,
                Convert.ToInt32(signal.Operator),
                signal.Date,
                signal.Rssnr,
                signal.Ecio,
                signal.EvdoSnr);

            return await _repository.SaveSignalData(item);
        }
    }
}
