using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Loggers
{
    //Interface for our custom error log to be implemented.
    public interface ITaxCalculatorLogger
    {
        //Log event 
        public void LogEvent(string message);
    }
}
