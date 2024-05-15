namespace ApiMobileNetwork.Entities
{
    public class SignalData
    {
        public Guid Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string Level { get; set; }
        public int Dbm { get; set; }
        public int? Rsrp { get; set; }
        public int? Rsrq { get; set; }
        public int? Rssi { get; set;}
        public int? Rssnr { get; set;}
        public int? Ecio { get; set;}
        public int? EvdoSnr { get; set;}
        public string Operator { get; set; }
        public DateTime Date { get; set; }
        public string? SpeedTest { get; set; }
        public int? Latency { get; set; }
    }
    public class ItemSignal
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
    }
}
