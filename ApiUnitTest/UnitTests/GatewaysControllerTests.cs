using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiGateways.Entities;
using WebApiGateways.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApiGateways.Tests.UnitTests
{
    [TestClass]
    public class GatewaysControllerTests : BaseTests
    {
        [TestMethod]
        public async Task GetAllGateways()
        {
            //Setting all
            var nameBD = Guid.NewGuid().ToString();
            var context = BuildContext(nameBD);

            context.Gateways.Add(new Gateway() { SerialNumber = "gw-001", Name = "Gateway 1", IpAddress = "10.0.0.1" });
            context.Gateways.Add(new Gateway() { SerialNumber = "gw-002", Name = "Gateway 2", IpAddress = "10.0.0.2" });
            await context.SaveChangesAsync();

            var testContext = BuildContext(nameBD);

            //Tests
            var controller = new GatewaysController(testContext);
            var response = await controller.Get();


            //Verification
            var gateways = response.Value;
            //A total of 2 Gateways
            Assert.AreEqual(2, gateways.Count);
        }
        
        [TestMethod]
        public async Task GetAGatewayByNonExistingSerialNumber()
        {
            //Setting all
            var nameBD = Guid.NewGuid().ToString();
            var context = BuildContext(nameBD);

            context.Gateways.Add(new Gateway() { SerialNumber = "gw-001", Name = "Gateway 1", IpAddress = "10.0.0.1" });
            context.Gateways.Add(new Gateway() { SerialNumber = "gw-002", Name = "Gateway 2", IpAddress = "10.0.0.2" });
            await context.SaveChangesAsync();

            var testContext = BuildContext(nameBD);

            //Tests
            var serialNumber = "gw-003";
            var controller = new GatewaysController(testContext);
            var response = await controller.Get(serialNumber);


            //Verification
            var result = response.Result as StatusCodeResult;
            //Not Found Http Status Code
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task GetAGatewayByExistingSerialNumber()
        {
            //Setting all
            var nameBD = Guid.NewGuid().ToString();
            var context = BuildContext(nameBD);

            context.Gateways.Add(new Gateway() { SerialNumber = "gw-001", Name = "Gateway 1", IpAddress = "10.0.0.1" });
            context.Gateways.Add(new Gateway() { SerialNumber = "gw-002", Name = "Gateway 2", IpAddress = "10.0.0.2" });
            await context.SaveChangesAsync();

            var testContext = BuildContext(nameBD);

            //Tests
            var serialNumber = "gw-001";
            var controller = new GatewaysController(testContext);
            var response = await controller.Get(serialNumber);


            //Verification
            var gateway = response.Value as Gateway;
            //A Gateway object
            Assert.AreEqual(serialNumber, gateway.SerialNumber);
        }

        [TestMethod]
        public async Task AddAGatewayBadIpAddress()
        {
            //Setting all
            var nameBD = Guid.NewGuid().ToString();

            var context = BuildContext(nameBD);
            var testContext = BuildContext(nameBD);

            //Tests
            var gateway = new Gateway() { SerialNumber = "gw-004", Name = "Gateway 4", IpAddress = "0.0.0.255" };
            var controller = new GatewaysController(context);

            var response = await controller.Post(gateway);
            //Verification
            var result = response.Result as BadRequestObjectResult;
            //Bad Request Http Status Code
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task AddAGatewayGoodIpAddress()
        {
            //Setting all
            var nameBD = Guid.NewGuid().ToString();

            var context = BuildContext(nameBD);
            var testContext = BuildContext(nameBD);

            //Tests
            var gateway = new Gateway() { SerialNumber = "gw-004", Name = "Gateway 4", IpAddress = "10.0.0.4" };
            var controller = new GatewaysController(context);

            var response = await controller.Post(gateway);
            //Verification
            var result = response.Result;
            Assert.IsNotNull(result);

            var countGateways = await testContext.Gateways.CountAsync();
            Assert.AreEqual(1, countGateways);
        }

    }
}
