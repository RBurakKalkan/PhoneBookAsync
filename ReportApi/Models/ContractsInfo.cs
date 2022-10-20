

namespace ReportApi.Models
{
    public class ContractsInfo
    {
        public int ContractsInfoId { get; set; }
        public int ContractsId { get; set; }
        public InfoType InfoType { get; set; }
        public string InfoValue { get; set; }
    }
    public enum InfoType
    {
        PhoneNumber,
        EmailAdress,
        Location
    }
}
