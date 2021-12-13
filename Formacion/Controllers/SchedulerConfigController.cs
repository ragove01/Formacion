using Formacion.Data.Context.Scheduler;
using D=Formacion.Data.Models.Scheluder;
using Formacion.Views; 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formacion.Enums;
using Formacion.Configs;
using Formacion.Instantiators;
using Formacion.TextsTranslations;

namespace Formacion.Controller
{
    public class SchedulerConfigController:IDisposable 
    {
        private SchedulerConfigContext context;
        public SchedulerConfigController(SchedulerConfigContext contextArgs)
        {
            this.context = contextArgs;
        }

        public async Task<SchedulerConfig> SaveConfig(SchedulerConfig config)
        {
            if (config == null)
            {
                return null;
            }
            this.Validate(config); 
            D.SchedulerConfig configBd = this.SetViewConfigToDataConfig(config, new D.SchedulerConfig()); 
            this.context.Configs.Add(configBd);
            if (configBd.DailyConfig != null)
            {
                this.context.DailyConfigs.Add(configBd.DailyConfig);
            }
            if (configBd.WeeklyConfig != null)
            {
                this.context.WeeklyConfigs.Add(configBd.WeeklyConfig);
            }
            if (configBd.MonthlyConfig != null)
            {
                this.context.MonthlyConfigs.Add(configBd.MonthlyConfig);
            }
            await context.SaveChangesAsync();
            config.SchedulerConfigId = configBd.SchedulerConfigId;  
            return config;
        }

        public async Task<bool> UpdateConfig(SchedulerConfig config)
        {
            if (config == null)
            {
                return false;
            }
            if(config.SchedulerConfigId.HasValue == false)
            {
                return false;
            }

            this.Validate(config); 
            D.SchedulerConfig configRecovered = await this.GetConfig(config.SchedulerConfigId.Value);
            if (configRecovered == null)
            {
                return false;
            }
            configRecovered = this.SetViewConfigToDataConfig(config, configRecovered);
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<SchedulerConfig> GetSchedulerConfiguration(string configurationName)
        {
            D.SchedulerConfig config = await this.GetConfig(configurationName);
            if (config == null)
            {
                throw new InvalidOperationException(String.Format(Translator.GetText(TextsIndex.NameConfigNotFound), configurationName));
            }
            return this.SetDataConfigToViewConfig(config);
        }

        public async Task<SchedulerConfig> GetSchedulerConfiguration(int configurationId)
        {
            D.SchedulerConfig config = await this.GetConfig(configurationId);
            if (config == null)
            {
                throw new InvalidOperationException(String.Format(Translator.GetText(TextsIndex.NameConfigNotFound), configurationId));
            }
            return this.SetDataConfigToViewConfig(config);
        }

        public async Task<D.SchedulerConfig> GetConfig(string nameConfig)
        {
            return await this.context.Configs
                .Include(C => C.DailyConfig)
                .Include(C => C.WeeklyConfig)
                .Include(C => C.MonthlyConfig)
                .FirstOrDefaultAsync(C => C.Name == nameConfig);

        }

        public async Task<D.SchedulerConfig> GetConfig(int configId)
        {
            return await this.context.Configs
                .Include(C => C.DailyConfig)
                .Include(C => C.WeeklyConfig)
                .Include(C => C.MonthlyConfig)
                .FirstOrDefaultAsync(C => C.SchedulerConfigId == configId);

        }

      

        public int? GetConfigId(string nameConfig)
        {
            var configsId = from config
                           in this.context.Configs
                            where config.Name == nameConfig
                            select config.SchedulerConfigId;
            if (configsId == null || configsId.Count() == 0)
            {
                return null;
            }
            return configsId.FirstOrDefault();
        }

        public async Task<bool> RemoveConfig(int configId)
        {
            D.SchedulerConfig config = await this.context.Configs
                .Include(C => C.DailyConfig)
                .Include(C => C.WeeklyConfig)
                .Include(C => C.MonthlyConfig)
                .FirstOrDefaultAsync(C => C.SchedulerConfigId == configId);
            if (config == null)
            {
                return false;
            }
            if (config.DailyConfig != null)
            {
                this.context.DailyConfigs.Remove(config.DailyConfig);
            }
            if (config.WeeklyConfig != null)
            {
                this.context.WeeklyConfigs.Remove(config.WeeklyConfig);
            }
            if (config.MonthlyConfig != null)
            {
                this.context.MonthlyConfigs.Remove(config.MonthlyConfig);
            }
            this.context.Configs.Remove(config);
            await this.context.SaveChangesAsync();
            return true;
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
            if(config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            if(string.IsNullOrEmpty(config.Name))
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.NameConfigNotFound));
            }
            int? schedulerConfigId = this.GetConfigId(config.Name);
            if (schedulerConfigId != config.SchedulerConfigId)
            {
                throw new ApplicationException(Translator.GetText(TextsIndex.NameDuplicate));
            }
        }
        #endregion
        



