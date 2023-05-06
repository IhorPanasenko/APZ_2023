using Core.Models.UpdateModels;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPeriodicMeasurmentsService
    {
        public Task<PeriodicMeasurments?> Create(PeriodicMeasurments PeriodicMeasurments);

        public Task<PeriodicMeasurments?> GetById(Guid id);

        public Task<List<PeriodicMeasurments>> GetAll();

        public Task<List<PeriodicMeasurments>> UserPeriodicMeasurmentss(Guid userId);

        public Task<bool> Delete(Guid id);

        public Task<PeriodicMeasurments?> Update(UpdatePeriodicMeasurmentsModel updatePeriodicMeasurments);
    }
}
