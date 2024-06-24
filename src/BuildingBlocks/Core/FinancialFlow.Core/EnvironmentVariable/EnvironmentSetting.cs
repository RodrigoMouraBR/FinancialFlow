using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialFlow.Core.EnvironmentVariable
{
    public class EnvironmentSetting
    {
        public string RABBITMQ_HOST { get; set; }
        public string POSTGRES_HOST { get; set; }
        public string POSTGRES_USER { get; set; }
        public string POSTGRES_PASSWORD { get; set; }
        public string POSTGRES_DB { get; set; }      
    }
}
