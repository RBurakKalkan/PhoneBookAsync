using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportApi.Controllers;
using ReportApi.DataLayer;
using ReportApi.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class ReportControllerTest
    {
        private IConfiguration _config;

        private IConnectionMultiplexer ConnectionMultiplexer;

        ReportsController reportsController;

        ReportsDataContext _Context;
        public ReportControllerTest()
        {
            InitContext();
            ConnectionMultiplexer = A.Fake<IConnectionMultiplexer>();
            _config = A.Fake<IConfiguration>();
        }
        [TestMethod]
        public  void ReportsControllerTestStatsSuccess()
        {
            reportsController = new ReportsController(_Context, _config, ConnectionMultiplexer);
            var result =  reportsController.GetStatisticReport("Test");
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [TestMethod]
        public void ReportsControllerTestGetSuccess()
        {
            reportsController = new ReportsController(_Context, _config, ConnectionMultiplexer);
            var result = reportsController.GetReports();
            result.Should().BeOfType<Task<List<Reports>>>();
        }
        [TestMethod]
        public void ReportsControllerTestGetDetailSuccess()
        {
            reportsController = new ReportsController(_Context, _config, ConnectionMultiplexer);
            var result = reportsController.GetReportDetails(1);
            result.Should().BeOfType<Task<List<Reports>>>();
        }
        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<ReportsDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            var context = new ReportsDataContext(builder.Options);
            var books = Enumerable.Range(1, 10)
                .Select(i => new Reports { ReportsId = i, ReportDate = DateTime.Now, ReportStatus = ReportStatus.Done });
            context.Reports.AddRange(books);
            int changed = context.SaveChanges();
            _Context = context;
        }

    }
}
