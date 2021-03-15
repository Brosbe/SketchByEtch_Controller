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

namespace SketchByEtch
{
    public partial class SettingsForm : Form
    {
        private Settings Settings;
        private Settings EditSettings;
        public SettingsForm()
        {
            Settings = new Settings();
            EditSettings = new Settings();
            InitializeComponent();
        }

        public SettingsForm(Settings settings)
        {
            InitializeComponent();
            Settings = settings;
            EditSettings = new Settings
            {
                InvertX = settings.InvertX,
                InvertY = settings.InvertY,
                ScreenHeight = settings.ScreenHeight,
                ScreenWidth = settings.ScreenWidth,
                UseFullScreen = settings.UseFullScreen,
                MaxKnobValue = settings.MaxKnobValue,
                SwapKnobs = settings.SwapKnobs
            };
            cboxInvertX.Checked = Settings.InvertX;
            cboxInvertY.Checked = Settings.InvertY;
            cboxSwapKnobs.Checked = Settings.SwapKnobs;
            cboxFullScreen.Checked = Settings.SwapKnobs;
            txtKnobsMaxValue.Text = Settings.MaxKnobValue.ToString();
            txtScreenWidth.Text = Settings.ScreenWidth.ToString();
            txtScreenHeight.Text = Settings.ScreenHeight.ToString();
        }


        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            var ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 190)
            {
                e.Handled = true;
            }

        }

        private void btnAutoSettings_Click(object sender, EventArgs e)
        {
            txtScreenWidth.Text = Screen.PrimaryScreen.Bounds.Width.ToString();
            txtScreenHeight.Text = Screen.PrimaryScreen.Bounds.Height.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtScreenWidth.Text.Trim() == string.Empty || txtScreenHeight.Text.Trim() == string.Empty || txtKnobsMaxValue.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please fill in all textboxes with the correct information", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Settings.ScreenHeight = int.Parse(txtScreenHeight.Text);
            Settings.ScreenWidth = int.Parse(txtScreenWidth.Text);
            Settings.InvertX = EditSettings.InvertX;
            Settings.InvertY = EditSettings.InvertY;
            Settings.MaxKnobValue = int.Parse(txtKnobsMaxValue.Text);
            Settings.UseFullScreen = EditSettings.UseFullScreen;
            Settings.SwapKnobs = EditSettings.SwapKnobs;
            var writer = new XmlSerializer(typeof(Settings));
            FileStream file = File.Create(@".\Settings.xml");
            writer.Serialize(file, Settings);
            file.Close();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboxSwapKnobs_CheckedChanged(object sender, EventArgs e)
        {
            EditSettings.SwapKnobs = cboxSwapKnobs.Checked;
        }

        private void cboxInvertX_CheckedChanged(object sender, EventArgs e)
        {
            EditSettings.InvertX = cboxInvertX.Checked;
        }

        private void cboxInvertY_CheckedChanged(object sender, EventArgs e)
        {
            EditSettings.InvertY = cboxInvertY.Checked;
        }

        private void cboxFullScreen_CheckedChanged(object sender, EventArgs e)
        {
            EditSettings.UseFullScreen = cboxFullScreen.Checked;
        }
    }
}
