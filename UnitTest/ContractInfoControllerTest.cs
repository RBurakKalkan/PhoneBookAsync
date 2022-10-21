using ContractApi.Controllers;
using ContractApi.DataLayer;
using ContractApi.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class ContractInfoControllerTest
    {

        ContractsInfoController contractInfoController;

        ContractsDataContext _Context; 
        public ContractInfoControllerTest()
        {
            InitContext();
        }
        [TestMethod]
        public void ContractControllerAddContract()
        {
            contractInfoController = new ContractsInfoController(_Context);
            var result = contractInfoController.AddContractInfo(1,InfoType.EmailAdress,"Test");
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [TestMethod]
        public void ContractControllerRemoveContract()
        {
            contractInfoController = new ContractsInfoController(_Context);
            var result = contractInfoController.RemoveContractInfo(1);
            result.Should().BeOfType<Task<IActionResult>>();
        }
        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<ContractsDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            var context = new ContractsDataContext(builder.Options);
            var contractsInfo = Enumerable.Range(1, 10)
                .Select(i => new ContractsInfo { ContractsInfoId = i, InfoType = InfoType.EmailAdress, InfoValue = "Test", ContractsId = 1 });
            context.ContractsInfo.AddRange(contractsInfo);
            int changed = context.SaveChanges();
            _Context = context;
        }
    }
   
}
