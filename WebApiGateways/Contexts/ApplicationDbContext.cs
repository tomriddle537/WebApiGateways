using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiGateways.Entities;

namespace WebApiGateways.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        //Entity Framework Core standard configuration
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options) {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //Setting string SerialNumber as primary key
            modelBuilder.Entity<Gateway>().HasKey(x => new { x.SerialNumber });
            //Setting long UID as primary key
            modelBuilder.Entity<Peripheral>().HasKey(x => new { x.UID });
            //Setting string GatewaySerialNumber and long PeripheralId (many to many) as primary key
            modelBuilder.Entity<PeripheralsGateways>().HasKey(x => new {x.GatewaySerialNumber, x.PeripheralId });
            //Passing modelBuilder to parent
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<Peripheral> Peripherals { get; set; }
        public DbSet<PeripheralsGateways> PeripheralGateways { get; set; }
    }
}
