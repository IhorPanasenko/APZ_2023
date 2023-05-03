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
    public class ActivityService
    {
        private readonly IActivityRepository activityRepository;
        private readonly ILogger<ActivityService> logger;

        public ActivityService(
            IActivityRepository activityRepository,
            ILogger<ActivityService> logger
            )
        {
            this.activityRepository = activityRepository;
            this.logger = logger;
        }

        public async Task<Activity?> Create(Activity activity)
        {
            try
            {
                var newActivity = await activityRepository.Create(activity);
                return newActivity;
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
                var res = await activityRepository.Delete(id);
                return res;
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
                activities = await activityRepository.GetAll();
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
                var activity = await activityRepository.GetById(id);
                return activity;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<Activity?> Update(UpdateActivityModel updateActivity)
        {
            try
            {
                var oldActivity = await activityRepository.GetById(updateActivity.Id);

                if (oldActivity is null)
                {
                    throw new ArgumentException($"No Activity with id {updateActivity.Id}");
                }

                update(oldActivity, updateActivity);
                var newActivity = await activityRepository.Update(oldActivity);
                return newActivity;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        private void update(Activity oldActivity, UpdateActivityModel updateActivity)
        {
            oldActivity.Id = updateActivity.Id;
            oldActivity.Type = String.IsNullOrEmpty(updateActivity.Type) ? oldActivity.Type : updateActivity.Type;
            oldActivity.Duration = updateActivity.Duration<=0 ? oldActivity.Duration : updateActivity.Duration;
            oldActivity.Distance = updateActivity.Distance<=0 ? oldActivity.Distance : updateActivity.Distance;
            oldActivity.Calories = updateActivity.Calories <= 0 ? oldActivity.Calories : updateActivity.Calories;
            oldActivity.UserId = updateActivity.UserId == null ? oldActivity.UserId : (Guid)updateActivity.UserId;
        }

        public async Task<List<Activity>> UserActivitys(Guid userId)
        {
            List<Activity> activitys = new List<Activity>();

            try
            {
                activitys = await activityRepository.GetAll();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return activitys;
        }
    }
}
