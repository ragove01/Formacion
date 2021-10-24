using Formacion;
using Formacion.Calculators;
using Formacion.Configs;
using Formacion.Enums;
using Formacion.Formatters;
using Formacion.Instantiators;
using Formacion.Validators;
using Formacion.Views;
using Formacion.Extensions;
using Tx = Formacion.Validators.Texts;
using System;
using System.Globalization;
using Xunit;

namespace Testing
{
    public class TestScheduler
    {
        #region Validations
        #region Validations config once
        private static readonly DateTimeFormatInfo formaterDateTimeSpanish = CultureInfo.GetCultureInfo("es-ES").DateTimeFormat;

        [Fact]
        public void ValidatorConfigOnce_no_config_value()
        {
            ValidatorConfigOnce validator = new ValidatorConfigOnce();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(DateTime.Now, null));
            Assert.Equal(Tx.ConfigMustHasValue, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigBase_dateTime_Max_value()
        {
            ValidatorConfigBase validator = new ValidatorConfigBase();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(DateTime.MaxValue, new SchedulerConfig()));
            Assert.Equal(Tx.CurrentDateInvalid, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigBase_start_dateTime_Max_value()
        {
            SchedulerConfig schedulerData = new SchedulerConfig();
            schedulerData.StartDate = DateTime.MaxValue;
            ValidatorConfigBase validator = new ValidatorConfigBase();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(DateTime.Now, schedulerData));
            Assert.Equal(Tx.StartDateInvalid, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigOnce_type_Config_incorrect()
        {
            SchedulerConfig schedulerData = new SchedulerConfig();
            schedulerData.StartDate = new DateTime(2020, 1, 1);
            ValidatorConfigOnce validator = new ValidatorConfigOnce();
            schedulerData.Type = TypesSchedule.Recurring;
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(new DateTime(2020, 1, 4), schedulerData));
            Assert.Equal(Tx.WrongConfiguration, TheException.Message);
        }

        [Fact]
        public void ValidatorConfigOnce_date_time_has_value()
        {
            SchedulerConfig schedulerData = new SchedulerConfig();
            schedulerData.StartDate = new DateTime(2020, 1, 1);
            ValidatorConfigOnce validator = new ValidatorConfigOnce();
            schedulerData.Type = TypesSchedule.Once;
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(new DateTime(2020, 1, 4), schedulerData));
            Assert.Equal(Tx.DateTimeMustHasValue, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigOnce_end_date_must_be_great_than_start_date()
        {
            SchedulerConfig schedulerData = new SchedulerConfig();
            schedulerData.StartDate = new DateTime(2020, 1, 1);
            ValidatorConfigOnce validator = new ValidatorConfigOnce();
            schedulerData.Type = TypesSchedule.Once;
            schedulerData.EndDate = new DateTime(2019, 12, 31);
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(new DateTime(2020, 1, 4), schedulerData));
            Assert.Equal(Tx.EndDateGreatStartDate, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigOnce_ok()
        {
            SchedulerConfig schedulerData = new SchedulerConfig();
            schedulerData.StartDate = new DateTime(2020, 1, 1);
            ValidatorConfigOnce validator = new ValidatorConfigOnce();
            schedulerData.Type = TypesSchedule.Once;
            schedulerData.EndDate = null;
            schedulerData.DateTime = new DateTime(2020, 1, 4, 14, 0, 0);
            validator.Validate(new DateTime(2020, 1, 4), schedulerData);
        }
        #endregion
        #region validation config recurring
        [Fact]
        public void ValidatorConfigRecurring_no_config()
        {

            ValidatorConfigRecurring validator = new ValidatorConfigRecurring();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(DateTime.Now, null));
            Assert.Equal(Tx.ConfigMustHasValue, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigRecurring_type_Config_incorrect()
        {
            SchedulerConfig schedulerData = new SchedulerConfig();
            ValidatorConfigRecurring validator = new ValidatorConfigRecurring();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(new DateTime(2020, 1, 4), schedulerData));
            Assert.Equal(Tx.WrongConfiguration, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigRecurring_number_occurs_great_zero()
        {
            SchedulerConfig schedulerData = new SchedulerConfig();
            ValidatorConfigRecurring validator = new ValidatorConfigRecurring();
            schedulerData.Type = TypesSchedule.Recurring;
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(new DateTime(2020, 1, 4), schedulerData));
            Assert.Equal(Tx.NumberMustGreaZero, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigRecurring_number_occurs_great_zero_with_start_date()
        {
            SchedulerConfig schedulerData = new SchedulerConfig();
            ValidatorConfigRecurring validator = new ValidatorConfigRecurring();
            schedulerData.Type = TypesSchedule.Recurring;
            schedulerData.StartDate = new DateTime(2020, 1, 1);
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(new DateTime(2020, 1, 4), schedulerData));
            Assert.Equal(Tx.NumberMustGreaZero, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigRecurring_end_date_be_great_start_date()
        {
            SchedulerConfig schedulerData = new SchedulerConfig();
            ValidatorConfigRecurring validator = new ValidatorConfigRecurring();
            schedulerData.Type = TypesSchedule.Recurring;
            schedulerData.StartDate = new DateTime(2020, 1, 1);
            schedulerData.EndDate = new DateTime(2019, 12, 31);
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(new DateTime(2020, 1, 4), schedulerData));
            Assert.Equal(Tx.EndDateGreatStartDate, TheException.Message);
        }
        [Fact]
        public void ValidatorConfig_Recurring_ok()
        {
            SchedulerConfig schedulerData = new SchedulerConfig();
            ValidatorConfigRecurring validator = new ValidatorConfigRecurring();
            schedulerData.Type = TypesSchedule.Recurring;
            schedulerData.StartDate = new DateTime(2020, 1, 1);
            schedulerData.EndDate = null;
            schedulerData.NumberOccurs = 1;
            validator.Validate(new DateTime(2020, 1, 4), schedulerData);
        }
        [Fact]
        public void ValidatorConfigDailyFrecuencyOnce_no_config()
        {
            var TheException = Assert.Throws<ApplicationException>(() => new ValidatorConfigDailyFrecuency().Validate(null));
            Assert.Equal(Tx.ConfigMustHasValue, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigDailyFrecuencyOnce_must_have_value()
        {
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Once
            };
            var TheException = Assert.Throws<ApplicationException>(() => new ValidatorConfigDailyFrecuency().Validate(configDailyFrecuenci));
            Assert.Equal(Tx.OnceAtValue, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigDailyFrecuency_Once_OK()
        {
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Once,
                OnceTime = new TimeSpan(0, 0, 0)
            };
            new ValidatorConfigDailyFrecuency().Validate(configDailyFrecuenci);
        }
       

        [Fact]
        public void ValidatorConfigDailyFrecuency_Every_number_occurs_less_zero()
        {
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = -1
            };
            var TheException = Assert.Throws<ApplicationException>(() => new ValidatorConfigDailyFrecuency().Validate(configDailyFrecuenci));
            Assert.Equal(Tx.OccursGreatZero, TheException.Message);
        }

        [Fact]
        public void ValidatorConfigDailyFrecuency_Every_number_starting_at_must_have_value()
        {
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 1
            };
            var TheException = Assert.Throws<ApplicationException>(() => new ValidatorConfigDailyFrecuency().Validate(configDailyFrecuenci));
            Assert.Equal(Tx.StartingAtNotHasValue, TheException.Message);
        }

        [Fact]
        public void ValidatorConfigDailyFrecuency_Every_number_end_at_must_have_value()
        {
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 1,
                StartTime = new TimeSpan(1, 0, 0)
            };
            var TheException = Assert.Throws<ApplicationException>(() => new ValidatorConfigDailyFrecuency().Validate(configDailyFrecuenci));
            Assert.Equal(Tx.EndAtNotHasValue, TheException.Message);

        }

        [Fact]
        public void ValidatorConfigDailyFrecuency_Every_number_startin_at_greater_end_at()
        {
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 1,
                StartTime = new TimeSpan(1, 0, 0),
                EndTime = new TimeSpan(0, 0, 0)
            };
            var TheException = Assert.Throws<ApplicationException>(() => new ValidatorConfigDailyFrecuency().Validate(configDailyFrecuenci));
            Assert.Equal(Tx.EndAtMinorStartingAt, TheException.Message);

        }

        [Fact]
        public void ValidatorConfigDailyFrecuency_Every_Ok()
        {
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 1,
                StartTime = new TimeSpan(1, 0, 0),
                EndTime = new TimeSpan(2, 0, 0)
            };
            new ValidatorConfigDailyFrecuency().Validate(configDailyFrecuenci);
        }

        [Fact]
        public void ValidatorConfigWeekly_no_config()
        {
            var TheException = Assert.Throws<ApplicationException>(() => new ValidatorConfigWeekly().Validate(null));
            Assert.Equal(Tx.ConfigMustHasValue, TheException.Message);
        }

        [Fact]
        public void ValidatorConfigWeekly_Every_less_than_one()
        {
            ConfigWeekly configWeekly = new ConfigWeekly()
            {
                Every = 0
            };
            var TheException = Assert.Throws<ApplicationException>(() => new ValidatorConfigWeekly().Validate(configWeekly));
            Assert.Equal(Tx.EveryMustGreatZero, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigWeekly_day_of_week_not_select()
        {
            ConfigWeekly configWeekly = new ConfigWeekly()
            {
                Every = 1
            };
            var TheException = Assert.Throws<ApplicationException>(() => new ValidatorConfigWeekly().Validate(configWeekly));
            Assert.Equal(Tx.MustSelectDayWeek, TheException.Message);
        }

        [Fact]
        public void ValidatorConfigWeekly_ok()
        {
            ConfigWeekly configWeekly = new ConfigWeekly()
            {
                Every = 1,
                Monday = true
            };
            new ValidatorConfigWeekly().Validate(configWeekly);
        }
        #endregion
        #endregion
        #region Calculators
        [Fact]
        public void Calculator_Once()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            var Calculator = InstantiatorCalculator.GetCalculator(TypesSchedule.Once);
            Assert.IsType<CalculatorOnce>(Calculator);
            TheConfig.DateTime = new DateTime(2020, 1, 8, 14, 0, 0);
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.Active = true;
            var Result = Calculator.Calculate(new DateTime(2020, 1, 4), TheConfig);
            Assert.Equal(TheConfig.DateTime, Result);
        }

        [Fact]
        public void CalculatorRecurring_none_Config_daily_weekly()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            var Calculator = InstantiatorCalculator.GetCalculator(TypesSchedule.Recurring);
            Assert.IsType<CalculatorRecurring>(Calculator);
            TheConfig.Type = TypesSchedule.Recurring;

            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.Active = true;
            TheConfig.NumberOccurs = 1;
            var Result = Calculator.Calculate(new DateTime(2020, 1, 4), TheConfig);
            Assert.Equal(new DateTime(2020, 1, 5), Result);
        }

        [Fact]
        public void CalculatorRecurring_none_Config_active()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            var Calculator = InstantiatorCalculator.GetCalculator(TypesSchedule.Recurring);
            Assert.IsType<CalculatorRecurring>(Calculator);
            TheConfig.Type = TypesSchedule.Recurring;

            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.Active = false;
            TheConfig.NumberOccurs = 1;
            var Result = Calculator.Calculate(new DateTime(2020, 1, 4), TheConfig);
            Assert.Equal(new DateTime(2020, 1, 4), Result);
        }

        [Fact]
        public void CalculatorRecurring_Config_daily()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            var Calculator = InstantiatorCalculator.GetCalculator(TypesSchedule.Recurring);
            Assert.IsType<CalculatorRecurring>(Calculator);
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.Active = true;
            TheConfig.NumberOccurs = 1;
            TheConfig.DailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(16, 0, 0)
            };
            var Result = Calculator.Calculate(new DateTime(2020, 1, 4), TheConfig);
            Assert.Equal(new DateTime(2020, 1, 4, 8, 0, 0), Result);
            Result = Calculator.Calculate(Result, TheConfig);
            Assert.Equal(new DateTime(2020, 1, 4, 10, 0, 0), Result);
            Result = Calculator.Calculate(new DateTime(2020, 1, 4, 16, 0, 0, 1), TheConfig);
            Assert.Equal(new DateTime(2020, 1, 5, 8, 0, 0), Result);
        }

        [Fact]
        public void CalculatorRecurring_Next_daily_Frecuenci_once()
        {
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Once,
                OnceTime = new TimeSpan(17, 00, 00)
            };
            CalculatorNextExecutionTimeDailyFrecuency Calculator = new CalculatorNextExecutionTimeDailyFrecuency(configDailyFrecuenci);
            DateTime Date = Calculator.GetNextTime(new DateTime(2020, 1, 4));
            Assert.Equal<DateTime>(new DateTime(2020, 1, 4, 17, 0, 0), Date);
            configDailyFrecuenci.OnceTime = null;
            Date = Calculator.GetNextTime(new DateTime(2020, 1, 4));
            Assert.Equal<DateTime>(new DateTime(2020, 1, 4, 0, 0, 0), Date);
        }

