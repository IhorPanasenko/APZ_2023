using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPolicyRepository
    {
        public Task<bool> Create(Policy policy);

        public Task<bool> Update(Policy policy);

        public Task<List<Policy>> GetAll();

        public Task<bool> Delete(Guid id);

        public Task<Policy?> GetById(Guid id);
    }
}
