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

            NetworkDiscoveryService discoveryService = new NetworkDiscoveryService();

            int prefixo = discoveryService.GetPrefixFromMask("255.255.255.0");

            MessageBox.Show("Prefixo: /" + prefixo);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NetworkDeviceManager manager = new NetworkDeviceManager();

            Router router = new Router();
            router.hostname = "RTR-CORE-01";
            router.ipAddress = "192.168.1.1";

            Switch switchCore = new Switch();
            switchCore.hostname = "SW-CORE-01";
            switchCore.ipAddress = "192.168.1.2";

            Server server = new Server();
            server.hostname = "SRV-01";
            server.ipAddress = "192.168.1.20";

            AccessPoint accessPoint = new AccessPoint();
            accessPoint.hostname = "AP-01";
            accessPoint.ipAddress = "192.168.1.10";

            manager.AddDevice(router);
            manager.AddDevice(switchCore);
            manager.AddDevice(server);
            manager.AddDevice(accessPoint);


            NetworkDevice dispositivoEncontrado = manager.FindByIp("192.168.1.20");

            if (dispositivoEncontrado != null)
            {
                MessageBox.Show(
                    $"Dispositivo encontrado!\n\n" +
                    $"Nome: {dispositivoEncontrado.hostname}\n" +
                    $"IP: {dispositivoEncontrado.ipAddress}\n" +
                    $"Tipo: {dispositivoEncontrado.GetDeviceType()}"
                );
            }
            else
            {
                MessageBox.Show("Dispositivo não encontrado.");
            }



            string resultado = "";

            IReadOnlyList<NetworkDevice> dispositivos = manager.GetDevices();

            foreach (NetworkDevice dispositivo in dispositivos)
            {
                resultado +=
                    $"Nome: {dispositivo.hostname}\n" +
                    $"IP: {dispositivo.ipAddress}\n" +
                    $"Tipo: {dispositivo.GetDeviceType()}\n\n";
            }

            MessageBox.Show(resultado);
        }
    }
}


