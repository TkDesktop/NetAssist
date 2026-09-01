using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Models
{
    public abstract class NetworkDevice
    {
        public string hostname { get; set; }
        public string ipAddress { get; set; }
        public string macAddress { get; set; }
        public string gateway { get; set; }
        public DeviceStatus status { get; set; }
        public abstract string GetDeviceType();
    }
}
