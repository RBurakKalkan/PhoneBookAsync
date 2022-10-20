using ContractApi.DataLayer;
using ContractApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ContractApi.Controllers
{
    public class ContractsController : Controller
    {
        private ContractsDataContext context;
        public ContractsController(ContractsDataContext context)
        {
            this.context = context;
        }

        [HttpGet("GetContractsAndInfos")]
        public async Task<IEnumerable<Contracts>> GetContractsAndInfos()
        {
            var query = await context.Contracts.Include(c => c.ContractsInfo).Select(c => new Contracts
            {
                ContractsId = c.ContractsId,
                Name = c.Name,
                Surname = c.Surname,
                FirmName = c.FirmName,
                ContractsInfo = c.ContractsInfo
            }).ToArrayAsync();

            return query;
        }
        [HttpGet("GetContracts")]
        public async Task<IEnumerable<Contracts>> GetContracts()
        {
            var query = await context.Contracts.Select(c => new Contracts
            {
                ContractsId = c.ContractsId,
                Name = c.Name,
                Surname = c.Surname,
                FirmName = c.FirmName
            }).ToArrayAsync();

            return query;
        }
        [HttpGet("GetContractDetails/{ContractId}")]
        public async Task<IEnumerable<Contracts>> GetContractsDetails(int ContractId)
        {
            var data = await GetContractsAndInfos();
            var query = data.Where(c => c.ContractsId == ContractId).ToArray();

            return query;
        }

        [HttpPost("AddContract/{Name}/{Surname}/{FirmName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contracts))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddContract(string Name, string Surname,string FirmName)
        {
            Contracts contracts = new()
            {
                Name = Name,
                Surname = Surname,
                FirmName = FirmName
            };

            await context.Contracts.AddAsync(contracts);
            await context.SaveChangesAsync();
            return Ok(contracts);

        }

        [HttpDelete("RemoveContract/{ContractId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contracts))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveContract(int ContractId)
        {
            if (!await context.Contracts.AnyAsync(x => x.ContractsId == ContractId))
            {
                return NotFound("Contract Not Found.");
            }
            var Contract = await context.Contracts
                .Where(c => c.ContractsId == ContractId)
                .FirstOrDefaultAsync();

            var ContractInfo = await context.ContractsInfo
                .Where(c => c.ContractsId == ContractId)
                .ToArrayAsync();

            context.ContractsInfo.RemoveRange(ContractInfo);
            context.Contracts.Remove(Contract);
            await context.SaveChangesAsync();
            return Ok(Contract);
        }
    }
}
