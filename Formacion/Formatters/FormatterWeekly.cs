using Formacion.Views;
using System;
using System.Text;

namespace Formacion.Formatters
{
    public class FormatterWeekly : FormatterBase
    {
        
        private static string[] dayOfWeekNames = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };

        public FormatterWeekly(SchedulerConfig TheConfig):base(TheConfig)
        {
           
        }
    
        public override string Formatter(DateTime nextExecution)
        {
            if(!this.HasConfig()) { return string.Empty; }
            if(!this.HasConfigWeekly()) { return string.Empty; }
            return string.Format(Texts.FormatterWeekly_TextBase, this.Config.Weekly.Every.ToString(), this.FormatterValue());
               

        }

        private bool HasConfig()
        {
            return this.Config != null; 
        }

        private bool HasConfigWeekly()
        {
            if(this.Config.Weekly == null)
            {
                return false;
            }
            return this.Config.Weekly.Monday || this.Config.Weekly.Tuesday ||
               this.Config.Weekly.Wednesday || this.Config.Weekly.Thursday ||
               this.Config.Weekly.Friday || this.Config.Weekly.Saturday ||
                this.Config.Weekly.Sunday;
        }

        private string FormatterValue()
        {
            
            StringBuilder stringBuilder = new StringBuilder();

            int LastPositionInsert = 0;
            for (int Index = 0;Index < 7;Index ++)
            {
                if (this.Config.Weekly.SelectedDays[Index])
                {
                    LastPositionInsert = this.AppendString(stringBuilder,
                                    Texts.ResourceManager.GetString(FormatterWeekly.dayOfWeekNames[Index]));
                }
            }
            if(LastPositionInsert > 0)
            {
                stringBuilder.Replace(",", Texts.FormatterWeekly_TextAnd, LastPositionInsert, 1);
                
            }
            return stringBuilder.ToString();  
        }
        private int AppendString(StringBuilder stringBuilder,string stringAppend)
        {
            int length = stringBuilder.Length;
            if((length = stringBuilder.Length) > 0)
            {
                stringBuilder.Append(", "); 
            }
            stringBuilder.Append(stringAppend);
            return length;  
        }
    }
}
