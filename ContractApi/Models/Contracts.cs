
using System.Collections.Generic;

namespace ContractApi.Models
{
    public class Contracts
    {
        public int ContractsId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<ContractsInfo> ContractsInfo { get; set; }
    }
}
