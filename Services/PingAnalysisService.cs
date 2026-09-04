using NetAssist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAssist.Services
{
    public class PingAnalysisService
    {
        public PingAnalysisResult Analyze(List<PingResult> resultadosPing)
        {
            PingAnalysisResult resultado = new PingAnalysisResult();

            foreach (PingResult resultadoPing in resultadosPing)
            {
                if (resultadoPing.status == DeviceStatus.Online)
                {
                    resultado.online++;
                }
                else if (resultadoPing.status == DeviceStatus.Offline)
                {
                    resultado.offline++;
                }
                else if (resultadoPing.status == DeviceStatus.Unknown)
                {
                    resultado.unknown++;
                }
            }
            resultado.total = resultadosPing.Count;

            return resultado;
        }
    }
}
