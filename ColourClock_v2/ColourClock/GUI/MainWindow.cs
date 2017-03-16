/*
 * COLOUR CLOCK
 * GUI Version by Matthew 2.0.3.2 Alpha
 * Copyright Matthew Knox 2013. All rights reserved.
 * Last Edit: 12/10/13
*/

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ColourClock.Clock;

namespace ColourClock.GUI
{
    public partial class ColourClock : Form
    {
        public ColourClockBase ClockBase;
        public bool FirstRun, TaskbarTime;
        public int Shape;
        public int[] Rad = new int[4];
        public Point WindSize;
        public Point[] Xy = new Point[4];

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style = 0x20000 | 0x80000;
                return cp;
            }
        }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);
            if (message.Msg == 0x84 && (int)message.Result == 0x1) message.Result = (IntPtr)0x2;
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);

        public ColourClock()
        {
            InitializeComponent();
            #if DEBUG // Debug Timer
            MessageBox.Show("THIS IS A DEBUG VERSION\nIt should not be distributed or used as a final version.","Debug",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            #endif
            LoadConfig(string.Empty);
        }

        private void ColourClockLoad(object sender, EventArgs e)
        {
            ClockBase.ClockDidProgress += ClockDidProgress;
            ClockBase.StartCallbacks();
            SetWindowTitle();
            MinimizeMemory();
        }

        private void ClockDidProgress(){ClockDidProgress(null,null);}
        private void ClockDidProgress(object sender, EventArgs eventArgs)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Invoker(ClockDidProgress));
                return;
            }
            SetWindowTitle();
            Refresh();
        }

        private void ColourClockPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            for (var i = 0; i < 4; i++)
            {
                var brush = new SolidBrush(ClockBase.GetColour(i));
                switch (Shape)
                {
                    case 0:
                        e.Graphics.FillEllipse(brush, Xy[i].X, Xy[i].Y, Rad[i], Rad[i]);
                        break;
                    case 1:
                        e.Graphics.FillRectangle(brush, Xy[i].X, Xy[i].Y, Rad[i], Rad[i]);
                        break;
                    case 2:
                        e.Graphics.DrawEllipse(new Pen(brush, 3), Xy[i].X, Xy[i].Y, Rad[i], Rad[i]);
                        break;
                    case 3:
                        e.Graphics.DrawRectangle(new Pen(brush, 3), Xy[i].X, Xy[i].Y, Rad[i], Rad[i]);
                        break;
                }
                brush.Dispose();
            }
            MinimizeMemory();
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {
            Console.WriteLine("ButtonExitClick");
            SaveFile(string.Empty);
            Console.WriteLine("ButtonExitClickComplete");
            Environment.Exit(0);
        }

        private void ButtonMinimiseClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            MinimizeMemory();
        }

        public void SetVals()
        {
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
                SaveFile(string.Empty);
                MinimizeMemory();
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
                time = ClockBase.ToString(true,true) + " - " + time;
            }
            Text = time;
        }

        private void ButtonAboutClick(object sender, EventArgs e)
        {
            var settings = new Settings(this);
            settings.ShowDialog();
            MinimizeMemory();
        }

        public void MinimizeMemory()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr) 0xFFFFFFFF, (UIntPtr) 0xFFFFFFFF);
        }

        public void LoadConfig(string filename)
        {
            var colours = new Color[4];
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
                    
                    colours[0] = Color.Red;
                    colours[1] = Color.LawnGreen;
                    colours[2] = Color.Yellow;
                    colours[3] = Color.Blue;
                    BackColor = Color.Black;

                    Shape = 0;
                    WindSize.X = 425;
                    WindSize.Y = 210;
                    TaskbarTime = FirstRun = true;
                    ClockBase = new ColourClockBase(colours);
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
                    colours[i] = Color.FromArgb(temp);
                }

                int.TryParse(inputStream.ReadLine(), out temp);
                BackColor = Color.FromArgb(temp);
                int.TryParse(inputStream.ReadLine(), out Shape);
                int.TryParse(inputStream.ReadLine(), out temp);
                WindSize.X = temp;
                int.TryParse(inputStream.ReadLine(), out temp);
                WindSize.Y = temp;
                bool.TryParse(inputStream.ReadLine(), out TaskbarTime);
                bool.TryParse(inputStream.ReadLine(), out FirstRun);

                var readLine = inputStream.ReadLine();
                if (!string.IsNullOrEmpty(readLine))
                {
                    var position = readLine.Split(',');
                    if (position.Length == 2 && int.TryParse(position[0], out temp) &&
                        int.TryParse(position[1], out temp))
                    {
                        WindowState = FormWindowState.Normal;
                        Location = new Point(int.Parse(position[0]), temp);
                    }
                }

                inputStream.Close();
                ClockBase = new ColourClockBase(colours);
                SetVals();
                MinimizeMemory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Settings could not be loaded due to an error. Error is as follows:\n" + ex.Message,
                                "Colour Clock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void SaveFile(string filename)
        {
            try
            {
                #if DEBUG
                Console.WriteLine("SaveFile Called");
                #endif

                if (filename == string.Empty)
                {
                    filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ColourClock\settings.ini";
                    if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ColourClock\"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ColourClock\");
                    }
                }
                #if DEBUG
                Console.WriteLine("FilePath = " + filename);
                #endif

                var saveSettings = new StreamWriter(filename, false);
                saveSettings.WriteLine("Colour Clock Configuration File.\r\nIt is advisable not to change this file.\r\n##########################\r\n");
                for (var i = 0; i < 4; i++)
                {
                    saveSettings.Write(Xy[i].X + "\r\n" + Xy[i].Y + "\r\n" + Rad[i] + "\r\n" + ClockBase.GetColour(i).ToArgb() + "\r\n");
                }
                saveSettings.Write(BackColor.ToArgb() + "\r\n" + Shape + "\r\n" + WindSize.X + "\r\n" + WindSize.Y + "\r\n" +
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