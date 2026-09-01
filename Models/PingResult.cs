using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Models
{
    public class PingResult
    {
        public DeviceStatus status { get; set; }

        public long latency { get; set; }

        public string message { get; set; }
    }
}

