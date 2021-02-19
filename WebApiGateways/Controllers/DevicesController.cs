using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiGateways.Contexts;
using WebApiGateways.Entities;

namespace WebApiGateways.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("AllowOriginLocal")]
    public class DevicesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DevicesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        /* GET api/devices
           Get all Devices */
        [HttpGet]
        public async Task<ActionResult<List<Peripheral>>> Get()
        {
            try
            {
                return await context.Peripherals.ToListAsync();
            }
            catch (Exception err)
            {
                return BadRequest(new InvalidOperationException(err.ToString()));
            }
        }

        /* GET api/devices/{uid}
           Get a Device by it's UID */
        [HttpGet("{uid}")]
        public async Task<ActionResult<Peripheral>> Get(long uid)
        {
            try
            {
                var device = await context.Peripherals.FirstOrDefaultAsync(x => x.UID == uid);
                if (device == null)
                {
                    return NotFound();
                }
                return device;
            }
            catch (Exception err)
            {
                return BadRequest(new InvalidOperationException(err.ToString()));
            }
        }

        /* GET api/devices/gw/{serialNumber}
           Get all Devices of a Gateway */
        [HttpGet("gw/{serialNumber}")]
        public async Task<ActionResult<List<Peripheral>>> Get(string serialNumber)
        {
            try
            {
                List<long> UIDs = await context.PeripheralGateways
                    .Where(x => x.GatewaySerialNumber == serialNumber)
                    .Select(x => x.PeripheralId).ToListAsync();
                List<Peripheral> devices = new List<Peripheral>();
                foreach (long uid in UIDs)
                {
                    var device = await context.Peripherals.FirstOrDefaultAsync(x => x.UID == uid);
                    devices.Add(device);
                }

                if (UIDs.Count == 0 || devices.Count == 0)
                {
                    return NotFound();
                }
                return devices;
            }
            catch (Exception err)
            {
                return BadRequest(new InvalidOperationException(err.ToString()));
            }
        }


        /* POST api/devices/{serialNumber}
         Add a Device to a Gateway */
        [HttpPost("{serialNumber}")]
        public async Task<ActionResult<Peripheral>> Post([FromBody] Peripheral device, string serialNumber)
        {
            try
            {
                //Check gateway exist
                var gateway = await context.Gateways.FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
                if (gateway == null)
                {
                    return NotFound();
                }

                //Check number of Devices for the Gateway
                var cantDevicesInGateway = await context.PeripheralGateways.CountAsync(x => x.GatewaySerialNumber == serialNumber);
                if (cantDevicesInGateway < 10)
                {
                    //Check if relation with Gateway does not already exist
                    var relation = await context.PeripheralGateways.CountAsync(x => x.GatewaySerialNumber == serialNumber && x.PeripheralId == device.UID);
                    if (relation != 0)
                    {
                        return BadRequest(new InvalidOperationException("This DevicesController it's already asociated with this Gateway."));
                    }


                    context.Add(device);
                    context.Add(new PeripheralsGateways { GatewaySerialNumber = serialNumber, PeripheralId = device.UID });
                    await context.SaveChangesAsync();
                    return Created("api/devices/" + device.UID, device);
                }
                else
                {
                    return BadRequest(new InvalidOperationException("This Gateway has the maximum number of DevicesController asociated."));
                }
            }
            catch (Exception err)
            {
                return BadRequest(new InvalidOperationException(err.ToString()));
            }

        }

        /* POST api/devices/{serialNumber}
         Add an existing Device to a Gateway */
        [HttpPost("{serialNumber}/{uid}")]
        public async Task<ActionResult<Peripheral>> Post(string serialNumber, long uid)
        {

            try
            {
                //Check both exist
                var device = await context.Peripherals.FirstOrDefaultAsync(x => x.UID == uid);
                var gateway = await context.Gateways.FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
                if (device == null || gateway == null)
                {
                    return NotFound();
                }

                //Check number of Devices for the Gateway
                var cantDevicesInGateway = await context.PeripheralGateways.CountAsync(x => x.GatewaySerialNumber == serialNumber);
                if (cantDevicesInGateway < 10)
                {

                    //Check if relation with Gateway does not already exist
                    var relation = await context.PeripheralGateways.CountAsync(x => x.GatewaySerialNumber == serialNumber && x.PeripheralId == uid);

                    if (relation != 0)
                    {
                        return BadRequest(new InvalidOperationException("This DevicesController it's already asociated with this Gateway."));
                    }

                    context.Add(new PeripheralsGateways { GatewaySerialNumber = serialNumber, PeripheralId = uid });
                    await context.SaveChangesAsync();
                    return Accepted();
                }
                else
                {
                    return BadRequest(new InvalidOperationException("This Gateway has the maximum number of DevicesController asociated."));
                }
            }
            catch (Exception err)
            {
                return BadRequest(new InvalidOperationException(err.ToString()));
            }

        }

        /* Delete api/devices/{serialNumber}/{uid}
         Delete a Device from a Gateway (if there is only a relation deletes the device) */
        [HttpDelete("{serialNumber}/{uid}")]
        public async Task<ActionResult<Peripheral>> Delete(string serialNumber, long uid)
        {
            try
            {
                PeripheralsGateways relation = await context.PeripheralGateways.FirstOrDefaultAsync(x => x.GatewaySerialNumber == serialNumber && x.PeripheralId == uid);
                var countOfRelations = await context.PeripheralGateways.CountAsync(x => x.GatewaySerialNumber == serialNumber && x.PeripheralId == uid);
                var device = await context.Peripherals.FirstOrDefaultAsync(x => x.UID == uid);
                if (device == null || relation == null)
                {
                    return NotFound();
                }
                context.Remove(relation);
                if (countOfRelations == 1)
                {
                    context.Remove(device);
                }
                await context.SaveChangesAsync();
                return Accepted();
            }
            catch (Exception err)
            {
                return BadRequest(new InvalidOperationException(err.ToString()));
            }
        }

        /* Delete api/devices/{uid}
         Delete a Device from everywhere */
        [HttpDelete("{uid}")]
        public async Task<ActionResult<Peripheral>> Delete(long uid)
        {
            try
            {
                List<PeripheralsGateways> relation = await context.PeripheralGateways.Where(x => x.PeripheralId == uid).ToListAsync();
                var device = await context.Peripherals.FirstOrDefaultAsync(x => x.UID == uid);
                if (device == null || relation.Count == 0)
                {
                    return NotFound();
                }
                foreach (PeripheralsGateways r in relation)
                {
                    context.Remove(r);
                }
                context.Remove(device);
                await context.SaveChangesAsync();
                return Accepted();
            }
            catch (Exception err)
            {
                return BadRequest(new InvalidOperationException(err.ToString()));
            }
        }
    }
}