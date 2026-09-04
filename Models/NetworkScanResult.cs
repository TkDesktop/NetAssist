using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Models
{
    public class NetworkScanResult
    {
        public string networkAddress { get; set; }
        public string broadcastAddress { get; set; }
        public int total { get; set; }
        public int online { get; set; }
        public int offline { get; set; }
        public int unknown { get; set; }
        public List<PingResult> resultadosPing { get; set; }
    }
}
