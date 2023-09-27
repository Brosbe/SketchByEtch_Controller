using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SketchByEtch.Communicator;
using Newtonsoft.Json.Serialization;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace SketchByEtch
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]

        private static extern bool SetCursorPos(int X, int Y);

        public SketchCommunicator communicator;
        private SettingsForm settingsForm;
        private Settings settings;
        private string[] prevPorts;
        private const string Path = @".\Settings.json";
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
            if (!File.Exists(Path))
            {
                settingsForm = new SettingsForm();
                ShowSettingsNotCreatedDialog();
            }
            else
            {
                try
                {
                    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path));
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Settings Formatting", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Delete(Path);
                }
                settingsForm = new SettingsForm(settings);
            }
            communicator = new SketchCommunicator();
            TimerUpdate.Enabled = true;
            TimerCursor.Enabled = false;
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

            T_testread1.Text = communicator.XValue.ToString();
            T_testread2.Text = communicator.YValue.ToString();
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if(communicator.HasConnected)communicator.End();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Path))
            {
                ShowSettingsNotCreatedDialog();
                return;
            }
            if (lstPorts.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a port", "error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            btnConnect.Enabled = false;
            communicator.Probe(lstPorts.SelectedItem.ToString());
            if (communicator.HasConnected)
            {
                communicator.Start();
                TimerCursor.Enabled = true;
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
            TimerCursor.Enabled = false;
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            Disconnect();
            settingsForm.ShowDialog();
        }

        private void Disconnect()
        {
            //timer1.Enabled = false;
            communicator.End();
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            //Thread.Sleep(500);
            //timer1.Enabled = true;
        }

        private void ShowSettingsNotCreatedDialog()
        {
            MessageBox.Show("Config File not found. Please use the following window to create one", "Error!");
            settings = new Settings();
            settingsForm.ShowDialog(settings);
        }

        private void CursorUpdate(object sender, EventArgs e)
        {
            if (!communicator.EtchMode) return;
            var positions = settings.CalculatePosition(communicator.XValue, communicator.YValue);
            SetCursorPos(positions[0], positions[1]);
        }
    }
}
