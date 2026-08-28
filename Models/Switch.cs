using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Models
{
    public class Switch : NetworkDevice
    {
        public int managementVlan { get; set; }
        public override string GetDeviceType()
        {
            return "Switch";
        }
    }
}
