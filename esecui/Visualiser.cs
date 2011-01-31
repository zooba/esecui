using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;

namespace esecui
{
    public partial class Visualiser : UserControl
    {
        public Visualiser()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            InitializeComponent();
        }

        #region Begin/End Update

        private int _AllowUpdate = 0;
        [Browsable(false)]
        public bool AllowUpdate { get { return _AllowUpdate == 0; } }

        private bool DeferAutoRange = false;
        private bool DeferInvalidate = false;

        /// <summary>
        /// Suppresses updates. Multiple calls are nested and an equal number
        /// of calls to <see cref="EndUpdate"/> are required.
        /// </summary>
        public void BeginUpdate()
        {
            _AllowUpdate += 1;
        }

        /// <summary>
        /// Allows updates. Multiple calls are nested and an equal number of
        /// calls to <see cref="BeginUpdate"/> are required.
        /// </summary>
        public void EndUpdate()
        {
            if ((--_AllowUpdate) == 0)
            {
                if (DeferAutoRange) PerformAutoRange(!DeferInvalidate);
                else if (DeferInvalidate) Refresh();

                DeferAutoRange = false;
                DeferInvalidate = false;
            }
        }

        public override void Refresh()
        {
            if (!AllowUpdate)
            {
                DeferInvalidate = true;
                return;
            }

            base.Refresh();
        }

        #endregion

        #region Range Properties and Methods

        const bool DefaultAutoRange = false;
        const AutoSizeMode DefaultAutoRangeMode = AutoSizeMode.GrowOnly;
        const double DefaultHorizontalOffset = 0.0;
        const double DefaultHorizontalRange = 100.0;
        const double DefaultMinimumHorizontalRange = 0.0;
        const double DefaultVerticalOffset = 0.0;
        const double DefaultVerticalRange = 100.0;
        const double DefaultMinimumVerticalRange = 0.0;

        private bool _AutoRange = DefaultAutoRange;
        [Browsable(true), DefaultValue(DefaultAutoRange)]
        [Description("Automatically adjust the range to contain all elements.")]
        public bool AutoRange
        {
            get { return _AutoRange; }
            set
            {
                _AutoRange = value;
                if (value) PerformAutoRange();
            }
        }

        private AutoSizeMode _AutoRangeMode = DefaultAutoRangeMode;
        [Browsable(true), DefaultValue(DefaultAutoRangeMode)]
        [Description("Specify whether auto-ranging can shrink the range as well as expand it.")]
        public AutoSizeMode AutoRangeMode
        {
            get { return _AutoRangeMode; }
            set
            {
                _AutoRangeMode = value;
                if (_AutoRange && value == System.Windows.Forms.AutoSizeMode.GrowAndShrink)
                {
                    PerformAutoRange();
                }
            }
        }

        private double _HorizontalOffset = DefaultHorizontalOffset;
        [Browsable(true), DefaultValue(DefaultHorizontalOffset)]
        [Description("The value to map to the left-hand side of the control.")]
        public double HorizontalOffset
        {
            get { return _HorizontalOffset; }
            set
            {
                _HorizontalOffset = value;
                Refresh();
            }
        }

        private double _HorizontalRange = DefaultHorizontalRange;
        [Browsable(true), DefaultValue(DefaultHorizontalRange)]
        [Description("The value greater than HorizontalOffset to map to the right-hand side of the control.")]
        public double HorizontalRange
        {
            get { return _HorizontalRange; }
            set
            {
                _HorizontalRange = value;
                Refresh();
            }
        }

        private double _MinimumHorizontalRange = DefaultMinimumHorizontalRange;
        [Browsable(true), DefaultValue(DefaultMinimumHorizontalRange)]
        [Description("The minimum value to allow HorizontalRange to take.")]
        public double MinimumHorizontalRange
        {
            get { return _MinimumHorizontalRange; }
            set
            {
                _MinimumHorizontalRange = value;
                if (HorizontalRange < value) HorizontalRange = value;
            }
        }

        private double _VerticalOffset = DefaultVerticalOffset;
        [Browsable(true), DefaultValue(DefaultVerticalOffset)]
        [Description("The value to map to the top of the control.")]
        public double VerticalOffset
        {
            get { return _VerticalOffset; }
            set
            {
                _VerticalOffset = value;
                Refresh();
            }
        }

        private double _VerticalRange = DefaultVerticalRange;
        [Browsable(true), DefaultValue(DefaultVerticalRange)]
        [Description("The value greater than VerticalOffset to map to the bottom of the control.")]
        public double VerticalRange
        {
            get { return _VerticalRange; }
            set
            {
                _VerticalRange = value;
                Refresh();
            }
        }

