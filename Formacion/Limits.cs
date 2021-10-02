using Formacion.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion
{
    public class Limits : ILimits
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool IsValid()
        {
            if(this.EndDate.HasValue && this.EndDate.Value < this.StartDate)
            {
                return false;
            }
            return true;
        }
    }
}
