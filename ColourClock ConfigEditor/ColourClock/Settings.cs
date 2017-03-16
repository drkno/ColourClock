using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ColourClock
{
    public partial class Settings : Form
    {
        private readonly ColourClock _mainWindow;
        private Color[] _col;
        private readonly Graphics _preview;

        // keep this variable to prevent runtime exceptions
        private int _tempTransfer;

        public Settings(ColourClock main)
        {
            InitializeComponent();
            _mainWindow = main;
            Populate(ref colourBox1);
            Populate(ref colourBox2);
            Populate(ref colourBox3);
            Populate(ref colourBox4);
            Populate(ref colorComboBoxBgnd);
            _preview = panelSettingsForeground.CreateGraphics();
            DisplaySettings();
            _mainWindow.MinimizeMemory();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Hide();
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
            int.TryParse(textBoxX1.Text, out _tempTransfer);
            _mainWindow.X1 = _tempTransfer;
            int.TryParse(textBoxY1.Text, out _tempTransfer);
            _mainWindow.Y1 = _tempTransfer;
            int.TryParse(textBoxR1.Text, out _tempTransfer);
            _mainWindow.R1 = _tempTransfer;
            _mainWindow.C1 = GetColour(ref colourBox1);

            int.TryParse(textBoxX2.Text, out _tempTransfer);
            _mainWindow.X2 = _tempTransfer;
            int.TryParse(textBoxY2.Text, out _tempTransfer);
            _mainWindow.Y2 = _tempTransfer;
            int.TryParse(textBoxR2.Text, out _tempTransfer);
            _mainWindow.R2 = _tempTransfer;
            _mainWindow.C2 = GetColour(ref colourBox2);

            int.TryParse(textBoxX3.Text, out _tempTransfer);
            _mainWindow.X3 = _tempTransfer;
            int.TryParse(textBoxY3.Text, out _tempTransfer);
            _mainWindow.Y3 = _tempTransfer;
            int.TryParse(textBoxR3.Text, out _tempTransfer);
            _mainWindow.R3 = _tempTransfer;
            _mainWindow.C3 = GetColour(ref colourBox3);

            int.TryParse(textBoxX4.Text, out _tempTransfer);
            _mainWindow.X4 = _tempTransfer;
            int.TryParse(textBoxY4.Text, out _tempTransfer);
            _mainWindow.Y4 = _tempTransfer;
            int.TryParse(textBoxR4.Text, out _tempTransfer);
            _mainWindow.R4 = _tempTransfer;
            _mainWindow.C4 = GetColour(ref colourBox4);

            _mainWindow.Background = GetColour(ref colorComboBoxBgnd);
            _mainWindow.Shape = comboBoxShape.SelectedIndex;
            int.TryParse(textBoxWindowX.Text, out _tempTransfer);
            _mainWindow.Wx = _tempTransfer;
            int.TryParse(textBoxWindowY.Text, out _tempTransfer);
            _mainWindow.WY = _tempTransfer;
            _mainWindow.FirstRun = checkBoxFirstRun.Checked;
            _mainWindow.TaskbarTime = checkBoxTaskbarTime.Checked;
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
            SetColour(ref colourBox1, _mainWindow.C1);

            _tempTransfer = _mainWindow.X2;
            textBoxX2.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.Y2;
            textBoxY2.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.R2;
            textBoxR2.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            SetColour(ref colourBox2, _mainWindow.C2);

            _tempTransfer = _mainWindow.X4;
            textBoxX3.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.Y3;
            textBoxY3.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.R3;
            textBoxR3.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            SetColour(ref colourBox3, _mainWindow.C3);

            _tempTransfer = _mainWindow.X4;
            textBoxX4.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.Y4;
            textBoxY4.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.R4;
            textBoxR4.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            SetColour(ref colourBox4, _mainWindow.C4);

            SetColour(ref colorComboBoxBgnd, _mainWindow.Background);
            comboBoxShape.SelectedIndex = _mainWindow.Shape;
            _tempTransfer = _mainWindow.Wx;
            textBoxWindowX.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            _tempTransfer = _mainWindow.WY;
            textBoxWindowY.Text = _tempTransfer.ToString(CultureInfo.InvariantCulture);
            checkBoxFirstRun.Checked = _mainWindow.FirstRun;
            checkBoxTaskbarTime.Checked = _mainWindow.TaskbarTime;

            DrawShape();
            _mainWindow.MinimizeMemory();
        }

        public void PushUpdates(int objectnum, Point xy)
        {
            switch (objectnum)
            {
                case 0:
                    textBoxX1.Text = xy.X.ToString(CultureInfo.InvariantCulture);
                    textBoxY1.Text = xy.Y.ToString(CultureInfo.InvariantCulture);
                    break;
                case 1:
                    textBoxX2.Text = xy.X.ToString(CultureInfo.InvariantCulture);
                    textBoxY2.Text = xy.Y.ToString(CultureInfo.InvariantCulture);
                    break;
                case 2:
                    textBoxX3.Text = xy.X.ToString(CultureInfo.InvariantCulture);
                    textBoxY3.Text = xy.Y.ToString(CultureInfo.InvariantCulture);
                    break;
                case 3:
                    textBoxX4.Text = xy.X.ToString(CultureInfo.InvariantCulture);
                    textBoxY4.Text = xy.Y.ToString(CultureInfo.InvariantCulture);
                    break;
                case 4:
                    textBoxWindowX.Text = xy.X.ToString(CultureInfo.InvariantCulture);
                    textBoxWindowY.Text = xy.Y.ToString(CultureInfo.InvariantCulture);
                    break;
                case 5:
                    textBoxR1.Text = xy.X.ToString(CultureInfo.InvariantCulture);
                    break;
                case 6:
                    textBoxR2.Text = xy.X.ToString(CultureInfo.InvariantCulture);
                    break;
                case 7:
                    textBoxR3.Text = xy.X.ToString(CultureInfo.InvariantCulture);
                    break;
                case 8:
                    textBoxR4.Text = xy.X.ToString(CultureInfo.InvariantCulture);
                    break;
            }
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void SettingsMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        public Color GetColour(ref ComboBox combo)
        {
            return combo.SelectedItem != null ? (Color) combo.SelectedItem : Color.Transparent;
        }

        public void SetColour(ref ComboBox combo, Color color)
        {
            if (combo.Items.IndexOf(color) < 0) return;
            combo.SelectedIndex = combo.Items.IndexOf(color);
        }

        public void Populate(ref ComboBox combo)
        {
            combo.Items.Clear();
            combo.DrawMode = DrawMode.OwnerDrawFixed;
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            if (_col == null || _col.Length == 0)
            {
                _col = typeof (Color).GetProperties(BindingFlags.Static | BindingFlags.Public).Where(
                    c => c.PropertyType == typeof (Color)).Select(
                        c => (Color) c.GetValue(c, null)).ToArray();
            }
            foreach (var t in _col.Where(t => t != Color.Transparent))
            {
                combo.Items.Add(t);
            }
        }

        private void ComboboxDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            var color = (Color) ((ComboBox) sender).Items[e.Index];
            e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
            e.Graphics.FillRectangle(new SolidBrush(color),
                                     new Rectangle(e.Bounds.X + 3, e.Bounds.Y + 3, e.Bounds.Height*2 - 8,
                                                   e.Bounds.Height - 6));
            e.Graphics.DrawString(color.Name, e.Font, new SolidBrush(e.ForeColor),
                                  new PointF(e.Bounds.Height*2, e.Bounds.Y + (e.Bounds.Height - e.Font.Height)/2));
        }

        private void DrawShape()
        {
            _preview.Clear(Color.Black);

            switch (comboBoxShape.SelectedIndex)
            {
                case 0: _preview.FillEllipse(new SolidBrush(GetColour(ref colourBox1)), 464, 29, 123, 123); break;
                case 1: _preview.FillRectangle(new SolidBrush(GetColour(ref colourBox1)), 464, 29, 123, 123); break;
                case 2: _preview.DrawEllipse(new Pen(GetColour(ref colourBox1), 2), 464, 29, 123, 123); break;
                case 3: _preview.DrawRectangle(new Pen(GetColour(ref colourBox1), 2), 464, 29, 123, 123); break;
            }
        }

        private void SettingsShown(object sender, EventArgs e)
        {
            DrawShape();
        }

        private void ButtonPreviewClick(object sender, EventArgs e)
        {
            PhraseSettings();
            _mainWindow.SetVals();
            _mainWindow.MinimizeMemory();
        }

        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            DrawShape();
        }
    }
}