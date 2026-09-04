using NetAssist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Services
{
    public class NetworkScanService
    {
        private NetworkDiscoveryService discoveryService;
        private PingService pingService;
        private PingAnalysisService pingAnalysisService;

        public NetworkScanService()
        {
            discoveryService = new NetworkDiscoveryService();

            pingService = new PingService();

            pingAnalysisService = new PingAnalysisService();
        }

        public NetworkScanResult Scan(string ipAddress,string subnetMask)
        {
            string networkAddress = discoveryService.GetNetworkAddress(ipAddress,subnetMask);

            string broadcastAddress = discoveryService.GetBroadcastAddress(ipAddress,subnetMask);

            List<string> ipAddresses = discoveryService.GetUsableIpAddresses(ipAddress,subnetMask);

            List<PingResult> resultadosPing = new List<PingResult>();

            Parallel.ForEach(ipAddresses,new ParallelOptions
                {
                    MaxDegreeOfParallelism = 20
                },
                enderecoIp =>
                {
                    PingResult resultadoPing = pingService.TestConnection(enderecoIp);

                    lock (resultadosPing)
                    {
                        resultadosPing.Add(resultadoPing);
                    }
                }
            );

            PingAnalysisResult resultadoAnalise = pingAnalysisService.Analyze(resultadosPing);

            NetworkScanResult resultadoScan = new NetworkScanResult
            {
                networkAddress = networkAddress,
                broadcastAddress = broadcastAddress,
                total = resultadoAnalise.total,
                online = resultadoAnalise.online,
                offline = resultadoAnalise.offline,
                unknown = resultadoAnalise.unknown,
                resultadosPing = resultadosPing
            };

            return resultadoScan;
        }
    }
}
