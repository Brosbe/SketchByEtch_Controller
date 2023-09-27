using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace SketchByEtch
{
    public partial class SettingsForm : Form
    {
        private const string Path = @".\Settings.json";
        private Settings _settings;
        public SettingsForm()
        {
            InitializeComponent();
        }

        public SettingsForm(Settings settings)
        {
            InitializeComponent();
            _settings = settings;
            UpdateShownSettings();
        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            var ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46 && ch != 190)
            {
                e.Handled = true;
            }

        }

        private void BtnAutoSettings_Click(object sender, EventArgs e)
        {
            txtScreenWidth.Text = Screen.PrimaryScreen.Bounds.Width.ToString();
            txtScreenHeight.Text = Screen.PrimaryScreen.Bounds.Height.ToString();
        }

        private void BtnSave_Click(object sender, EventArgs e)
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
            File.WriteAllText(Path, JsonConvert.SerializeObject(_settings));
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void ShowDialog(Settings settings)
        {
            _settings = settings;
            base.ShowDialog();
        }

        public void ShowDialog()
        {
            UpdateShownSettings();
            base.ShowDialog();
        }

        private void UpdateShownSettings()
        {
            cboxInvertX.Checked = _settings.InvertX;
            cboxInvertY.Checked = _settings.InvertY;
            cboxSwapKnobs.Checked = _settings.SwapKnobs;
            cboxFullScreen.Checked = _settings.SwapKnobs;
            txtKnobsMaxValue.Text = _settings.MaxKnobValue.ToString();
            txtScreenWidth.Text = _settings.ScreenWidth.ToString();
            txtScreenHeight.Text = _settings.ScreenHeight.ToString();
        }
    }
}
