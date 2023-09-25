using System;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SketchByEtch.Communicator
{
    public class Communicator : SerialPort
    {
        private Regex pattern = new Regex("[0-9]{1,4}[,][0-9]{1,4}[|][01]");
        private bool _EtchMode { get; set; }
        private bool _HasConnected { get; set; }
        private bool _IsHandling { get; set; }
        private bool _IsProbing { get; set; }
        private int _XValue { get; set; }
        private int _YValue { get; set; }

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

        public bool IsProbing
        {
            get { return _IsProbing; }
            set { _IsProbing = value; }
        }

        public int XValue
        {
            get { return _XValue; }
            set { _XValue = value; }
        }

        public int YValue
        {
            get { return _YValue; }
            set { _YValue = value; }
        }

        public Communicator()
        {

        }

        public void SetPort(string portString)
        {
            base.BaudRate = 19200;
            base.PortName = portString;
        }

        private void HandleReceivedDate(object sender, EventArgs e)
        {
            if (!HasConnected)
            {
                return;
            }
            IsHandling = true;
            var serialInfo = base.ReadLine();
            serialInfo = serialInfo.Substring(0, serialInfo.Length - 1);
            serialInfo = pattern.Match(serialInfo).ToString();
            if (!pattern.IsMatch(serialInfo))
            {
                HasConnected = false;
                MessageBox.Show("Unexpected data received", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] boolSplit = serialInfo.Split('|');

            EtchMode = boolSplit[1] == "1";
            string[] posSplit = boolSplit[0].Split(',');

            XValue = Convert.ToInt32(posSplit[0]);
            YValue = Convert.ToInt32(posSplit[0]);
            IsHandling = false;
        }

        public void Probe(string portString)
        {
            try
            {
                var probePort = new SerialPort { PortName = portString, BaudRate = 19200 };
                probePort.Open();
                Thread.Sleep(2500);
                probePort.WriteLine("-");
                Thread.Sleep(50);
                HasConnected = probePort.ReadLine() == "?\r";
                probePort.Close();
                base.PortName = portString;
                base.DataReceived += HandleReceivedDate;
            }
            catch (Exception) { }
        }

    }
}
