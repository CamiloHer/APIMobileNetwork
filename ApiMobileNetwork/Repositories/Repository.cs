using ApiMobileNetwork.Entities;
using System.Data;
using System.Data.SqlClient;

namespace ApiMobileNetwork.Repositories
{
    public class Repository : IRepository
    {
        private readonly string _conectionString;
        public Repository(IConfiguration configuration) { _conectionString = configuration.GetConnectionString("defaultConnection"); }
        public async Task<SignalData> GetSignalData(Guid id)
        {
            using(SqlConnection sql = new SqlConnection(_conectionString))
            {
                using(SqlCommand cmd = new SqlCommand("GetSignalDataById", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    SignalData response = new SignalData();
                    await sql.OpenAsync();
                    using( var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToSignalData(reader);
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<List<ItemSignal>> GetSignals()
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(_conectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("getSignalsData", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        List<ItemSignal> response = new List<ItemSignal>();
                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(MapToItemSignal(reader));
                            }
                        }
                        return response;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private SignalData MapToSignalData(SqlDataReader reader)
        {
            return new SignalData()
            {
                Id = new Guid(reader["Id"].ToString()),
                Longitude = (decimal)reader["Longitude"],
                Latitude = (decimal)reader["Latitude"],
                Level = reader["Description"].ToString(),
                Dbm = (int)reader["Dbm"],
                Rsrp = string.IsNullOrEmpty(reader["Rsrp"].ToString())? null : (int?)reader["Rsrp"],
                Rsrq = string.IsNullOrEmpty(reader["Rsrq"].ToString()) ? null : (int?)reader["Rsrq"],
                Rssi = string.IsNullOrEmpty(reader["Rssi"].ToString()) ? null : (int?)reader["Rssi"],
                Rssnr = string.IsNullOrEmpty(reader["Rssnr"].ToString()) ? null : (int?)reader["Rssnr"],
                Ecio = string.IsNullOrEmpty(reader["Ecio"].ToString()) ? null : (int?)reader["Ecio"],
                EvdoSnr = string.IsNullOrEmpty(reader["EvdoSnr"].ToString()) ? null : (int?)reader["EvdoSnr"],
                Operator = reader["Name"].ToString(),
                Date = (DateTime)reader["CaptureDate"],
                SpeedTest = reader["TransferSpeed"].ToString(),
                Latency = string.IsNullOrEmpty(reader["Latency"].ToString()) ? null : (int?)reader["Latency"]
            };
        }
        private ItemSignal MapToItemSignal(SqlDataReader reader)
        {
            return new ItemSignal()
            {
                Id = new Guid(reader["Id"].ToString()),
                Date = (DateTime)reader["CaptureDate"]
            };
        }

        public async Task<bool> SaveSignalData(DataTable signal)
        {
            try
            {
                using (SqlConnection sql = new SqlConnection(_conectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertSignalInidators", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@signalData", signal));
                        bool response = false;
                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response = (bool)reader["Result"];
                            }
                        }
                        return response;
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}
