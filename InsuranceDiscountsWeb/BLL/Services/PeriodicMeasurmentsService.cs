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
    public class PeriodicMeasurmentsService : IPeriodicMeasurmentsService
    {
        private readonly IPeriodicMeasurmentsRepository periodicMeasurmentsRepository;
        private readonly ILogger<PeriodicMeasurmentsService> logger;

        public PeriodicMeasurmentsService(
            IPeriodicMeasurmentsRepository periodicMeasurmentsRepository,
            ILogger<PeriodicMeasurmentsService> logger
            )
        {
            this.periodicMeasurmentsRepository = periodicMeasurmentsRepository;
            this.logger = logger;
        }

        public async Task<PeriodicMeasurments?> Create(PeriodicMeasurments periodicMeasurments)
        {
            try
            {
                var newPeriodicMeasurments = await periodicMeasurmentsRepository.Create(periodicMeasurments);
                return newPeriodicMeasurments;
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
                var res = await periodicMeasurmentsRepository.Delete(id);
                return res;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<PeriodicMeasurments>> GetAll()
        {
            List<PeriodicMeasurments> periodicMeasurmentss = new List<PeriodicMeasurments>();

            try
            {
                periodicMeasurmentss = await periodicMeasurmentsRepository.GetAll();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return periodicMeasurmentss;
        }

        public async Task<PeriodicMeasurments?> GetById(Guid id)
        {
            try
            {
                var periodicMeasurments = await periodicMeasurmentsRepository.GetById(id);
                return periodicMeasurments;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<PeriodicMeasurments?> Update(UpdatePeriodicMeasurmentsModel updatePeriodicMeasurments)
        {
            try
            {
                var oldPeriodicMeasurments = await periodicMeasurmentsRepository.GetById(updatePeriodicMeasurments.Id);

                if (oldPeriodicMeasurments is null)
                {
                    throw new ArgumentException($"No periodicMeasurments with id {updatePeriodicMeasurments.Id}");
                }

                update(oldPeriodicMeasurments, updatePeriodicMeasurments);
                var newPeriodicMeasurments = await periodicMeasurmentsRepository.Update(oldPeriodicMeasurments);
                return newPeriodicMeasurments;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        private void update(PeriodicMeasurments oldPeriodicMeasurments, UpdatePeriodicMeasurmentsModel updatePeriodicMeasurments)
        {
            oldPeriodicMeasurments.Id = updatePeriodicMeasurments.Id;
            oldPeriodicMeasurments.Pulse = updatePeriodicMeasurments.Pulse <= 0 ? oldPeriodicMeasurments.Pulse : updatePeriodicMeasurments.Pulse;
            oldPeriodicMeasurments.Glucose = updatePeriodicMeasurments.Glucose <= 0 ? oldPeriodicMeasurments.Glucose : updatePeriodicMeasurments.Glucose;
            oldPeriodicMeasurments.Cholesterol = updatePeriodicMeasurments.Cholesterol <= 0 ? oldPeriodicMeasurments.Cholesterol : updatePeriodicMeasurments.Cholesterol;
            oldPeriodicMeasurments.BloodPreasure = updatePeriodicMeasurments.BloodPreasure < 0? oldPeriodicMeasurments.BloodPreasure : updatePeriodicMeasurments.BloodPreasure;
            oldPeriodicMeasurments.MesurmentDate = updatePeriodicMeasurments.MesurmentDate == null ? oldPeriodicMeasurments.MesurmentDate : (DateTime)updatePeriodicMeasurments.MesurmentDate;
        }

        public async Task<List<PeriodicMeasurments>> UserPeriodicMeasurmentss(Guid userId)
        {
            List<PeriodicMeasurments> periodicMeasurmentss = new List<PeriodicMeasurments>();

            try
            {
                periodicMeasurmentss = await periodicMeasurmentsRepository.GetAll();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return periodicMeasurmentss;
        }
    }
}
