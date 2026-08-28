using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Models
{
    public class Server : NetworkDevice
    {
        public string OperatingSystem { get; set; }
        public override string GetDeviceType()
        {
            return "Servidor";
        }
    }
}
