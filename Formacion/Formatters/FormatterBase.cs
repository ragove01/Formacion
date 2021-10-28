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
            ScheluderGenerator.SetCulture(config?.Culture);
        }

        public abstract string Formatter(DateTime nextExecution);
        


       
        public string GetStringEnum(object value)
        {
            if (value.GetType().IsEnum == false)
            {
                throw new ApplicationException(string.Format(Translator.GetText(TextsIndex.EnumConversionError),value,"enum value"));
            }
            
            return Translator.GetText($"{value.GetType().Name}_{value.ToString()}");
        }
    }
}
