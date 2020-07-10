using log4net;

namespace CurrencyConverter.Helpers
{
    public class Logger
    {
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
