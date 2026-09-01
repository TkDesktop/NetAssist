using NetAssist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Services
{
    public class NetworkDeviceManager
    {
       private List<NetworkDevice> dispositivos = new List<NetworkDevice>();

       public void AddDevice(NetworkDevice dispositivo)
       {
           dispositivos.Add(dispositivo);
       }

       public IReadOnlyList<NetworkDevice> GetDevices()
       {
           return dispositivos.AsReadOnly();
       }

       public NetworkDevice FindByIp(string ipAddress)
       {
           foreach (NetworkDevice dispositivo in dispositivos)
           {
               if (dispositivo.ipAddress == ipAddress)
               {
                   return dispositivo;
               }
           }

           return null;
       }
    }
}


