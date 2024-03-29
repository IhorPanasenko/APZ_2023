﻿using Core.Models.UpdateModels;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAgentService
    {
        public Task<Agent?> GetById(Guid id);

        public Task<List<Agent>> GetAll();

        public Task<bool> Create(Agent agent);

        public Task<bool> Delete(Guid id);

        public Task<Agent?> Update(UpdateAgentModel agent);

        public Task<List<Agent>> GetAgentsByCompany(Guid companyId);
    }
}
