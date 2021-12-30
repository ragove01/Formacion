using Formacion.Data.Context.Resources;
using Formacion.Data.Context.Scheduler;
using Formacion.Controllers;
using Formacion.Data.Models.Resources;
using System;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Formacion.Data.Models.Scheduler;
using Formacion.Data;

namespace Testing
{
    public class TestFormacionData
    {

       
        public TestFormacionData()
        {
            DataBaseConections<ResourcesContext>.InitializeContext(new DbContextOptionsBuilder<ResourcesContext>()
                    .UseInMemoryDatabase(databaseName: "Test")
                    .Options);
            DataBaseConections<SchedulerConfigurationContext>.InitializeContext(new DbContextOptionsBuilder<SchedulerConfigurationContext>()
                    .UseInMemoryDatabase(databaseName: "Test")
                    .Options);

        }
        #region testing Resources 
        [Fact]
        public void Validate_Context_Initailize()
        {
            using (ResourcesContext context = this.CreateResourcesContext())
            {
                var resources = context.TextResources.ToList();
            }
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
            var resources = new ResourceController(this.CreateResourcesContext()).GetResources();
            Assert.NotNull(resources);

        }

        [Fact]
        public async void Test_get_resurces_cultures()
        {
            ResourceController resourceController = new ResourceController(this.CreateResourcesContext());
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
            using (SchedulerConfigurationContext context = this.CreateSchedulerContext())
            {
                SchedulerConfiguration config = new SchedulerConfiguration()
                {
                    Active = true,
                    Occurs = 0,
                    NumberOccurs = 1,
                    StartDate = DateTime.Now
                };
                context.SchedulerConfigurations.Add(config);
                context.SaveChanges();
                int configId = config.SchedulerId;
                config = null;
                config = context.SchedulerConfigurations.FirstOrDefault(c => c.SchedulerId == configId);
                Assert.NotNull(config);
                context.SchedulerConfigurations.Remove(config);
                context.SaveChanges();
                int numberConfigs = context.SchedulerConfigurations.Count(C => C.SchedulerId == config.SchedulerId);
                Assert.Equal(0, numberConfigs);
            }

        }

        [Fact]
        public void test_context_scheduler_daily_config()
        {
            using (SchedulerConfigurationContext context = this.CreateSchedulerContext())
            {
                SchedulerConfiguration config = this.CreateConfig();
                this.SetDailyConfig(config);
                context.SchedulerConfigurations.Add(config);
                context.SaveChanges();
                int configId = config.SchedulerId;
                config = null;
                config = context.SchedulerConfigurations.FirstOrDefault(D => D.SchedulerId == configId);
                Assert.NotNull(config.NumberOccursDailyConfiguration);
                context.SchedulerConfigurations.Remove(config);
                context.SaveChanges();
                int numberConfigs = context.SchedulerConfigurations.Count(C => C.SchedulerId == config.SchedulerId);
                Assert.Equal(0, numberConfigs);
            }
        }

        [Fact]
        public void test_context_scheduler_weekly_config()
        {
            using (SchedulerConfigurationContext context = this.CreateSchedulerContext())
            {
                SchedulerConfiguration config = this.CreateConfig();
                this.SetWeeklyConfig(config);
                context.SchedulerConfigurations.Add(config);
                context.SaveChanges();
                int configId = config.SchedulerId;
                config = null;
                config = context.SchedulerConfigurations.FirstOrDefault(C => C.SchedulerId == configId);
                Assert.NotNull(config.Every);
                config.Every = 4;
                context.SaveChanges();
                config = null;
                config = context.SchedulerConfigurations.FirstOrDefault(C => C.SchedulerId == configId);
                Assert.Equal((short)4, config.Every);
                context.SchedulerConfigurations.Remove(config);
                context.SaveChanges();
                int numberConfigs = context.SchedulerConfigurations.Count(C => C.SchedulerId == configId);
                Assert.Equal(0, numberConfigs);
            }
        }

