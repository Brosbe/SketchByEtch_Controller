using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing;
using System.Runtime.InteropServices;

namespace etchASketch
{
    public class Communicator
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;


        private SerialPort port;

        private bool _IsMouseDown { get; set; }
        private bool _Loop { get; set; }
        private bool _EtchMode { get; set; }
        private bool _HasConnected { get; set; }

        public bool IsMouseDown
        {
            get { return _IsMouseDown; }
            private set { _IsMouseDown = value; }
        }

        public bool Loop
        {
            get { return _Loop; }
            private set { _Loop = value; }
        }

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


        public Communicator()
        {
            port = new SerialPort();
            Loop = true;
        }

        public void serial()
        {

            try
            {
                Point prevVal = new Point(0, 0);
                Point cursorpoint = new Point(0, 0);
                while (Loop)
                {

                    port.WriteLine(".");
                    var serialInfo = port.ReadLine();
                    serialInfo = serialInfo.Substring(0, serialInfo.Length - 1);
                    string[] split = serialInfo.Split('|');

                    if (split[2] == "1")
                    {
                        EtchMode = !EtchMode;
                        if (IsMouseDown)
                        {
                            ToggleMouse();
                        }
                    }

                    if (EtchMode)
                    {
                        if (split[1] == "1")
                        {
                            ToggleMouse();
                        }
                        var val = split[0].ToPoint();

                        val.X += 449;
                        val.Y += 42;
                        if (val.X != prevVal.X || val.Y != prevVal.X)
                        {
                            cursorpoint.X = val.X;
                            cursorpoint.Y = val.Y;
                            Cursor.Position = cursorpoint;
                            prevVal.X = val.X;
                            prevVal.Y = val.Y;
                        }
                    }
                }


            }
            catch (Exception)
            {
                Loop = false;
                HasConnected = false;
                EtchMode = false;
                if (IsMouseDown)
                {
                    ToggleMouse();
                }
                MessageBox.Show("Somthing went wrong while trying to communicate with the controller", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            port.Close();
        }

        private void ToggleMouse()
        {
            IsMouseDown = !IsMouseDown;
            if (IsMouseDown)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                return;
            }
            mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);

        }

        public void EndConnection()
        {
            Loop = false;
        }

        public void SetPort(string portString)
        {
            port.BaudRate = 9600;
            port.PortName = portString;
            port.Open();
            port.WriteLine("-");
            if (port.ReadLine() != "?\r")
            {
                throw new Exception();
            }
            HasConnected = true;
        }
    }
}
