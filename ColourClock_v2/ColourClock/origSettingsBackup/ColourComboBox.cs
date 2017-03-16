using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace ColourClock
{
    public class ColorComboBox : ComboBox
    {
        public ColorComboBox()
        {
            this.Items.Clear();
            foreach (Color color in typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.Public).Where(c => c.PropertyType == typeof(Color)).Select(c => (Color)c.GetValue(c, null)))
            {
                this.Items.Add(color);
            }

            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                Color color = (Color)this.Items[e.Index];

                int nextX = 0;

                e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
                DrawColor(e, color, ref nextX);
                e.Graphics.DrawString(color.Name, e.Font, new SolidBrush(e.ForeColor), new PointF(nextX, e.Bounds.Y + (e.Bounds.Height - e.Font.Height) / 2));
            }
            else
            {
                base.OnDrawItem(e);
            }
        }

        private void DrawColor(DrawItemEventArgs e, Color color, ref int nextX)
        {
            int width = e.Bounds.Height * 2 - 8;
            Rectangle rectangle = new Rectangle(e.Bounds.X + 3, e.Bounds.Y + 3, width, e.Bounds.Height - 6);
            e.Graphics.FillRectangle(new SolidBrush(color), rectangle);

            nextX = width + 8;
        }

        public Color Color
        {
            get
            {
                if (this.SelectedItem != null)
                    return (Color)this.SelectedItem;

                return Color.Black;
            }
            set
            {
                int ix = this.Items.IndexOf(value);
                if (ix >= 0)
                    this.SelectedIndex = ix;
            }
        }
    }
}
