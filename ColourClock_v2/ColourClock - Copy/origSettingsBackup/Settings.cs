using System;
using System.Globalization;
using System.Windows.Forms;

namespace ColourClock
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
            int.TryParse(textBoxX1.Text, out _tempTransfer);
            _mainWindow.X1 = _tempTransfer;
            int.TryParse(textBoxY1.Text, out _tempTransfer);
            _mainWindow.Y1 = _tempTransfer;
            int.TryParse(textBoxR1.Text, out _tempTransfer);
            _mainWindow.R1 = _tempTransfer;
            _mainWindow.C1 = colourBox1.Color;

            int.TryParse(textBoxX2.Text, out _tempTransfer);
            _mainWindow.X2 = _tempTransfer;
            int.TryParse(textBoxY2.Text, out _tempTransfer);
            _mainWindow.Y2 = _tempTransfer;
            int.TryParse(textBoxR2.Text, out _tempTransfer);
            _mainWindow.R2 = _tempTransfer;
            _mainWindow.C2 = colourBox2.Color;

            int.TryParse(textBoxX3.Text, out _tempTransfer);
            _mainWindow.X3 = _tempTransfer;
            int.TryParse(textBoxY3.Text, out _tempTransfer);
            _mainWindow.Y3 = _tempTransfer;
            int.TryParse(textBoxR3.Text, out _tempTransfer);
            _mainWindow.R3 = _tempTransfer;
            _mainWindow.C3 = colourBox3.Color;

            int.TryParse(textBoxX4.Text, out _tempTransfer);
            _mainWindow.X4 = _tempTransfer;
            int.TryParse(textBoxY4.Text, out _tempTransfer);
            _mainWindow.Y4 = _tempTransfer;
            int.TryParse(textBoxR4.Text, out _tempTransfer);
            _mainWindow.R4 = _tempTransfer;
            _mainWindow.C4 = colourBox4.Color;

            _mainWindow.Background = colorComboBoxBgnd.Color;
            _mainWindow.Shape = comboBoxShape.SelectedIndex;
            int.TryParse(textBoxWindowX.Text, out _tempTransfer);
            _mainWindow.WX = _tempTransfer;
            int.TryParse(textBoxWindowY.Text, out _tempTransfer);
            _mainWindow.WY = _tempTransfer;
            _mainWindow.FirstRun = checkBoxFirstRun.Checked;
            _mainWindow.TaskbarTime = checkBoxTaskbarTime.Checked;

            _mainWindow.SetVals();
            _mainWindow.SaveFile(string.Empty);
            _mainWindow.MinimizeMemory();
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
            _tempTransfer = _mainWindow.X1;
            textBoxX1.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.Y1;
            textBoxY1.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.R1;
            textBoxR1.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            colourBox1.Color = _mainWindow.C1;

            _tempTransfer = _mainWindow.X2;
            textBoxX2.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.Y2;
            textBoxY2.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.R2;
            textBoxR2.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            colourBox2.Color = _mainWindow.C2;

            _tempTransfer = _mainWindow.X4;
            textBoxX3.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.Y3;
            textBoxY3.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.R3;
            textBoxR3.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            colourBox3.Color = _mainWindow.C3;

            _tempTransfer = _mainWindow.X4;
            textBoxX4.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.Y4;
            textBoxY4.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.R4;
            textBoxR4.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            colourBox4.Color = _mainWindow.C4;

            colorComboBoxBgnd.Color = _mainWindow.Background;
            comboBoxShape.SelectedIndex = _mainWindow.Shape;
            _tempTransfer = _mainWindow.WX;
            textBoxWindowX.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.WY;
            textBoxWindowY.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            checkBoxFirstRun.Checked = _mainWindow.FirstRun;
            checkBoxTaskbarTime.Checked = _mainWindow.TaskbarTime;

            _mainWindow.MinimizeMemory();
        }
    }
}