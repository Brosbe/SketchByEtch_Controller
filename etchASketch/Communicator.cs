using System;
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


        public Communicator()
        {
            port = new SerialPort();
            Loop = true;
            port.DataReceived += HandleReceivedDate;
        }

        public void serial()
        {

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
            port.Close();
        }

        public void SetPort(string portString)
        {
            port.BaudRate = 19200;
            port.PortName = portString;
            port.Open();
            try
            {
                port.WriteLine("-");
                Thread.Sleep(50);
                if (!HasConnected)
                {
                    port.Close();
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Task.Run( () => MessageBox.Show("Controller was either not valid or didn't start up yet.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            Loop = true;
        }

        private void HandleReceivedDate(object sender, EventArgs e)
        {
            IsHandling = true;
            if (!HasConnected)
            {
                try
                {
                    if (port.ReadLine() == "?\r")
                    {
                        HasConnected = true;
                        IsHandling = false;
                        return;
                    }
                }
                catch (Exception exception)
                {
                    Task.Run(() => MessageBox.Show("Could not read serial data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
                    HasConnected = true;
                    IsHandling = false;
                    return;
                }
                IsHandling = false;
                return;
            }

            Point prevVal = new Point(0, 0);
            Point cursorpoint = new Point(0, 0);
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
                var val = split[0].ToPoint();

                val.Y = 1080 - val.Y;

                val.X += 420;
                if (val.X != prevVal.X || val.Y != prevVal.X)
                {
                    SetCursorPos(val.X, val.Y);
                    //cursorpoint.X = val.X;
                    //cursorpoint.Y = val.Y;
                    //Cursor.Position = cursorpoint;
                    //prevVal.X = val.X;
                    //prevVal.Y = val.Y;
                }
            }
            IsHandling = false;
        }

        public void End()
        {
            Loop = false;
            HasConnected = false;
            EtchMode = false;
        }

        private async Task UnexpectedData()
        {
            MessageBox.Show("Unexpected data received", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}