        [Fact]
        public void CalculatorRecurring_Next_daily_Frecuenci_every()
        {
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                TypeUnit = TypesUnitsDailyFrecuency.Hours,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 00, 00),
                EndTime = new TimeSpan(8, 0, 0)
            };
            CalculatorNextExecutionTimeDailyFrecuency Calculator = new CalculatorNextExecutionTimeDailyFrecuency(configDailyFrecuenci);
            DateTime Date = Calculator.GetNextTime(new DateTime(2020, 1, 4));
            Assert.Equal<DateTime>(new DateTime(2020, 1, 4, 4, 0, 0), Date);
            Date = Calculator.GetNextTime(Date);
            Assert.Equal<DateTime>(new DateTime(2020, 1, 4, 6, 0, 0), Date);
            Date = Calculator.GetNextTime(Date);
            Assert.Equal<DateTime>(new DateTime(2020, 1, 4, 8, 0, 0), Date);
        }

        [Fact]
        public void CalculatorRecurring_next_day_weekly_no_daily_frecuenci()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            ConfigWeekly configWeekly = new ConfigWeekly()
            {
                Every = 2,
                Monday = true,
                Thursday = true,
                Friday = true
            };
            TheConfig.Weekly = configWeekly;
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Active = true;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.Occurs = TypesOccurs.Weekly;
            CalculatorRecurring Calculator = new CalculatorRecurring();
            DateTime result = Calculator.Calculate(new DateTime(2020, 1, 1), TheConfig);
            Assert.Equal<DateTime>(new DateTime(2020, 1, 2), result);
            result = Calculator.Calculate(new DateTime(2020, 1, 7), TheConfig);
            Assert.Equal<DateTime>(new DateTime(2020, 1, 13), result);
            result = Calculator.Calculate(new DateTime(2020, 1, 16), TheConfig);
            Assert.Equal<DateTime>(new DateTime(2020, 1, 17), result);
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(new DateTime(2020, 1, 27), result);
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(new DateTime(2020, 1, 30), result);
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(new DateTime(2020, 1, 31), result);
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(new DateTime(2020, 2, 10), result);
        }

        [Fact]
        public void CalculatorRecurring_next_day_time_no_weekly()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();

            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };

            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Daily;
            TheConfig.Active = true;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorRecurring Calculator = new CalculatorRecurring();

            DateTime result = Calculator.Calculate(new DateTime(2020, 1, 1), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 1, 4, 0, 0));
            result = Calculator.Calculate(new DateTime(2020, 1, 7), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 7, 4, 0, 0));
            result = Calculator.Calculate(new DateTime(2020, 1, 16), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 4, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 6, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 8, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 17, 4, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 17, 6, 0, 0));


        }

        [Fact]
        public void CalculatorRecurring_next_week_time_no_weekly()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();

            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };

            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Weekly;
            TheConfig.Active = true;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorRecurring Calculator = new CalculatorRecurring();

            DateTime result = Calculator.Calculate(new DateTime(2020, 1, 1), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 1, 4, 0, 0));
            result = Calculator.Calculate(new DateTime(2020, 1, 7), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 7, 4, 0, 0));
            result = Calculator.Calculate(new DateTime(2020, 1, 16), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 4, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 6, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 8, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 23, 4, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 23, 6, 0, 0));


        }

        [Fact]
        public void CalculatorRecurring_next_Monthly_time_no_weekly()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();

            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };

            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorRecurring Calculator = new CalculatorRecurring();

            DateTime result = Calculator.Calculate(new DateTime(2020, 1, 1), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 1, 4, 0, 0));
            result = Calculator.Calculate(new DateTime(2020, 1, 7), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 7, 4, 0, 0));
            result = Calculator.Calculate(new DateTime(2020, 1, 16), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 4, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 6, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 8, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 2, 16, 4, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 2, 16, 6, 0, 0));


        }

        [Fact]
        public void CalculatorRecurring_next_Monthly_day_time_no_weekly()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();

            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };

            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorRecurring Calculator = new CalculatorRecurring();

            DateTime result = Calculator.Calculate(new DateTime(2020, 1, 1), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 1, 4, 0, 0));
            result = Calculator.Calculate(new DateTime(2020, 1, 7), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 7, 4, 0, 0));
            result = Calculator.Calculate(new DateTime(2020, 1, 16), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 4, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 6, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 8, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 2, 16, 4, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 2, 16, 6, 0, 0));


        }


        [Fact]
        public void CalculatorRecurring_next_day_time()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            ConfigWeekly configWeekly = new ConfigWeekly()
            {
                Every = 2,
                Monday = true,
                Thursday = true,
                Sunday = true
            };
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };

            TheConfig.Weekly = configWeekly;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Active = true;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.Occurs = TypesOccurs.Weekly;
            CalculatorRecurring Calculator = new CalculatorRecurring();
            DateTime result = Calculator.Calculate(new DateTime(2020, 1, 1), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 2, 4, 0, 0));
            result = Calculator.Calculate(new DateTime(2020, 1, 7), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 13, 4, 0, 0));
            result = Calculator.Calculate(new DateTime(2020, 1, 16), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 4, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 6, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 8, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 19, 4, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 19, 6, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 19, 8, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 27, 4, 0, 0));
        }
        [Fact]
        public void CalculatorRecurring_next_day_time_start_date_in_sunday()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            ConfigWeekly configWeekly = new ConfigWeekly()
            {
                Every = 2,
                Monday = true,
                Thursday = true,
                Sunday = true
            };
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };

            TheConfig.Weekly = configWeekly;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Active = true;
            TheConfig.StartDate = new DateTime(2020, 1, 5);
            TheConfig.Occurs = TypesOccurs.Weekly;
            CalculatorRecurring Calculator = new CalculatorRecurring();
            DateTime result = Calculator.Calculate(new DateTime(2020, 1, 1), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 5, 4, 0, 0));
            result = Calculator.Calculate(new DateTime(2020, 1, 7), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 13, 4, 0, 0));
            result = Calculator.Calculate(new DateTime(2020, 1, 16), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 4, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 6, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 16, 8, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 19, 4, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 19, 6, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 19, 8, 0, 0));
            result = Calculator.Calculate(result, TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 27, 4, 0, 0));
        }

        [Fact]
        public void CalculatorRecurring_next_day_time_end_date_first_date()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            ConfigWeekly configWeekly = new ConfigWeekly()
            {
                Every = 2,
                Monday = true,
                Thursday = true,
                Sunday = true
            };
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-GB");
            TheConfig.Weekly = configWeekly;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Active = true;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.EndDate = new DateTime(2020, 12, 31);
            TheConfig.Occurs = TypesOccurs.Weekly;
            CalculatorRecurring Calculator = new CalculatorRecurring();
            var TheException = Assert.Throws<ApplicationException>(() => Calculator.Calculate(new DateTime(2021, 1, 1), TheConfig));
            Assert.Equal(Tx.EndDateAerlierCurrentDate, TheException.Message);
            DateTime result = Calculator.Calculate(new DateTime(2019, 1, 2), TheConfig);
            Assert.Equal<DateTime>(result, new DateTime(2020, 1, 2, 4, 0, 0));

        }

        [Fact]
        public void CalculatorRecurring_next_day_time_end_date_first_date_calculate_last_day()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            ConfigWeekly configWeekly = new ConfigWeekly()
            {
                Every = 2,
                Monday = true
            };
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };
            TheConfig.Weekly = configWeekly;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Active = true;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.EndDate = new DateTime(2020, 12, 31);
            TheConfig.Occurs = TypesOccurs.Weekly;
            CalculatorRecurring Calculator = new CalculatorRecurring();
            var TheException = Assert.Throws<ApplicationException>(() => Calculator.Calculate(new DateTime(2020, 12, 31, 8, 0, 0), TheConfig));
            Assert.Equal(Tx.NotNextExecution, TheException.Message);
        }
        #endregion
        #region Test Formatters

        [Fact]
        public void Validator_Formatter_No_config()
        {
            var TheException = Assert.Throws<ArgumentNullException>(() => InstantiatorFormatter.GetFormatter(null));
        }
        [Fact]
        public void Validator_Formatter_Once()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-GB");
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            var Formatter = InstantiatorFormatter.GetFormatter(TheConfig);
            DateTime DateToFormat = new DateTime(2020, 1, 4, 14, 0, 0);
            Assert.IsType<FormatterOnce>(Formatter);
            Assert.Equal($"Occurs once. Schedule will be used on {DateToFormat:d} at 14:00 starting on {TheConfig.StartDate:d}",
                Formatter.Formatter(DateToFormat));
        }

        [Fact]
        public void Validator_Formatter_Recurring()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-GB");
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.NumberOccurs = 1;
            var Formatter = InstantiatorFormatter.GetFormatter(TheConfig);
            Assert.IsType<FormatterRecurring>(Formatter);
            DateTime DateToFormat = new DateTime(2020, 1, 4, 14, 0, 0);
            Assert.Equal($"Occurs every daily. Schedule will be used on {DateToFormat:d} at 14:00 starting on {TheConfig.StartDate:d}",
                Formatter.Formatter(DateToFormat));
        }

        [Fact]
        public void Validator_Formatter_Recurring_Number_Occurs_great_one()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-GB");
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.NumberOccurs = 3;
            var Formatter = InstantiatorFormatter.GetFormatter(TheConfig);
            Assert.IsType<FormatterRecurring>(Formatter);
            DateTime DateToFormat = new DateTime(2020, 1, 4, 14, 0, 0);
            Assert.Equal($"Occurs every 3 daily. Schedule will be used on {DateToFormat:d} at 14:00 starting on {TheConfig.StartDate:d}",
                Formatter.Formatter(DateToFormat));
        }

        [Fact]
        public void Validator_Formatter_Recurring_Daily_Config()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-GB");
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Daily;
            TheConfig.Active = true;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            var Formatter = InstantiatorFormatter.GetFormatter(TheConfig);
            Assert.IsType<FormatterRecurring>(Formatter);
            DateTime DateToFormat = new DateTime(2020, 1, 4, 4, 0, 0);
            Assert.Equal($"Occurs every daily ever 2 hours between 04:00 and 08:00. Schedule will be used on {DateToFormat:d} at 04:00 starting on {TheConfig.StartDate:d}",
                Formatter.Formatter(DateToFormat));
        }

        [Fact]
        public void Validator_Formatter_Recurring_Daily_once_time_Config()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-GB");
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Once,
                OnceTime = new TimeSpan(16, 0, 0),
                NumberOccurs = 1
            };
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Daily;
            TheConfig.Active = true;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            var Formatter = InstantiatorFormatter.GetFormatter(TheConfig);
            Assert.IsType<FormatterRecurring>(Formatter);
            DateTime DateToFormat = new DateTime(2020, 1, 4, 16, 0, 0);
            Assert.Equal($"Occurs every daily occurs once at 16:00. Schedule will be used on {DateToFormat:d} at 16:00 starting on {TheConfig.StartDate:d}",
                Formatter.Formatter(DateToFormat));
        }

        [Fact]
        public void Validator_Formatter_Recurring_Weekly_Daily_Config()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-GB");
            ConfigWeekly configWeekly = new ConfigWeekly()
            {
                Every = 2,
                Monday = true,
                Thursday = true,
                Friday = true
            };
            TheConfig.Weekly = configWeekly;
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };

            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Daily;
            TheConfig.Active = true;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            var Formatter = InstantiatorFormatter.GetFormatter(TheConfig);
            Assert.IsType<FormatterRecurring>(Formatter);

            Assert.Equal($"Occurs every 2 weeks on monday, thursday and  friday ever 2 hours between 04:00 and 08:00. starting on {TheConfig.StartDate:d}",
                Formatter.Formatter(new DateTime(2020, 1, 4, 4, 0, 0)));

        }

        [Fact]
        public void Validator_Formatter_Weekly_no_Config()
        {

            Assert.Equal(string.Empty,
                new FormatterWeekly(null).Formatter(new DateTime(2020, 1, 4, 4, 0, 0)));
        }

        [Fact]
        public void Validator_Formatter_Weekly_scheduler_config_weekly_no_Config()
        {
            Assert.Equal(string.Empty,
                new FormatterWeekly(new SchedulerConfig()).Formatter(new DateTime(2020, 1, 4, 4, 0, 0)));
            Assert.Equal(string.Empty,
                new FormatterWeekly(new SchedulerConfig()
                {
                    Weekly = new ConfigWeekly()
                }).Formatter(new DateTime(2020, 1, 4, 4, 0, 0)));
        }

        [Fact]
        public void Validator_Formatter_Recurring_Weekly_Config()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-GB");
            ConfigWeekly configWeekly = new ConfigWeekly()
            {
                Every = 2,
                Monday = true,
                Thursday = true,
                Friday = true
            };
            TheConfig.Weekly = configWeekly;

            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Daily;
            TheConfig.Active = true;
           
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            var Formatter = InstantiatorFormatter.GetFormatter(TheConfig);
            Assert.IsType<FormatterRecurring>(Formatter);

            Assert.Equal($"Occurs every 2 weeks on monday, thursday and  friday. starting on {TheConfig.StartDate:d}",
                Formatter.Formatter(new DateTime(2020, 1, 4, 4, 0, 0)));

        }


        [Fact]
        public void Validator_Formatter_Recurring_Monthly_Daily_Config()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-GB");
            ConfigMonthly configMonthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Day,
                DayMonth = 1,
                EveryNumberMonths = 3
            };
            
            TheConfig.Monthly = configMonthly;
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };

            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Daily;
            TheConfig.Active = true;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            var Formatter = InstantiatorFormatter.GetFormatter(TheConfig);
            Assert.IsType<FormatterRecurring>(Formatter);

            Assert.Equal($"Occurs the 1 of very 3 months ever 2 hours between 04:00 and 08:00. starting on {TheConfig.StartDate:d}",
                Formatter.Formatter(new DateTime(2020, 1, 4, 4, 0, 0)));

        }

        [Fact]
        public void Validator_Formatter_Monthly_no_Config()
        {

            Assert.Equal(string.Empty,
                new FormatterMonthly(null).Formatter(new DateTime(2020, 1, 4, 4, 0, 0)));
        }

        [Fact]
        public void Validator_Formatter_Weekly_scheduler_config_monthly_no_Config()
        {
            Assert.Equal(string.Empty,
                new FormatterMonthly(new SchedulerConfig()).Formatter(new DateTime(2020, 1, 4, 4, 0, 0)));
            Assert.Equal(string.Empty,
                new FormatterMonthly(new SchedulerConfig()
                {
                    Monthly = new ConfigMonthly()
                }).Formatter(new DateTime(2020, 1, 4, 4, 0, 0)));
        }

        [Fact]
        public void Validator_Formatter_Recurring_Monthly_Config()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-GB");
            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First,
                TypesDayEvery = TypesEveryDayMonthly.Thursday,
                EveryNumberMonths = 3
            };
            
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Daily;
            TheConfig.Active = true;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            var Formatter = InstantiatorFormatter.GetFormatter(TheConfig);
            Assert.IsType<FormatterRecurring>(Formatter);

            Assert.Equal($"Occurs the first thursday of very 3 months. starting on {TheConfig.StartDate:d}",
                Formatter.Formatter(new DateTime(2020, 1, 4, 4, 0, 0)));
        }
        #endregion

        #region Generators
        [Theory]
        [InlineData(TypesSchedule.Once, "08/01/2020 14:00:00")]
        [InlineData(TypesSchedule.Recurring, "05/01/2020 00:00:00")]
        public void Generator_no_daily_no_weekly_config(TypesSchedule TheType, string NextDateExpected)
        {
            var TheGenerator = new ScheluderGenerator();
         
            var Config = new SchedulerConfig();
            Config.Culture = CultureInfo.GetCultureInfo("en-GB");
            Config.Type = TheType;
            Config.Active = true;
            Config.StartDate = new DateTime(2020, 1, 1);
            Config.NumberOccurs = 1;
            Config.DateTime = new DateTime(2020, 1, 8, 14, 0, 0);
            DateTime CurrentDate = new DateTime(2020, 1, 4);
            var Result = TheGenerator.Calculate(CurrentDate, Config);
            Assert.NotNull(Result);
            Assert.NotNull(Result.NextExecution);
            Assert.Equal(NextDateExpected, Result.NextExecution.Value.ToString("dd/MM/yyyy HH:mm:ss"));


        }

        [Theory]
        [InlineData("en-GB", "Occurs every daily ever 2 hours between 04:00 and 08:00. Schedule will be used on 04/01/2020 at 04:00 starting on 01/01/2020")]
        [InlineData("en-US", "Occurs every daily ever 2 hours between 04:00 and 08:00. Schedule will be used on 1/4/2020 at 04:00 starting on 1/1/2020")]
        [InlineData("es-ES", "Ocurre cada día cada 2 horas entre las 04:00 y las 08:00. El horario se utilizará el 04/01/2020 a las 04:00 empezando el 01/01/2020")]
        public void Generator_frecuency_daly_config_no_weekly_config(string culture, string textExpected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo(culture);
            TheConfig.Type = TypesSchedule.Recurring;

            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.Active = true;
            TheConfig.NumberOccurs = 1;
            TheConfig.DailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };
            var TheGenerator = new ScheluderGenerator();
            DateTime CurrentDate = new DateTime(2020, 1, 4);
            var Result = TheGenerator.Calculate(CurrentDate, TheConfig);
            Assert.NotNull(Result);
            Assert.NotNull(Result.NextExecution);
            DateTime DateExpected = new DateTime(2020, 1, 4, 4, 0, 0);
            Assert.Equal(DateExpected, Result.NextExecution.Value);
            Assert.Equal(textExpected, Result.NextExecutionTimeString);
        }

        [Theory]
        [InlineData("en-GB", "Occurs every 2 weeks on monday, thursday and  friday ever 2 hours between 04:00 and 08:00. starting on 01/01/2020")]
        [InlineData("en-US", "Occurs every 2 weeks on monday, thursday and  friday ever 2 hours between 04:00 and 08:00. starting on 1/1/2020")]
        [InlineData("es-ES", "Ocurre cada 2 semanas en lunes, jueves y viernes cada 2 horas entre las 04:00 y las 08:00. empezando el 01/01/2020")]
        public void Generator_frecuency_daly_weekly_config(string culture, string textExpected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo(culture);
            ConfigWeekly configWeekly = new ConfigWeekly()
            {
                Every = 2,
                Monday = true,
                Thursday = true,
                Friday = true
            };
            TheConfig.Weekly = configWeekly;
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };

            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Daily;
            TheConfig.Active = true;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            var TheGenerator = new ScheluderGenerator();
            DateTime CurrentDate = new DateTime(2020, 1, 1);
            DateTime DateExpected = new DateTime(2020, 1, 2, 4, 0, 0);
            var Result = TheGenerator.Calculate(CurrentDate, TheConfig);
            Assert.NotNull(Result);
            Assert.NotNull(Result.NextExecution);
            Assert.Equal(DateExpected, Result.NextExecution.Value);
            Assert.Equal(textExpected, Result.NextExecutionTimeString);
        }

        [Theory]
        [InlineData("en-GB", "Occurs the first thursday of very 3 months ever 2 hours between 04:00 and 08:00. starting on 01/01/2020")]
        [InlineData("en-US", "Occurs the first thursday of very 3 months ever 2 hours between 04:00 and 08:00. starting on 1/1/2020")]
        [InlineData("es-ES", "Ocurre el primer jueves cada 3 meses cada 2 horas entre las 04:00 y las 08:00. empezando el 01/01/2020")]
        public void Generator_frecuency_daly_monthly_config_first_thursday(string culture, string textExpected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo(culture);
            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First,
                TypesDayEvery = TypesEveryDayMonthly.Thursday,
                EveryNumberMonths = 3
            };
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };

            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Daily;
            TheConfig.Active = true;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            var TheGenerator = new ScheluderGenerator();
            DateTime CurrentDate = new DateTime(2020, 1, 1);
            DateTime DateExpected = new DateTime(2020, 1, 2, 4, 0, 0);
            var Result = TheGenerator.Calculate(CurrentDate, TheConfig);
            Assert.NotNull(Result);
            Assert.NotNull(Result.NextExecution);
            Assert.Equal(DateExpected, Result.NextExecution.Value);
            Assert.Equal(textExpected, Result.NextExecutionTimeString);
        }

        [Theory]
        [InlineData("01/01/2020", "05/01/2020 03:00:00")]
        [InlineData("05/01/2020 03:00:00", "05/01/2020 04:00:00")]
        [InlineData("05/01/2020 04:00:00", "05/01/2020 05:00:00")]
        [InlineData("05/01/2020 05:00:00", "05/01/2020 06:00:00")]
        [InlineData("05/01/2020 06:00:00", "02/02/2020 03:00:00")]
        [InlineData("02/02/2020 03:00:00", "02/02/2020 04:00:00")]
        public void Generator_frecuency_daly_monthly_second_Weekend_config(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-US");
            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.Second,
                TypesDayEvery = TypesEveryDayMonthly.Weekend,
                EveryNumberMonths = 1
            };
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 1,
                StartTime = new TimeSpan(3, 0, 0),
                EndTime = new TimeSpan(6, 0, 0)
            };

            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Daily;
            TheConfig.Active = true;
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            var TheGenerator = new ScheluderGenerator();
            DateTime CurrentDate = new DateTime(2020, 1, 1);
            DateTime DateExpected = new DateTime(2020, 1, 2, 4, 0, 0);
            var Result = TheGenerator.Calculate(ParseSpanish(DateCalculate), TheConfig);
            Assert.NotNull(Result);
            Assert.NotNull(Result.NextExecution);
            Assert.Equal(ParseSpanish(DateEspected), Result.NextExecution.Value);
        }

        #endregion
        #region test ValidatorConfigMonthly
        [Fact]
        public void ValidatorConfigMonthly_no_config_value()
        {
            ValidatorConfigMonthly validator = new ValidatorConfigMonthly();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(null));
            Assert.Equal(Tx.ConfigMustHasValue, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigMonthly_day_no_day_value()
        {
            ConfigMonthly config = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Day,

            };
            ValidatorConfigMonthly validator = new ValidatorConfigMonthly();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(config));
            Assert.Equal(Tx.MustIndicateDayOfMonth, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigMonthly_day_zero_day_value()
        {
            ConfigMonthly config = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Day,
                DayMonth = 0
            };
            ValidatorConfigMonthly validator = new ValidatorConfigMonthly();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(config));
            Assert.Equal(Tx.MustIndicateDayGreatZero, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigMonthly_day_graet_30_day_value()
        {
            ConfigMonthly config = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Day,
                DayMonth = 32
            };
            ValidatorConfigMonthly validator = new ValidatorConfigMonthly();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(config));
            Assert.Equal(Tx.MustIndicateDayLes31, TheException.Message);
        }

        [Fact]
        public void ValidatorConfigMonthly_day_months_lees_zero_value()
        {
            ConfigMonthly config = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Day,
                DayMonth = 1
            };
            ValidatorConfigMonthly validator = new ValidatorConfigMonthly();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(config));
            Assert.Equal(Tx.MonthBeBetween1And12, TheException.Message);
        }
        [Fact]
        public void ValidatorConfigMonthly_day_months_great_12_value()
        {
            ConfigMonthly config = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Day,
                DayMonth = 1,
                EveryNumberMonths = 13

            };
            ValidatorConfigMonthly validator = new ValidatorConfigMonthly();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(config));
            Assert.Equal(Tx.MonthBeBetween1And12, TheException.Message);
        }

        [Fact]
        public void ValidatorConfigMonthly_day_ok()
        {
            ConfigMonthly config = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Day,
                DayMonth = 1,
                EveryNumberMonths = 1

            };
            ValidatorConfigMonthly validator = new ValidatorConfigMonthly();
            validator.Validate(config);
        }

        [Fact]
        public void ValidatorConfigMonthly_every_no_type_every()
        {
            ConfigMonthly config = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every
            };
            ValidatorConfigMonthly validator = new ValidatorConfigMonthly();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(config));
            Assert.Equal(Tx.MustIndicateTypeEveryDay, TheException.Message);
        }

        [Fact]
        public void ValidatorConfigMonthly_every_months_lees_zero_value()
        {
            ConfigMonthly config = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First,
                TypesDayEvery = TypesEveryDayMonthly.Sunday
            };
            ValidatorConfigMonthly validator = new ValidatorConfigMonthly();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(config));
            Assert.Equal(Tx.MonthBeBetween1And12, TheException.Message);
        }

        [Fact]
        public void ValidatorConfigMonthly_every_no_day_of_week()
        {
            ConfigMonthly config = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First
            };
            ValidatorConfigMonthly validator = new ValidatorConfigMonthly();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(config));
            Assert.Equal(Tx.MustIndicateTypeOfDayWeek, TheException.Message);
        }

        [Fact]
        public void ValidatorConfigMonthly_every_months_great_12_value()
        {
            ConfigMonthly config = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First,
                TypesDayEvery = TypesEveryDayMonthly.Sunday,
                EveryNumberMonths = 13
            };
            ValidatorConfigMonthly validator = new ValidatorConfigMonthly();
            var TheException = Assert.Throws<ApplicationException>(() => validator.Validate(config));
            Assert.Equal(Tx.MonthBeBetween1And12, TheException.Message);
        }

        [Fact]
        public void ValidatorConfigMonthly_every_ok()
        {
            ConfigMonthly config = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First,
                TypesDayEvery = TypesEveryDayMonthly.Sunday,
                EveryNumberMonths = 1
            };
            new ValidatorConfigMonthly().Validate(config);
        }
        #endregion
        #region test Calculate Monthly
        [Theory]
        [InlineData("01/01/2020", "01/04/2020")]
        [InlineData("01/04/2020", "01/07/2020")]
        [InlineData("01/07/2020", "01/10/2020")]
        public void CalculatorRecurring_next_Monthly_config_on_day_Month_no_daily_config(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;
            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Day,
                DayMonth = 1,
                EveryNumberMonths = 3

            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyDay(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
            CalculatorRecurring CalculatorRecurring = new CalculatorRecurring();
            result = CalculatorRecurring.Calculate(ParseSpanish(DateCalculate), TheConfig);
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);

        }

        [Theory]
        [InlineData("01/01/2020", "31/01/2020")]
        [InlineData("31/01/2020", "01/05/2020")]
        [InlineData("01/05/2020", "31/07/2020")]
        [InlineData("31/07/2020", "31/10/2020")]
        [InlineData("31/10/2020", "31/01/2021")]
        public void CalculatorRecurring_next_Monthly_config_on_day_Month_31_no_daily_config_(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();


            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;
            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Day,
                DayMonth = 31,
                EveryNumberMonths = 3

            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyDay(TheConfig);
            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "01/02/2020")]
        [InlineData("01/02/2020", "01/05/2020")]
        [InlineData("01/07/2020", "01/08/2020")]
        public void CalculatorRecurring_next_Monthly_config_on_day_Month_no_daily_config_start_date_great(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;
            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Day,
                DayMonth = 1,
                EveryNumberMonths = 3

            };
            TheConfig.StartDate = new DateTime(2020, 1, 2);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyDay(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "01/01/2020 04:00:00")]
        [InlineData("01/01/2020 04:00:00", "01/01/2020 06:00:00")]
        [InlineData("01/01/2020 06:00:00", "01/01/2020 08:00:00")]
        [InlineData("01/01/2020 08:00:00", "01/04/2020 04:00:00")]
        public void CalculatorRecurring_next_Monthly_config_on_day_Month_daily_config(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();

            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;
            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Day,
                DayMonth = 1,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyDay(TheConfig);
            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "01/02/2020 04:00:00")]
        [InlineData("01/02/2020 04:00:00", "01/02/2020 06:00:00")]
        [InlineData("01/02/2020 06:00:00", "01/02/2020 08:00:00")]
        [InlineData("01/02/2020 08:00:00", "01/05/2020 04:00:00")]
        public void CalculatorRecurring_next_Monthly_config_on_day_Month_daily_config_start_date_greater(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-US");

            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;
            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Day,
                DayMonth = 1,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 2);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyDay(TheConfig);
            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "02/01/2020")]
        [InlineData("02/01/2020", "02/04/2020")]
        [InlineData("02/04/2020", "02/07/2020")]
        [InlineData("02/07/2020", "01/10/2020")]
        [InlineData("01/10/2020", "07/01/2021")]
        [InlineData("07/01/2021", "01/04/2021")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_start_date(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;

            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First,
                TypesDayEvery = TypesEveryDayMonthly.Thursday,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "30/01/2020")]
        [InlineData("30/01/2020", "30/04/2020")]
        [InlineData("30/04/2020", "30/07/2020")]
        [InlineData("30/07/2020", "29/10/2020")]
        [InlineData("29/10/2020", "28/01/2021")]
        [InlineData("28/01/2021", "29/04/2021")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_last_date(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Culture = CultureInfo.GetCultureInfo("en-US");
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;

            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.Last,
                TypesDayEvery = TypesEveryDayMonthly.Thursday,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "02/01/2020 04:00:00")]
        [InlineData("02/01/2020 04:00:00", "02/01/2020 06:00:00")]
        [InlineData("02/01/2020 06:00:00", "02/01/2020 08:00:00")]
        [InlineData("02/01/2020 08:00:00", "02/04/2020 04:00:00")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_start_date_daily_config(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First,
                TypesDayEvery = TypesEveryDayMonthly.Thursday,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "06/02/2020")]
        [InlineData("06/02/2020", "07/05/2020")]
        [InlineData("07/05/2020", "06/08/2020")]
        [InlineData("06/07/2020", "06/08/2020")]
        [InlineData("28/07/2020", "06/08/2020")]
        [InlineData("29/07/2020", "06/08/2020")]
        [InlineData("30/07/2020", "06/08/2020")]
        [InlineData("31/07/2020", "06/08/2020")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_start_date_great(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;

            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First,
                TypesDayEvery = TypesEveryDayMonthly.Thursday,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 3);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "06/02/2020 04:00:00")]
        [InlineData("06/02/2020 04:00:00", "06/02/2020 06:00:00")]
        [InlineData("06/02/2020 06:00:00", "06/02/2020 08:00:00")]
        [InlineData("06/02/2020 08:00:00", "07/05/2020 04:00:00")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_start_date_daily_config_great(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;
            ConfigDailyFrecuency configDailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                NumberOccurs = 2,
                StartTime = new TimeSpan(4, 0, 0),
                EndTime = new TimeSpan(8, 0, 0)
            };
            TheConfig.DailyFrecuenci = configDailyFrecuenci;
            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First,
                TypesDayEvery = TypesEveryDayMonthly.Thursday,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 3);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "01/04/2020")]
        [InlineData("02/01/2020", "01/04/2020")]
        [InlineData("15/01/2020", "01/04/2020")]
        [InlineData("02/04/2020", "01/07/2020")]
        [InlineData("02/07/2020", "01/10/2020")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_first_weekday_date(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;

            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First,
                TypesDayEvery = TypesEveryDayMonthly.Weekday,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }
        [Fact]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_first_weekday_date_start_day_weekend()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;
                TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First,
                TypesDayEvery = TypesEveryDayMonthly.Weekday,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2021, 5, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(new DateTime(2021,4,1));
            Assert.Equal<DateTime>(new DateTime(2021,5,3), result);
        }

        [Theory]
        [InlineData("01/01/2020", "02/01/2020")]
        [InlineData("04/01/2020", "02/04/2020")]
        [InlineData("15/01/2020", "02/04/2020")]
        [InlineData("02/04/2020", "02/07/2020")]
        [InlineData("02/07/2020", "02/10/2020")]
        [InlineData("02/10/2020", "04/01/2021")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_second_weekday_date(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;

            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.Second,
                TypesDayEvery = TypesEveryDayMonthly.Weekday,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "03/01/2020")]
        [InlineData("04/01/2020", "03/04/2020")]
        [InlineData("15/01/2020", "03/04/2020")]
        [InlineData("03/04/2020", "03/07/2020")]
        [InlineData("03/07/2020", "05/10/2020")]
        [InlineData("05/10/2020", "05/01/2021")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_third_weekday_date(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;

            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.Third,
                TypesDayEvery = TypesEveryDayMonthly.Weekday,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "06/01/2020")]
        [InlineData("06/01/2020", "06/04/2020")]
        [InlineData("15/01/2020", "06/04/2020")]
        [InlineData("06/04/2020", "06/07/2020")]
        [InlineData("06/07/2020", "06/10/2020")]
        [InlineData("06/10/2020", "06/01/2021")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_Fourth_weekday_date(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;

            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.Fourth,
                TypesDayEvery = TypesEveryDayMonthly.Weekday,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "31/01/2020")]
        [InlineData("31/01/2020", "30/04/2020")]
        [InlineData("15/01/2020", "31/01/2020")]
        [InlineData("30/04/2020", "31/07/2020")]
        [InlineData("31/07/2020", "30/10/2020")]
        [InlineData("30/10/2020", "29/01/2021")]
        [InlineData("29/01/2021", "30/04/2021")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_last_weekday_date(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;

            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.Last,
                TypesDayEvery = TypesEveryDayMonthly.Weekday,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "04/01/2020")]
        [InlineData("04/01/2020", "04/04/2020")]
        [InlineData("15/01/2020", "04/04/2020")]
        [InlineData("04/04/2020", "04/07/2020")]
        [InlineData("04/07/2020", "03/10/2020")]
        [InlineData("03/10/2020", "02/01/2021")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_first_weekend_date(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;

            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.First,
                TypesDayEvery = TypesEveryDayMonthly.Weekend,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "05/01/2020")]
        [InlineData("05/01/2020", "05/04/2020")]
        [InlineData("15/01/2020", "05/04/2020")]
        [InlineData("05/04/2020", "05/07/2020")]
        [InlineData("05/07/2020", "04/10/2020")]
        [InlineData("04/10/2020", "03/01/2021")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_second_weekend_date(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;

            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.Second,
                TypesDayEvery = TypesEveryDayMonthly.Weekend,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }

        [Theory]
        [InlineData("01/01/2020", "26/01/2020")]
        [InlineData("26/01/2020", "26/04/2020")]
        [InlineData("15/01/2020", "26/01/2020")]
        [InlineData("26/04/2020", "26/07/2020")]
        [InlineData("26/07/2020", "31/10/2020")]
        [InlineData("31/10/2020", "31/01/2021")]
        public void CalculatorRecurring_next_Monthly_config_on_the_Month_daily_config_last_weekend_date(string DateCalculate, string DateEspected)
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;

            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.Last,
                TypesDayEvery = TypesEveryDayMonthly.Weekend,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            CalculatorNextExecutionTimeMonthly Calculator = new CalculatorNextExecutionTimeMonthlyEvery(TheConfig);

            DateTime result = Calculator.CalculateNextDate(ParseSpanish(DateCalculate));
            Assert.Equal<DateTime>(ParseSpanish(DateEspected), result);
        }
        #endregion
        #region Test Auxiliar class
        [Fact]
        public void test_CalculatorLastDateTimeCalc()
        {
            SchedulerConfig TheConfig = new SchedulerConfig();
            TheConfig.Type = TypesSchedule.Recurring;
            TheConfig.Occurs = TypesOccurs.Monthly;
            TheConfig.Active = true;

            TheConfig.Monthly = new ConfigMonthly()
            {
                Type = TypesMontlyFrecuency.Every,
                TypesEvery = TypesEveryMonthly.Second,
                TypesDayEvery = TypesEveryDayMonthly.Weekend,
                EveryNumberMonths = 3
            };
            TheConfig.StartDate = new DateTime(2020, 1, 1);
            TheConfig.EndDate = new DateTime(2020, 12, 31);

            Assert.Equal<DateTime>(TheConfig.EndDate.Value, CalculatorLastDateTimeCalc.GetLastDateTime(TheConfig));
            TheConfig.DailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Every,
                EndTime = new TimeSpan(8, 0, 0)
            };
            Assert.Equal<DateTime>(new DateTime(2020,12,31,8,0,0), CalculatorLastDateTimeCalc.GetLastDateTime(TheConfig));
            TheConfig.DailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Once,
                OnceTime = new TimeSpan(8, 0, 0)
            };
            Assert.Equal<DateTime>(new DateTime(2020, 12, 31, 8, 0, 0), CalculatorLastDateTimeCalc.GetLastDateTime(TheConfig));
            TheConfig.DailyFrecuenci = new ConfigDailyFrecuency()
            {
                Frecuenci = TypesOccursDailyFrecuency.Once
            };
            Assert.Equal<DateTime>(new DateTime(2020, 12, 31, 0, 0, 0), CalculatorLastDateTimeCalc.GetLastDateTime(TheConfig));
        }
        #endregion

        [Fact]
        public void TextStringsEnums()
        {
            FormatterBase Formatter = new FormatterBase(new SchedulerConfig()
            {
                Culture = CultureInfo.GetCultureInfo("es-ES")
            });
            var TheException = Assert.Throws<ApplicationException>(() => Formatter.GetStringEnum("Hours"));
            Assert.Equal("horas", Formatter.GetStringEnum(TypesUnitsDailyFrecuency.Hours));
            Assert.Equal("minutos", Formatter.GetStringEnum(TypesUnitsDailyFrecuency.Minutes));
        }

        [Fact]
        public void Test_DateTime_extensions()
        {
            DateTime DateToSet = new DateTime(2020, 1, 1);
            DateTime DateCalc = DateToSet.AddInteval(TypesUnitsDailyFrecuency.Hours, 1);
            Assert.Equal<DateTime>(new DateTime(2020, 1, 1, 1, 0, 0, 0), DateCalc);
            DateCalc = DateToSet.AddInteval(TypesUnitsDailyFrecuency.Minutes, 1);
            Assert.Equal<DateTime>(new DateTime(2020, 1, 1, 0,1, 0, 0), DateCalc);
            DateCalc = DateToSet.AddInteval(TypesUnitsDailyFrecuency.Seconds, 1);
            Assert.Equal<DateTime>(new DateTime(2020, 1, 1, 0, 0, 1, 0), DateCalc);
        }
        [Fact]
        public void Test_ScheduleReuls()
        {
            
            SchedulerResults result = new SchedulerResults(new FormatterBase(new SchedulerConfig()));
            var TheException = Assert.Throws<ApplicationException>(() => result.NextExecutionTimeString);
            Assert.Equal(Tx.NotCalculate,TheException.Message);
            result.NextExecution = DateTime.Now;
            var TheExceptionNotImplemented = Assert.Throws<NotImplementedException>(() => result.NextExecutionTimeString);
        }

        #region Auxiliar methods
        public static DateTime ParseSpanish(string dateTimeString)
        {
            return DateTime.Parse(dateTimeString, formaterDateTimeSpanish);
        }
        #endregion
    }
}
