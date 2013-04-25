using System.Drawing;
using System.Windows.Forms;

namespace Datatypes.Controls
{
    /// <summary>
    /// A simple resizable canvas providing basic drawing functions.
    /// </summary>
    public sealed class Canvas : Panel
    {

        #region "Properties"
        private Graphics _g;
        private const int CGripSize = 10;
        private bool _mDragging;
        private Point _mDragPos;
        #endregion

        /// <summary>
        /// Default constructor for canvas.
        /// </summary>
        public Canvas()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            _g = CreateGraphics();
        }

        /// <summary>
        /// Draws a point on the canvas.
        /// </summary>
        /// <param name="x">The x-coordinate to draw on.</param>
        /// <param name="y">The y-coordinate to draw on.</param>
        /// <param name="color">The color to draw the point.</param>
        public void DrawPoint(int x, int y, Color color)
        {
            var b = new SolidBrush(color);
            _g.FillRectangle(b, x, y, 1, 1);
        }

        /// <summary>
        /// Draws a cross through the canvas.
        /// </summary>
        public void DrawCross()
        {
            var n = new Point(Width / 2, 0);
            var e = new Point(Width, Height / 2);
            var s = new Point(Width / 2, Height);
            var w = new Point(0, Height / 2);
            _g.DrawLine(new Pen(new SolidBrush(Color.Black)), n, s);
            _g.DrawLine(new Pen(new SolidBrush(Color.Black)), w, e);
        }


        #region "resize related stuff"

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data. </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawSizeGrip(e.Graphics, BackColor,
              new Rectangle(ClientSize.Width - CGripSize, ClientSize.Height - CGripSize, CGripSize, CGripSize));

            _g = CreateGraphics();

            base.OnPaint(e);
        }

        private bool IsOnGrip(Point pos)
        {
            return pos.X >= ClientSize.Width - CGripSize &&
                   pos.Y >= ClientSize.Height - CGripSize;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _mDragging = IsOnGrip(e.Location);
            _mDragPos = e.Location;
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _mDragging = false;
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_mDragging)
            {
                Size = new Size(Width + e.X - _mDragPos.X,
                  Height + e.Y - _mDragPos.Y);
                _mDragPos = e.Location;
            }
            else if (IsOnGrip(e.Location)) Cursor = Cursors.SizeNWSE;
            else Cursor = Cursors.Default;
            base.OnMouseMove(e);
        }
        #endregion
    }
}
