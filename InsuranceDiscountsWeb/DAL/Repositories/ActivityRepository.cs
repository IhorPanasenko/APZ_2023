using Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ActivityRepository
    {
        private readonly InsuranceDiscountsDbContext dbContext;
        private readonly ILogger<ActivityRepository> logger;

        public ActivityRepository(
            InsuranceDiscountsDbContext dbContext,
            ILogger<ActivityRepository> logger
            )
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<Activity?> Create(Activity activity)
        {
            try
            {
                await dbContext.AddAsync(activity);
                await dbContext.SaveChangesAsync();

                var createdActivity = await dbContext.Activities.FindAsync(activity.Id);

                if (createdActivity is null)
                {
                    throw new ApplicationException("Can't creat Activity now");
                }

                return createdActivity;
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
                var activity = await dbContext.Activities.FindAsync(id);

                if (activity is null)
                {
                    throw new ArgumentException($"No Activity with Id {id} was found");
                }

                dbContext.Activities.Remove(activity);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<Activity>> GetAll()
        {
            List<Activity> activities = new List<Activity>();

            try
            {
                activities = await dbContext.Activities.ToListAsync();

                foreach (var Activity in activities)
                {
                    await addUserInfoToActivity(Activity);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return activities;
        }

        public async Task<Activity?> GetById(Guid id)
        {
            try
            {
                var activity = await dbContext.Activities.FindAsync(id);

                if (activity is null)
                {
                    throw new ArgumentException($"Activity with id {id} was not found");
                }

                await addUserInfoToActivity(activity);
                return activity;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<Activity?> Update(Activity updateActivity)
        {
            try
            {
                dbContext.Activities.Update(updateActivity);
                await dbContext.SaveChangesAsync();

                var activity = await dbContext.Activities.FindAsync(updateActivity.Id);

                if (activity is null)
                {
                    throw new ArgumentException($"Can't find Activity with id {updateActivity.Id} id for updating");
                }

                return activity;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<List<Activity>> UserActivities(Guid userId)
        {
            List<Activity> activities = new List<Activity>();

            try
            {
                activities = await dbContext.Activities.Where(n => n.UserId == userId).ToListAsync();

                foreach (var activity in activities)
                {
                    await addUserInfoToActivity(activity);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return activities;
        }

        private async Task addUserInfoToActivity(Activity activity)
        {
            var user = await dbContext.AppUsers.FindAsync(activity.UserId);

            if (user is not null)
            {
                activity.AppUser = user;
            }
        }
    }
}
