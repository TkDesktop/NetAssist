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
 


na linha  + $"Mensagem: {resultadoPing.message}"; Colocar um if else pra nao dificultar a leitura da mensagem 
 */ 



