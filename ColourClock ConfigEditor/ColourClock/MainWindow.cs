/*
 * COLOUR CLOCK CONFIG EDITOR
 * GUI Version by Matthew 1.0
 * Copyright Matthew Knox 2012. All rights reserved.
 * Last Edit: 19/09/12
*/

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ColourClock
{
    public partial class ColourClock : Form
    {
        // Settings Window
        private Settings _settings;
        public Color Background;
        public Color C1, C2, C3, C4;
        public bool FirstRun;
        public int R1, R2, R3, R4;
        public int Shape;
        public bool TaskbarTime;
        public int Wx, WY;

        // Settings Variables
        public int X1, X2, X3, X4, Y1, Y2, Y3, Y4;
        private bool _drag;

        // Display Variabes
        private bool _resize;
        private Point _xy;

        public ColourClock()
        {
            InitializeComponent();
            MinimizeMemory();
        }

        // Dll imports: window drag
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        // Dll imports: memory management
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize,
                                                            UIntPtr maximumWorkingSetSize);

        // Timer invoker

        // onload event
        private void ColourClockLoad(object sender, EventArgs e)
        {
            var agr = new Agreement();
            agr.ShowDialog();
            LoadConfig(string.Empty);
            _settings = new Settings(this);
            Bounds = new Rectangle(Location.X, Location.Y, Wx, WY);
            DisplayTime();
        }

        // onpaint event
        private void ColourClockPaint(object sender, PaintEventArgs e)
        {
            MinimizeMemory();
        }

        // drag event
        private void ColourClockMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }

        // exit button
        private void ButtonExitClick(object sender, EventArgs e)
        {
            Close();
        }

        // position saving
        private void ColourClockFormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to save changes before you quit?\n(Saves to the main config file)","Save",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveFile(string.Empty);
            }
            _settings.Dispose();
        }

        // value setter
        public void SetVals()
        {
            BackColor = Background;
            Refresh();
            DisplayTime();

            Width = Wx;
            Height = WY;

            MinimizeMemory();
        }

        // draw clock
        private void DisplayTime()
        {
            for (var i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            pictureBoxCol1.Bounds = new Rectangle(X1, Y1, R1, R1);
                            pictureBoxCol1.BackColor = C1;
                            break;
                        }
                    case 1:
                        {
                            pictureBoxCol2.Bounds = new Rectangle(X2, Y2, R2, R2);
                            pictureBoxCol2.BackColor = C2;
                            break;
                        }
                    case 2:
                        {
                            pictureBoxCol3.Bounds = new Rectangle(X3, Y3, R3, R3);
                            pictureBoxCol3.BackColor = C3;
                            break;
                        }
                    case 3:
                        {
                            pictureBoxCol4.Bounds = new Rectangle(X4, Y4, R4, R4);
                            pictureBoxCol4.BackColor = C4;
                            break;
                        }
                }
            }
            MinimizeMemory();
        }

        // about window
        private void ButtonSettingsClick(object sender, EventArgs e)
        {
            _settings.Show();
        }

        // force .Net memory cleanup
        public void MinimizeMemory()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr) 0xFFFFFFFF,
                                     (UIntPtr) 0xFFFFFFFF);
        }

        // import configuration
        public void LoadConfig(string filename)
        {
            if (filename == string.Empty)
            {
                filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                           @"\ColourClock\settings.ini";
                if (
                    File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                @"\ColourClock\settings.ini") == false)
                {
                    X1 = 160;
                    Y1 = 43;
                    R1 = 45;
                    C1 = Color.Red;

                    X2 = 160;
                    Y2 = 110;
                    R2 = 45;
                    C2 = Color.LawnGreen;

                    X3 = 229;
                    Y3 = 43;
                    R3 = 45;
                    C3 = Color.Yellow;

                    X4 = 229;
                    Y4 = 110;
                    R4 = 45;
                    C4 = Color.Blue;

                    Background = Color.Black;
                    Shape = 0;
                    Wx = 425;
                    WY = 210;
                    TaskbarTime = true;
                    FirstRun = true;
                    SetVals();
                    return;
                }
            }

            try
            {
                var inputStream = new StreamReader(filename);
                inputStream.ReadLine();
                inputStream.ReadLine();
                inputStream.ReadLine();
                inputStream.ReadLine();
                int temp;

                int.TryParse(inputStream.ReadLine(), out X1);
                int.TryParse(inputStream.ReadLine(), out Y1);
                int.TryParse(inputStream.ReadLine(), out R1);
                int.TryParse(inputStream.ReadLine(), out temp);
                C1 = GetKnownColor(temp);

                int.TryParse(inputStream.ReadLine(), out X2);
                int.TryParse(inputStream.ReadLine(), out Y2);
                int.TryParse(inputStream.ReadLine(), out R2);
                int.TryParse(inputStream.ReadLine(), out temp);
                C2 = GetKnownColor(temp);

                int.TryParse(inputStream.ReadLine(), out X3);
                int.TryParse(inputStream.ReadLine(), out Y3);
                int.TryParse(inputStream.ReadLine(), out R3);
                int.TryParse(inputStream.ReadLine(), out temp);
                C3 = GetKnownColor(temp);

                int.TryParse(inputStream.ReadLine(), out X4);
                int.TryParse(inputStream.ReadLine(), out Y4);
                int.TryParse(inputStream.ReadLine(), out R4);
                int.TryParse(inputStream.ReadLine(), out temp);
                C4 = GetKnownColor(temp);

                int.TryParse(inputStream.ReadLine(), out temp);
                Background = GetKnownColor(temp);
                int.TryParse(inputStream.ReadLine(), out Shape);
                int.TryParse(inputStream.ReadLine(), out Wx);
                int.TryParse(inputStream.ReadLine(), out WY);
                bool.TryParse(inputStream.ReadLine(), out TaskbarTime);
                bool.TryParse(inputStream.ReadLine(), out FirstRun);

                // window position
                var readLine = inputStream.ReadLine();
                if (readLine != null)
                {
                    var position = readLine.Split(',');
                    if (position.Length == 2 && int.TryParse(position[0], out temp) &&
                        int.TryParse(position[1], out temp))
                    {
                        Location = new Point(int.Parse(position[0]), temp);
                    }
                }

                inputStream.Close();
                SetVals();
                MinimizeMemory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Settings could not be loaded due to an error. Error is as follows:\n" + ex.Message,
                                "Colour Clock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // convert argb value to named colour
        public Color GetKnownColor(int value)
        {
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>().Where(color => Color.FromKnownColor(color).ToArgb() == value))
            {
                return Color.FromKnownColor(color);
            }
            return Color.Black;
        }

        // save file
        public void SaveFile(string filename)
        {
            try
            {
                if (
                    Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                     @"\ColourClock\") == false && filename == string.Empty)
                {
                    Directory.CreateDirectory(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ColourClock\");
                }
                if (filename == string.Empty)
                {
                    filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                               @"\ColourClock\settings.ini";
                }

                var saveSettings = new StreamWriter(filename);
                saveSettings.WriteLine(
                    "Colour Clock Configuration File.\r\nThis program comes with no warrantee and is entirely your own risk.\r\nAs there is little to NO error checking changing this file could be\r\na very stupid thing to do.");
                saveSettings.Write(X1 + "\r\n" + Y1 + "\r\n" + R1 + "\r\n" + C1.ToArgb() + "\r\n" + X2 + "\r\n" + Y2 +
                                   "\r\n" + R2 + "\r\n" + C2.ToArgb() + "\r\n" + X3 + "\r\n" + Y3 + "\r\n" + R3 + "\r\n" +
                                   C3.ToArgb() + "\r\n" + X4 + "\r\n" + Y4 + "\r\n" + R4 + "\r\n" + C4.ToArgb() + "\r\n" +
                                   Background.ToArgb() + "\r\n" + Shape + "\r\n" + Wx + "\r\n" + WY + "\r\n" +
                                   TaskbarTime + "\r\n" + FirstRun + "\r\n" + Location.X + "," + Location.Y);
                saveSettings.Close();
                MinimizeMemory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Settings could not be saved to file due to an error. Error is as follows:\n" + ex.Message,
                    "Colour Clock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int wmNcHitTest = 0x84;
            const int htBottomLeft = 16;
            const int htBottomRight = 17;
            if (m.Msg == wmNcHitTest)
            {
                var x = (int) (m.LParam.ToInt64() & 0xFFFF);
                var y = (int) ((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                var pt = PointToClient(new Point(x, y));
                var clientSize = ClientSize;
                if (pt.X >= clientSize.Width - 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr) (IsMirrored ? htBottomLeft : htBottomRight);
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void ColourClockResizeEnd(object sender, EventArgs e)
        {
            Wx = Width;
            WY = Height;
            _settings.PushUpdates(4, new Point(Width, Height));
        }


        private void MoveMouseDown(PictureBox sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (e.X <= 5 || e.X >= sender.Width - 5 || e.Y <= 5 || e.Y >= sender.Height - 5)
            {
                _resize = true;
            }
            else
            {
                _drag = true;
            }
            _xy = e.Location;
        }

        private void MoveMouseUp()
        {
            _drag = false;
            _resize = false;
        }

        private void MoveMouseMove(PictureBox sender, MouseEventArgs e)
        {
            if (e.X <= 5 || e.X >= sender.Width - 5 || e.Y <= 5 || e.Y >= sender.Height - 5)
            {
                Cursor = Cursors.SizeNWSE;
                if (_resize)
                {
                    if (e.X >= e.Y)
                    {
                        sender.Width = e.X;
                        sender.Height = e.X;
                    }
                    else
                    {
                        sender.Width = e.Y;
                        sender.Height = e.Y;
                    }

                    switch (sender.Name)
                    {
                        case "pictureBoxCol1":
                            _settings.PushUpdates(5, new Point(sender.Width, 0));
                            break;
                        case "pictureBoxCol2":
                            _settings.PushUpdates(6, new Point(sender.Width, 0));
                            break;
                        case "pictureBoxCol3":
                            _settings.PushUpdates(7, new Point(sender.Width, 0));
                            break;
                        case "pictureBoxCol4":
                            _settings.PushUpdates(8, new Point(sender.Width, 0));
                            break;
                    }
                }
            }
            else
            {
                Cursor = Cursors.SizeAll;
                if (_drag)
                {
                    sender.Left += e.X - _xy.X;
                    sender.Top += e.Y - _xy.Y;

                    switch (sender.Name)
                    {
                        case "pictureBoxCol1":
                            _settings.PushUpdates(0, sender.Location);
                            break;
                        case "pictureBoxCol2":
                            _settings.PushUpdates(1, sender.Location);
                            break;
                        case "pictureBoxCol3":
                            _settings.PushUpdates(2, sender.Location);
                            break;
                        case "pictureBoxCol4":
                            _settings.PushUpdates(3, sender.Location);
                            break;
                    }
                }
            }
        }

        private void MoveMouseLeave()
        {
            Cursor = Cursors.Arrow;
        }

        private void PictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            MoveMouseDown((PictureBox) sender, e);
        }

        private void PictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            MoveMouseMove((PictureBox) sender, e);
        }

        private void PictureBoxMouseUp(object sender, MouseEventArgs e)
        {
            MoveMouseUp();
        }

        private void PictureBoxMouseLeave(object sender, EventArgs e)
        {
            MoveMouseLeave();
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            SaveFile(string.Empty);
            MinimizeMemory();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style = 0x20000 | 0x80000;
                return cp;
            }
        }





        private void ButtonMouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.Yellow;
        }

        private void ButtonMouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.White;
        }

        private void ButtonMouseDown(object sender, MouseEventArgs e)
        {
            ((Label)sender).ForeColor = Color.Red;
        }

        private void ButtonMouseUp(object sender, MouseEventArgs e)
        {
            ((Label)sender).ForeColor = Color.Yellow;
        }
    }
}