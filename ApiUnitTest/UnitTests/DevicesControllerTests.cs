using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiGateways.Entities;
using WebApiGateways.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiGateways.Tests.UnitTests;

namespace WebApiGateways.Tests.UnitTests
{
    [TestClass]
    public class DevicesControllerTests : BaseTests
    {

        [TestMethod]
        public async Task GetAllDevices()
        {
            //Setting all
            var nameBD = Guid.NewGuid().ToString();
            var context = BuildContext(nameBD);

            context.Gateways.Add(new Gateway() { SerialNumber = "gw-001", Name = "Gateway 1", IpAddress = "10.0.0.1" });
            context.Peripherals.Add(new Peripheral() { UID = 1, Vendor = "Device 1", Date = DateTime.Parse("2021-02-16T16:17:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 1 });
            context.Peripherals.Add(new Peripheral() { UID = 2, Vendor = "Device 2", Date = DateTime.Parse("2021-02-16T16:18:00"), Status = false });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 2 });

            await context.SaveChangesAsync();

            var testContext = BuildContext(nameBD);

            //Tests
            var controller = new DevicesController(testContext);
            var response = await controller.Get();


            //Verification
            var devices = response.Value;
            //A total of 2 Devices
            Assert.AreEqual(2, devices.Count);
        }

        [TestMethod]
        public async Task GetADeviceByNonExistingUID()
        {
            //Setting all
            var nameBD = Guid.NewGuid().ToString();
            var context = BuildContext(nameBD);

            context.Gateways.Add(new Gateway() { SerialNumber = "gw-001", Name = "Gateway 1", IpAddress = "10.0.0.1" });
            context.Peripherals.Add(new Peripheral() { UID = 1, Vendor = "Device 1", Date = DateTime.Parse("2021-02-16T16:17:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 1 });
            context.Peripherals.Add(new Peripheral() { UID = 2, Vendor = "Device 2", Date = DateTime.Parse("2021-02-16T16:18:00"), Status = false });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 2 });
            await context.SaveChangesAsync();

            var testContext = BuildContext(nameBD);

            //Tests
            var uid = 3;
            var controller = new DevicesController(testContext);
            var response = await controller.Get(uid);


            //Verification
            var result = response.Result as StatusCodeResult;
            //Not Found Http Status Code
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task GetADevicesByExistingUID()
        {
            //Setting all
            var nameBD = Guid.NewGuid().ToString();
            var context = BuildContext(nameBD);

            context.Gateways.Add(new Gateway() { SerialNumber = "gw-001", Name = "Gateway 1", IpAddress = "10.0.0.1" });
            context.Peripherals.Add(new Peripheral() { UID = 1, Vendor = "Device 1", Date = DateTime.Parse("2021-02-16T16:17:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 1 });
            context.Peripherals.Add(new Peripheral() { UID = 2, Vendor = "Device 2", Date = DateTime.Parse("2021-02-16T16:18:00"), Status = false });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 2 });
            await context.SaveChangesAsync();

            var testContext = BuildContext(nameBD);

            //Tests
            var uid = 1;
            var controller = new DevicesController(testContext);
            var response = await controller.Get(uid);


            //Verification
            var gateway = response.Value as Peripheral;
            //A Gateway object
            Assert.AreEqual(uid, gateway.UID);
        }

        [TestMethod]
        public async Task GetAllDevicesOfAGatewayByItsSerialNumber()
        {
            //Setting all
            var nameBD = Guid.NewGuid().ToString();
            var context = BuildContext(nameBD);

            context.Gateways.Add(new Gateway() { SerialNumber = "gw-001", Name = "Gateway 1", IpAddress = "10.0.0.1" });
            context.Peripherals.Add(new Peripheral() { UID = 1, Vendor = "Device 1", Date = DateTime.Parse("2021-02-16T16:17:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 1 });
            context.Peripherals.Add(new Peripheral() { UID = 2, Vendor = "Device 2", Date = DateTime.Parse("2021-02-16T16:18:00"), Status = false });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 2 });
            context.Peripherals.Add(new Peripheral() { UID = 3, Vendor = "Device 3", Date = DateTime.Parse("2021-02-16T16:19:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 3 });

            await context.SaveChangesAsync();

            var testContext = BuildContext(nameBD);

            //Tests
            var controller = new DevicesController(testContext);
            var response = await controller.Get("gw-001");


            //Verification
            var devices = response.Value;
            //A total of 3 Devices
            Assert.AreEqual(3, devices.Count);
        }

        [TestMethod]
        public async Task AddADevice()
        {
            //Setting all
            var nameBD = Guid.NewGuid().ToString();

            var context = BuildContext(nameBD);
            context.Gateways.Add(new Gateway() { SerialNumber = "gw-001", Name = "Gateway 1", IpAddress = "10.0.0.1" });

            await context.SaveChangesAsync();

            var testContext = BuildContext(nameBD);

            //Tests
            var device = new Peripheral() { UID = 1, Vendor = "Device 1", Date = DateTime.Parse("2021-02-16T16:17:00"), Status = true };
            var controller = new DevicesController(testContext);

            var response = await controller.Post(device, "gw-001");
            //Verification
            var result = response.Result;
            Assert.IsNotNull(result);

            var countDevices = await testContext.Peripherals.CountAsync();
            Assert.AreEqual(1, countDevices);
            var countRelations = await testContext.PeripheralGateways.CountAsync();
            Assert.AreEqual(1, countRelations);
        }

        [TestMethod]
        public async Task AddADeviceToAFullGateway()
        {
            //Setting all
            var nameBD = Guid.NewGuid().ToString();

            var context = BuildContext(nameBD);
            context.Gateways.Add(new Gateway() { SerialNumber = "gw-001", Name = "Gateway 1", IpAddress = "10.0.0.1" });
            context.Peripherals.Add(new Peripheral() { UID = 1, Vendor = "Device 1", Date = DateTime.Parse("2021-02-16T16:17:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 1 });
            context.Peripherals.Add(new Peripheral() { UID = 2, Vendor = "Device 2", Date = DateTime.Parse("2021-02-16T16:18:00"), Status = false });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 2 });
            context.Peripherals.Add(new Peripheral() { UID = 3, Vendor = "Device 3", Date = DateTime.Parse("2021-02-16T16:19:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 3 });
            context.Peripherals.Add(new Peripheral() { UID = 4, Vendor = "Device 4", Date = DateTime.Parse("2021-02-16T16:20:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 4 });
            context.Peripherals.Add(new Peripheral() { UID = 5, Vendor = "Device 5", Date = DateTime.Parse("2021-02-16T16:21:00"), Status = false });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 5 });
            context.Peripherals.Add(new Peripheral() { UID = 6, Vendor = "Device 6", Date = DateTime.Parse("2021-02-16T16:22:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 6 });
            context.Peripherals.Add(new Peripheral() { UID = 7, Vendor = "Device 7", Date = DateTime.Parse("2021-02-16T16:23:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 7 });
            context.Peripherals.Add(new Peripheral() { UID = 8, Vendor = "Device 8", Date = DateTime.Parse("2021-02-16T16:24:00"), Status = false });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 8 });
            context.Peripherals.Add(new Peripheral() { UID = 9, Vendor = "Device 9", Date = DateTime.Parse("2021-02-16T16:25:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 9 });
            context.Peripherals.Add(new Peripheral() { UID = 10, Vendor = "Device 10", Date = DateTime.Parse("2021-02-16T16:26:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 10 });

            await context.SaveChangesAsync();

            var testContext = BuildContext(nameBD);

            //Tests
            var device = new Peripheral() { UID = 11, Vendor = "Device 11", Date = DateTime.Parse("2021-02-16T16:27:00"), Status = true };
            var controller = new DevicesController(testContext);

            var response = await controller.Post(device, "gw-001");
            //Verification
            var result = response.Result as BadRequestObjectResult;
            //Bad Request Http Status Code
            Assert.AreEqual(400, result.StatusCode);

            var countDevices = await testContext.Peripherals.CountAsync();
            Assert.AreEqual(10, countDevices);
            var countRelations = await testContext.PeripheralGateways.CountAsync();
            Assert.AreEqual(10, countRelations);
        }

        [TestMethod]
        public async Task DeleteADevicesByItsUID()
        {
            //Setting all
            var nameBD = Guid.NewGuid().ToString();

            var context = BuildContext(nameBD);
            context.Gateways.Add(new Gateway() { SerialNumber = "gw-001", Name = "Gateway 1", IpAddress = "10.0.0.1" });
            context.Peripherals.Add(new Peripheral() { UID = 1, Vendor = "Device 1", Date = DateTime.Parse("2021-02-16T16:17:00"), Status = true });
            context.PeripheralGateways.Add(new PeripheralsGateways() { GatewaySerialNumber = "gw-001", PeripheralId = 1 });

            await context.SaveChangesAsync();

            var testContext = BuildContext(nameBD);

            //Tests
            var controller = new DevicesController(testContext);

            var response = await controller.Delete(1);
            //Verification
            var result = response.Result as AcceptedResult;
            //Bad Request Http Status Code
            Assert.AreEqual(202, result.StatusCode);

            var countDevices = await testContext.Peripherals.CountAsync();
            Assert.AreEqual(0, countDevices);
            var countRelations = await testContext.PeripheralGateways.CountAsync();
            Assert.AreEqual(0, countRelations);
        }
    }
}