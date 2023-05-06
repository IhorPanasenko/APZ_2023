using Core.Models.UpdateModels;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IStaticMeasurmentsService
    {
        public Task<StaticMeasurments?> Create(StaticMeasurments staticMeasurments);

        public Task<StaticMeasurments?> GetById(Guid id);

        public Task<List<StaticMeasurments>> GetAll();

        public Task<List<StaticMeasurments>> UserStaticMeasurmentss(Guid userId);

        public Task<bool> Delete(Guid id);

        public Task<StaticMeasurments?> Update(UpdateStaticMeasurmentsModel updateStaticMeasurments);
    }
}
