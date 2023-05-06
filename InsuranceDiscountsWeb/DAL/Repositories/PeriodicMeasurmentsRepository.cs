using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PeriodicMeasurmentsRepository: IPeriodicMeasurmentsRepository
    {
        private readonly InsuranceDiscountsDbContext dbContext;
        private readonly ILogger<PeriodicMeasurmentsRepository> logger;

        public PeriodicMeasurmentsRepository(
            InsuranceDiscountsDbContext dbContext,
            ILogger<PeriodicMeasurmentsRepository> logger
            )
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<PeriodicMeasurments?> Create(PeriodicMeasurments periodicMeasurments)
        {
            try
            {
                await dbContext.AddAsync(periodicMeasurments);
                await dbContext.SaveChangesAsync();

                var createdPeriodicMeasurments = await dbContext.PeriodicMeasurments.FindAsync(periodicMeasurments.Id);

                if (createdPeriodicMeasurments is null)
                {
                    throw new ApplicationException("Can't creat periodicMeasurments for user now");
                }

                return createdPeriodicMeasurments;
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
                var periodicMeasurments = await dbContext.PeriodicMeasurments.FindAsync(id);

                if (periodicMeasurments is null)
                {
                    throw new ArgumentException($"No PeriodicMeasurments with Id {id} was found");
                }

                dbContext.PeriodicMeasurments.Remove(periodicMeasurments);
                await dbContext.SaveChangesAsync();
                return true;
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
                periodicMeasurmentss = await dbContext.PeriodicMeasurments.ToListAsync();

                foreach (var periodicMeasurments in periodicMeasurmentss)
                {
                    await addUserInfoToPeriodicMeasurments(periodicMeasurments);
                }
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
                var periodicMeasurments = await dbContext.PeriodicMeasurments.FindAsync(id);

                if (periodicMeasurments is null)
                {
                    throw new ArgumentException($"PeriodicMeasurments with id {id} was not found");
                }

                await addUserInfoToPeriodicMeasurments(periodicMeasurments);
                return periodicMeasurments;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<PeriodicMeasurments?> Update(PeriodicMeasurments updatePeriodicMeasurments)
        {
            try
            {
                dbContext.PeriodicMeasurments.Update(updatePeriodicMeasurments);
                await dbContext.SaveChangesAsync();

                var periodicMeasurments = await dbContext.PeriodicMeasurments.FindAsync(updatePeriodicMeasurments.Id);

                if (periodicMeasurments is null)
                {
                    throw new ArgumentException($"Can't find periodicMeasurments with id {updatePeriodicMeasurments.Id} id for updating");
                }

                return periodicMeasurments;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<List<PeriodicMeasurments>> UserPeriodicMeasurmentss(Guid userId)
        {
            List<PeriodicMeasurments> periodicMeasurmentss = new List<PeriodicMeasurments>();

            try
            {
                periodicMeasurmentss = await dbContext.PeriodicMeasurments.Where(n => n.UserId == userId).ToListAsync();

                foreach (var periodicMeasurments in periodicMeasurmentss)
                {
                    await addUserInfoToPeriodicMeasurments(periodicMeasurments);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return periodicMeasurmentss;
        }

        private async Task addUserInfoToPeriodicMeasurments(PeriodicMeasurments periodicMeasurments)
        {
            var user = await dbContext.AppUsers.FindAsync(periodicMeasurments.UserId);

            if (user is not null)
            {
                periodicMeasurments.AppUser = user;
            }
        }

    }
}
