using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Models
{
    public class Router : NetworkDevice
    {
        public string gateway { get; set; }
        public override string GetDeviceType()
        {
            return "Roteador";
        }
    }
}
