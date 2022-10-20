using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReportApi.DataLayer;
using ReportApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReportApi.Controllers
{

    public class ReportsController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private ReportsDataContext context;
        public ReportsController(ReportsDataContext context, IConfiguration config)
        {
            this.context = context;
            this.Configuration = config;
        }
        public async Task<string> GetJson()
        {
            var url = Configuration["ContractApi"] + "/GetContractsAndInfos";
            var responseString = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";

            using (var response = await request.GetResponseAsync())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    responseString = await reader.ReadToEndAsync();
                }
            }
            return responseString;
        }
        [HttpGet("GetContractCount")]
        public async Task<List<Contracts>> GetContractCount(string Location)
        {
            string json = await GetJson();

            var rss = JsonConvert.DeserializeObject<List<Contracts>>(json);

            var data = rss.SelectMany(x => x.ContractsInfo, (parent, child) => new { child.ContractsId, child.InfoType, child.InfoValue }).ToList();

            int[] ContractsOnLocation = data.Where(x => x.InfoValue == Location)
                                            .Select(x => x.ContractsId).ToArray();
            var PhoneCount = (from c in data 
                              where ContractsOnLocation.Contains(c.ContractsId) &&
                              c.InfoType == InfoType.PhoneNumber select c).ToList().Count;

            var ContractCount = data.Where(x => x.InfoValue == Location)
                                    .GroupBy(g => g.ContractsId).ToList().Count;
            return rss;
        }
    }
}
