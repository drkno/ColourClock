#region

using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ColourClock.GUI
{
    public partial class Settings : Form
    {
        private readonly ColourClock _mainWindow;

        // keep this variable to prevent runtime exceptions
        private int _tempTransfer;

        public Settings(ColourClock main)
        {
            InitializeComponent();
            _mainWindow = main;
            DisplaySettings();
            _mainWindow.MinimizeMemory();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            PhraseSettings();
            _mainWindow.SetVals();
            _mainWindow.SaveFile(string.Empty);
            _mainWindow.MinimizeMemory();
        }

        private void PhraseSettings()
        {
            for (var i = 0; i < 4; i++)
            {
                var txtBox =
                    (TextBox)
                        GetType()
                            .GetField("textBoxX" + (i + 1), BindingFlags.Instance | BindingFlags.NonPublic)
                            .GetValue(this);
                int.TryParse(txtBox.Text, out _tempTransfer);
                _mainWindow.Xy[i].X = _tempTransfer;
                txtBox =
                    (TextBox)
                        GetType()
                            .GetField("textBoxY" + (i + 1), BindingFlags.Instance | BindingFlags.NonPublic)
                            .GetValue(this);
                int.TryParse(txtBox.Text, out _tempTransfer);
                _mainWindow.Xy[i].Y = _tempTransfer;
                txtBox =
                    (TextBox)
                        GetType()
                            .GetField("textBoxR" + (i + 1), BindingFlags.Instance | BindingFlags.NonPublic)
                            .GetValue(this);
                int.TryParse(txtBox.Text, out _tempTransfer);
                _mainWindow.Rad[i] = _tempTransfer;
            }

            _mainWindow.Shape = comboBoxShape.SelectedIndex;
            int.TryParse(textBoxWindowX.Text, out _tempTransfer);
            _mainWindow.WindSize.X = _tempTransfer;
            int.TryParse(textBoxWindowY.Text, out _tempTransfer);
            _mainWindow.WindSize.Y = _tempTransfer;
            _mainWindow.FirstRun = checkBoxFirstRun.Checked;
        }

        private void ButtonExportClick(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog {Filter = "Configuration File (*.ini)|*.ini"};
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _mainWindow.SaveFile(saveFileDialog.FileName);
            }
            _mainWindow.MinimizeMemory();
        }

        private void ButtonImportClick(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog {Filter = "Configuration File (*.ini)|*.ini"};
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _mainWindow.LoadConfig(openFileDialog.FileName);
                DisplaySettings();
            }
            _mainWindow.MinimizeMemory();
        }

        public void DisplaySettings()
        {
            for (var i = 0; i < 4; i++)
            {
                var txtBox =
                    (TextBox)
                        GetType()
                            .GetField("textBoxX" + (i + 1), BindingFlags.Instance | BindingFlags.NonPublic)
                            .GetValue(this);
                _tempTransfer = _mainWindow.Xy[i].X;
                txtBox.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
                txtBox =
                    (TextBox)
                        GetType()
                            .GetField("textBoxY" + (i + 1), BindingFlags.Instance | BindingFlags.NonPublic)
                            .GetValue(this);
                _tempTransfer = _mainWindow.Xy[i].Y;
                txtBox.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
                txtBox =
                    (TextBox)
                        GetType()
                            .GetField("textBoxR" + (i + 1), BindingFlags.Instance | BindingFlags.NonPublic)
                            .GetValue(this);
                _tempTransfer = _mainWindow.Rad[i];
                txtBox.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            }

            comboBoxShape.SelectedIndex = _mainWindow.Shape;
            _tempTransfer = _mainWindow.WindSize.X;
            textBoxWindowX.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.WindSize.Y;
            textBoxWindowY.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            checkBoxFirstRun.Checked = _mainWindow.FirstRun;

            _mainWindow.MinimizeMemory();
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void SettingsMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }
    }
}