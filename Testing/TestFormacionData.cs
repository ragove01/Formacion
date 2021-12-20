using Formacion.Data.Context.Resources;
using Formacion.Data.Context.Scheduler;
using Formacion.Controller;
using Formacion.Data.Models.Resources;
using Formacion.Data.Models.Scheluder;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Testing
{
    public class TestFormacionData
    {

        private readonly ResourcesContext context;
        public TestFormacionData()
        {
            this.context = new ResourcesContext();
        }
        #region testing Resources 
        [Fact]
        public void Validate_Context_Initailize()
        {
            ResourcesContext context = new ResourcesContext();
            var resources = context.TextResources.ToList();
        }
        [Fact]
        public void test_getter_setter_text_resources()
        {
            TextResource textResouce = new TextResource();
            textResouce.TextResourceId = 1;
            Assert.Equal(1,textResouce.TextResourceId);
            textResouce.TextIndex = "TextTest";
            Assert.Equal("TextTest", textResouce.TextIndex);
            textResouce.TextValue = "TextTestValue";
            Assert.Equal("TextTestValue", textResouce.TextValue);
        }
        [Fact]
        public void test_getter_setter_resource_culture()
        {
            ResourceCulture culture = new ResourceCulture();
            culture.ResourceCultureId = 1;
            Assert.Equal(1, culture.ResourceCultureId);
            culture.CultureName = "es-ES";
            Assert.Equal("es-ES", culture.CultureName);
        }

        [Fact]
        public void test_getter_setter_text_resource_culture()
        {
            TextResourceCulture textCulture = new TextResourceCulture();
            textCulture.TextResourceCultureId = 1;
            Assert.Equal(1, textCulture.TextResourceCultureId);

            textCulture.TextResourceId = 1;
            Assert.Equal(1, textCulture.TextResourceId);
            textCulture.ResourceCultureId = 1;
            Assert.Equal(1,textCulture.ResourceCultureId);
            textCulture.TextValue = "TextValue";
            Assert.Equal("TextValue",textCulture.TextValue);

            textCulture.Resource = new TextResource();
            Assert.NotNull(textCulture.Resource);
            textCulture.Culture = new ResourceCulture();
            Assert.NotNull(textCulture.Culture);
        }



        [Fact]
        public void Test_get_resurces()
        {
            var resources = new ResourceController(this.context).GetResources();
            Assert.NotNull(resources);

        }

        [Fact]
        public async void Test_get_resurces_cultures()
        {
            ResourceController resourceController = new ResourceController(this.context);
            var resources = await resourceController.GetResources("es-ES");
            Assert.Null(resources);
          
            resources = await resourceController.GetResources("en-GB");
            Assert.NotNull(resources);
            Assert.Equal("en-GB", resources.ElementAtOrDefault(0).Culture.CultureName);

        }
        #endregion
        #region Test scheduler config
        [Fact]
        public void Test_create_update_scheduler_config()
        {
            SchedulerConfigContext context = new SchedulerConfigContext();
            SchedulerConfig config = new SchedulerConfig()
            {
                Name = "Test create",
                Active = true,
                Occurs = 0,
                NumberOccurs = 1,
                StartDate = DateTime.Now
            };
            context.Configs.Add(config);
            context.SaveChanges();
            config.Name = "Test create modified";
            context.SaveChanges();
            config = context.Configs.FirstOrDefault(c => c.SchedulerConfigId == config.SchedulerConfigId);
            Assert.Equal("Test create modified", config.Name);
            context.Configs.Remove(config);
            context.SaveChanges();
            int numberConfigs = context.Configs.Count(C => C.SchedulerConfigId == config.SchedulerConfigId);
            Assert.Equal(0, numberConfigs);

        }

        [Fact]
        public void test_context_scheduler_daily_config()
        {
            SchedulerConfigContext context = new SchedulerConfigContext();
            SchedulerConfig config = this.CreateConfig();
            config.DailyConfig = this.CreateDailyConfig(); 
            context.Configs.Add(config);
            context.DailyConfigs.Add(config.DailyConfig); 
            context.SaveChanges();
            SchedulerDailyConfig dailyConfig = context.DailyConfigs.FirstOrDefault(D => D.SchedulerConfigId == config.SchedulerConfigId);
            Assert.NotNull(dailyConfig);  
            dailyConfig.Frecuency = 2;
            context.SaveChanges();
            config.DailyConfig = null;
            context.DailyConfigs.Remove(dailyConfig);
            context.SaveChanges();
            int numberConfigs = context.DailyConfigs.Count(D => D.SchedulerConfigId == config.SchedulerConfigId);
            Assert.Equal(0, numberConfigs);
            context.Configs.Remove(config);
            context.SaveChanges();
            numberConfigs = context.Configs.Count(C => C.SchedulerConfigId == config.SchedulerConfigId);
            Assert.Equal(0, numberConfigs);
        }

        [Fact]
        public void test_context_scheduler_weekly_config()
        {
            SchedulerConfigContext context = new SchedulerConfigContext();
            SchedulerConfig config = this.CreateConfig();
            config.WeeklyConfig  = this.CreateWeeklyConfig();
            context.Configs.Add(config);
            context.WeeklyConfigs.Add(config.WeeklyConfig);
            context.SaveChanges();
            SchedulerWeeklyConfig weeklyConfig = context.WeeklyConfigs.FirstOrDefault(W => W.SchedulerConfigId == config.SchedulerConfigId);
            Assert.NotNull(weeklyConfig);
            weeklyConfig.Every  = 4;
            context.SaveChanges();
            config.WeeklyConfig = null;
            context.WeeklyConfigs.Remove(weeklyConfig);
            context.SaveChanges();
            int numberConfigs = context.WeeklyConfigs.Count(W => W.SchedulerConfigId == config.SchedulerConfigId);
            Assert.Equal(0, numberConfigs);
            context.Configs.Remove(config);
            context.SaveChanges();
            numberConfigs = context.Configs.Count(C => C.SchedulerConfigId == config.SchedulerConfigId);
            Assert.Equal(0, numberConfigs);
        }

        [Fact]
        public void test_context_scheduler_moonthly_config()
        {
            SchedulerConfigContext context = new SchedulerConfigContext();
            SchedulerConfig config = this.CreateConfig();
            config.MonthlyConfig = this.CreateMonthlyConfig();
            context.Configs.Add(config);
            context.MonthlyConfigs.Add(config.MonthlyConfig);
            context.SaveChanges();
            SchedulerMonthlyConfig monthlyConfig = context.MonthlyConfigs.FirstOrDefault(M => M.SchedulerConfigId == config.SchedulerConfigId);
            Assert.NotNull(monthlyConfig);
            monthlyConfig.EveryNumberMonths = 10;
            context.SaveChanges();
            config.MonthlyConfig = null;
            context.MonthlyConfigs.Remove(monthlyConfig);
            context.SaveChanges();
            int numberConfigs = context.MonthlyConfigs.Count(M => M.SchedulerConfigId == config.SchedulerConfigId);
            Assert.Equal(0, numberConfigs);
            context.Configs.Remove(config);
            context.SaveChanges();
            numberConfigs = context.Configs.Count(C => C.SchedulerConfigId == config.SchedulerConfigId);
            Assert.Equal(0, numberConfigs);
        }
        [Fact]
        public void test_getter_setter_scheduler_config()
        {
            SchedulerConfig config = new SchedulerConfig();
            config.SchedulerConfigId = 0;
            Assert.Equal(0, config.SchedulerConfigId);
            config.Name = "Name";
            Assert.Equal("Name", config.Name);
            config.Type = 1;
            Assert.Equal(1, config.Type);
            config.Active = true;
            Assert.True(config.Active);
            config.Occurs = 1;
            Assert.Equal(1, config.Occurs);
            config.NumberOccurs = 1;
            Assert.Equal(1, config.NumberOccurs);
            config.DateTime = new DateTime(2020, 1, 1);
            Assert.Equal(new DateTime(2020, 1, 1), config.DateTime);
            config.StartDate = new DateTime(2020, 1, 1);
            Assert.Equal(new DateTime(2020, 1, 1), config.StartDate);
            config.EndDate = new DateTime(2020, 1, 1);
            Assert.Equal(new DateTime(2020, 1, 1), config.EndDate);
            config.DailyConfig = new SchedulerDailyConfig();
            Assert.NotNull(config.DailyConfig);
            config.WeeklyConfig = new SchedulerWeeklyConfig();
            Assert.NotNull(config.WeeklyConfig);
            config.MonthlyConfig = new SchedulerMonthlyConfig();
            Assert.NotNull(config.MonthlyConfig);
        }

        [Fact]
        public void test_getter_setter_daily_config()
        {
            SchedulerDailyConfig dailyConfig = new SchedulerDailyConfig();
            dailyConfig.SchedulerDailyConfigId = 1;
            Assert.Equal(1, dailyConfig.SchedulerDailyConfigId);
            dailyConfig.SchedulerConfigId = 1;
            Assert.Equal(1, dailyConfig.SchedulerConfigId);
            dailyConfig.Frecuency = 1;
            Assert.Equal(1, dailyConfig.Frecuency);
            dailyConfig.OneTime = new TimeSpan(16, 0, 0);
            Assert.Equal(new TimeSpan(16, 0, 0), dailyConfig.OneTime);
            dailyConfig.TypeUnit = 1;
            Assert.Equal((short?)1, dailyConfig.TypeUnit);
            dailyConfig.NumberOccurs = 1;
            Assert.Equal(1, dailyConfig.NumberOccurs);
            dailyConfig.StartTime = new TimeSpan(16, 0, 0);
            Assert.Equal(new TimeSpan(16, 0, 0), dailyConfig.StartTime);
            dailyConfig.EndTime = new TimeSpan(16, 0, 0);
            Assert.Equal(new TimeSpan(16, 0, 0), dailyConfig.EndTime);

            dailyConfig.Config = new SchedulerConfig();
            Assert.NotNull(dailyConfig.Config); 


        }

        [Fact]
        public void test_getter_setter_weekly_config()
        {
            SchedulerWeeklyConfig weeklyConfig = new SchedulerWeeklyConfig();
            weeklyConfig.SchedulerWeeklyConfigId = 1;
            Assert.Equal(1, weeklyConfig.SchedulerWeeklyConfigId);
            weeklyConfig.SchedulerConfigId = 1;
            Assert.Equal(1, weeklyConfig.SchedulerConfigId);
            weeklyConfig.Every = 1;
            Assert.Equal(1, weeklyConfig.Every);

            weeklyConfig.Monday = true;
            Assert.True(weeklyConfig.Monday); 
            weeklyConfig.Tuesday = true;
            Assert.True(weeklyConfig.Tuesday);
            weeklyConfig.Wednesday = true;
            Assert.True(weeklyConfig.Wednesday);
            weeklyConfig.Thursday = true;
            Assert.True(weeklyConfig.Thursday);
            weeklyConfig.Friday = true;
            Assert.True(weeklyConfig.Friday);
            weeklyConfig.Saturday = true;
            Assert.True(weeklyConfig.Saturday);
            weeklyConfig.Sunday = true;
            Assert.True(weeklyConfig.Sunday);
            weeklyConfig.Config = new SchedulerConfig();
            Assert.NotNull(weeklyConfig.Config);
        }

        [Fact]
        public void test_getter_setter_monthly_config()
        {
            SchedulerMonthlyConfig monthlyConfig = new SchedulerMonthlyConfig();
            monthlyConfig.SchedulerMonthlyConfigId = 1;
            Assert.Equal(1, monthlyConfig.SchedulerMonthlyConfigId);
            monthlyConfig.SchedulerConfigId = 1;
            Assert.Equal(1, monthlyConfig.SchedulerConfigId);
            monthlyConfig.Type = 1;
            Assert.Equal(1, monthlyConfig.Type);
            monthlyConfig.DayMonth = 1;
            Assert.Equal((short)1, monthlyConfig.DayMonth);
            monthlyConfig.EveryNumberMonths = 1;
            Assert.Equal((short)1, monthlyConfig.EveryNumberMonths);
            monthlyConfig.TypesEvery = 1;
            Assert.Equal((short)1, monthlyConfig.TypesEvery);
            monthlyConfig.TypesDayEvery = 1;
            Assert.Equal((short)1, monthlyConfig.TypesDayEvery);
            monthlyConfig.Config = new SchedulerConfig();
            Assert.NotNull(monthlyConfig.Config);
        }

        private SchedulerConfig CreateConfig()
        {
            return new SchedulerConfig()
            {
                Name = "Test create",
                Active = true,
                Occurs = 0,
                Type = 1,
                NumberOccurs = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(365)
            };
        }

        private SchedulerDailyConfig CreateDailyConfig()
        {
            return new SchedulerDailyConfig()
            {
                Frecuency = 4,
                OneTime = new TimeSpan(12, 0, 0),
                TypeUnit = 0,
                NumberOccurs = 1,
                StartTime = new TimeSpan(0, 0, 0),
                EndTime = new TimeSpan(23, 59, 59)
            };
        }
        private SchedulerWeeklyConfig CreateWeeklyConfig()
        {
            return new SchedulerWeeklyConfig()
            {
                Every = 1,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = true,
                Sunday = true
            };
        }
        private SchedulerMonthlyConfig CreateMonthlyConfig()
        {
            return new SchedulerMonthlyConfig()
            {
                Type = 1,
                DayMonth = 15,
                EveryNumberMonths = 1,
                TypesEvery = 0,
                TypesDayEvery = 0
            };
        }
        #endregion
    }
}
