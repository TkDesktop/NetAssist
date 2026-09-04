using NetAssist.Models;
using NetAssist.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace NetAssist
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            NetworkScanService scanService =
        new NetworkScanService();

            NetworkScanResult resultadoScan =
                scanService.Scan(
                    "10.24.86.18",
                    "255.255.255.0"
                );

            List<string> dispositivosEncontrados =
    new List<string>();

            NetworkDeviceManager manager =
    new NetworkDeviceManager();

            foreach (PingResult resultadoPing in resultadoScan.resultadosPing)
            {
                if (resultadoPing.status == DeviceStatus.Online)
                {
                    dispositivosEncontrados.Add(
                        resultadoPing.ipAddress
                    );
                }
            }

            foreach (string ipAddress in dispositivosEncontrados)
            {
                NetworkDevice dispositivo = CriarDispositivo(ipAddress);

                manager.AddDevice(dispositivo);
            }

            DeviceInfoService deviceInfoService =
    new DeviceInfoService();

            string hostname =
                deviceInfoService.GetHostname(
                    "10.24.86.18"
                );

            MessageBox.Show(
                $"IP: 10.24.86.18\n" +
                $"Hostname: {hostname}",
                "Informações do dispositivo"
            );

            IReadOnlyList<NetworkDevice> dispositivos =
    manager.GetDevices();

            MessageBox.Show(
    $"Dispositivos cadastrados: {dispositivos.Count}",
    "NetworkDeviceManager"
);

            string ipsEncontrados =
    string.Join(
        Environment.NewLine,
        dispositivosEncontrados
    );

            string listaDispositivos = "";

            foreach (NetworkDevice dispositivo in dispositivos)
            {
                listaDispositivos +=
                    $"IP: {dispositivo.ipAddress}\n" +
                    $"Tipo: {dispositivo.GetDeviceType()}\n" +
                    $"Status: {dispositivo.status}\n\n";
            }

            MessageBox.Show(
                listaDispositivos,
                "Dispositivos Cadastrados"
            );

            MessageBox.Show(
                $"Rede: {resultadoScan.networkAddress}\n" +
                $"Broadcast: {resultadoScan.broadcastAddress}\n" +
                $"Total: {resultadoScan.total}\n" +
                $"Online: {resultadoScan.online}\n" +
                $"Offline: {resultadoScan.offline}\n" +
                $"Desconhecido: {resultadoScan.unknown}",
                "Resultado da Varredura"
            );

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private NetworkDevice CriarDispositivo(string ipAddress)
        {
            AccessPoint accessPoint =
                new AccessPoint();

            accessPoint.ipAddress = ipAddress;
            accessPoint.status = DeviceStatus.Online;
            accessPoint.ssid = "Rede-Teste";

            return accessPoint;
        }
    }
}

/*
 * Quero continuar o desenvolvimento do meu projeto C# Windows Forms chamado NetAssist.

Estamos fazendo o projeto de forma didática, com foco em C# e POO, e quero seguir UMA ETAPA POR VEZ. Não avance para a próxima etapa sem eu testar a atual e dizer "foi".

ESTADO ATUAL DO PROJETO:

Estrutura:
NetAssist
├── Models
│   ├── NetworkDevice.cs
│   ├── Router.cs
│   ├── Switch.cs
│   ├── Server.cs
│   ├── AccessPoint.cs
│   ├── DeviceStatus.cs
│   ├── PingResult.cs
│   ├── PingAnalysisResult.cs
│   └── NetworkScanResult.cs
│
├── Services
│   ├── PingService.cs
│   ├── NetworkDeviceManager.cs
│   ├── NetworkDiscoveryService.cs
│   ├── PingAnalysisService.cs
│   ├── NetworkScanService.cs
│   └── DeviceInfoService.cs
│
└── Form1.cs

CONCEITOS JÁ IMPLEMENTADOS E TESTADOS:

1. NetworkDevice é uma classe abstrata.
2. Router, Switch, Server e AccessPoint herdam de NetworkDevice.
3. DeviceStatus possui:
   Unknown,
   Online,
   Offline.
4. PingService testa conectividade.
5. Timeout do Ping é 500 ms.
6. PingResult possui:
   ipAddress,
   hostname,
   status,
   latency,
   message.
7. NetworkDiscoveryService:
   - valida máscara;
   - calcula prefixo;
   - calcula endereço de rede;
   - calcula broadcast;
   - gera IPs utilizáveis.
8. A varredura usa Parallel.ForEach com:
   MaxDegreeOfParallelism = 20
9. Resultados compartilhados usam lock.
10. PingAnalysisService conta:
    online,
    offline,
    unknown,
    total.
11. NetworkScanResult possui:
    networkAddress,
    broadcastAddress,
    total,
    online,
    offline,
    unknown,
    resultadosPing.
12. NetworkScanService coordena:
    descoberta → Ping → análise → NetworkScanResult.
13. NetworkDeviceManager possui:
    AddDevice()
    GetDevices()
    FindByIp()
14. Testamos polimorfismo com Router, Switch, Server e AccessPoint.
15. Criamos CriarDispositivo() no Form1.
16. Não estamos classificando automaticamente um IP como Router/Switch/etc. ainda.
17. Já criamos DeviceInfoService para tentar obter hostname através de:
    Dns.GetHostEntry(ipAddress)

ETAPA 14 ATUAL:

Acabamos de adicionar hostname ao PingResult:

public string hostname { get; set; }

Criamos DeviceInfoService.cs:

using System.Net;

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

O serviço foi testado isoladamente no Form1 usando:

DeviceInfoService deviceInfoService =
    new DeviceInfoService();

string hostname =
    deviceInfoService.GetHostname(
        "10.24.86.18"
    );

MessageBox.Show(
    $"IP: 10.24.86.18\n" +
    $"Hostname: {hostname}",
    "Informações do dispositivo"
);

O teste funcionou. Pode aparecer um hostname ou "Não identificado", ambos são resultados aceitáveis.

IMPORTANTE:
- Parei exatamente após esse teste.
- NÃO integrar hostname ao NetworkScanService ainda até iniciar a próxima etapa.
- NÃO avançar para MAC, fabricante, portas, SNMP, classificação automática, NetworkDeviceManager avançado, DNS, traceroute, monitoramento ou async/await sem etapas intermediárias.
- Não alterar código que já está funcionando sem necessidade.
- Manter os nomes das propriedades em lower camel case, como:
  hostname, ipAddress, macAddress, gateway, status, managementVlan, operatingSystem, ssid, latency, message.
- Classes continuam em PascalCase.
- Quero explicação didática: objetivo, arquivo alterado, código completo da alteração, conceitos de C#/POO e teste.
- Depois da etapa, espere eu dizer "foi".
- Quando eu disser "vamos", continue exatamente da próxima etapa.

PRÓXIMO PASSO:
Primeiro explique a próxima etapa antes de fazer qualquer alteração.
*/s






