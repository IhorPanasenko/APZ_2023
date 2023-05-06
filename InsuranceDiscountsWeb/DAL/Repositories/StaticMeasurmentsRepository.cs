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
    public class StaticMeasurmentsRepository: IStaticMeasurmentsRepository
    {
        private readonly InsuranceDiscountsDbContext dbContext;
        private readonly ILogger<StaticMeasurmentsRepository> logger;

        public StaticMeasurmentsRepository(
            InsuranceDiscountsDbContext dbContext,
            ILogger<StaticMeasurmentsRepository> logger
            )
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<StaticMeasurments?> Create(StaticMeasurments staticMeasurments)
        {
            try
            {
                await dbContext.AddAsync(staticMeasurments);
                await dbContext.SaveChangesAsync();

                var createdStaticMeasurments = await dbContext.StaticMeasurments.FindAsync(staticMeasurments.Id);

                if (createdStaticMeasurments is null)
                {
                    throw new ApplicationException("Can't creat staticMeasurments for user now");
                }

                return createdStaticMeasurments;
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
                var staticMeasurments = await dbContext.StaticMeasurments.FindAsync(id);

                if (staticMeasurments is null)
                {
                    throw new ArgumentException($"No StaticMeasurments with Id {id} was found");
                }

                dbContext.StaticMeasurments.Remove(staticMeasurments);
                await dbContext.SaveChangesAsync();
                return true;
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
                staticMeasurmentss = await dbContext.StaticMeasurments.ToListAsync();

                foreach (var staticMeasurments in staticMeasurmentss)
                {
                    await addUserInfoToStaticMeasurments(staticMeasurments);
                }
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
                var staticMeasurments = await dbContext.StaticMeasurments.FindAsync(id);

                if (staticMeasurments is null)
                {
                    throw new ArgumentException($"StaticMeasurments with id {id} was not found");
                }

                await addUserInfoToStaticMeasurments(staticMeasurments);
                return staticMeasurments;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<StaticMeasurments?> Update(StaticMeasurments updateStaticMeasurments)
        {
            try
            {
                dbContext.StaticMeasurments.Update(updateStaticMeasurments);
                await dbContext.SaveChangesAsync();

                var staticMeasurments = await dbContext.StaticMeasurments.FindAsync(updateStaticMeasurments.Id);

                if (staticMeasurments is null)
                {
                    throw new ArgumentException($"Can't find staticMeasurments with id {updateStaticMeasurments.Id} id for updating");
                }

                return staticMeasurments;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<List<StaticMeasurments>> UserStaticMeasurmentss(Guid userId)
        {
            List<StaticMeasurments> staticMeasurmentss = new List<StaticMeasurments>();

            try
            {
                staticMeasurmentss = await dbContext.StaticMeasurments.Where(n => n.UserId == userId).ToListAsync();

                foreach (var staticMeasurments in staticMeasurmentss)
                {
                    await addUserInfoToStaticMeasurments(staticMeasurments);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return staticMeasurmentss;
        }

        private async Task addUserInfoToStaticMeasurments(StaticMeasurments staticMeasurments)
        {
            var user = await dbContext.AppUsers.FindAsync(staticMeasurments.UserId);

            if (user is not null)
            {
                staticMeasurments.AppUser = user;
            }
        }
    }
}
