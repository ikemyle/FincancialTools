using System.Data;

namespace CurrencyData.Models
{
/// <summary>
/// Definition of the sqls to be executed
/// </summary>
    public class Sqls
    {
        /// <summary>
        /// sql string to be executed
        /// </summary>
        public string sql { get; set; }
        /// <summary>
        /// command type of the sql (as text, table or stored proc)
        /// </summary>
        public CommandType SqlCommandType { get; set; }
    }
}
