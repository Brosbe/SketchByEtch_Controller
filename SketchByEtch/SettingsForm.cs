using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SketchByEtch
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
            Settings.InvertX = cboxInvertX.Checked;
            Settings.InvertY = cboxInvertY.Checked;
            Settings.MaxKnobValue = int.Parse(txtKnobsMaxValue.Text);
            Settings.UseFullScreen = cboxFullScreen.Checked;
            Settings.SwapKnobs = cboxSwapKnobs.Checked;
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

        public void ShowDialog(Settings settings)
        {
            txtKnobsMaxValue.Text = settings.MaxKnobValue.ToString();
            txtScreenHeight.Text = settings.ScreenHeight.ToString();
            txtScreenWidth.Text = settings.ScreenWidth.ToString();

            cboxFullScreen.Checked = settings.UseFullScreen;
            cboxInvertX.Checked = settings.InvertX;
            cboxInvertY.Checked = settings.InvertY;
            cboxSwapKnobs.Checked = settings.SwapKnobs;
            base.ShowDialog();
        }
    }
}
