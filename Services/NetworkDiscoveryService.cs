    using System;
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
                    throw new ArgumentException(
                        "A máscara deve estar no formato 255.255.255.0."
                    );
                }

                int prefixo = 0;
                bool encontrouZero = false;

                foreach (string parte in partes)
                {
                    if (!int.TryParse(parte, out int valor) || valor < 0 || valor > 255)
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

        }
    }
