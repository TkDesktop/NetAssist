using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Services
{
    public class DeviceInfoService
    {
        public string GetHostname(string ipAddress)
        {
            try
            {
                IPHostEntry hostEntry =
                    Dns.GetHostEntry(ipAddress);

                return hostEntry.HostName;
            }
            catch
            {
                return "Não identificado";
            }
        }
    }
}
