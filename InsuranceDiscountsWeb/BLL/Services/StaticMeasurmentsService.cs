using BLL.Interfaces;
using Core.Models.UpdateModels;
using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StaticMeasurmentsService : IStaticMeasurmentsService
    {
        private readonly IStaticMeasurmentsRepository staticMeasurmentsRepository;
        private readonly ILogger<StaticMeasurmentsService> logger;

        public StaticMeasurmentsService(
            IStaticMeasurmentsRepository staticMeasurmentsRepository,
            ILogger<StaticMeasurmentsService> logger
            )
        {
            this.staticMeasurmentsRepository = staticMeasurmentsRepository;
            this.logger = logger;
        }

        public async Task<StaticMeasurments?> Create(StaticMeasurments staticMeasurments)
        {
            try
            {
                var newStaticMeasurments = await staticMeasurmentsRepository.Create(staticMeasurments);
                return newStaticMeasurments;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var res = await staticMeasurmentsRepository.Delete(id);
                return res;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<StaticMeasurments>> GetAll()
        {
            List<StaticMeasurments> staticMeasurmentss = new List<StaticMeasurments>();

            try
            {
                staticMeasurmentss = await staticMeasurmentsRepository.GetAll();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return staticMeasurmentss;
        }

        public async Task<StaticMeasurments?> GetById(Guid id)
        {
            try
            {
                var staticMeasurments = await staticMeasurmentsRepository.GetById(id);
                return staticMeasurments;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<StaticMeasurments?> Update(UpdateStaticMeasurmentsModel updateStaticMeasurments)
        {
            try
            {
                var oldStaticMeasurments = await staticMeasurmentsRepository.GetById(updateStaticMeasurments.Id);

                if (oldStaticMeasurments is null)
                {
                    throw new ArgumentException($"No staticMeasurments with id {updateStaticMeasurments.Id}");
                }

                update(oldStaticMeasurments, updateStaticMeasurments);
                var newStaticMeasurments = await staticMeasurmentsRepository.Update(oldStaticMeasurments);
                return newStaticMeasurments;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        private void update(StaticMeasurments oldStaticMeasurments, UpdateStaticMeasurmentsModel updateStaticMeasurments)
        {
            oldStaticMeasurments.Id = updateStaticMeasurments.Id;
            oldStaticMeasurments.Height = updateStaticMeasurments.Height <= 0 ? oldStaticMeasurments.Height : updateStaticMeasurments.Height;
            oldStaticMeasurments.Weight = updateStaticMeasurments.Weight <= 0 ? oldStaticMeasurments.Weight : updateStaticMeasurments.Weight;
            oldStaticMeasurments.Waist = updateStaticMeasurments.Waist <= 0 ? oldStaticMeasurments.Waist : updateStaticMeasurments.Waist;
            oldStaticMeasurments.MesurmentDate = updateStaticMeasurments.MesurmentDate == null ? oldStaticMeasurments.MesurmentDate : (DateTime)updateStaticMeasurments.MesurmentDate;
        }

        public async Task<List<StaticMeasurments>> UserStaticMeasurmentss(Guid userId)
        {
            List<StaticMeasurments> staticMeasurmentss = new List<StaticMeasurments>();

            try
            {
                staticMeasurmentss = await staticMeasurmentsRepository.GetAll();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return staticMeasurmentss;
        }
    }
}
