using HMS.Service.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Implementations
{
    public class LogService : ILogService
    {

        #region Fields
        private readonly ILog _error;
        private readonly ILog _info;
        #endregion

        #region Constructor
        public LogService()
        {
            _error = LogManager.GetLogger("ErrorLogFile");
            _info = LogManager.GetLogger("InfoLogFile");
        }
        #endregion

        #region Method
        public async Task Error(Exception message)
        {
            await Task.Yield();
            string text = $"{Environment.NewLine}Exception Message: {message.Message}{Environment.NewLine}Exception Type: {message.GetType()}{Environment.NewLine}Exception Source: {message.TargetSite}{Environment.NewLine}";
            _error.Error(text);
        }

        public async Task Info(string message)
        {
            await Task.Yield();
            _info.Info(message);
        }
        #endregion
    }
}
