using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Configs
{
    public class ConfigWeekly
    {
        public int Every { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public int NumberDaysEvery => this.Every * 7;
        public bool[] SelectedDays => new bool[] {this.Monday,this.Tuesday,
                this.Wednesday,this.Thursday,this.Friday,
                this.Saturday,this.Sunday};
    } 
    
}
