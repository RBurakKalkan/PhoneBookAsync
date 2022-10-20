using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReportApi.DataLayer;
using ReportApi.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ReportApi.Controllers
{

    public class ReportsController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private ReportsDataContext context;
        public IConnectionMultiplexer ConnectionMultiplexer;
        public ReportsController(ReportsDataContext context, IConfiguration config, IConnectionMultiplexer connection)
        {
            this.context = context;
            this.Configuration = config;
            this.ConnectionMultiplexer = connection;
        }
        public async Task<List<Contracts>> GetJson()
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
            var rss = JsonConvert.DeserializeObject<List<Contracts>>(responseString);
            return rss;
        }

        [HttpGet("GetReports")]
        public async Task<List<Reports>> GetReports()
        {
            var data = await (from c in context.Reports select c).ToListAsync();
            return data;
        }
        [HttpGet("GetReportDetails/{ReportId}")]
        public async Task<List<Reports>> GetReportDetails(int ReportId)
        {
            var data = await (from c in context.Reports where c.ReportsId == ReportId select c).ToListAsync();
            return data;
        }
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReportViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("GetStatisticReport/{Location}")]
        public async Task<IActionResult> GetStatisticReport(string Location)
        {
            var pubSub = ConnectionMultiplexer.GetSubscriber(); 
            pubSub.Subscribe("report-q", (channel, message) => DisplayMessage(message));
            pubSub.Publish("report-q","Report Preparing...");

            Reports reports = new()
            {
                ReportDate = DateTime.Now,
                ReportStatus = ReportStatus.Preparing
            };
            await context.Reports.AddAsync(reports);
            await context.SaveChangesAsync();

            var rss = await GetJson();

            var data = rss.SelectMany(x => x.ContractsInfo, (parent, child) => new { child.ContractsId, child.InfoType, child.InfoValue }).ToList();

            int[] ContractsOnLocation = data.Where(x => x.InfoValue == Location.ToUpper()) // gets the contracts IDs who has a phone in the specified location
                                            .Select(x => x.ContractsId).ToArray();
            var PhoneCount = (from c in data
                              where ContractsOnLocation.Contains(c.ContractsId) &&
                              c.InfoType == InfoType.PhoneNumber
                              select c).ToList().Count;

            var ContractCount = data.Where(x => x.InfoValue == Location.ToUpper())
                                    .GroupBy(g => g.ContractsId).ToList().Count;

            var list = new List<ReportViewModel>();
            var viewModel = new ReportViewModel();

            viewModel.Location = Location.ToUpper();
            viewModel.TotalContract = ContractCount;
            viewModel.TotalPhone = PhoneCount;
            list.Add(viewModel);

            reports.ReportStatus = ReportStatus.Done;
            context.Reports.Attach(reports);
            await context.SaveChangesAsync();
            await pubSub.PublishAsync("report-q", "Report is ready!");
            await pubSub.PublishAsync("report-q",JsonConvert.SerializeObject(list));
            return Ok(list);
        }

        private void DisplayMessage(RedisValue message)
        {

        }
    }
}