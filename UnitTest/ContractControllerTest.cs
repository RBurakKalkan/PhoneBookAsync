using ContractApi.DataLayer;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportApi.Controllers;
using ContractApi.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContractApi.Controllers;

namespace UnitTest
{
    [TestClass]
    public class ContractControllerTest
    {

        ContractsController contractsController;

        ContractsDataContext _Context;
        public ContractControllerTest()
        {
            InitContext();
        }
        [TestMethod]
        public void ContractControllerAddContract()
        {
            contractsController = new ContractsController(_Context);
            var result = contractsController.AddContract("Test","Test","Test");
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [TestMethod]
        public void ContractControllerRemoveContract()
        {
            contractsController = new ContractsController(_Context);
            var result = contractsController.RemoveContract(1);
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [TestMethod]
        public void ContractControllerGetContractDetail()
        {
            contractsController = new ContractsController(_Context);
            var result = contractsController.GetContractsDetails(1);
            result.Should().BeOfType<Task<IEnumerable<Contracts>>>();
        }
        [TestMethod]
        public void ContractControllerTestGetContractsAndInfos()
        {
            contractsController = new ContractsController(_Context);
            var result = contractsController.GetContractsAndInfos();
            result.Should().BeOfType<Task<IEnumerable<Contracts>>>();
        }
        [TestMethod]
        public void ContractControllerGetContracts()
        {
            contractsController = new ContractsController(_Context);
            var result = contractsController.GetContracts();
            result.Should().BeOfType<Task<IEnumerable<Contracts>>>();
        }
        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<ContractsDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            var context = new ContractsDataContext(builder.Options);
            var contracts = Enumerable.Range(1, 10)
                .Select(i => new Contracts { ContractsId = i, Name = "Test", Surname = "Test", FirmName = "Test" });
            var contractsInfo = Enumerable.Range(1, 10)
                .Select(i => new ContractsInfo { ContractsInfoId = i, InfoType = InfoType.EmailAdress, InfoValue= "Test", ContractsId= 1 });
            context.Contracts.AddRange(contracts);
            context.ContractsInfo.AddRange(contractsInfo);
            int changed = context.SaveChanges();
            _Context = context;
        }

    }
}
