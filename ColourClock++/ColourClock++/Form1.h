#pragma once

namespace ColourClock {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	// Settings Variables
public: int X1, X2, X3, X4, Y1, Y2, Y3, Y4, R1, R2, R3, R4, Shape;
public: System::Color C1, C2, C3, C4;
public: bool TaskbarTime, FirstRun;

        // Display Variabes
public: int[] lights = new int[4];
public: System::Graphics paper;

        // Dll imports: window drag
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
public: static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
public: static extern bool ReleaseCapture();

        // Dll imports: memory management
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);

        // Timer invoker
        delegate void Invoker();

        // Timer : Keep global or else GC will dispose
        System.Threading.Timer timed;

	/// <summary>
	/// Summary for Form1
	/// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->SuspendLayout();
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(632, 335);
			this->Name = L"Form1";
			this->Text = L"Form1";
			this->Load += gcnew System::EventHandler(this, &Form1::Form1_Load);
			this->Paint += gcnew System::Windows::Forms::PaintEventHandler(this, &Form1::Form1_Paint);
			this->MouseDown += gcnew System::Windows::Forms::MouseEventHandler(this, &Form1::Form1_MouseDown);
			this->ResumeLayout(false);

		}
#pragma endregion

	private: System::Void Form1_Load(System::Object^  sender, System::EventArgs^  e) {
			 }
	private: System::Void Form1_Paint(System::Object^  sender, System::Windows::Forms::PaintEventArgs^  e) {
			 }
	private: System::Void Form1_MouseDown(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e) {
			 }


	// value setter
	public: System::Void SetVals()
        {
            this.Refresh();
            DisplayTime();
            minimizeMemory();
        }

        // draw clock
	private: System::Void DisplayTime()
        {
            int x = 0, y = 0, rad = 0; Color col = Color.Black;
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            x = X1;
                            y = Y1;
                            rad = R1;
                            break;
                        }
                    case 1:
                        {
                            x = X2;
                            y = Y2;
                            rad = R2;
                            break;
                        }
                    case 2:
                        {
                            x = X3;
                            y = Y3;
                            rad = R3;
                            break;
                        }
                    case 3:
                        {
                            x = X4;
                            y = Y4;
                            rad = R4;
                            break;
                        }
                }

                switch (lights[i])
                {
                    case 1: col = C1; break;
                    case 2: col = C2; break;
                    case 3: col = C3; break;
                    case 4: col = C4; break;
                }

                switch (Shape)
                {
                    case 0: paper.FillEllipse(new SolidBrush(col), x, y, rad, rad); break;
                    case 1: paper.FillRectangle(new SolidBrush(col), x, y, rad, rad); break;
                    case 2: paper.DrawEllipse(new Pen(col, 3), x, y, rad, rad); break;
                    case 3: paper.DrawRectangle(new Pen(col, 3), x, y, rad, rad); break;
                }
            }
            minimizeMemory();
        }

        // set window title and refresh window content
		private: System::Void SetWindowTitle()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Invoker(SetWindowTitle));
                return;
            }

            this.Refresh();

            string time = "Colour Clock";
            if (TaskbarTime == true)
            {
                int min = ((lights[2] - 1) * 3 + (lights[3] - 1)) * 5;
                int hr = ((lights[0] - 1) * 3 + (lights[1] - 1));
                time = " - " + time;
                if (DateTime.Now.Hour >= 12)
                {
                    hr += 12;
                    time = " pm" + time;
                }
                else
                {
                    time = " am" + time;
                }
                if (min < 10)
                {
                    time = hr.ToString() + ":0" + min.ToString() + time;
                }
                else
                {
                    time = hr.ToString() + ":" + min.ToString() + time;
                }
            }
            this.Text = time;
            minimizeMemory();
        }

        // timer function
		private: System::Void TimerFunction()
        {
            int add = 5 - (DateTime.Now.Minute % 5);
            if (add == 0) { add = 5; }
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0, 0).AddMinutes(add);
            TimeSpan difference = date.Subtract(DateTime.Now);
            if (difference < TimeSpan.FromSeconds(1))
            {
                difference = TimeSpan.Zero;
            }
            timed = new System.Threading.Timer(Increment, null, difference, TimeSpan.FromMinutes(5.0));
            // /* TESTING VERSION:*/ timed = new System.Threading.Timer(Increment, null, TimeSpan.Zero, TimeSpan.FromSeconds(1.0));
        }

        // Increases the time by one colour
		private: System::Void Increment(System::Object^ sender)
        {
            if (lights[3] < 3)
            {
                lights[3]++;
            }
            else
            {
                lights[3] = 1;
                if (lights[2] < 4)
                {
                    lights[2]++;
                }
                else
                {
                    lights[2] = 1;
                    if (lights[1] < 3)
                    {
                        lights[1]++;
                    }
                    else
                    {
                        lights[1] = 1;
                        if (lights[0] < 4)
                        {
                            lights[0]++;
                        }
                        else
                        {
                            lights[0] = 1;
                            lights[1] = 1;
                            lights[2] = 1;
                            lights[3] = 1;
                        }
                    }
                }
            }

            SetWindowTitle();
            minimizeMemory();
        }

        // force .Net memory cleanup
		public: System::Void minimizeMemory()
        {
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
        }

        // import configuration
		public: System::Void loadConfig(System::String^ filename)
        {
            if (filename == string.Empty)
            {
                filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ColourClock\settings.ini";
                if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ColourClock\settings.ini") == false)
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

                    this.BackColor = Color.Black;
                    Shape = 0;
                    TaskbarTime = true;
                    FirstRun = true;
                    SetVals();
                    return;
                }
            }

            try
            {
                System.IO.StreamReader inputStream = new System.IO.StreamReader(filename);
                inputStream.ReadLine();
                inputStream.ReadLine();
                inputStream.ReadLine();
                inputStream.ReadLine();
                int temp = 0;

                int.TryParse(inputStream.ReadLine(), out X1);
                int.TryParse(inputStream.ReadLine(), out Y1);
                int.TryParse(inputStream.ReadLine(), out R1);
                int.TryParse(inputStream.ReadLine(), out temp); C1 = Color.FromName(GetKnownColor(temp));

                int.TryParse(inputStream.ReadLine(), out X2);
                int.TryParse(inputStream.ReadLine(), out Y2);
                int.TryParse(inputStream.ReadLine(), out R2);
                int.TryParse(inputStream.ReadLine(), out temp); C2 = Color.FromName(GetKnownColor(temp));

                int.TryParse(inputStream.ReadLine(), out X3);
                int.TryParse(inputStream.ReadLine(), out Y3);
                int.TryParse(inputStream.ReadLine(), out R3);
                int.TryParse(inputStream.ReadLine(), out temp); C3 = Color.FromName(GetKnownColor(temp));

                int.TryParse(inputStream.ReadLine(), out X4);
                int.TryParse(inputStream.ReadLine(), out Y4);
                int.TryParse(inputStream.ReadLine(), out R4);
                int.TryParse(inputStream.ReadLine(), out temp); C4 = Color.FromName(GetKnownColor(temp));

                int.TryParse(inputStream.ReadLine(), out temp); this.BackColor = Color.FromName(GetKnownColor(temp));
                int.TryParse(inputStream.ReadLine(), out Shape);
                int.TryParse(inputStream.ReadLine(), out temp);
                this.Width = temp;
                int.TryParse(inputStream.ReadLine(), out temp);
                this.Height = temp;
                bool.TryParse(inputStream.ReadLine(), out TaskbarTime);
                bool.TryParse(inputStream.ReadLine(), out FirstRun);

                // window position
                string[] position = inputStream.ReadLine().Split(',');
                if (position.Length == 2 && int.TryParse(position[0], out temp) == true && int.TryParse(position[1], out temp) == true)
                {
                    this.Location = new Point(int.Parse(position[0]), temp);
                }

                inputStream.Close();
                SetVals();
                minimizeMemory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Settings could not be loaded due to an error. Error is as follows:\n" + ex.Message, "Colour Clock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        // convert argb value to named colour
		public: string GetKnownColor(int value)
        {
            Color someColor;
            Array aListofKnownColors = Enum.GetValues(typeof(KnownColor));
            foreach (KnownColor eKnownColor in aListofKnownColors)
            {
                someColor = Color.FromKnownColor(eKnownColor);
                if (value == someColor.ToArgb() && !someColor.IsSystemColor)
                {
                    return someColor->Name;
                }
            }
            return "Black";
        }


	};
}

