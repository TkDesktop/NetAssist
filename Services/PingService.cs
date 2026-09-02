using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using NetAssist.Models;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Services
{

        public class PingService
        {
            public PingResult TestConnection(string ipAddress)
            {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(ipAddress, 500);


                    if (reply.Status == IPStatus.Success)
                    {
                        return new PingResult
                        {
                            status = DeviceStatus.Online,
                            latency = reply.RoundtripTime,
                            message = "Ping realizado com sucesso."
                        };
                    }

                    return new PingResult
                    {
                        status = DeviceStatus.Offline,
                        latency = 0,
                        message = "O dispositivo não respondeu ao Ping."
                    };
                }
            }
            catch (Exception ex)
            {
                return new PingResult
                {
                    status = DeviceStatus.Unknown,
                    latency = 0,
                    message = $"Erro ao executar o Ping: {ex.Message}"
                };
            }

        }
    }
}
