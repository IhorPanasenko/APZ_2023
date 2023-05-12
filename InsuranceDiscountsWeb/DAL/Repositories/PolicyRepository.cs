using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PolicyRepository
    {
        private readonly ILogger<AgentRepository> logger;
        private readonly InsuranceDiscountsDbContext dbContext;

        public PolicyRepository(ILogger<AgentRepository> logger, InsuranceDiscountsDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }


    }
}
