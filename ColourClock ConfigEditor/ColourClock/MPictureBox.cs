using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColourClock
{
    public partial class MPictureBox : PictureBox
    {
        // Holds the mouse position relative to the inside of our control when the mouse button goes down.
        private Point _mCursorOffset;
        // Used by the MoveMove event handler to show that the setup to move the control has completed.
        private bool _mMoving;
        // Used to store the current cursor shape when we start to move the control.
        private Cursor _mCurrentCursor;
        // Used to specify if our control should stay with the visible bounds of our parent container.

        public MPictureBox()
        {
            MouseUp += MovableMouseUp;
            MouseMove += MovableMouseMove;
            MouseDown += MovableMouseDown;
            Paint += OnPaint;
            InitializeComponent();
        }

        private void OnPaint(object sender, PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.FillEllipse(new SolidBrush(Color.Blue), 0,0,Width,Height);
        }

        private void MovableMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _mCursorOffset = e.Location;
            _mCurrentCursor = base.Cursor;
            base.Cursor = Cursors.SizeAll;
            _mMoving = true;
        }

        private void MovableMouseMove(object sender, MouseEventArgs e)
        {
            if (!_mMoving) return;
            var clientPosition = Parent.PointToClient(Cursor.Position);
            var adjustedLocation = new Point(clientPosition.X - _mCursorOffset.X, clientPosition.Y - _mCursorOffset.Y);

            Location = adjustedLocation;
        }

        private void MovableMouseUp(object sender, MouseEventArgs e)
        {
            _mMoving = false;
            base.Cursor = _mCurrentCursor;
        }
    }
}
