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
   // [EnableCors("AllowOriginLocal")]
    public class GatewaysController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GatewaysController(ApplicationDbContext context)
        {
            this.context = context;
        }

        /* GET api/gateways
           Get all Gateways */
        [HttpGet]
        public async Task<ActionResult<List<Gateway>>> Get()
        {
            try
            {
                return await context.Gateways.ToListAsync();
            }
            catch (Exception err)
            {
                return BadRequest(new InvalidOperationException(err.ToString()));
            }
        }

        /* GET api/gateways/{serialNumber}
          Get a Gateway by it's serial number */
        [HttpGet("{serialNumber}")]
        public async Task<ActionResult<Gateway>> Get(string serialNumber)
        {
            try
            {
                var gateway = await context.Gateways.FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
                if (gateway == null)
                {
                    return NotFound();
                }
                return gateway;
            }
            catch (Exception err)
            {
                return BadRequest(new InvalidOperationException(err.ToString()));
            }
        }

        
        /* POST api/gateways
           Add a Gateway */
        [HttpPost]
        public async Task<ActionResult<Gateway>> Post([FromBody] Gateway gateway)
        {
            var ip = gateway.IpAddress;
            //Check ip
            if (!ValidateIPv4(ip))
            {
                return BadRequest(new InvalidOperationException("Invalid IPv4 Address: " + ip));
            }
            try
            {
                context.Add(gateway);
                await context.SaveChangesAsync();
                return Created("api/gateways/" + gateway.SerialNumber, gateway);
            }
            catch (Exception err)
            {
                return BadRequest(new InvalidOperationException(err.ToString()));
            }

        }

        private static bool ValidateIPv4(string _ip)
        {
            //IPAdress Parse Test
            try { System.Net.IPAddress ip = System.Net.IPAddress.Parse(_ip); }
            catch { return false; }

            string[] ipSplits = _ip.Split(".");
            //Checking all < 255 and first and last > 0 
            return ipSplits.All(x => int.Parse(x) < 255) &&
                   int.Parse(ipSplits.FirstOrDefault()) > 0 &&
                   int.Parse(ipSplits.LastOrDefault()) > 0;
        }


        /* Delete api/gateways/{serialNumber}
           Delete a Gateway - Private method, not public in API, only for future requests */
        [HttpDelete("{serialNumber}")]
        private async Task<ActionResult<Gateway>> Delete(string serialNumber, bool deleteAllDevices = false)
        {
            try
            {
                List<long> UIDs = await context.PeripheralGateways
                    .Where(x => x.GatewaySerialNumber == serialNumber)
                    .Select(x => x.PeripheralId).ToListAsync();

                if (deleteAllDevices)
                {
                    List<Peripheral> devices = new List<Peripheral>();
                    foreach (long uid in UIDs)
                    {
                        PeripheralsGateways relation = await context.PeripheralGateways.FirstOrDefaultAsync(x => x.GatewaySerialNumber == serialNumber && x.PeripheralId == uid);
                        var device = await context.Peripherals.FirstOrDefaultAsync(x => x.UID == uid);
                        context.Remove(relation);
                        context.Remove(device);
                    }
                }
                else if (UIDs.Count > 0)
                {
                    return BadRequest(new InvalidOperationException("Can't delete a Gateway with associated DevicesController"));
                }

                var gateway = await context.Gateways.FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
                if (gateway == null)
                {
                    return NotFound();
                }
                context.Remove(gateway);
                await context.SaveChangesAsync();
                return Accepted(gateway);
            }
            catch (Exception err)
            {
                return BadRequest(new InvalidOperationException(err.ToString()));
            }
        }
    }
}
