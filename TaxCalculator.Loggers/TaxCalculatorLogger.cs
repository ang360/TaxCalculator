using System;

namespace TaxCalculator.Loggers
{
    //The system is currently using the built-in logger to save the errors in the debugger
    //This logger is currently being used only to write to the console the incoming http requests
    //Customized error log to be implemented. 
    public class TaxCalculatorLogger: ITaxCalculatorLogger
    {
        //Log event
        public void LogEvent(string message)
        {
            Console.WriteLine(message);
            //To Be Implemented
        }
    }
}