        #region Mapped methods
        private D.SchedulerConfig SetViewConfigToDataConfig(SchedulerConfig schedulerConfig, D.SchedulerConfig schedulerConfigBd)
        {
            schedulerConfigBd.SchedulerConfigId = schedulerConfig.SchedulerConfigId.GetValueOrDefault(); 
            schedulerConfigBd.Name = schedulerConfig.Name;
            schedulerConfigBd.Active = schedulerConfig.Active;
            schedulerConfigBd.Type = (int)schedulerConfig.Type;
            schedulerConfigBd.Occurs = (int)schedulerConfig.Occurs;
            schedulerConfigBd.DateTime = schedulerConfig.DateTime;
            schedulerConfigBd.StartDate = schedulerConfig.StartDate;
            schedulerConfigBd.EndDate = schedulerConfig.EndDate;
            schedulerConfigBd.NumberOccurs = (int)schedulerConfig.NumberOccurs;
            schedulerConfigBd.DailyConfig = this.SetViewDailyConfigToDataDailyConfig(schedulerConfig, schedulerConfigBd.DailyConfig);
            schedulerConfigBd.WeeklyConfig = this.SetViewWeeklyConfigToDataWeeklyConfig(schedulerConfig, schedulerConfigBd.WeeklyConfig);
            schedulerConfigBd.MonthlyConfig = this.SetViewMonthlyConfigToDataMonthlyConfig(schedulerConfig, schedulerConfigBd.MonthlyConfig);
            return schedulerConfigBd;

        }
        private D.SchedulerDailyConfig SetViewDailyConfigToDataDailyConfig(SchedulerConfig schedulerConfig, D.SchedulerDailyConfig dailyConfigDb)
        {
            if (schedulerConfig.DailyFrecuenci == null)
            {
                return null;
            }
            if (dailyConfigDb == null)
            {
                dailyConfigDb = new D.SchedulerDailyConfig();
            }

            dailyConfigDb.Frecuency = (short)schedulerConfig.DailyFrecuenci.Frecuenci;
            dailyConfigDb.NumberOccurs = schedulerConfig.DailyFrecuenci.NumberOccurs;
            dailyConfigDb.OneTime = schedulerConfig.DailyFrecuenci.OnceTime;
            dailyConfigDb.EndTime = schedulerConfig.DailyFrecuenci.EndTime;
            dailyConfigDb.StartTime = schedulerConfig.DailyFrecuenci.StartTime;
            dailyConfigDb.TypeUnit = (short)schedulerConfig.DailyFrecuenci.TypeUnit;
            return dailyConfigDb;
        }
        private D.SchedulerWeeklyConfig SetViewWeeklyConfigToDataWeeklyConfig(SchedulerConfig schedulerConfig, D.SchedulerWeeklyConfig weeklyConfigBd)
        {
            if (schedulerConfig.Weekly == null)
            {
                return null;
            }
            if (weeklyConfigBd == null)
            {
                weeklyConfigBd = new D.SchedulerWeeklyConfig();
            }
            weeklyConfigBd.Every = (short)schedulerConfig.Weekly.Every;
            weeklyConfigBd.Monday = schedulerConfig.Weekly.Monday;
            weeklyConfigBd.Tuesday = schedulerConfig.Weekly.Tuesday;
            weeklyConfigBd.Wednesday = schedulerConfig.Weekly.Wednesday;
            weeklyConfigBd.Thursday = schedulerConfig.Weekly.Thursday;
            weeklyConfigBd.Friday = schedulerConfig.Weekly.Friday;
            weeklyConfigBd.Saturday = schedulerConfig.Weekly.Saturday;
            weeklyConfigBd.Sunday = schedulerConfig.Weekly.Sunday;
            return weeklyConfigBd;
        }