        [Fact]
        public void test_context_scheduler_moonthly_config()
        {
            using (SchedulerConfigurationContext context = this.CreateSchedulerContext())
            {
                SchedulerConfiguration config = this.CreateConfig();
                this.SetMonthlyConfig(config);
                context.SchedulerConfigurations.Add(config);

                context.SaveChanges();
                int configId = config.SchedulerId;
                config = null;
                config = context.SchedulerConfigurations.FirstOrDefault(C => C.SchedulerId == configId);
                Assert.NotNull(config.EveryNumberMonths);
                config.EveryNumberMonths = 10;
                config = null;
                config = context.SchedulerConfigurations.FirstOrDefault(C => C.SchedulerId == configId);
                context.SaveChanges();
                Assert.Equal((short)10,config.EveryNumberMonths);
                context.SchedulerConfigurations.Remove(config);
                context.SaveChanges();
                int numberConfigs = context.SchedulerConfigurations.Count(C => C.SchedulerId == config.SchedulerId);
                Assert.Equal(0, numberConfigs);
            }
        }
        [Fact]
        public void test_getter_setter_scheduler_config()
        {
            SchedulerConfiguration config = new SchedulerConfiguration();
            config.SchedulerId = 0;
            Assert.Equal(0, config.SchedulerId);
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
            config.DateTimeNextExecution = new DateTime(2020, 1, 1);
            Assert.Equal(new DateTime(2020, 1, 1), config.DateTimeNextExecution);

            config.Frecuency = 1;
            Assert.Equal((short)1, config.Frecuency);
            config.OneTime = new TimeSpan(16, 0, 0);
            Assert.Equal(new TimeSpan(16, 0, 0), config.OneTime);
            config.TypeUnit = 1;
            Assert.Equal((short?)1, config.TypeUnit);
            config.NumberOccurs = 1;
            Assert.Equal(1, config.NumberOccurs);
            config.StartTime = new TimeSpan(16, 0, 0);
            Assert.Equal(new TimeSpan(16, 0, 0), config.StartTime);
            config.EndTime = new TimeSpan(16, 0, 0);
            Assert.Equal(new TimeSpan(16, 0, 0), config.EndTime);
            config.Every = 1;
            Assert.Equal((short)1, config.Every);
            config.Monday = true;
            Assert.True(config.Monday);
            config.Tuesday = true;
            Assert.True(config.Tuesday);
            config.Wednesday = true;
            Assert.True(config.Wednesday);
            config.Thursday = true;
            Assert.True(config.Thursday);
            config.Friday = true;
            Assert.True(config.Friday);
            config.Saturday = true;
            Assert.True(config.Saturday);
            config.Sunday = true;
            Assert.True(config.Sunday);
            config.TypeMonthlyConfiguration = 1;
            Assert.Equal((short)1, config.TypeMonthlyConfiguration);
            config.DayMonth = 1;
            Assert.Equal((short)1, config.DayMonth);
            config.EveryNumberMonths = 1;
            Assert.Equal((short)1, config.EveryNumberMonths);
            config.TypesEvery = 1;
            Assert.Equal((short)1, config.TypesEvery);
            config.TypesDayEvery = 1;
            Assert.Equal((short)1, config.TypesDayEvery);
        }

      

        private SchedulerConfiguration CreateConfig()
        {
            return new SchedulerConfiguration()
            {
                
                Active = true,
                Occurs = 0,
                Type = 1,
                NumberOccurs = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(365)
            };
        }

        private void SetDailyConfig(SchedulerConfiguration config)
        {
            config.Frecuency = 4;
            config.OneTime = new TimeSpan(12, 0, 0);
            config.TypeUnit = 0;
            config.NumberOccursDailyConfiguration = 1;
            config.StartTime = new TimeSpan(0, 0, 0);
            config.EndTime = new TimeSpan(23, 59, 59);
            
        }
        private void SetWeeklyConfig(SchedulerConfiguration config)
        {
            config.Every = 1;
            config.Monday = true;
            config.Tuesday = true;
            config.Wednesday = true;
            config.Thursday = true;
            config.Friday = true;
            config.Saturday = true;
            config.Sunday = true;
        }
        private void SetMonthlyConfig(SchedulerConfiguration config)
        {
            config.TypeMonthlyConfiguration = 1;
            config.DayMonth = 15;
            config.EveryNumberMonths = 1;
            config.TypesEvery = 0;
            config.TypesDayEvery = 0;
        }

        private ResourcesContext CreateResourcesContext()
        {
            var options = new DbContextOptionsBuilder<ResourcesContext>()
                    .UseInMemoryDatabase(databaseName: "Test")
                    .Options;
            ResourcesContext context = new ResourcesContext(DataBaseConections<ResourcesContext>.ContextOptions);
            context.Database.EnsureCreated();
            return context;
        }

        private SchedulerConfigurationContext CreateSchedulerContext()
        {
            var options = new DbContextOptionsBuilder<SchedulerConfigurationContext>()
                    .UseInMemoryDatabase(databaseName: "Test")
                    .Options;
            SchedulerConfigurationContext context = new SchedulerConfigurationContext(DataBaseConections<SchedulerConfigurationContext>.ContextOptions);
            context.Database.EnsureCreated();
            return context;
        }
        #endregion
    }
}
