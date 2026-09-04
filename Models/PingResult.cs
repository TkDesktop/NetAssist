using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Models
{
    public class PingResult
    {
        public string ipAddress { get; set; }
        public DeviceStatus status { get; set; }
        public long latency { get; set; }
        public string message { get; set; }
        public string hostname { get; set; }
    }
}

