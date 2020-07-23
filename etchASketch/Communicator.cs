using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace etchASketch
{
    public class Communicator
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private SerialPort port;

        private bool _EtchMode { get; set; }
        private bool _HasConnected { get; set; }
        private bool _IsHandling { get; set; }
        private bool _Loop { get; set; }
        private bool _IsProbing { get; set; }
        public Settings Settings { get; set; }

        public bool EtchMode
        {
            get { return _EtchMode; }
            private set { _EtchMode = value; }
        }

        public bool HasConnected
        {
            get { return _HasConnected; }
            private set { _HasConnected = value; }
        }

        public bool IsHandling
        {
            get { return _IsHandling; }
            private set { _IsHandling = value; }
        }

        public bool Loop
        {
            get { return _Loop; }
            private set { _Loop = value; }
        }

        public bool IsProbing
        {
            get { return _IsProbing; }
            set { _IsProbing = value; }
        }

        public Communicator()
        {
            port = new SerialPort();
            Loop = true;
        }

        public Communicator(Settings settings)
        {
            port = new SerialPort();
            Loop = true;
            Settings = settings;
        }

        public void Serial()
        {
            port.Open();
            Loop = true;
            try
            {
                while (Loop)
                {
                    if (!IsHandling)
                    {
                        port.WriteLine(".");
                        Thread.Sleep(6);
                    }
                }
            }
            catch (Exception)
            {
                Task.Run(() => MessageBox.Show("Somthing went wrong while trying to communicate with the controller", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            HasConnected = false;
            if (port.IsOpen)
            {
                port.Close();
            }
            port.DataReceived -= HandleReceivedDate;
        }

        public void SetPort(string portString)
        {
            port.BaudRate = 19200;
            port.PortName = portString;
        }

        private void HandleReceivedDate(object sender, EventArgs e)
        {
            if (!HasConnected)
            {
                return;
            }
            IsHandling = true;
            var serialInfo = port.ReadLine();
            serialInfo = serialInfo.Substring(0, serialInfo.Length - 1);
            var pattern = new Regex("[0-9]{1,4}[,][0-9]{1,4}[|][01]");
            serialInfo = pattern.Match(serialInfo).ToString();
            if (!pattern.IsMatch(serialInfo))
            {
                Loop = false;
                HasConnected = false;
                Task.Run(() => MessageBox.Show("Unexpected data received", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
                return;
            }
            string[] split = serialInfo.Split('|');

            EtchMode = split[1] == "1";

            if (EtchMode)
            {
                var val = Settings.CalculatePosition(split[0]);
                SetCursorPos(val.X, val.Y);
            }
            IsHandling = false;
        }

        public void End()
        {
            Loop = false;
            HasConnected = false;
            EtchMode = false;
            if (port.IsOpen)
            {
                port.Close();
            }
            port.DataReceived -= HandleReceivedDate;
        }

        public void Probe(string portString)
        {
            try
            {
                var probePort = new SerialPort { PortName = portString, BaudRate = 19200};
                probePort.Open();
                Thread.Sleep(2500);
                probePort.WriteLine("-");
                Thread.Sleep(50);
                HasConnected = probePort.ReadLine() == "?\r";
                probePort.Close();
                SetPort(portString);
                port.DataReceived += HandleReceivedDate;
            }
            catch (Exception){}
        }
    }
}