        private double _MinimumVerticalRange = DefaultMinimumVerticalRange;
        [Browsable(true), DefaultValue(DefaultMinimumVerticalRange)]
        [Description("The minimum value to allow VerticalRange to take.")]
        public double MinimumVerticalRange
        {
            get { return _MinimumVerticalRange; }
            set
            {
                _MinimumVerticalRange = value;
                if (VerticalRange < value) VerticalRange = value;
            }
        }


        /// <summary>
        /// Determines the range based on the current set of points. The value
        /// of <see cref="AutoRangeMode"/> is used; the value of
        /// <see cref="AutoRange"/> is ignored.
        /// </summary>
        /// <param name="suppressRefresh">If <b>false</b>, redraws the
        /// control after adjusting the range.</param>
        /// <remarks>If <see cref="AllowUpdate"/> is <b>false</b>, this method
        /// is deferred until updates are enabled.</remarks>
        public void PerformAutoRange(bool suppressRefresh = false)
        {
            if (!AllowUpdate)
            {
                DeferAutoRange = true;
                DeferInvalidate = !suppressRefresh;
                return;
            }

            if (_Points == null || _Points.Count == 0)
            {
                return;
            }

            double horizontalMin, horizontalMax, verticalMin, verticalMax;

            horizontalMin = horizontalMax = _Points[0].X;
            verticalMin = verticalMax = _Points[0].Y;

            foreach (var pt in _Points.Skip(1))
            {
                horizontalMin = (pt.X < horizontalMin) ? pt.X : horizontalMin;
                horizontalMax = (pt.X > horizontalMax) ? pt.X : horizontalMax;
                verticalMin = (pt.Y < verticalMin) ? pt.Y : verticalMin;
                verticalMax = (pt.Y > verticalMax) ? pt.Y : verticalMax;
            }

            bool adjusted = false;
            if (AutoRangeMode == System.Windows.Forms.AutoSizeMode.GrowOnly)
            {
                double origHorizontalMin = _HorizontalOffset;
                double origVerticalMin = _VerticalOffset;
                double origHorizontalMax = _HorizontalRange + _HorizontalOffset;
                double origVerticalMax = _VerticalRange + _VerticalOffset;

                if (horizontalMin > origHorizontalMin) { horizontalMin = origHorizontalMin; adjusted = true; }
                if (horizontalMax < origHorizontalMax) { horizontalMax = origHorizontalMax; adjusted = true; }
                if (verticalMin > origVerticalMin) { verticalMin = origVerticalMin; adjusted = true; }
                if (verticalMax < origVerticalMax) { verticalMax = origVerticalMax; adjusted = true; }
            }

            _HorizontalOffset = horizontalMin;
            _HorizontalRange = horizontalMax - horizontalMin;
            _VerticalOffset = verticalMin;
            _VerticalRange = verticalMax - verticalMin;

            if (adjusted && !suppressRefresh) Refresh();
        }

        #endregion

        #region Point Properties and Methods

        private List<VisualiserPoint> _Points;

        [Browsable(false)]
        public ReadOnlyCollection<VisualiserPoint> Points
        {
            get { return new ReadOnlyCollection<VisualiserPoint>(_Points); }
        }

        public void Add(VisualiserPoint point)
        {

        }

        /// <summary>
        /// Specifies all the points to be displayed.
        /// </summary>
        /// <param name="points"></param>
        public void SetPoints(IEnumerable<VisualiserPoint> points)
        {
            _Points = points.ToList();
            if (AutoRange) PerformAutoRange(true);
            Refresh();
        }

        /// <summary>
        /// Specifies all the points to be displayed. Any <c>null</c> parameter
        /// is replaced by the default value specified on
        /// <see cref="VisualiserPoint"/>.
        /// </summary>
        /// <param name="points">A sequence of points.</param>
        /// <param name="color">The color of each point.</param>
        /// <param name="size">The size of each point.</param>
        /// <param name="shape">The shape of each point.</param>
        /// <param name="scale">The scale mode for each point.</param>
        public void SetPoints(IEnumerable<Point> points, Color? color = null, double? size = null,
            VisualiserPointShape? shape = null,
            VisualiserPointScaleMode? scale = null)
        {
            var actualColor = color ?? VisualiserPoint.DefaultColor;
            var actualSize = size ?? VisualiserPoint.DefaultSize;
            var actualShape = shape ?? VisualiserPoint.DefaultShape;
            var actualScale = scale ?? VisualiserPoint.DefaultScale;
            SetPoints(points
                .Select(p => new VisualiserPoint(p.X, p.Y, actualColor, actualSize, actualShape, actualScale))
            );
        }

