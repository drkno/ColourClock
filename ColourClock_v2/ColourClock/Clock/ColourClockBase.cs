#region

using System;
using System.Drawing;
using System.Threading;

#endregion

namespace ColourClock.Clock
{
    public class ColourClockBase : IDisposable
    {
        private readonly byte[] _lights = {1, 1, 1, 1};
        public EventHandler ClockDidProgress;
        private Color[] _colors = {Color.Red, Color.LawnGreen, Color.Yellow, Color.Blue};
        private Timer _timed;

        public ColourClockBase()
        {
            SetTimeToNow();
        }

        public ColourClockBase(Color[] colors)
        {
            SetColours(colors);
            SetTimeToNow();
        }

        public void Dispose()
        {
            _timed.Dispose();
        }

        public void SetTimeToNow()
        {
            var colValue = DateTime.Now.Minute;
            switch ((colValue/15))
            {
                case 0:
                    _lights[2] = 1;
                    _lights[3] = (byte) (Math.Floor(colValue/5.0) + 1);
                    break;
                case 1:
                    _lights[2] = 2;
                    _lights[3] = (byte) (Math.Floor(colValue/5.0) - 2);
                    break;
                case 2:
                    _lights[2] = 3;
                    _lights[3] = (byte) (Math.Floor(colValue/5.0) - 5);
                    break;
                default:
                    _lights[2] = 4;
                    _lights[3] = (byte) (Math.Floor(colValue/5.0) - 8);
                    break;
            }

            colValue = DateTime.Now.Hour - ((DateTime.Now.Hour >= 12) ? 12 : 0);
            switch ((colValue/3))
            {
                case 0:
                    _lights[0] = 1;
                    _lights[1] = (byte) (colValue + 1);
                    break;
                case 1:
                    _lights[0] = 2;
                    _lights[1] = (byte) (colValue - 2);
                    break;
                case 2:
                    _lights[0] = 3;
                    _lights[1] = (byte) (colValue - 5);
                    break;
                default:
                    _lights[0] = 4;
                    _lights[1] = (byte) (colValue - 8);
                    break;
            }
        }

        public void NextColour()
        {
            for (var i = 3; i >= 0; i--)
            {
                _lights[i] = (byte) ((_lights[i] < ((i%2 == 0) ? 4 : 3)) ? _lights[i] + 1 : 1);
                if (_lights[i] != 1) break;
            }
        }

        public Color GetColour(int index)
        {
            return _colors[_lights[index] - 1];
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
            var min = ((_lights[2] - 1)*3 + (_lights[3] - 1))*5;
            var hr = ((_lights[0] - 1)*3 + (_lights[1] - 1)) + ((DateTime.Now.Hour >= 12 && isTwentyFourHour) ? 12 : 0);
            var time = hr.ToString("00") + ":" + min.ToString("00");
            if (displayIndicator)
            {
                time += ((DateTime.Now.Hour >= 12) ? " pm" : " am");
            }

            return time;
        }

        public void StartCallbacks()
        {
            #if DEBUG // Debug Timer
            _timed = new Timer(IncrementCallback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1.0));
            return;
            #endif

            if (ClockDidProgress == null) throw new NullReferenceException("ClockDidProgress hasn't been set! Without this the clock won't progress.");
            var difference = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                DateTime.Now.Minute, 0, 0).AddMinutes(((5 - (DateTime.Now.Minute%5)) == 0)
                    ? 5
                    : (5 - (DateTime.Now.Minute%5))).Subtract(DateTime.Now);
            if (difference < TimeSpan.FromSeconds(1))
            {
                difference = TimeSpan.Zero;
            }
            _timed = new Timer(IncrementCallback, null, difference, TimeSpan.FromMinutes(5.0));
        }

        private void IncrementCallback(object sender)
        {
            NextColour();
            ClockDidProgress(sender, null);
        }
    }
}