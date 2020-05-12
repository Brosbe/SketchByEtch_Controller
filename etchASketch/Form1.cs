using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Linq;
using System.Xml.Serialization;

namespace etchASketch
{
    public partial class Form1 : Form
    {
        public Communicator communicator;
        private Thread serialThread;
        private string[] prevPorts;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            init();
        }

        private void init()
        {
            communicator = new Communicator();
            timer1.Enabled = true;
            serialThread = new Thread(communicator.serial);
            prevPorts = SerialPort.GetPortNames();
            lstPorts.DataSource = prevPorts;
            lstPorts.Refresh();
        }

        private void Update(object sender, EventArgs e)
        {
            if (communicator.EtchMode)
            {
                lblEtchMode.Text = "On";
                lblEtchMode.ForeColor = Color.Green;
            }
            else
            {
                lblEtchMode.Text = "Off";
                lblEtchMode.ForeColor = Color.Red;
            }

            if (communicator.HasConnected)
            {
                lblConnection.Text = "On";
                lblConnection.ForeColor = Color.Green;
            }

            else
            {
                lblConnection.Text = "Off";
                lblConnection.ForeColor = Color.Red;
            }

            var curPorts = SerialPort.GetPortNames();
            if (!curPorts.SequenceEqual(prevPorts))
            {
                lstPorts.DataSource = curPorts;
                lstPorts.Refresh();
            }

            if (curPorts.Length == 0)
                btnConnect.Enabled = false;
            
            else
                btnConnect.Enabled = !communicator.HasConnected;

            prevPorts = curPorts;
            lstPorts.Enabled = !communicator.HasConnected;
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            communicator.End();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                communicator.SetPort(lstPorts.SelectedItem.ToString());
                serialThread.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
