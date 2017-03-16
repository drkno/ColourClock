using System;
using System.Drawing;

namespace ColourClock
{
    class ColourClockBase
    {
        private Color[] _colors = new Color[]{Color.Red,Color.LawnGreen,Color.Yellow,Color.Blue};
        private readonly byte[] _lights = new byte[]{1,1,1,1};

        public ColourClockBase(Color[] colors)
        {
            SetColours(colors);

            var colValue = DateTime.Now.Minute;
            switch ((colValue / 15))
            {
                case 0: _lights[2] = 1; _lights[3] = (byte)(Math.Floor(colValue / 5.0) + 1); break;
                case 1: _lights[2] = 2; _lights[3] = (byte)(Math.Floor(colValue / 5.0) - 2); break;
                case 2: _lights[2] = 3; _lights[3] = (byte)(Math.Floor(colValue / 5.0) - 5); break;
                default: _lights[2] = 4; _lights[3] = (byte)(Math.Floor(colValue / 5.0) - 8); break;
            }

            colValue = DateTime.Now.Hour - ((DateTime.Now.Hour >= 12) ? 12 : 0);
            switch ((colValue / 3))
            {
                case 0: _lights[0] = 1; _lights[1] = (byte)(colValue + 1); break;
                case 1: _lights[0] = 2; _lights[1] = (byte)(colValue - 2); break;
                case 2: _lights[0] = 3; _lights[1] = (byte)(colValue - 5); break;
                default: _lights[0] = 4; _lights[1] = (byte)(colValue - 8); break;
            }
        }

        public void NextColour()
        {
            for (var i = 3; i >= 0; i--)
            {
                _lights[i] = (byte)((_lights[i] < ((i % 2 == 0) ? 4 : 3)) ? _lights[i] + 1 : 1);
                if (_lights[i] != 1) break;
            }
        }

        public Color GetColour(int index)
        {
            return _colors[_lights[index]];
        }

        public void SetColours(Color[] colors)
        {
            _colors = colors;
        }

        public override string ToString()
        {
            return ToString(false, true);
        }

        public string ToString(bool isTwentyFourHour, bool displayIndicator)
        {
            var min = ((_lights[2] - 1) * 3 + (_lights[3] - 1)) * 5;
            var hr = ((_lights[0] - 1) * 3 + (_lights[1] - 1)) + ((DateTime.Now.Hour >= 12 && isTwentyFourHour) ? 12 : 0);
            var time = hr.ToString("00") + ":" + min.ToString("00");
            if(displayIndicator)
            {
                time += ((DateTime.Now.Hour >= 12) ? " pm" : " am");
            }
            
            return time;
        }
    }
}
