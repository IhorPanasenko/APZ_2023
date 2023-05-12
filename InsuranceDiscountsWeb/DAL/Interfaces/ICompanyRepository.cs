using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICompanyRepository
    {
        public Task<Company?> GetById(Guid id);

        public Task<List<Company>> GetAll();

        public Task<Company?> Create(Company company);

        public Task<bool> Delete(Guid id);

        public Task<bool> Update(Company company);

        public Task<List<Company>> SearchByName(string searchString);
    }
}