/*
PROMPT DE CONTINUIDADE — PROJETO NETASSIST
Quero continuar o desenvolvimento do projeto NetAssist exatamente a partir do estado descrito abaixo.

IMPORTANTE:

O desenvolvimento deve continuar de forma PASSO A PASSO e DIDÁTICA.

NÃO entregar várias etapas de uma vez.

Para cada etapa:

Explicar o objetivo;
Informar qual arquivo criar ou alterar;
Mostrar o código completo daquela etapa;
Explicar as partes importantes do código;
Explicar o conceito de C# e POO utilizado;
Mostrar como testar;
Esperar eu testar;
Somente depois avançar para a próxima etapa.
Não alterar funcionalidades existentes sem necessidade.

Evitar código desnecessariamente complexo.

Priorizar código organizado, reutilizável, simples e fácil de manter.

O objetivo não é apenas construir o NetAssist, mas também aprender:

C#;
Programação Orientada a Objetos;
Classes;
Objetos;
Herança;
Abstração;
Polimorfismo;
Encapsulamento;
Interfaces;
Coleções;
Separação de responsabilidades;
Serviços;
Arquitetura;
Boas práticas.
================================================================================
PROJETO
Nome:

NetAssist

Tecnologias:

C#
.NET
Windows Forms
Visual Studio
Programação Orientada a Objetos
Objetivo:

Criar uma aplicação desktop para Windows destinada a auxiliar profissionais
de redes em tarefas como:

Diagnóstico de conectividade;
Ping;
DNS;
Traceroute;
Scanner de portas;
Descoberta de dispositivos;
Inventário;
Monitoramento;
Alertas;
Registro de eventos.
Futuramente:

Banco de dados;
Dashboard;
Gráficos;
Histórico;
Relatórios;
Logs;
Descoberta automática de dispositivos;
Informações de interfaces de rede;
Wake-on-LAN;
Sistema de usuários e permissões.
================================================================================
PADRÃO DE NOMENCLATURA
Manter este padrão em todo o projeto.

Classes:

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

Variáveis:

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

Propriedades devem utilizar camelCase:

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

Métodos:

GetDeviceType()
AddDevice()
GetDevices()
FindByIp()
TestConnection()
GetPrefixFromMask()

IMPORTANTE:

Não voltar a utilizar propriedades como:

OperatingSystem
Ssid
Gateway
ManagementVlan

quando a intenção for seguir o padrão definido.

Utilizar:

operatingSystem
ssid
gateway
managementVlan

================================================================================
ESTRUTURA ATUAL DO PROJETO
NetAssist
│
├── Models
│ ├── NetworkDevice.cs
│ ├── Router.cs
│ ├── Switch.cs
│ ├── Server.cs
│ ├── AccessPoint.cs
│ ├── DeviceStatus.cs
│ └── PingResult.cs
│
├── Services
│ ├── PingService.cs
│ ├── NetworkDeviceManager.cs
│ └── NetworkDiscoveryService.cs
│
└── Form1.cs

================================================================================
MODEL — NetworkDevice
Arquivo:

Models/NetworkDevice.cs

Código atual:

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

Conceitos já aprendidos:

Abstração;
Classe abstrata;
Propriedades;
Método abstrato.
================================================================================
MODEL — Router
Arquivo:

Models/Router.cs

Código atual:

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

Router herda de NetworkDevice.

Atualmente não possui propriedades específicas.

================================================================================
MODEL — Switch
Arquivo:

Models/Switch.cs

Código atual:

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

================================================================================
MODEL — Server
Arquivo:

Models/Server.cs

Código atual:

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

================================================================================
MODEL — AccessPoint
Arquivo:

Models/AccessPoint.cs

Código atual:

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

================================================================================
MODEL — DeviceStatus
Arquivo:

Models/DeviceStatus.cs

Código atual:

namespace NetAssist.Models
{
public enum DeviceStatus
{
Unknown,
Online,
Offline
}
}

Conceito aprendido:

enum;
representação de estados conhecidos.
================================================================================
MODEL — PingResult
Arquivo:

Models/PingResult.cs

Código atual:

namespace NetAssist.Models
{
public class PingResult
{
public DeviceStatus status { get; set; }

    public long latency { get; set; }

    public string message { get; set; }
}

}

Objetivo:

Representar o resultado de uma operação de Ping.

O serviço executa a operação.

O objeto representa o resultado.

Isso ajuda na separação de responsabilidades.

================================================================================
SERVICE — PingService
Arquivo:

Services/PingService.cs

Código atual:

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
PingReply reply = ping.Send(ipAddress);

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

O PingService já foi testado.

Testes realizados:

Ping para 8.8.8.8 funcionou;
Ping retornou status;
Ping retornou latência;
Ping retornou mensagem;
endereço inválido foi tratado com try/catch;
projeto compilou corretamente.
Conceitos aprendidos:

Serviços;
separação de responsabilidades;
retorno de objetos;
try/catch;
Exception;
enum;
composição entre classes.
================================================================================
SERVICE — NetworkDeviceManager
Arquivo:

Services/NetworkDeviceManager.cs

Código atual:

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

Funcionalidades implementadas:

AddDevice()
GetDevices()
FindByIp()

Conceitos aprendidos:

Encapsulamento;
private;
coleções;
List<T>;
IReadOnlyList<T>;
herança;
polimorfismo;
retorno de objetos;
null;
foreach.
O FindByIp() já foi testado.

Teste realizado:

Busca:

192.168.1.20

Resultado:

SRV-01
Servidor

Também foi testado:

192.168.1.99

Resultado:

null / dispositivo não encontrado.

Portanto o FindByIp() está funcionando corretamente.

================================================================================
SERVICE — NetworkDiscoveryService
Arquivo:

Services/NetworkDiscoveryService.cs

A classe foi criada e agora possui uma primeira funcionalidade.

Código atual:

using System;

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
}

}

IMPORTANTE:

A versão acima foi ligeiramente melhorada durante o desenvolvimento.

Foi utilizado:

int.TryParse()

em vez de:

int.Parse()

Também foi criada a variável:

bool encontrouZero = false;

Essa variável permite rejeitar máscaras não contíguas.

Exemplo válido:

255.255.255.0

Exemplo inválido:

255.255.0.255

O switch converte os valores possíveis da máscara em quantidade de bits:

255 → 8
254 → 7
252 → 6
248 → 5
240 → 4
224 → 3
192 → 2
128 → 1
0 → 0

Exemplos:

255.255.255.0 → /24
255.255.255.128 → /25
255.255.255.192 → /26

A funcionalidade já foi testada e está funcionando.

================================================================================
DECISÃO DE PROJETO — ENTRADA DA REDE
Foi decidido que o usuário NÃO deverá precisar informar diretamente o CIDR.

Não queremos depender de:

192.168.1.0/24

como entrada principal.

A interface futura deverá preferencialmente trabalhar com:

IP:

192.168.1.0

Máscara:

255.255.255.0

O NetAssist será responsável por converter a máscara para o prefixo.

Exemplo:

255.255.255.0 → /24

255.255.255.128 → /25

255.255.255.192 → /26

Motivo:

Isso é mais amigável para o usuário que pode conhecer IP e máscara,
mas não necessariamente lembrar o prefixo CIDR.

Arquitetura futura:

TextBox do IP
↓
TextBox da Máscara
↓
Form1
↓
NetworkDiscoveryService
↓
Processamento da rede

IMPORTANTE:

A interface definitiva ainda NÃO foi criada.

Os valores continuam sendo utilizados diretamente nos testes.

================================================================================
FORM1 — ESTADO ATUAL
O Form1 continua sendo utilizado para testes temporários.

O teste antigo:

discoveryService.ValidateNetwork("192.168.1.0/24");

foi removido porque o método ValidateNetwork() não faz mais parte da versão
atual da NetworkDiscoveryService.

O teste atual utiliza:

NetworkDiscoveryService discoveryService =
new NetworkDiscoveryService();

int prefixo =
discoveryService.GetPrefixFromMask("255.255.255.0");

MessageBox.Show(
"Prefixo: /" + prefixo
);

Resultado esperado:

Prefixo: /24

Esse teste já foi realizado com sucesso.

O Form1 ainda é provisório.

Ainda NÃO criar a interface definitiva de diagnóstico ou descoberta.

================================================================================
CONCEITOS JÁ APRENDIDOS
Até este ponto já foram praticados:

Classes;
Objetos;
Propriedades;
Métodos;
Métodos abstratos;
Classes abstratas;
Abstração;
Herança;
Polimorfismo;
override;
enum;
List<T>;
IReadOnlyList<T>;
private;
Encapsulamento;
foreach;
null;
try/catch;
Exception;
serviços;
separação de responsabilidades;
retorno de objetos;
retorno de valores;
string.Split();
arrays;
int.Parse();
int.TryParse();
switch;
validação de dados;
conceitos básicos de IPv4;
máscara de rede;
prefixo CIDR.
================================================================================
IDEIA FUTURA — DESCOBERTA DE DISPOSITIVOS
A funcionalidade futura deverá permitir ao usuário informar:

IP:

192.168.1.0

Máscara:

255.255.255.0

O NetAssist deverá então:

Identificar a rede;
Determinar a faixa de endereços;
Gerar os endereços que deverão ser analisados;
Fazer Ping;
Tentar descobrir hostname;
Obter MAC/fabricante quando possível;
Coletar outras informações;
Fazer classificação com base em evidências;
Criar objetos NetworkDevice;
Adicionar os dispositivos ao NetworkDeviceManager.
Fluxo futuro:

IP + Máscara
↓
Endereço da rede
↓
Faixa de IPs
↓
Ping
↓
Hostname
↓
MAC/Fabricante
↓
Outras informações
↓
Classificação
↓
NetworkDevice
↓
NetworkDeviceManager

IMPORTANTE:

A classificação NÃO deve assumir que um equipamento é Router,
Switch, Server ou AccessPoint apenas porque respondeu ao Ping.

A classificação deverá considerar informações como:

Ping;
hostname;
MAC;
fabricante;
portas;
SNMP quando disponível e autorizado;
outras informações de rede.
Quando não houver informação suficiente:

DeviceStatus.Unknown

ou classificação desconhecida deve ser aceita.

Não inventar classificações.

================================================================================
PRÓXIMA ETAPA
O próximo passo lógico é descobrir o endereço da rede a partir de:

IP:

192.168.1.25

Máscara:

255.255.255.0

Resultado:

192.168.1.0

Isso é necessário porque o usuário pode informar o IP de qualquer
equipamento da rede, e não necessariamente o endereço da rede.

Exemplo:

192.168.1.25
255.255.255.0
↓
192.168.1.0

IMPORTANTE:

Ainda NÃO implementar:

Ping da rede;
geração completa dos IPs;
hostname;
MAC;
fabricante;
classificação;
criação automática de NetworkDevice;
NetworkDeviceManager integrado à descoberta.
Tudo deve continuar sendo implementado em pequenas etapas.

================================================================================
REGRA DE DESENVOLVIMENTO
Sempre avançar uma etapa por vez.

Não entregar o projeto inteiro.

Não implementar descoberta automática, DNS, Traceroute, Scanner e Monitoramento
ao mesmo tempo.

Primeiro terminar corretamente a etapa atual.

Depois testar.

Esperar minha confirmação.

Somente então continuar.

================================================================================
PONTO EXATO ONDE PARAMOS
A conversão de máscara para prefixo já está implementada e testada.

Exemplo:

255.255.255.0 → /24

A versão atual utiliza int.TryParse(), switch e a variável encontrouZero
para validar máscaras.

A decisão de projeto é utilizar IP + Máscara na interface, e não exigir que
o usuário conheça CIDR.

O próximo passo é implementar somente o cálculo do endereço da rede.

Exemplo:

IP:
192.168.1.25

Máscara:
255.255.255.0

Resultado:

192.168.1.0

Não avançar além dessa etapa.

================================================================================
INSTRUÇÃO FINAL
Continue o projeto exatamente desse ponto.

Primeiro confirme que entendeu o estado atual.

Não repita etapas que já foram concluídas.

Não recrie classes que já existem.

Não altere funcionalidades existentes sem necessidade.

Não avance várias etapas.

Siga o formato:

Objetivo;
Arquivo;
Código completo;
Explicação;
Conceito de POO/C#;
Teste;
Esperar minha confirmação.
O foco é construir o NetAssist enquanto eu aprendo C# e POO na prática.

FIM DO PROMPT DE CONTINUIDADE
*/




