using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiGateways.Contexts;

namespace WebApiGateways.Tests.UnitTests
{
    public class BaseTests
    {
        protected ApplicationDbContext BuildContext(string nameDB) {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(nameDB).Options;
            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }
    }
}
