using Formacion.Views;
using System;
using System.Globalization;
using System.Threading;

namespace Formacion.Formatters
{
    public class FormatterBase
    {
        protected readonly SchedulerConfig Config;
        public FormatterBase(SchedulerConfig theConfig)
        {
            this.Config = theConfig;
            ScheluderGenerator.SetCulture(theConfig?.Culture);
        }

        public virtual string Formatter(DateTime nextExecution)
        {
            throw new NotImplementedException();
        }


       
        public string GetStringEnum(object value)
        {
            if (value.GetType().IsEnum == false)
            {
                throw new ApplicationException("the object isn't a enum value");
            }
            return Texts.ResourceManager.GetString($"{value.GetType().Name}.{value.ToString()}");
        }
    }
}
