using NetAssist.Models;
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

            List<NetworkDevice> dispositivos = new List<NetworkDevice>();

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

            dispositivos.Add(router);
            dispositivos.Add(switchCore);
            dispositivos.Add(server);
            dispositivos.Add(accessPoint);

            string resultado = "";

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