        #endregion

        #region Rendering

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!AllowUpdate) return;

            //base.OnPaint(e);
            //e.Graphics.Clear(BackColor);

            if (_Points == null || _Points.Count == 0) return;

            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            float horizontalScale = (float)(ClientSize.Width / _HorizontalRange);
            float verticalScale = (float)(ClientSize.Height / _VerticalRange);
            if (float.IsInfinity(horizontalScale)) horizontalScale = 1.0f;
            if (float.IsInfinity(verticalScale)) verticalScale = 1.0f;

            using(var axisBrush = new SolidBrush(Color.Black))
            {
                float x = (float)-_HorizontalOffset * horizontalScale;
                float y = (float)-_VerticalOffset * verticalScale;

                e.Graphics.FillRectangle(axisBrush, x - 0.5f, 0.0f, 1.0f, ClientSize.Height);
                e.Graphics.FillRectangle(axisBrush, 0.0f, y - 0.5f, ClientSize.Width, 1.0f);
            }

            foreach (var pt in _Points)
            {
                float x = (float)(pt.X - _HorizontalOffset) * horizontalScale;
                float y = (float)(pt.Y - _VerticalOffset) * verticalScale;

                float dx = (pt.Scale == VisualiserPointScaleMode.Pixels) ?
                    (float)pt.Size :
                    (float)(pt.Size) * Math.Min(horizontalScale, verticalScale);
                float r = (float)Math.Sqrt(0.5 * dx);
                var rect = new RectangleF(x - dx, y - dx, dx * 2, dx * 2);
                var square = new RectangleF(x - r, y - r, r * 2, r * 2);
                float thick = (pt.Scale == VisualiserPointScaleMode.Pixels) ?
                    1.0f :
                    r * 0.25f;
                if (thick < 1.0f) thick = 1.0f;

                if (rect.IntersectsWith(e.ClipRectangle))
                {
                    Draw(e.Graphics, pt.Shape, rect, square, pt.Color, thick);
                }
            }
        }

        private static void Draw(Graphics g, VisualiserPointShape shape,
            RectangleF rect, RectangleF square, Color color, float thick)
        {
            switch (shape)
            {
            case VisualiserPointShape.Circle:
                using (var pen = new Pen(color, thick))
                {
                    g.DrawEllipse(pen, rect);
                }
                break;
            case VisualiserPointShape.FilledCircle:
                using (var brush = new SolidBrush(color))
                {
                    g.FillEllipse(brush, rect);
                }
                break;
            case VisualiserPointShape.Square:
                using (var pen = new Pen(color, thick))
                {
                    g.DrawRectangle(pen, square.X, square.Y, square.Width, square.Height);
                }
                break;
            case VisualiserPointShape.FilledSquare:
                using (var brush = new SolidBrush(color))
                {
                    g.FillRectangle(brush, rect);
                }
                break;
            case VisualiserPointShape.Diamond:
                using (var pen = new Pen(color, thick))
                {
                    g.RotateTransform(45.0f);
                    g.DrawRectangle(pen, square.X, square.Y, square.Width, square.Height);
                    g.ResetTransform();
                }
                break;
            case VisualiserPointShape.FilledDiamond:
                using (var brush = new SolidBrush(color))
                {
                    g.RotateTransform(45.0f);
                    g.FillRectangle(brush, rect);
                    g.ResetTransform();
                }
                break;
            case VisualiserPointShape.Cross:
                using (var pen = new Pen(color, thick))
                {
                    g.DrawLine(pen, square.Left, square.Top, square.Right, square.Bottom);
                    g.DrawLine(pen, square.Right, square.Top, square.Left, square.Bottom);
                }
                break;
            case VisualiserPointShape.Plus:
                using (var pen = new Pen(color, thick))
                {
                    float centre = square.Left + square.Width * 0.5f;
                    float middle = square.Top + square.Height * 0.5f;
                    g.DrawLine(pen, centre, square.Top, centre, square.Bottom);
                    g.DrawLine(pen, square.Left, middle, square.Right, middle);
                }
                break;
            default:
                System.Diagnostics.Debug.Assert(false, "Invalid Shape");
                break;
            }
        }

        #endregion
    }
}