        private D.SchedulerMonthlyConfig SetViewMonthlyConfigToDataMonthlyConfig(SchedulerConfig schedulerConfig, D.SchedulerMonthlyConfig monthlyConfigBd)
        {
            if (schedulerConfig.Monthly == null)
            {
                return null;
            }
            if (monthlyConfigBd == null)
            {
                monthlyConfigBd = new D.SchedulerMonthlyConfig();
            }
            monthlyConfigBd.Type = (short)schedulerConfig.Monthly.Type;
            monthlyConfigBd.DayMonth = (short?)schedulerConfig.Monthly.DayMonth;
            monthlyConfigBd.EveryNumberMonths = (short)schedulerConfig.Monthly.EveryNumberMonths;
            monthlyConfigBd.TypesDayEvery = (short?)schedulerConfig.Monthly.TypesDayEvery;
            monthlyConfigBd.TypesEvery = (short?)schedulerConfig.Monthly.TypesEvery;
            return monthlyConfigBd;
        }

       


        private SchedulerConfig SetDataConfigToViewConfig(D.SchedulerConfig config)
        {
            return new SchedulerConfig()
            {
                SchedulerConfigId = config.SchedulerConfigId,
                Name = config.Name,
                Active = config.Active,
                Type = (TypesSchedule)config.Type,
                Occurs = (TypesOccurs)config.Occurs,
                DateTime = (DateTime)config.DateTime,
                StartDate = config.StartDate,
                EndDate = config.EndDate,
                NumberOccurs = (int)config.NumberOccurs,
                DailyFrecuenci = this.SetDataDailyConfigToViewDataDailyConfig(config),
                Weekly = this.SetDatraWeeklyConfigToViewWeeklyConfig(config),
                Monthly = this.SetDataMonthlyConfigToViewMonthlyConfig(config)
            };
        }

        private ConfigDailyFrecuency SetDataDailyConfigToViewDataDailyConfig(D.SchedulerConfig config)
        {
            if (config == null || config.DailyConfig == null)
            {
                return null;
            }
            return new ConfigDailyFrecuency()
            {
                Frecuenci = (TypesOccursDailyFrecuency)config.DailyConfig.Frecuency,
                NumberOccurs = config.DailyConfig.NumberOccurs,
                OnceTime = config.DailyConfig.OneTime,
                EndTime = config.DailyConfig.EndTime,
                StartTime = config.DailyConfig.StartTime,
                TypeUnit = (TypesUnitsDailyFrecuency)config.DailyConfig.TypeUnit
            };
        }

        private ConfigWeekly SetDatraWeeklyConfigToViewWeeklyConfig(D.SchedulerConfig config)
        {
            if (config == null || config.WeeklyConfig == null)
            {
                return null;
            }
            return new ConfigWeekly()
            {
                Every = (int)config.WeeklyConfig.Every,
                Monday = config.WeeklyConfig.Monday,
                Tuesday = config.WeeklyConfig.Tuesday,
                Wednesday = config.WeeklyConfig.Wednesday,
                Thursday = config.WeeklyConfig.Thursday,
                Friday = config.WeeklyConfig.Friday,
                Saturday = config.WeeklyConfig.Saturday,
                Sunday = config.WeeklyConfig.Sunday
            };

        }

        private ConfigMonthly SetDataMonthlyConfigToViewMonthlyConfig(D.SchedulerConfig config)
        {
            if (config == null || config.MonthlyConfig == null)
            {
                return null;
            }
            return new ConfigMonthly()
            {
                Type = (TypesMontlyFrecuency)config.MonthlyConfig.Type,
                DayMonth = (int?)config.MonthlyConfig.DayMonth,
                EveryNumberMonths = (short)config.MonthlyConfig.EveryNumberMonths,
                TypesDayEvery = (TypesEveryDayMonthly?)config.MonthlyConfig.TypesDayEvery,
                TypesEvery = (TypesEveryMonthly?)config.MonthlyConfig.TypesEvery
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
