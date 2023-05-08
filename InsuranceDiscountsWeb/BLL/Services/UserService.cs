using BLL.Interfaces;
using Core.Models;
using Core.Models.UpdateModels;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace BLL.Services
{
    public class UserService : IUserService
    {
        //private IUserRepository userRepository;
        private readonly ILogger<UserService> logger;
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly INutritionRepository nutritionRepository;
        private readonly IActivityRepository activityRepository;
        private readonly IStaticMeasurmentsRepository staticMeasurmentsRepository;
        private readonly IPeriodicMeasurmentsRepository periodicMeasurmentsRepository;
        private readonly IUserBadHabitRepository userBadHabitRepository;

        public UserService(
            ILogger<UserService> logger,
            IConfiguration configuration,
            IUserRepository userRepository,
            UserManager<AppUser> userManager,
            INutritionRepository nutritionRepository,
            IActivityRepository activityRepository,
            IStaticMeasurmentsRepository staticMeasurmentsRepository,
            IPeriodicMeasurmentsRepository periodicMeasurmentsRepository,
            IUserBadHabitRepository userBadHabitRepository
            )
        {
            this.logger = logger;
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.nutritionRepository = nutritionRepository;
            this.activityRepository = activityRepository;
            this.staticMeasurmentsRepository = staticMeasurmentsRepository;
            this.periodicMeasurmentsRepository = periodicMeasurmentsRepository;
            this.userBadHabitRepository = userBadHabitRepository;
        }

        public async Task<bool> DeleteUser(string email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);

                if (user is null)
                {
                    throw new Exception($"No user with email {email}");
                }

                var result = await userRepository.DeleteUser(user);
                return result;

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            var users = new List<AppUser>();

            try
            {
                users = await userRepository.GetAllUsers();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return users;
        }

        public async Task<AppUser?> GetUserByEmail(string email)
        {
            try
            {
                var user = await userRepository.GetUserByEmail(email);
                return user;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<AppUser?> GetUserById(Guid userId)
        {
            try
            {
                var user = await userRepository.GetUserById(userId);
                return user;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> UpdateUser(UpdateAppUserModel updateUser)
        {
            try
            {
                var oldUser = await userRepository.GetUserById(updateUser.Id);

                if (oldUser == null)
                {
                    throw new Exception($"Can't find user with Id {updateUser.Id}");
                }

                update(oldUser, updateUser);
                return await userRepository.UpdateUser(oldUser);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        private void update(AppUser oldUser, UpdateAppUserModel updateUser)
        {
            oldUser.UserName = String.IsNullOrEmpty(updateUser.UserName) ? oldUser.UserName : updateUser.UserName;
            oldUser.FirstName = String.IsNullOrEmpty(updateUser.FirstName) ? oldUser.FirstName : updateUser.FirstName;
            oldUser.LastName = String.IsNullOrEmpty(updateUser.LastName) ? oldUser.LastName : updateUser.LastName;
            oldUser.BirthdayDate = (DateTime)(updateUser.BirthdayDate == null ? oldUser.BirthdayDate : updateUser.BirthdayDate);
            oldUser.Email = String.IsNullOrEmpty(updateUser.Email) ? oldUser.Email : updateUser.Email;
            oldUser.Address = String.IsNullOrEmpty(updateUser.Address) ? oldUser.Address : updateUser.Address;
            oldUser.PhoneNumber = String.IsNullOrEmpty(updateUser.PhoneNumber) ? oldUser.PhoneNumber : updateUser.PhoneNumber;
        }

        public async Task CalculateDiscount(Guid userId)
        {
            var oldUser = await userRepository.GetUserById(userId);

            if (oldUser is null)
            {
                throw new ArgumentException($"No Users with Id {userId}");
            }

            try
            {
                var nutritionCoeficient = await calculateNutritionCoeficient(userId);
                var activitiesCoeficient = await calculateActivityCoeficient(userId);
                var staticMeasurmentsCoeficient = await calculateStaticMeasurmentsCoeficient(userId);
                var periodicMeasurmentsCoeficient = await calculatePeriodocMeasurmentsCoeficient(userId);
                var badHabitsCoeficient = await calculateBadHabitsCoeficient(userId);

                var discount = nutritionCoeficient + activitiesCoeficient + staticMeasurmentsCoeficient + periodicMeasurmentsCoeficient + badHabitsCoeficient;
                oldUser.Discount = discount;

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        private async Task<double> calculateBadHabitsCoeficient(Guid userId)
        {
            var badHabits = await userBadHabitRepository.GetByUser(userId);
            var levelSum = 0;
            var habitsCounter = 0;

            foreach (var userBadHabit in badHabits)
            {
                levelSum += userBadHabit.BadHabit!.Level;
                habitsCounter++;
            }

            var averageLevel = levelSum / habitsCounter;
            return (1 - (averageLevel / 100)) / 5;
        }

        private async Task<double> calculatePeriodocMeasurmentsCoeficient(Guid userId)
        {
            var periodicMeasurments = await periodicMeasurmentsRepository.GetAll();
            periodicMeasurments = periodicMeasurments.Where(measure => measure.UserId == userId).ToList();

            double cholesterolSum = 0;
            double glucoseSum = 0;
            double pulseSum = 0;
            double bloodPreasureSum = 0;
            double measurentsCounter = 0;

            foreach (var periodicMeasurment in periodicMeasurments)
            {
                cholesterolSum += periodicMeasurment.Cholesterol;
                glucoseSum += periodicMeasurment.Glucose;
                pulseSum += periodicMeasurment.Pulse;
                bloodPreasureSum += periodicMeasurment.BloodPreasure;
                measurentsCounter++;
            }

            var averageCholesterol = cholesterolSum / measurentsCounter;
            var averageGlucose = glucoseSum / measurentsCounter;
            var averagePulse = pulseSum / measurentsCounter;
            var averageBloodPreasure = bloodPreasureSum / measurentsCounter;

            double coeficient = 0;

            if (!(averageCholesterol > 3 && averageCholesterol < 6))
            {
                coeficient += 25;
            }

            if (!(averageGlucose > 3.2 && averageGlucose < 5.8))
            {
                coeficient += 25;
            }

            if (!(averagePulse > 55 && averagePulse < 85))
            {
                coeficient += 25;
            }

            if (!(averageBloodPreasure > 115 && averageBloodPreasure < 130))
            {
                coeficient += 25;
            }

            return (1 - (coeficient / 100)) / 5;
        }

        private async Task<double> calculateStaticMeasurmentsCoeficient(Guid userId)
        {
            var staticMeasurments = await staticMeasurmentsRepository.GetAll();
            staticMeasurments = staticMeasurments.Where(measure => measure.UserId == userId).ToList();

            var heightSum = staticMeasurments.Sum(measure => measure.Height);
            var weightSum = staticMeasurments.Sum(measure => measure.Weight);
            var counter = staticMeasurments.Count;

            var averageHeight = heightSum / counter;
            var averageWeight = weightSum / counter;

            var IMT = averageWeight / (averageHeight * averageHeight);

            var coeficient = 0;

            if (IMT > 18.5 && IMT < 25)
            {
                return 0;
            }
            else
            {
                if (IMT < 18.5)
                {
                    coeficient = 50;
                }
                else
                {
                    if (IMT > 25 && IMT < 30)
                    {
                        coeficient = 75;
                    }
                    else
                    {
                        coeficient = 100;
                    }
                }
            }

            return (1 - (coeficient / 100)) / 5;
        }

        private async Task<double> calculateActivityCoeficient(Guid userId)
        {
            var activities = await activityRepository.GetAll();
            activities = activities.Where(activity => activity.UserId == userId).ToList();

            var caloriesSum = activities.Sum(activity => activity.Calories);
            var averageCalories = caloriesSum / activities.Count;

            var coeficient = 0;

            if (averageCalories < 100)
            {
                coeficient = 100;
            }
            if (averageCalories >= 100 && averageCalories < 200)
            {
                coeficient = 80;
            }
            if (averageCalories >= 200 && averageCalories < 300)
            {
                coeficient = 50;
            }
            if (averageCalories >= 300 && averageCalories < 600)
            {
                coeficient = 0;
            }
            if (averageCalories >= 600 && averageCalories < 800)
            {
                coeficient = 50;
            }
            if (averageCalories >= 800 && averageCalories < 1000)
            {
                coeficient = 80;
            }
            if (averageCalories >= 1000)
            {
                coeficient = 100;
            }


            return (1 - (coeficient / 100)) / 5;
        }

        private async Task<double> calculateNutritionCoeficient(Guid userId)
        {
            var nutritions = await nutritionRepository.GetAll();
            nutritions = nutritions.Where(n => n.UserId == userId).ToList();

            var averageFat = nutritions.Sum(nutrition => nutrition.Fat) / nutritions.Count;
            var averageProtein = nutritions.Sum(nutrition => nutrition.Protein) / nutritions.Count;
            var averageCarbs = nutritions.Sum(nutrition => nutrition.Cards) / nutritions.Count;
            var averageCalories = nutritions.Sum(nutrition => nutrition.Calories) / nutritions.Count;

            double coeficient = 0;

            if (averageCalories < 300)
            {
                coeficient += 25;
            }
            if (averageCalories > 700)
            {
                coeficient += 25;
            }
            if (averageCalories > 1100)
            {
                coeficient += 25;
            }

            var average = averageFat + averageProtein + averageCarbs / 3;

            if (!(averageFat > averageFat - average && averageFat < averageFat + average))
            {
                coeficient += 25;
            }

            if (!(averageCarbs > averageCarbs - average && averageCarbs < averageCarbs + average))
            {
                coeficient += 25;
            }

            if (!(averageProtein > averageProtein - average && averageProtein < averageProtein + average))
            {
                coeficient += 25;
            }

            return (1 - (coeficient / 100)) / 5;
        }
    }
}
