using ContractApi.DataLayer;
using ContractApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContractApi.Controllers
{
    public class ContractsInfoController : Controller
    {
        private ContractsDataContext context;
        public ContractsInfoController(ContractsDataContext context)
        {
            this.context = context;
        }

        [HttpPost("AddContractInfo/{ContractId}/{InfoType}/{InfoValue}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contracts))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddContractInfo(int ContractId, InfoType InfoType, string InfoValue)
        {
            if (!await context.Contracts.AnyAsync(x => x.ContractsId == ContractId)) // If Contract does not exist, do not add info.
            {
                return NotFound("Contract Not Found.");
            }
            if (await context.ContractsInfo.AnyAsync(x => x.InfoType == InfoType.Location && x.ContractsId == ContractId && InfoType == InfoType.Location))// If Contact has a location do not add one.
            {
                return NotFound("Contract already has a Location Info");
            }
            ContractsInfo contractsInfo = new()
            {
                ContractsId = ContractId,
                InfoType = InfoType,
                InfoValue = InfoValue
            };

            await context.ContractsInfo.AddAsync(contractsInfo);
            await context.SaveChangesAsync();
            return Ok(contractsInfo);
        }

        [HttpDelete("RemoveContractInfo/{ContractInfoId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contracts))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveContractInfo(int ContractInfoId)
        {
            if (!await context.ContractsInfo.AnyAsync(x => x.ContractsInfoId == ContractInfoId))
            {
                return NotFound("Contract Not Found.");
            }
            var ContractInfo = await context.ContractsInfo
           .Where(c => c.ContractsInfoId == ContractInfoId)
           .FirstOrDefaultAsync();
            context.ContractsInfo.Remove(ContractInfo);
            await context.SaveChangesAsync();
            return Ok(ContractInfo);
        }

    }
}
