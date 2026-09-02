using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetAssist.Services
{
    public class NetworkDiscoveryService
    {
        public int GetPrefixFromMask(string subnetMask)
        {
            string[] partes = subnetMask.Split('.');

            if (partes.Length != 4)
            {
                throw new ArgumentException("A máscara deve estar no formato 255.255.255.0.");
            }

            int prefixo = 0;
            bool encontrouZero = false;

            foreach (string parte in partes)
            {
                if (!int.TryParse(parte, out int valor) ||valor < 0 ||valor > 255)
                {
                    throw new ArgumentException("Máscara de rede inválida.");
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
                        throw new ArgumentException("Máscara de rede inválida.");
                }

                if (encontrouZero && valor != 0)
                {
                    throw new ArgumentException("Máscara de rede inválida.");
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
                throw new ArgumentException("O endereço IP deve possuir 4 octetos.");
            }

            if (!IPAddress.TryParse(ipAddress, out IPAddress ip))
            {
                throw new ArgumentException("O endereço IP informado é inválido.");
            }

            if (!IPAddress.TryParse(subnetMask, out IPAddress mask))
            {
                throw new ArgumentException("A máscara de rede informada é inválida.");
            }

            if (ip.AddressFamily != AddressFamily.InterNetwork)
            {
                throw new ArgumentException("O endereço IP deve ser IPv4.");
            }

            if (mask.AddressFamily != AddressFamily.InterNetwork)
            {
                throw new ArgumentException("A máscara deve ser IPv4.");
            }

            byte[] ipBytes = ip.GetAddressBytes();
            byte[] maskBytes = mask.GetAddressBytes();

            byte[] networkBytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                networkBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
            }

            IPAddress networkAddress = new IPAddress(networkBytes);

            return networkAddress.ToString();
        }

        public string GetBroadcastAddress(string ipAddress, string subnetMask)
        {
            string[] partesIp = ipAddress.Split('.');

            if (partesIp.Length != 4)
            {
                throw new ArgumentException("O endereço IP deve possuir 4 octetos.");
            }

            if (!IPAddress.TryParse(ipAddress, out IPAddress ip))
            {
                throw new ArgumentException("O endereço IP informado é inválido.");
            }

            if (!IPAddress.TryParse(subnetMask, out IPAddress mask))
            {
                throw new ArgumentException("A máscara de rede informada é inválida.");
            }

            if (ip.AddressFamily != AddressFamily.InterNetwork)
            {
                throw new ArgumentException("O endereço IP deve ser IPv4.");
            }

            if (mask.AddressFamily != AddressFamily.InterNetwork)
            {
                throw new ArgumentException("A máscara deve ser IPv4.");
            }

            byte[] ipBytes = ip.GetAddressBytes();
            byte[] maskBytes = mask.GetAddressBytes();

            byte[] broadcastBytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                broadcastBytes[i] = (byte)(ipBytes[i] | (maskBytes[i] ^ 255));
            }

            IPAddress broadcastAddress = new IPAddress(broadcastBytes);

            return broadcastAddress.ToString();
        }
        public string[] GetUsableIpRange(string ipAddress, string subnetMask)
        {
            string networkAddress = GetNetworkAddress(ipAddress,subnetMask);

            string broadcastAddress = GetBroadcastAddress(ipAddress,subnetMask);

            IPAddress networkIp = IPAddress.Parse(networkAddress);
            IPAddress broadcastIp = IPAddress.Parse(broadcastAddress);

            byte[] networkBytes = networkIp.GetAddressBytes();
            byte[] broadcastBytes = broadcastIp.GetAddressBytes();

            byte[] firstIpBytes = (byte[])networkBytes.Clone();
            byte[] lastIpBytes = (byte[])broadcastBytes.Clone();

            firstIpBytes[3]++;
            lastIpBytes[3]--;

            IPAddress firstIp = new IPAddress(firstIpBytes);
            IPAddress lastIp = new IPAddress(lastIpBytes);

            return new string[]
            {
                firstIp.ToString(),
                lastIp.ToString()
            };
        }

        public List<string> GetUsableIpAddresses(string ipAddress,string subnetMask)
        {
            string networkAddress = GetNetworkAddress(ipAddress,subnetMask);

            string broadcastAddress = GetBroadcastAddress(ipAddress,subnetMask);

            IPAddress networkIp = IPAddress.Parse(networkAddress);

            IPAddress broadcastIp = IPAddress.Parse(broadcastAddress);

            byte[] networkBytes = networkIp.GetAddressBytes();

            byte[] broadcastBytes = broadcastIp.GetAddressBytes();

            uint networkValue = ((uint)networkBytes[0] << 24) | ((uint)networkBytes[1] << 16) | ((uint)networkBytes[2] << 8) | networkBytes[3];

            uint broadcastValue = ((uint)broadcastBytes[0] << 24) | ((uint)broadcastBytes[1] << 16) | ((uint)broadcastBytes[2] << 8) | broadcastBytes[3];

            List<string> ipAddresses = new List<string>();

            for ( uint valor = networkValue + 1; valor < broadcastValue; valor++)
            {
                byte primeiroOcteto = (byte)(valor >> 24);

                byte segundoOcteto = (byte)(valor >> 16);

                byte terceiroOcteto = (byte)(valor >> 8);

                byte quartoOcteto = (byte)valor;

                string endereco = $"{primeiroOcteto}." +$"{segundoOcteto}." +$"{terceiroOcteto}." + $"{quartoOcteto}";

                ipAddresses.Add(endereco);
            }

            return ipAddresses;
        }
    }
}