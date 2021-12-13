using Formacion.TextsTranslations;
using Formacion.Views;
using System;
using System.Globalization;
using System.Threading;

namespace Formacion.Formatters
{
    public abstract class FormatterBase
    {
        protected readonly SchedulerConfig Config;
        public FormatterBase(SchedulerConfig config)
        {
            this.Config = config;
            SchedulerGenerator.SetCulture(config?.Culture);
        }

        public abstract string Formatter(DateTime nextExecution);
        


       
        public string GetStringEnum(object enumValue)
        {
            if (enumValue.GetType().IsEnum == false)
            {
                throw new ApplicationException(string.Format(Translator.GetText(TextsIndex.EnumConversionError),enumValue,"enum value"));
            }
            
            return Translator.GetText($"{enumValue.GetType().Name}_{enumValue.ToString()}");
        }
    }
}
