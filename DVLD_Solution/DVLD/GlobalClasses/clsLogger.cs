using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.GlobalClasses
{
    public class clsLogger
    {
        public  delegate void LogAction(string message);

        private LogAction _logAction;
        public clsLogger(LogAction logAction) 
        {
            _logAction = logAction;
        }

        public void Log(string message)
        {
            _logAction(message);
        }
    }
}
