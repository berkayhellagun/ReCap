using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class Database : LoggerServiceBase
    {
        public Database():base("DatabaseLogger")
        {

        }
    }
}
