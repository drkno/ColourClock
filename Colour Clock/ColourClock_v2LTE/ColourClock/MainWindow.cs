/*
 * COLOUR CLOCK LTE
 * GUI Version by Matthew 2.0.2 Alpha
 * Copyright Matthew Knox 2012. All rights reserved.
 * Last Edit: 30/09/12
*/

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace ColourClock
{
    public partial class ColourClock : Form
    {
        private readonly int[] _lights = new int[4];
        private readonly Graphics _paper;
        public Color Background;
        public Color[] Colours = new Color[4];
        public bool FirstRun;
        public bool TaskbarTime;
        public int Shape;
        public int[] Rad = new int[4];
        public Point WindSize;
        public Point[] Xy = new Point[4];
        // ReSharper disable NotAccessedField.Local
        private Timer _timed;
        // ReSharper restore NotAccessedField.Local

        public ColourClock()
        {
            InitializeComponent();
            _paper = CreateGraphics();
            LoadConfig(string.Empty);
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

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);

        private void ColourClockLoad(object sender, EventArgs e)
        {
            var colValue = DateTime.Now.Minute;
            switch ((colValue / 15))
            {
                case 0: _lights[2] = 1; _lights[3] = (int)Math.Floor(colValue / 5.0) + 1; break;
                case 1: _lights[2] = 2; _lights[3] = (int)Math.Floor(colValue / 5.0) - 2; break;
                case 2: _lights[2] = 3; _lights[3] = (int)Math.Floor(colValue / 5.0) - 5; break;
                default: _lights[2] = 4; _lights[3] = (int)Math.Floor(colValue / 5.0) - 8; break;
            }

            colValue = DateTime.Now.Hour - ((DateTime.Now.Hour >= 12) ? 12 : 0);
            switch ((colValue / 3))
            {
                case 0: _lights[0] = 1; _lights[1] = colValue + 1; break;
                case 1: _lights[0] = 2; _lights[1] = colValue - 2; break;
                case 2: _lights[0] = 3; _lights[1] = colValue - 5; break;
                default: _lights[0] = 4; _lights[1] = colValue - 8; break;
            }

            SetWindowTitle();
            TimerFunction();
            MinimizeMemory();
        }

        private void ColourClockPaint(object sender, PaintEventArgs e)
        {
            DisplayTime();
            MinimizeMemory();
        }

        private void ColourClockMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void ButtonMinimiseClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            MinimizeMemory();
        }

        public void SetVals()
        {
            BackColor = Background;
            Refresh();
            DisplayTime();
            Width = WindSize.X;
            Height = WindSize.Y;

            if (FirstRun)
            {
                FirstRun = false;
                if (
                    MessageBox.Show(
                        "Colour Clock comes with no warantee and it is entirely\nyour own fault if it does something bad to your computer.\nDo you agree to these terms?",
                        "Colour Clock", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    Environment.Exit(0);
                }
                MinimizeMemory();
            }
            MinimizeMemory();
        }

        private void DisplayTime()
        {
            for (var i = 0; i < 4; i++)
            {
                var col = Color.Black;
                switch (_lights[i])
                {
                    case 1:
                        col = Colours[0];
                        break;
                    case 2:
                        col = Colours[1];
                        break;
                    case 3:
                        col = Colours[2];
                        break;
                    case 4:
                        col = Colours[3];
                        break;
                }
                switch (Shape)
                {
                    case 0:
                        _paper.FillEllipse(new SolidBrush(col), Xy[i].X, Xy[i].Y, Rad[i], Rad[i]);
                        break;
                    case 1:
                        _paper.FillRectangle(new SolidBrush(col), Xy[i].X, Xy[i].Y, Rad[i], Rad[i]);
                        break;
                    case 2:
                        _paper.DrawEllipse(new Pen(col, 3), Xy[i].X, Xy[i].Y, Rad[i], Rad[i]);
                        break;
                    case 3:
                        _paper.DrawRectangle(new Pen(col, 3), Xy[i].X, Xy[i].Y, Rad[i], Rad[i]);
                        break;
                }
            }
            MinimizeMemory();
        }

        private void SetWindowTitle()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Invoker(SetWindowTitle));
                return;
            }

            var time = "Colour Clock";
            if (TaskbarTime)
            {
                var min = ((_lights[2] - 1) * 3 + (_lights[3] - 1)) * 5;
                var hr = ((_lights[0] - 1) * 3 + (_lights[1] - 1));
                time = ((DateTime.Now.Hour >= 12) ? hr + 12 : hr).ToString("00") + ":" + min.ToString("00") + ((DateTime.Now.Hour >= 12) ? " pm" : " am") + " - " + time;
            }
            Text = time;
            Refresh();
            MinimizeMemory();
        }

        private void TimerFunction()
        {
            var difference = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                             DateTime.Now.Minute, 0, 0).AddMinutes(((5 - (DateTime.Now.Minute % 5)) == 0) ? 5 : (5 - (DateTime.Now.Minute % 5))).Subtract(DateTime.Now);
            if (difference < TimeSpan.FromSeconds(1))
            {
                difference = TimeSpan.Zero;
            }
            _timed = new Timer(Increment, null, difference, TimeSpan.FromMinutes(5.0));
            //* TESTING VERSION:*/_timed = new Timer(Increment, null, TimeSpan.Zero, TimeSpan.FromSeconds(1.0));
        }

        private void Increment(object sender)
        {
            for (var i = 3; i >= 0; i--)
            {
                _lights[i] = (_lights[i] < ((i % 2 == 0) ? 4 : 3)) ? _lights[i] + 1 : 1;
                if (_lights[i] != 1) break;
            }

            SetWindowTitle();
            MinimizeMemory();
        }

        public void MinimizeMemory()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
        }

        public void LoadConfig(string filename)
        {
            if (filename == string.Empty)
            {
                filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ColourClock\settings.ini";
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ColourClock\settings.ini"))
                {
                    Xy[0].X = Xy[1].X = 160;
                    Xy[0].Y = Xy[2].Y = 43;
                    Xy[1].Y = Xy[3].Y = 110;
                    Xy[2].X = Xy[3].X = 229;
                    Rad[0] = Rad[1] = Rad[2] = Rad[3] = 45;

                    Colours[0] = Color.Red;
                    Colours[1] = Color.LawnGreen;
                    Colours[2] = Color.Yellow;
                    Colours[3] = Color.Blue;
                    Background = Color.Black;

                    Shape = 0;
                    WindSize.X = 425;
                    WindSize.Y = 210;
                    TaskbarTime = FirstRun = true;
                    SetVals();
                    return;
                }
            }

            try
            {
                var inputStream = new StreamReader(filename);
                inputStream.ReadLine(); inputStream.ReadLine(); inputStream.ReadLine(); inputStream.ReadLine();

                int temp;
                for (var i = 0; i < 4; i++)
                {
                    int.TryParse(inputStream.ReadLine(), out temp);
                    Xy[i].X = temp;
                    int.TryParse(inputStream.ReadLine(), out temp);
                    Xy[i].Y = temp;
                    int.TryParse(inputStream.ReadLine(), out temp);
                    Rad[i] = temp;
                    int.TryParse(inputStream.ReadLine(), out temp);
                    Colours[i] = Color.FromArgb(temp);
                }

                int.TryParse(inputStream.ReadLine(), out temp);
                Background = Color.FromArgb(temp);
                int.TryParse(inputStream.ReadLine(), out Shape);
                int.TryParse(inputStream.ReadLine(), out temp);
                WindSize.X = temp;
                int.TryParse(inputStream.ReadLine(), out temp);
                WindSize.Y = temp;
                bool.TryParse(inputStream.ReadLine(), out TaskbarTime);
                bool.TryParse(inputStream.ReadLine(), out FirstRun);

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

        #region Nested type: Invoker
        private delegate void Invoker();
        #endregion

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