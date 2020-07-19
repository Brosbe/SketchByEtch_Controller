using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
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
            Init();
        }

        private void Init()
        {
            communicator = new Communicator();
            timer1.Enabled = true;
            prevPorts = SerialPort.GetPortNames();
            btnDisconnect.Enabled = false;
            btnConnect.Enabled = false;
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
                if (curPorts.Length > prevPorts.Length)
                {
                    if (communicator.IsProbing)
                    {
                        communicator.IsProbing = false;
                    }

                }
            }

            btnConnect.Enabled = !communicator.IsProbing;

            if (curPorts.Length == 0)
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = false;
            }

            else
            {
                btnConnect.Enabled = !communicator.HasConnected;
                btnDisconnect.Enabled = communicator.HasConnected;
            }

            prevPorts = curPorts;
            lstPorts.Enabled = !communicator.HasConnected;
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            communicator.End();
            try
            {
                serialThread.Abort();
            }
            catch (Exception){}
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (lstPorts.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a port", "error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            btnConnect.Enabled = false;
            communicator.Probe(lstPorts.SelectedItem.ToString());
            if (communicator.HasConnected)
            {
                serialThread = new Thread(communicator.Serial);
                serialThread.Name = "serialThread";
                serialThread.Start();
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            communicator.End();
            //btnConnect.Enabled = false;
            //btnDisconnect.Enabled = false;
            //Thread.Sleep(500);
            //timer1.Enabled = true;
        }
    }
}
