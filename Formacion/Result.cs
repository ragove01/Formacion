using Formacion.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formacion
{
    public class Result : IResult
    {
        private readonly DateTime nextExecution;
        private readonly DateTime startDate;
        public Result(DateTime theStartDate, DateTime theNextExecution)
        {
            this.nextExecution = theNextExecution;
            this.startDate = theStartDate;
        }
        public DateTime NextExecution => this.nextExecution;
        public DateTime StartDate => this.startDate;
    }
}

