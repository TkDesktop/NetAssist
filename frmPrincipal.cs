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

            NetworkDiscoveryService discoveryService =
    new NetworkDiscoveryService();

            PingService pingService =
                new PingService();

            List<string> ipAddresses =
                discoveryService.GetUsableIpAddresses(
                    "192.168.56.1",
                    "255.255.255.0"
                );

            List<string> dispositivosEncontrados =
                new List<string>();

            foreach (string ipAddress in ipAddresses)
            {
                PingResult resultadoPing =
                    pingService.TestConnection(ipAddress);

                if (resultadoPing.status == DeviceStatus.Online)
                {
                    dispositivosEncontrados.Add(ipAddress);
                }
            }

            MessageBox.Show(
                $"IPs encontrados: {dispositivosEncontrados.Count}"
            );


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}


/*
 # CONTINUIDADE DO PROJETO NETASSIST

Quero continuar o desenvolvimento do meu projeto **NetAssist** exatamente de onde paramos.

## 1. Projeto

* Nome: **NetAssist**
* Linguagem: **C#**
* Plataforma: **.NET Windows Forms**
* IDE: **Visual Studio**
* Arquitetura: orientação a objetos, separação de responsabilidades e serviços.
* Objetivo: criar uma ferramenta desktop para diagnóstico e descoberta de dispositivos em redes.

Quero aprender C# e OOP enquanto desenvolvo o projeto.

### REGRA PRINCIPAL

Quero desenvolver **UMA ETAPA POR VEZ**.

Não avance várias etapas de uma vez.

Para cada etapa:

1. Explique o objetivo.
2. Diga qual arquivo será alterado.
3. Mostre o código completo da etapa.
4. Explique as partes importantes.
5. Explique o conceito de C#/OOP envolvido.
6. Mostre como testar.
7. Espere eu dizer **"foi"** antes de continuar.

Não complique o projeto desnecessariamente e não altere funcionalidades que já estão funcionando.

---

# 2. Estrutura atual

```text
NetAssist
│
├── Models
│   ├── NetworkDevice.cs
│   ├── Router.cs
│   ├── Switch.cs
│   ├── Server.cs
│   ├── AccessPoint.cs
│   ├── DeviceStatus.cs
│   └── PingResult.cs
│
├── Services
│   ├── PingService.cs
│   ├── NetworkDeviceManager.cs
│   └── NetworkDiscoveryService.cs
│
└── Form1.cs
```

---

# 3. Convenção de nomes

Classes:

```text
NetworkDevice
Router
Switch
Server
AccessPoint
DeviceStatus
PingResult
PingService
NetworkDeviceManager
NetworkDiscoveryService
```

Propriedades devem seguir o padrão que estamos usando:

```text
hostname
ipAddress
macAddress
gateway
status
managementVlan
operatingSystem
ssid
latency
message
```

Não voltar para propriedades PascalCase como:

```text
OperatingSystem
Ssid
Gateway
ManagementVlan
```

Variáveis:

```text
router
switchCore
server
accessPoint
dispositivos
dispositivo
pingService
resultadoPing
manager
discoveryService
subnetMask
prefixo
partes
valor
bits
```

---

# 4. NetworkDevice.cs

Código atual:

```csharp
namespace NetAssist.Models
{
    public abstract class NetworkDevice
    {
        public string hostname { get; set; }

        public string ipAddress { get; set; }

        public string macAddress { get; set; }

        public string gateway { get; set; }

        public DeviceStatus status { get; set; }

        public abstract string GetDeviceType();
    }
}
```

---

# 5. Router.cs

```csharp
namespace NetAssist.Models
{
    public class Router : NetworkDevice
    {
        public override string GetDeviceType()
        {
            return "Roteador";
        }
    }
}
```

---

# 6. Switch.cs

```csharp
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
```

---

# 7. Server.cs

```csharp
namespace NetAssist.Models
{
    public class Server : NetworkDevice
    {
        public string operatingSystem { get; set; }

        public override string GetDeviceType()
        {
            return "Servidor";
        }
    }
}
```

---

# 8. AccessPoint.cs

```csharp
namespace NetAssist.Models
{
    public class AccessPoint : NetworkDevice
    {
        public string ssid { get; set; }

        public override string GetDeviceType()
        {
            return "Access Point";
        }
    }
}
```

---

# 9. DeviceStatus.cs

```csharp
namespace NetAssist.Models
{
    public enum DeviceStatus
    {
        Unknown,
        Online,
        Offline
    }
}
```

---

# 10. PingResult.cs

```csharp
namespace NetAssist.Models
{
    public class PingResult
    {
        public DeviceStatus status { get; set; }

        public long latency { get; set; }

        public string message { get; set; }
    }
}
```

---

# 11. PingService.cs

IMPORTANTE: recentemente alteramos o timeout do Ping.

Código atual:

```csharp
using System;
using System.Net.NetworkInformation;
using NetAssist.Models;

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
```

O timeout atual é:

```csharp
ping.Send(ipAddress, 500);
```

Ou seja:

```text
500 ms = 0,5 segundo
```

Já testei e funcionou.

---

# 12. NetworkDeviceManager.cs

Código atual:

```csharp
using NetAssist.Models;
using System.Collections.Generic;

namespace NetAssist.Services
{
    public class NetworkDeviceManager
    {
        private List<NetworkDevice> dispositivos =
            new List<NetworkDevice>();

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
```

Já testei `FindByIp()` e funcionou.

---

# 13. NetworkDiscoveryService.cs

O serviço já consegue:

* validar máscara;
* calcular prefixo;
* calcular endereço de rede;
* calcular broadcast;
* gerar todos os IPs utilizáveis.

Código atual:

```csharp
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace NetAssist.Services
{
    public class NetworkDiscoveryService
    {
        public int GetPrefixFromMask(string subnetMask)
        {
            string[] partes = subnetMask.Split('.');

            if (partes.Length != 4)
            {
                throw new ArgumentException(
                    "A máscara deve estar no formato 255.255.255.0."
                );
            }

            int prefixo = 0;
            bool encontrouZero = false;

            foreach (string parte in partes)
            {
                if (!int.TryParse(parte, out int valor) ||
                    valor < 0 ||
                    valor > 255)
                {
                    throw new ArgumentException(
                        "Máscara de rede inválida."
                    );
                }

                int bits;

                switch (valor)
                {
                    case 255:
                        bits = 8;
                        break;

                    case 254:
                        bits = 7;
                        break;

                    case 252:
                        bits = 6;
                        break;

                    case 248:
                        bits = 5;
                        break;

                    case 240:
                        bits = 4;
                        break;

                    case 224:
                        bits = 3;
                        break;

                    case 192:
                        bits = 2;
                        break;

                    case 128:
                        bits = 1;
                        break;

                    case 0:
                        bits = 0;
                        break;

                    default:
                        throw new ArgumentException(
                            "Máscara de rede inválida."
                        );
                }

                if (encontrouZero && valor != 0)
                {
                    throw new ArgumentException(
                        "Máscara de rede inválida."
                    );
                }

                prefixo += bits;

                if (valor != 255)
                {
                    encontrouZero = true;
                }
            }

            return prefixo;
        }

        public string GetNetworkAddress(string ipAddress, string subnetMask)
        {
            string[] partesIp = ipAddress.Split('.');

            if (partesIp.Length != 4)
            {
                throw new ArgumentException(
                    "O endereço IP deve possuir 4 octetos."
                );
            }

            if (!IPAddress.TryParse(ipAddress, out IPAddress ip))
            {
                throw new ArgumentException(
                    "O endereço IP informado é inválido."
                );
            }

            if (!IPAddress.TryParse(subnetMask, out IPAddress mask))
            {
                throw new ArgumentException(
                    "A máscara de rede informada é inválida."
                );
            }

            if (ip.AddressFamily != AddressFamily.InterNetwork)
            {
                throw new ArgumentException(
                    "O endereço IP deve ser IPv4."
                );
            }

            if (mask.AddressFamily != AddressFamily.InterNetwork)
            {
                throw new ArgumentException(
                    "A máscara deve ser IPv4."
                );
            }

            byte[] ipBytes = ip.GetAddressBytes();
            byte[] maskBytes = mask.GetAddressBytes();

            byte[] networkBytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                networkBytes[i] =
                    (byte)(ipBytes[i] & maskBytes[i]);
            }

            IPAddress networkAddress =
                new IPAddress(networkBytes);

            return networkAddress.ToString();
        }

        public string GetBroadcastAddress(
            string ipAddress,
            string subnetMask)
        {
            string[] partesIp = ipAddress.Split('.');

            if (partesIp.Length != 4)
            {
                throw new ArgumentException(
                    "O endereço IP deve possuir 4 octetos."
                );
            }

            if (!IPAddress.TryParse(ipAddress, out IPAddress ip))
            {
                throw new ArgumentException(
                    "O endereço IP informado é inválido."
                );
            }

            if (!IPAddress.TryParse(subnetMask, out IPAddress mask))
            {
                throw new ArgumentException(
                    "A máscara de rede informada é inválida."
                );
            }

            if (ip.AddressFamily != AddressFamily.InterNetwork)
            {
                throw new ArgumentException(
                    "O endereço IP deve ser IPv4."
                );
            }

            if (mask.AddressFamily != AddressFamily.InterNetwork)
            {
                throw new ArgumentException(
                    "A máscara deve ser IPv4."
                );
            }

            byte[] ipBytes = ip.GetAddressBytes();
            byte[] maskBytes = mask.GetAddressBytes();

            byte[] broadcastBytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                broadcastBytes[i] =
                    (byte)(ipBytes[i] |
                    (maskBytes[i] ^ 255));
            }

            IPAddress broadcastAddress =
                new IPAddress(broadcastBytes);

            return broadcastAddress.ToString();
        }

        public List<string> GetUsableIpAddresses(
            string ipAddress,
            string subnetMask)
        {
            string networkAddress =
                GetNetworkAddress(
                    ipAddress,
                    subnetMask
                );

            string broadcastAddress =
                GetBroadcastAddress(
                    ipAddress,
                    subnetMask
                );

            IPAddress networkIp =
                IPAddress.Parse(networkAddress);

            IPAddress broadcastIp =
                IPAddress.Parse(broadcastAddress);

            byte[] networkBytes =
                networkIp.GetAddressBytes();

            byte[] broadcastBytes =
                broadcastIp.GetAddressBytes();

            uint networkValue =
                ((uint)networkBytes[0] << 24) |
                ((uint)networkBytes[1] << 16) |
                ((uint)networkBytes[2] << 8) |
                networkBytes[3];

            uint broadcastValue =
                ((uint)broadcastBytes[0] << 24) |
                ((uint)broadcastBytes[1] << 16) |
                ((uint)broadcastBytes[2] << 8) |
                broadcastBytes[3];

            List<string> ipAddresses =
                new List<string>();

            for (
                uint valor = networkValue + 1;
                valor < broadcastValue;
                valor++)
            {
                byte primeiroOcteto =
                    (byte)(valor >> 24);

                byte segundoOcteto =
                    (byte)(valor >> 16);

                byte terceiroOcteto =
                    (byte)(valor >> 8);

                byte quartoOcteto =
                    (byte)valor;

                string endereco =
                    $"{primeiroOcteto}." +
                    $"{segundoOcteto}." +
                    $"{terceiroOcteto}." +
                    $"{quartoOcteto}";

                ipAddresses.Add(endereco);
            }

            return ipAddresses;
        }
    }
}
```

---

# 14. Testes já realizados

### Máscara

```text
255.255.255.0 → /24
255.255.255.128 → /25
255.255.255.192 → /26
```

Máscaras inválidas também foram tratadas.

### Endereço de rede

```text
IP: 192.168.1.25
Máscara: 255.255.255.0

Resultado:
192.168.1.0
```

### Broadcast

```text
IP: 192.168.1.25
Máscara: 255.255.255.0

Resultado:
192.168.1.255
```

### Lista de IPs

```text
IP: 192.168.1.25
Máscara: 255.255.255.0

Quantidade:
254

Primeiro:
192.168.1.1

Último:
192.168.1.254
```

Também testamos `/25`:

```text
IP: 192.168.1.150
Máscara: 255.255.255.128

Primeiro:
192.168.1.129

Último:
192.168.1.254

Quantidade:
126
```

---

# 15. Objetivo real do NetAssist

Minha intenção não é simplesmente descobrir o primeiro e último IP.

Quero que o NetAssist futuramente descubra quais IPs estão:

```text
OCUPADOS
```

e quais estão:

```text
POSSIVELMENTE DISPONÍVEIS
```

Exemplo:

```text
192.168.1.1    OCUPADO
192.168.1.2    OCUPADO
192.168.1.3    NÃO RESPONDEU
192.168.1.4    OCUPADO
192.168.1.5    NÃO RESPONDEU
```

IMPORTANTE:

Um IP que não responde ao Ping **não deve ser automaticamente considerado livre**, porque um equipamento pode estar ligado e bloquear ICMP.

Portanto, inicialmente:

```text
Ping respondeu     → OCUPADO / ONLINE
Ping não respondeu → NÃO RESPONDEU
```

Depois poderemos fazer outras verificações.

---

# 16. Próxima etapa EXATA

A última coisa que estávamos prestes a fazer era utilizar:

```csharp
Parallel.ForEach()
```

para acelerar os Pings.

Eu sugeri usar:

```csharp
List<string> dispositivosEncontrados =
    new List<string>();

Parallel.ForEach(
    ipAddresses,
    new ParallelOptions
    {
        MaxDegreeOfParallelism = 20
    },
    ipAddress =>
    {
        PingResult resultadoPing =
            pingService.TestConnection(ipAddress);

        if (resultadoPing.status == DeviceStatus.Online)
        {
            lock (dispositivosEncontrados)
            {
                dispositivosEncontrados.Add(ipAddress);
            }
        }
    }
);

MessageBox.Show(
    $"IPs encontrados: {dispositivosEncontrados.Count}"
);
```

O usuário perguntou se isso diminuiria o tempo dos Pings.

Expliquei que sim, provavelmente bastante, porque atualmente os Pings são executados sequencialmente.

Também expliquei:

```text
MaxDegreeOfParallelism = 20
```

significa no máximo 20 operações simultâneas.

E:

```csharp
lock (dispositivosEncontrados)
```

é necessário para proteger a lista quando várias threads tentam modificá-la.

Ainda NÃO implementamos esse código.

---

# 17. O que fazer quando eu disser "vamos"

Quando eu disser **"vamos"**, continue EXATAMENTE nessa etapa:

**Implementar o `Parallel.ForEach` para testar os IPs simultaneamente.**

Explique:

* o que é uma thread;
* por que o `Parallel.ForEach` pode acelerar;
* o que significa `MaxDegreeOfParallelism`;
* por que usamos `lock`;
* como o código funciona;
* como testar;
* comparar mentalmente com o método sequencial.

Não avance ainda para:

* `async/await`;
* hostname;
* MAC;
* fabricante;
* portas;
* SNMP;
* classificação de dispositivos;
* criação automática de `NetworkDevice`;
* integração com `NetworkDeviceManager`;
* DNS;
* Traceroute;
* monitoramento.

Tudo isso fica para etapas posteriores.

## Regra final

Sempre mantenha o desenvolvimento didático.

**Uma etapa por vez.**

Depois de eu testar, espere eu dizer **"foi"**.

 */




