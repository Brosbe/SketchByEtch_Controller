using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace etchASketch
{
    public partial class SettingsForm : Form
    {
        private Settings Settings;
        public SettingsForm()
        {
            Settings = new Settings();
            InitializeComponent();
        }

        public SettingsForm(Settings settings)
        {
            InitializeComponent();
            Settings = settings;
        }


        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            var ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void btnAutoSettings_Click(object sender, EventArgs e)
        {
            int x = Screen.PrimaryScreen.Bounds.Width;
            int y = Screen.PrimaryScreen.Bounds.Height;
            Settings.ScreenWidth = x;
            Settings.ScreenHeight = y;
            txtScreenWidth.Text = x.ToString();
            txtScreenHeight.Text = y.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtScreenWidth.Text.Trim() == string.Empty || txtScreenHeight.Text.Trim() == string.Empty || txtKnobsMaxValue.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please fill in all textboxes with the correct information", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var writer = new XmlSerializer(typeof(Settings));
            FileStream file = File.Create(@".\Settings.xml");
            writer.Serialize(file, Settings);
            file.Close();
            Close();
        }
    }
}
