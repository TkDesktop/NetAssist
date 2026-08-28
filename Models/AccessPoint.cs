using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Models
{
    public class AccessPoint : NetworkDevice
    {
        public string Ssid { get; set; }
        public override string GetDeviceType()
        {
            return "Access Point";
        }
    }
}
