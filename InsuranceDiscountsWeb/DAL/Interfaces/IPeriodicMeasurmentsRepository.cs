using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPeriodicMeasurmentsRepository
    {
        public Task<PeriodicMeasurments?> Create(PeriodicMeasurments periodicMeasurments);

        public Task<PeriodicMeasurments?> GetById(Guid id);

        public Task<List<PeriodicMeasurments>> GetAll();

        public Task<List<PeriodicMeasurments>> UserPeriodicMeasurmentss(Guid userId);

        public Task<bool> Delete(Guid id);

        public Task<PeriodicMeasurments?> Update(PeriodicMeasurments updatePeriodicMeasurments);
    }
}
