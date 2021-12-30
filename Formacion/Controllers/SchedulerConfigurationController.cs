using Formacion.Configs;
using Formacion.Data.Context.Scheduler;
using Formacion.Data.Models.Scheduler;
using Formacion.Enums;
using Formacion.Instantiators;
using Formacion.TextsTranslations;
using Formacion.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Formacion.Controllers
{
    public class SchedulerConfigurationController : IDisposable
    {
         
    
        private SchedulerConfigurationContext context;
        public SchedulerConfigurationController(SchedulerConfigurationContext contextArgs)
        {
            this.context = contextArgs;
        }


        public async Task<SchedulerConfig> SaveConfiguration(SchedulerConfig config, SchedulerResults nextExecution)
        {
            if (config == null)
            {
                return null;
            }
            this.Validate(config);
            SchedulerConfiguration configBd = this.SetViewConfigToDataConfig(config, nextExecution, new SchedulerConfiguration());
            this.context.SchedulerConfigurations.Add(configBd);
           
            await context.SaveChangesAsync();
            config.SchedulerId = configBd.SchedulerId;
            return config;
        }

        public async Task<bool> UpdateConfig(SchedulerConfig config, SchedulerResults nextExecution)
        {
            if (config == null)
            {
                return false;
            }
            if (config.SchedulerId.HasValue == false)
            {
                return false;
            }

            this.Validate(config);
            SchedulerConfiguration configRecovered = await this.GetConfig(config.SchedulerId.Value);
            if (configRecovered == null)
            {
                return false;
            }
            configRecovered = this.SetViewConfigToDataConfig(config,nextExecution, configRecovered);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteConfig(int schedulerId)
        {
            SchedulerConfiguration configRecovered = await this.GetConfig(schedulerId);
            if (configRecovered == null)
            {
                return false;
            }
            this.context.SchedulerConfigurations.Remove(configRecovered); 
            await this.context.SaveChangesAsync();
            return true;
        }


        public async Task<SchedulerConfig> GetSchedulerConfiguration(int configurationId)
        {
            SchedulerConfiguration config = await this.GetConfig(configurationId);
            if (config == null)
            {
                throw new InvalidOperationException(String.Format(Translator.GetText(TextsIndex.NameConfigNotFound), configurationId));
            }
            return this.SetDataConfigToViewConfig(config);
        }

        public async Task<SchedulerConfig[]> GetSchedulerConfigurations(DateTime dateSearch)
        {
            SchedulerConfiguration[] configs = await this.GetConfigs(dateSearch);
            if (configs == null || configs.Length == 0)
            {
                return new SchedulerConfig[0];
            }
            SchedulerConfig[] configsReturn = new SchedulerConfig[configs.Length];
            for (int index = 0; index < configs.Length; index++)
            {
                configsReturn[index] = this.SetDataConfigToViewConfig(configs[index]);  
            }
            return configsReturn;
        }

        private async Task<SchedulerConfiguration[]> GetConfigs(DateTime dateSearch)
        {
            return await this.context.SchedulerConfigurations.Where(S=> S.DateTimeNextExecution <= dateSearch).OrderBy(S=> S.SchedulerId).ToArrayAsync();

        }

        private async Task<SchedulerConfiguration> GetConfig(int configId)
        {
            return await this.context.SchedulerConfigurations
                    .FirstOrDefaultAsync(C => C.SchedulerId == configId);

        }

        #region Methods to validate

        public void Validate(SchedulerConfig config)
        {
            this.ValidateSchedulerConfig(config);
            this.ValidateConfigBd(config);
        }

        public void ValidateSchedulerConfig(SchedulerConfig config)
        {
            InstantiatorValidator.GetValidator(config.Type).Validate(DateTime.Now, config);
        }
        public void ValidateConfigBd(SchedulerConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
     
        }
        #endregion

        #region Private methods to set values to models or models to views

        private SchedulerConfiguration SetViewConfigToDataConfig(SchedulerConfig schedulerConfig,
            SchedulerResults nextExecution, SchedulerConfiguration schedulerConfiguration)
        {
            
            schedulerConfiguration.Active = schedulerConfig.Active;
            schedulerConfiguration.Type = (int)schedulerConfig.Type;
            schedulerConfiguration.Occurs = (int)schedulerConfig.Occurs;
            schedulerConfiguration.DateTime = schedulerConfig.DateTime;
            schedulerConfiguration.StartDate = schedulerConfig.StartDate;
            schedulerConfiguration.EndDate = schedulerConfig.EndDate;
            schedulerConfiguration.DateTimeNextExecution = nextExecution != null?nextExecution.NextExecution:schedulerConfig.DateTimeNextExecution;  
            schedulerConfiguration.NumberOccurs = (int)schedulerConfig.NumberOccurs;
            this.SetViewDailyConfigToDataDailyConfig(schedulerConfig, schedulerConfiguration);
            this.SetViewWeeklyConfigToDataWeeklyConfig(schedulerConfig, schedulerConfiguration);
            this.SetViewMonthlyConfigToDataMonthlyConfig(schedulerConfig, schedulerConfiguration);
            return schedulerConfiguration; 

        }

        private void SetViewDailyConfigToDataDailyConfig(SchedulerConfig schedulerConfig, SchedulerConfiguration schedulerConfiguration)
        {
            if (schedulerConfig.DailyFrecuenci == null)
            {
                this.InitializeDailyConfiguration(schedulerConfiguration);
                return;
            }
            schedulerConfiguration.Frecuency = (short)schedulerConfig.DailyFrecuenci.Frecuenci;
            schedulerConfiguration.NumberOccursDailyConfiguration = schedulerConfig.DailyFrecuenci.NumberOccurs;
            schedulerConfiguration.OneTime = schedulerConfig.DailyFrecuenci.OnceTime;
            schedulerConfiguration.EndTime = schedulerConfig.DailyFrecuenci.EndTime;
            schedulerConfiguration.StartTime = schedulerConfig.DailyFrecuenci.StartTime;
            schedulerConfiguration.TypeUnit = (short)schedulerConfig.DailyFrecuenci.TypeUnit;
            
        }

        private void InitializeDailyConfiguration(SchedulerConfiguration schedulerConfiguration)
        {
            schedulerConfiguration.Frecuency = null;
            schedulerConfiguration.NumberOccursDailyConfiguration = null;
            schedulerConfiguration.OneTime = null;
            schedulerConfiguration.EndTime = null;
            schedulerConfiguration.StartTime = null;
            schedulerConfiguration.TypeUnit = null;
        }


        private void SetViewWeeklyConfigToDataWeeklyConfig(SchedulerConfig schedulerConfig, SchedulerConfiguration schedulerConfiguration)
        {
            if (schedulerConfig.Weekly == null)
            {
                this.InitializeWeeklyConfiguration(schedulerConfiguration);
                return;
            }
            schedulerConfiguration.Every = (short)schedulerConfig.Weekly.Every;
            schedulerConfiguration.Monday = schedulerConfig.Weekly.Monday;
            schedulerConfiguration.Tuesday = schedulerConfig.Weekly.Tuesday;
            schedulerConfiguration.Wednesday = schedulerConfig.Weekly.Wednesday;
            schedulerConfiguration.Thursday = schedulerConfig.Weekly.Thursday;
            schedulerConfiguration.Friday = schedulerConfig.Weekly.Friday;
            schedulerConfiguration.Saturday = schedulerConfig.Weekly.Saturday;
            schedulerConfiguration.Sunday = schedulerConfig.Weekly.Sunday;
            
        }

        private void InitializeWeeklyConfiguration(SchedulerConfiguration schedulerConfiguration)
        {
            schedulerConfiguration.Every = null;
            schedulerConfiguration.Monday = false;
            schedulerConfiguration.Tuesday = false;
            schedulerConfiguration.Wednesday = false;
            schedulerConfiguration.Thursday = false;
            schedulerConfiguration.Friday = false;
            schedulerConfiguration.Saturday = false;
            schedulerConfiguration.Sunday = false;
        }

        private void SetViewMonthlyConfigToDataMonthlyConfig(SchedulerConfig schedulerConfig, SchedulerConfiguration schedulerConfiguration)
        {
            if (schedulerConfig.Monthly == null)
            {
                this.InitializeMonthlyConfiguaration(schedulerConfiguration);
                return;
            }
            schedulerConfiguration.TypeMonthlyConfiguration = (short)schedulerConfig.Monthly.Type;
            schedulerConfiguration.DayMonth = (short?)schedulerConfig.Monthly.DayMonth;
            schedulerConfiguration.EveryNumberMonths = (short)schedulerConfig.Monthly.EveryNumberMonths;
            schedulerConfiguration.TypesDayEvery = (short?)schedulerConfig.Monthly.TypesDayEvery;
            schedulerConfiguration.TypesEvery = (short?)schedulerConfig.Monthly.TypesEvery;
          
        }

        private void InitializeMonthlyConfiguaration(SchedulerConfiguration schedulerConfiguration)
        {
            schedulerConfiguration.TypeMonthlyConfiguration = null;
            schedulerConfiguration.DayMonth = null;
            schedulerConfiguration.EveryNumberMonths = null;
            schedulerConfiguration.TypesDayEvery = null;
            schedulerConfiguration.TypesEvery = null;
        }


        private SchedulerConfig SetDataConfigToViewConfig(SchedulerConfiguration config)
        {
            return new SchedulerConfig()
            {
                SchedulerId = config.SchedulerId,
                Active = config.Active,
                Type = (TypesSchedule)config.Type,
                Occurs = (TypesOccurs)config.Occurs,
                DateTime = config.DateTime,
                StartDate = config.StartDate,
                EndDate = config.EndDate,
                DateTimeNextExecution = config.DateTimeNextExecution, 

                NumberOccurs = (int)config.NumberOccurs,
                DailyFrecuenci = this.SetDataDailyConfigToViewDataDailyConfig(config),
                Weekly = this.SetDataWeeklyConfigToViewWeeklyConfig(config),
                Monthly = this.SetDataMonthlyConfigToViewMonthlyConfig(config)
            };
        }

        private ConfigDailyFrecuency SetDataDailyConfigToViewDataDailyConfig(SchedulerConfiguration config)
        {
            if (config == null || config.Frecuency == null)
            {
                return null;
            }
            return new ConfigDailyFrecuency()
            {
                Frecuenci = (TypesOccursDailyFrecuency)config.Frecuency,
                NumberOccurs = config.NumberOccursDailyConfiguration.GetValueOrDefault(),
                OnceTime = config.OneTime,
                EndTime = config.EndTime,
                StartTime = config.StartTime,
                TypeUnit = (TypesUnitsDailyFrecuency)config.TypeUnit
            };
        }

        private ConfigWeekly SetDataWeeklyConfigToViewWeeklyConfig(SchedulerConfiguration config)
        {
            if (config == null || config.Every == null)
            {
                return null;
            }
            return new ConfigWeekly()
            {
                Every = (int)config.Every.GetValueOrDefault(),
                Monday = config.Monday,
                Tuesday = config.Tuesday,
                Wednesday = config.Wednesday,
                Thursday = config.Thursday,
                Friday = config.Friday,
                Saturday = config.Saturday,
                Sunday = config.Sunday
            };

        }

        private ConfigMonthly SetDataMonthlyConfigToViewMonthlyConfig(SchedulerConfiguration config)
        {
            if (config == null || config.TypeMonthlyConfiguration == null)
            {
                return null;
            }
            return new ConfigMonthly()
            {
                Type = (TypesMontlyFrecuency)config.TypeMonthlyConfiguration,
                DayMonth = (int?)config.DayMonth,
                EveryNumberMonths = (short)config.EveryNumberMonths,
                TypesDayEvery = (TypesEveryDayMonthly?)config.TypesDayEvery,
                TypesEvery = (TypesEveryMonthly?)config.TypesEvery
            };
        }
        #endregion

        public void Dispose()
        {
            if (this.context != null)
            {
                this.context.Dispose();
            }
        }
    }
}
