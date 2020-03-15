using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;

namespace etchASketch
{
    public partial class Form1 : Form
    {
        public Communicator communicator;
        private Thread serialThread;
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

            if (communicator.IsMouseDown)
            {
                lblMouseToggle.Text = "On";
                lblMouseToggle.ForeColor = Color.Green;
            }
            else
            {
                lblMouseToggle.Text = "Off";
                lblMouseToggle.ForeColor = Color.Red;
            }
            lstPorts.DataSource = SerialPort.GetPortNames();
            lstPorts.Refresh();
            btnConnect.Enabled = !communicator.HasConnected;
            lstPorts.Enabled = !communicator.HasConnected;
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            communicator.EndConnection();
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
