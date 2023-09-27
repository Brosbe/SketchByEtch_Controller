using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SketchByEtch
{
    public partial class SettingsForm : Form
    {
        private Settings _settings;
        public SettingsForm()
        {
            InitializeComponent();
        }

        public SettingsForm(Settings settings)
        {
            InitializeComponent();
            _settings = settings;
            cboxInvertX.Checked = _settings.InvertX;
            cboxInvertY.Checked = _settings.InvertY;
            cboxSwapKnobs.Checked = _settings.SwapKnobs;
            cboxFullScreen.Checked = _settings.SwapKnobs;
            txtKnobsMaxValue.Text = _settings.MaxKnobValue.ToString();
            txtScreenWidth.Text = _settings.ScreenWidth.ToString();
            txtScreenHeight.Text = _settings.ScreenHeight.ToString();
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
            _settings.ScreenHeight = int.Parse(txtScreenHeight.Text);
            _settings.ScreenWidth = int.Parse(txtScreenWidth.Text);
            _settings.InvertX = cboxInvertX.Checked;
            _settings.InvertY = cboxInvertY.Checked;
            _settings.MaxKnobValue = int.Parse(txtKnobsMaxValue.Text);
            _settings.UseFullScreen = cboxFullScreen.Checked;
            _settings.SwapKnobs = cboxSwapKnobs.Checked;
            var writer = new XmlSerializer(typeof(Settings));
            FileStream file = File.Create(@".\Settings.xml");
            writer.Serialize(file, _settings);
            file.Close();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void ShowDialog(Settings settings)
        {
            _settings = settings;
            base.ShowDialog();
        }
    }
}
