using Formacion.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion.Calculators
{
    public class CalculatorNextExecutionTime : ICalculatorNextExecutionTime
    {
        private readonly TypesOccurs type;

        public CalculatorNextExecutionTime(TypesOccurs TheType)
        {
            this.type = TheType;
        }
        public TypesOccurs Type => this.type;

        public DateTime GetNext(DateTime NextTime)
        {
            
            if(this.Type == TypesOccurs.Weekly)
            {
                return NextTime.AddDays(7); 
            }
            if(this.Type == TypesOccurs.Monthly)
            {
                return NextTime.AddMonths(1); 
            }
            return NextTime.AddDays(1);
        }
    }
}
