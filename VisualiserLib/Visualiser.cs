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
using System.Threading;

namespace VisualiserLib
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

            if (MaintainSquareAspect) View.GrowToSquare();
            
            base.Refresh();
        }

        #endregion

        #region Range Properties and Methods

        const bool DefaultAutoRange = false;
        const AutoSizeMode DefaultAutoRangeMode = AutoSizeMode.GrowOnly;
        static readonly ViewRectangle DefaultView = new ViewRectangle();
        static readonly ViewRectangle DefaultMaximumView = new ViewRectangle(-1.0e5, -1.0e5, 1.0e5, 1.0e5);
        const bool DefaultMustIncludeHorizontalZero = false;
        const bool DefaultMustIncludeVerticalZero = false;
        const bool DefaultMaintainSquareAspect = false;

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

        private ViewRectangle _View = DefaultView;
        [Browsable(true)]
        [Description("Specify the viewable untransformed values.")]
        public ViewRectangle View
        {
            get { return _View; }
            set
            {
                if (value == _View) return;
                if (_View != null)
                {
                    _View.PropertyChanged -= View_PropertyChanged;
                }
                _View = value;
                _View.PropertyChanged += new PropertyChangedEventHandler(View_PropertyChanged);
                Refresh();
            }
        }
        private bool ShouldSerializeView() { return _View.Equals(DefaultView); }
        private void ResetView() { View = DefaultView; }
        private void View_PropertyChanged(object sender, PropertyChangedEventArgs e) { Refresh(); }

        private ViewRectangle _MaximumView = DefaultMaximumView;
        [Browsable(true)]
        [Description("Specify the maximum viewable untransformed values.")]
        public ViewRectangle MaximumView
        {
            get { return _MaximumView; }
            set
            {
                _MaximumView = value;
                View.ShrinkToWithin(_MaximumView);
            }
        }
        private bool ShouldSerializeMaximumView() { return _MaximumView.Equals(DefaultMaximumView); }
        private void ResetMaximumView() { MaximumView = DefaultMaximumView; }


        const bool DefaultHorizontalFlip = false;
        private bool _HorizontalFlip = DefaultHorizontalFlip;
        [Browsable(true), DefaultValue(DefaultHorizontalFlip)]
        [Description("True to prefer positive values at the left of view when auto-ranging.")]
        public bool HorizontalFlip
        {
            get { return _HorizontalFlip; }
            set
            {
                _HorizontalFlip = value;
            }
        }

        const bool DefaultVerticalFlip = false;
        private bool _VerticalFlip = DefaultVerticalFlip;
        [Browsable(true), DefaultValue(DefaultVerticalFlip)]
        [Description("True to prefer positive values at the top of view when auto-ranging.")]
        public bool VerticalFlip
        {
            get { return _VerticalFlip; }
            set
            {
                _VerticalFlip = value;
            }
        }

        private bool _MustIncludeHorizontalZero = DefaultMustIncludeHorizontalZero;
        [Browsable(true), DefaultValue(DefaultMustIncludeHorizontalZero)]
        [Description("True to require that the horizontal zero is always visible.")]
        public bool MustIncludeHorizontalZero
        {
            get { return _MustIncludeHorizontalZero; }
            set
            {
                _MustIncludeHorizontalZero = value;
                if (value) Refresh();
            }
        }

        private bool _MustIncludeVerticalZero = DefaultMustIncludeVerticalZero;
        [Browsable(true), DefaultValue(DefaultMustIncludeVerticalZero)]
        [Description("True to require that the vertical zero is always visible.")]
        public bool MustIncludeVerticalZero
        {
            get { return _MustIncludeVerticalZero; }
            set
            {
                _MustIncludeVerticalZero = value;
                if (value) Refresh();
            }
        }

        private bool _MaintainSquareAspect = DefaultMaintainSquareAspect;
        [Browsable(true), DefaultValue(DefaultMaintainSquareAspect)]
        [Description("True to automatically ensure that the vertical and horizontal ranges always match.")]
        public bool MaintainSquareAspect
        {
            get { return _MaintainSquareAspect; }
            set
            {
                _MaintainSquareAspect = value;
                if (value) Refresh();
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

            if (!Series.AnyPoints()) return;
            var firstPoint = Series.First().Points[0];
            var newView = new ViewRectangle(firstPoint.X, firstPoint.Y, firstPoint.X, firstPoint.Y);

            foreach (var pt in Series.Where(s => s.Visible).SelectMany(s => s.Points))
            {
                newView.GrowToInclude(pt.X, pt.Y, !HorizontalFlip, !VerticalFlip);
            }

            if (AutoRangeMode == AutoSizeMode.GrowOnly)
            {
                _View.GrowToInclude(newView);
            }
            else
            {
                _View = newView;
            }
            
            if (MustIncludeHorizontalZero || MustIncludeHorizontalZero)
            {
                _View.GrowToInclude(
                    MustIncludeVerticalZero ? (double?)0.0 : null,
                    MustIncludeHorizontalZero ? (double?)0.0 : null);
            }


            if (!suppressRefresh) Refresh();
        }

        #endregion

        #region Display Properties and Methods


        const bool DefaultHorizontalAxis = true;
        private bool _HorizontalAxis = DefaultHorizontalAxis;
        [Browsable(true), DefaultValue(DefaultHorizontalAxis)]
        [Description("True to display the horizontal axis.")]
        public bool HorizontalAxis
        {
            get { return _HorizontalAxis; }
            set
            {
                _HorizontalAxis = value;
                if (!value) _HorizontalAxisTicks = false;
                Refresh();
            }
        }

        const bool DefaultVerticalAxis = true;
        private bool _VerticalAxis = DefaultVerticalAxis;
        [Browsable(true), DefaultValue(DefaultVerticalAxis)]
        [Description("True to display the vertical axis.")]
        public bool VerticalAxis
        {
            get { return _VerticalAxis; }
            set
            {
                _VerticalAxis = value;
                if (!value) _VerticalAxisTicks = false;
                Refresh();
            }
        }

        const bool DefaultHorizontalAxisTicks = true;
        private bool _HorizontalAxisTicks = DefaultHorizontalAxisTicks;
        [Browsable(true), DefaultValue(DefaultHorizontalAxisTicks)]
        [Description("True to display ticks on the horizontal axis.")]
        public bool HorizontalAxisTicks
        {
            get { return _HorizontalAxisTicks; }
            set
            {
                _HorizontalAxisTicks = value;
                if (value) _HorizontalAxis = true;
                Refresh();
            }
        }

        const int DefaultHorizontalAxisTickSize = 0;
        private int _HorizontalAxisTickSize = DefaultHorizontalAxisTickSize;
        [Browsable(true), DefaultValue(DefaultHorizontalAxisTickSize)]
        [Description("The number of pixels tall to make ticks on the horizontal axis. Zero indicates automatic scaling.")]
        public int HorizontalAxisTickSize
        {
            get { return _HorizontalAxisTickSize; }
            set
            {
                _HorizontalAxisTickSize = value;
                Refresh();
            }
        }

        const double DefaultHorizontalAxisTickInterval = 0.0;
        private double _HorizontalAxisTickInterval = DefaultHorizontalAxisTickInterval;
        [Browsable(true), DefaultValue(DefaultHorizontalAxisTickInterval)]
        [Description("The spacing of ticks on the horizontal axis. Zero indicates automatic scaling (recommended).")]
        public double HorizontalAxisTickInterval
        {
            get { return _HorizontalAxisTickInterval; }
            set
            {
                _HorizontalAxisTickInterval = value;
                Refresh();
            }
        }

        const double DefaultHorizontalAxisThickness = 1.0;
        private double _HorizontalAxisThickness = DefaultHorizontalAxisThickness;
        [Browsable(true), DefaultValue(DefaultHorizontalAxisThickness)]
        [Description("The number of pixels tall to make the horizontal axis.")]
        public double HorizontalAxisThickness
        {
            get { return _HorizontalAxisThickness; }
            set
            {
                _HorizontalAxisThickness = value;
                Refresh();
            }
        }


        static readonly Color DefaultHorizontalAxisColor = Color.Black;
        private Color _HorizontalAxisColor = DefaultHorizontalAxisColor;
        [Browsable(true)]
        [Description("The color of the horizontal axis.")]
        public Color HorizontalAxisColor
        {
            get { return _HorizontalAxisColor; }
            set
            {
                _HorizontalAxisColor = value;
                Refresh();
            }
        }
        private bool ShouldSerializeHorizontalAxisColor() { return _HorizontalAxisColor == DefaultHorizontalAxisColor; }
        private void ResetHorizontalAxisColor() { HorizontalAxisColor = DefaultHorizontalAxisColor; }

        const bool DefaultVerticalAxisTicks = true;
        private bool _VerticalAxisTicks = DefaultVerticalAxisTicks;
        [Browsable(true), DefaultValue(DefaultVerticalAxisTicks)]
        [Description("True to display ticks on the vertical axis.")]
        public bool VerticalAxisTicks
        {
            get { return _VerticalAxisTicks; }
            set
            {
                _VerticalAxisTicks = value;
                if (value) _VerticalAxis = true;
                Refresh();
            }
        }

        const int DefaultVerticalAxisTickSize = 0;
        private int _VerticalAxisTickSize = DefaultVerticalAxisTickSize;
        [Browsable(true), DefaultValue(DefaultVerticalAxisTickSize)]
        [Description("The number of pixels tall to make ticks on the vertical axis. Zero indicates automatic scaling.")]
        public int VerticalAxisTickSize
        {
            get { return _VerticalAxisTickSize; }
            set
            {
                _VerticalAxisTickSize = value;
                Refresh();
            }
        }

        const double DefaultVerticalAxisTickInterval = 0.0;
        private double _VerticalAxisTickInterval = DefaultVerticalAxisTickInterval;
        [Browsable(true), DefaultValue(DefaultVerticalAxisTickInterval)]
        [Description("The spacing of ticks on the vertical axis. Zero indicates automatic scaling (recommended).")]
        public double VerticalAxisTickInterval
        {
            get { return _VerticalAxisTickInterval; }
            set
            {
                _VerticalAxisTickInterval = value;
                Refresh();
            }
        }


        const double DefaultVerticalAxisThickness = 1.0;
        private double _VerticalAxisThickness = DefaultVerticalAxisThickness;
        [Browsable(true), DefaultValue(DefaultVerticalAxisThickness)]
        [Description("The number of pixels wide to make the vertical axis.")]
        public double VerticalAxisThickness
        {
            get { return _VerticalAxisThickness; }
            set
            {
                _VerticalAxisThickness = value;
                Refresh();
            }
        }


        static readonly Color DefaultVerticalAxisColor = Color.Black;
        private Color _VerticalAxisColor = DefaultVerticalAxisColor;
        [Browsable(true)]
        [Description("The color of the vertical axis.")]
        public Color VerticalAxisColor
        {
            get { return _VerticalAxisColor; }
            set
            {
                _VerticalAxisColor = value;
            }
        }
        private bool ShouldSerializeVerticalAxisColor() { return _VerticalAxisColor == DefaultVerticalAxisColor; }
        private void ResetVerticalAxisColor() { VerticalAxisColor = DefaultVerticalAxisColor; }


        static readonly Color DefaultGridColor = Color.Gray;
        private Color _GridColor = DefaultGridColor;
        [Browsable(true)]
        [Description("The color of the grid. Set to Color.Transparent to hide.")]
        public Color GridColor
        {
            get { return _GridColor; }
            set
            {
                _GridColor = value;
            }
        }
        private bool ShouldSerializeGridColor() { return _GridColor == DefaultGridColor; }
        private void ResetGridColor() { GridColor = DefaultGridColor; }


        const double DefaultGridThickness = 1.0;
        private double _GridThickness = DefaultGridThickness;
        [Browsable(true), DefaultValue(DefaultGridThickness)]
        [Description("The thickness of the grid lines.")]
        public double GridThickness
        {
            get { return _GridThickness; }
            set
            {
                _GridThickness = value;
            }
        }

        const bool DefaultShowMouseCoordinates = false;
        private bool _ShowMouseCoordinates = DefaultShowMouseCoordinates;
        [Browsable(true), DefaultValue(DefaultShowMouseCoordinates)]
        [Description("True to display the real location of the mouse.")]
        public bool ShowMouseCoordinates
        {
            get { return _ShowMouseCoordinates; }
            set
            {
                _ShowMouseCoordinates = value;
            }
        }

        const ContentAlignment DefaultMouseCoordinatesAlign = ContentAlignment.TopLeft;
        private ContentAlignment _MouseCoordinatesAlign = DefaultMouseCoordinatesAlign;
        [Browsable(true), DefaultValue(DefaultMouseCoordinatesAlign)]
        [Description("The alignment of the mouse coordinate display text.")]
        public ContentAlignment MouseCoordinatesAlign
        {
            get { return _MouseCoordinatesAlign; }
            set
            {
                _MouseCoordinatesAlign = value;
            }
        }

        #endregion

        #region Point Properties and Methods

        /// <summary>
        /// Represents the information associated with a particular series.
        /// </summary>
        public class SeriesInfo
        {
            /// <summary>
            /// Initialises a default series.
            /// </summary>
            public SeriesInfo() { Points = new List<VisualiserPoint>(); Visible = true; Style = null; }
            /// <summary>
            /// The points belonging to this series.
            /// </summary>
            public List<VisualiserPoint> Points { get; set; }
            /// <summary>
            /// True if the series should be displayed; otherwise, false.
            /// </summary>
            public bool Visible { get; set; }
            /// <summary>
            /// The style to use for points in this series if they are not
            /// overloaded.
            /// </summary>
            public VisualiserPointStyle Style { get; set; }
        }

        /// <summary>
        /// Represents the collection of series in this visualiser.
        /// </summary>
        public class SeriesCollection : IEnumerable<SeriesInfo>
        {
            SortedList<int, SeriesInfo> Dict = new SortedList<int, SeriesInfo>();

            /// <summary>
            /// Removes all series.
            /// </summary>
            public void Clear()
            {
                Dict.Clear();
            }

            /// <summary>
            /// Gets the specifies series. If it does not exist, an empty
            /// series is created.
            /// </summary>
            /// <param name="index">The index of the series.</param>
            /// <returns>The series at the specified index.</returns>
            public SeriesInfo this[int index]
            {
                get
                {
                    if (!Dict.ContainsKey(index)) Dict[index] = new SeriesInfo();
                    return Dict[index];
                }
            }

            /// <summary>
            /// Returns True if there are any points in any series.
            /// </summary>
            /// <param name="visibleOnly">If True, ignores points for invisible
            /// series.</param>
            /// <returns>True if there are any points in any series.</returns>
            public bool AnyPoints(bool visibleOnly = true)
            {
                if (visibleOnly)
                    return Dict.Count > 0 && Dict.Any(kv => kv.Value.Visible && kv.Value.Points.Count > 0);
                else
                    return Dict.Count > 0 && Dict.Any(kv => kv.Value.Points.Count > 0);
            }

            /// <summary>
            /// Gets the number of series.
            /// </summary>
            public int Count
            {
                get { return Dict.Count; }
            }

            /// <summary>
            /// Gets the total number of points in all series.
            /// </summary>
            public int PointCount
            {
                get { return Dict.Sum(kv => kv.Value.Points.Count); }
            }

            /// <summary>
            /// Returns an enumerator for the series in this collection.
            /// </summary>
            /// <returns>
            /// An enumerator for the series in this collection.
            /// </returns>
            public IEnumerator<SeriesInfo> GetEnumerator()
            {
                return Dict.Values.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private readonly SeriesCollection _Series = new SeriesCollection();
        /// <summary>
        /// Gets the collection of series. New series may be added by accessing
        /// an otherwise unused index. Series may only be removed by clearing
        /// the collection.
        /// 
        /// Use of the methods bound to <see cref="Visualiser"/> is recommended
        /// to ensure the display is updated. Otherwise, after directly
        /// manipulating a series, call <see cref="PerformAutoRange"/> if
        /// desired, followed by <see cref="Refresh"/>.
        /// </summary>
        [Browsable(false)]
        public SeriesCollection Series { get { return _Series; } }

        /// <summary>
        /// Adds a point to the visualiser.
        /// </summary>
        /// <param name="point">The point to add.</param>
        /// <param name="series">The series to add the point to.</param>
        public void Add(VisualiserPoint point, int series = 0)
        {
            var s = Series[series];
            s.Points.Add(point);
            if (s.Visible)
            {
                if (AutoRange) PerformAutoRange(true);
                Refresh();
            }
        }

        /// <summary>
        /// Removes all series from the visualiser.
        /// </summary>
        public void ClearAll(bool pointsOnly = false)
        {
            if (pointsOnly)
            {
                foreach (var s in Series) s.Points.Clear();
            }
            else
            {
                Series.Clear();
            }
            if (AutoRange) PerformAutoRange(true);
            Refresh();
        }

        /// <summary>
        /// Removes all points from the specified series.
        /// </summary>
        /// <param name="series">The series to clear.</param>
        public void Clear(int series = 0)
        {
            var s = Series[series];
            s.Points.Clear();
            if (s.Visible)
            {
                if (AutoRange) PerformAutoRange(true);
                Refresh();
            }
        }

        /// <summary>
        /// Makes the specified series visible.
        /// </summary>
        /// <param name="series">The series to update.</param>
        /// <param name="visible">
        /// <c>true</c> to make the series visible.
        /// </param>
        public void ShowSeries(int series, bool visible = true)
        {
            Series[series].Visible = visible;
            if (AutoRange) PerformAutoRange(true);
            Refresh();
        }

        /// <summary>
        /// Changes the default style for a series.
        /// </summary>
        /// <param name="series">The series to update.</param>
        /// <param name="style">The style to use.</param>
        public void SetSeriesStyle(int series, VisualiserPointStyle style)
        {
            var s = Series[series];
            s.Style = style;
            if (s.Visible)
            {
                Refresh();
            }
        }

        /// <summary>
        /// Specifies all the points to be displayed.
        /// </summary>
        /// <param name="points">A sequence of points.</param>
        /// <param name="series">The series to set.</param>
        public void SetPoints(IEnumerable<VisualiserPoint> points, int series = 0)
        {
            var s = Series[series];
            s.Points = points.ToList();
            if (s.Visible)
            {
                if (AutoRange) PerformAutoRange(true);
                Refresh();
            }
        }

        /// <summary>
        /// Specifies all the points to be displayed. Any <c>null</c> parameter
        /// is replaced by the default value specified on
        /// <see cref="VisualiserPoint"/>.
        /// </summary>
        /// <param name="points">A sequence of points.</param>
        /// <param name="style">The style for every point.</param>
        /// <param name="series">The series to set.</param>
        public void SetPoints(IEnumerable<Point> points, VisualiserPointStyle style = null, int series = 0)
        {
            SetPoints(points.Select(p => new VisualiserPoint(p.X, p.Y, 0.0, style)), series);
        }



        #endregion

        #region Mouse Tracking

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Zoom_MouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Zoom_MouseMove(e);
            CurrentMouseLocation = e.Location;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Zoom_MouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            CurrentMouseLocation = null;
        }

        private Point? _CurrentMouseLocation;
        private Point? CurrentMouseLocation
        {
            get { return _CurrentMouseLocation; }
            set
            {
                _CurrentMouseLocation = value;
                if (ShowMouseCoordinates)
                {
                    Invalidate();
                }
            }
        }


        #endregion

        #region Rendering

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!AllowUpdate) return;

            if (!Series.AnyPoints(true)) return;

            var view = View.Clone();

            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            using (var horizontalAxisPen = new Pen(ForeColor, (float)_HorizontalAxisThickness))
            using (var verticalAxisPen = new Pen(ForeColor, (float)_VerticalAxisThickness))
            using (var gridPen = new Pen(GridColor, (float)_GridThickness))
                DrawAxes(e.Graphics, horizontalAxisPen, verticalAxisPen, gridPen, view);

            var visibleSeries = Series.Where(s => s.Visible).ToList();

            var defStyle = VisualiserPoint.DefaultStyle;
            defStyle.BeginRender(ClientRectangle, view);
            foreach (var s in visibleSeries)
            {
                if (s.Style != null) s.Style.BeginRender(ClientRectangle, view);
                foreach (var pt in s.Points)
                {
                    if (pt.Style != null) pt.Style.BeginRender(ClientRectangle, view);
                }
            }

            foreach (var s in visibleSeries)
            {
                if (s.Points.Count > 1)
                {
                    if (s.Points.All(pt => pt.Style == null))
                    {
                        (s.Style ?? defStyle).RenderLinePath(e.Graphics, s.Points);
                    }
                    else
                    {
                        var previous = s.Points[0];
                        foreach (var pt in s.Points.Skip(1))
                        {
                            (pt.Style ?? s.Style ?? defStyle).RenderLine(e.Graphics, previous, pt);
                            previous = pt;
                        }

                        {
                            var style = (s.Points[0].Style ?? s.Style ?? defStyle);
                            if (style.LineLoop)
                            {
                                style.RenderLine(e.Graphics, previous, s.Points[0]);
                            }
                        }
                    }
                }

                foreach (var pt in s.Points)
                {
                    (pt.Style ?? s.Style ?? defStyle).RenderPoint(e.Graphics, pt);
                }
            }

            defStyle.EndRender();
            foreach (var s in visibleSeries)
            {
                if (s.Style != null) s.Style.EndRender();
                foreach (var pt in s.Points)
                {
                    if (pt.Style != null) pt.Style.EndRender();
                }
            }

            DrawZoomRectangle(e.Graphics);

            e.Graphics.ResetTransform();
            using (var textBrush = new SolidBrush(ForeColor))
            {
                DrawMouseLocation(e.Graphics, textBrush, view);
            }
        }

        private void DrawMouseLocation(Graphics g, Brush textBrush, ViewRectangle view)
        {
            if (_ShowMouseCoordinates && CurrentMouseLocation.HasValue)
            {
                var cml = CurrentMouseLocation.Value;

                int precisionX = (int)Math.Floor(Math.Log10(ClientSize.Width / Math.Abs(view.Width)));
                int precisionY = (int)Math.Floor(Math.Log10(ClientSize.Height / Math.Abs(view.Height)));

                PointF pt = view.Unmap(ClientRectangle, new PointF(cml.X, cml.Y));

                string formatX = (precisionX > 0) ? "." + new string('0', precisionX) : "";
                string formatY = (precisionY > 0) ? "." + new string('0', precisionY) : "";

                var formatString = "{0:0" + formatX + "}, {1:0" + formatY + "}";
                var display = string.Format(formatString, pt.X, pt.Y);

                var sf = new StringFormat();
                switch (MouseCoordinatesAlign)
                {
                    case ContentAlignment.BottomCenter:
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomLeft:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomRight:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.MiddleCenter:
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.MiddleLeft:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.MiddleRight:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.TopCenter:
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopLeft:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopRight:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                    default:
                        break;
                }

                g.DrawString(display, Font, textBrush, (RectangleF)ClientRectangle, sf);
            }
        }

        private IEnumerable<float> GetPointsBetween(int firstPixel, int lastPixel,
            double firstValue, double lastValue, double padding, double step)
        {
            if (lastPixel < firstPixel || firstValue == lastValue) yield break;
            double range = Math.Abs(lastValue - firstValue);
            double min = Math.Min(lastValue, firstValue);

            double pad = range * padding;
            double scale = (lastPixel - firstPixel) / (range + pad);
            double pixel = (-min + pad * 0.5) * scale;
            double pixelStep = step * scale;
            
            for (; pixel > firstPixel; pixel -= pixelStep) { }
            for (; pixel < (firstPixel - pixelStep); pixel += pixelStep) { }
            for (; pixel <= lastPixel; pixel += pixelStep) { yield return (float)pixel; }
        }

        private void DrawAxes(Graphics g, Pen horizontalAxisPen, Pen verticalAxisPen, Pen gridPen, ViewRectangle view)
        {
            if (!_HorizontalAxis && !_VerticalAxis) return;

            if (_HorizontalAxisTicks)
            {
                double tickInterval = _HorizontalAxisTickInterval;
                int tickSize = _HorizontalAxisTickSize;
                double autoTickInterval = 0.01;
                int autoTickSize = 2;
                if (tickInterval <= 0.0 || tickSize == 0)
                {
                    while (Math.Abs(view.Width) > 50 * autoTickInterval && autoTickInterval < 100000)
                    {
                        autoTickInterval *= 10;
                        autoTickSize *= 2;
                    }
                    if (tickInterval <= 0.0) tickInterval = autoTickInterval;
                    if (tickSize == 0) tickSize = autoTickSize;
                }
                if (tickInterval < 100000)
                {
                    var pt = view.Map(ClientRectangle, 0.0, 0.0);
                    float y1 = pt.Y + (float)_HorizontalAxisThickness * 0.5f;
                    float y2 = y1 + (float)tickSize;

                    foreach (var x in GetPointsBetween(
                        0,
                        ClientRectangle.Width,
                        view.Left,
                        view.Right,
                        view.Padding,
                        tickInterval))
                    {
                        g.DrawLine(gridPen, x, 0, x, ClientSize.Height);
                        g.DrawLine(horizontalAxisPen, x, y1, x, y2);
                    }
                }
            }
            if (_VerticalAxisTicks)
            {
                double tickInterval = _VerticalAxisTickInterval;
                int tickSize = _VerticalAxisTickSize;
                double autoTickInterval = 0.01;
                int autoTickSize = 2;
                if (tickInterval <= 0.0 || tickSize == 0)
                {
                    while (Math.Abs(view.Height) > 50 * autoTickInterval && autoTickInterval < 100000)
                    {
                        autoTickInterval *= 10;
                        autoTickSize *= 2;
                    }
                    if (tickInterval <= 0.0) tickInterval = autoTickInterval;
                    if (tickSize == 0) tickSize = autoTickSize;
                }

                if (tickInterval < 100000)
                {
                    var pt = view.Map(ClientRectangle, 0.0, 0.0);
                    float x1 = pt.X + (float)_VerticalAxisThickness * 0.5f;
                    float x2 = x1 + (float)tickSize;

                    foreach (var y in GetPointsBetween(
                        0,
                        ClientRectangle.Height,
                        view.Top,
                        view.Bottom,
                        view.Padding,
                        tickInterval))
                    {
                        g.DrawLine(gridPen, 0, y, ClientSize.Width, y);
                        g.DrawLine(verticalAxisPen, x1, y, x2, y);
                    }
                }
            }

            PointF zero = view.Map(ClientRectangle, PointF.Empty);

            if (_HorizontalAxis && zero.Y < ClientSize.Height) g.DrawLine(horizontalAxisPen, 0.0f, zero.Y, ClientSize.Width, zero.Y);
            if (_VerticalAxis && zero.X < ClientSize.Width) g.DrawLine(verticalAxisPen, zero.X, 0.0f, zero.X, ClientSize.Height);
        }

        #endregion

        #region Zoom and Translate

        protected Point? ZoomStartLocation = null;
        protected Point? ZoomEndLocation = null;
        private bool CanUnzoom = false;
        private ViewRectangle UnzoomView;
        private bool UnzoomAutoRange;

        protected Rectangle? GetZoomRect()
        {
            if (ZoomStartLocation.HasValue && ZoomEndLocation.HasValue)
            {
                int l = ZoomStartLocation.Value.X;
                int t = ZoomStartLocation.Value.Y;
                int r = ZoomEndLocation.Value.X;
                int b = ZoomEndLocation.Value.Y;
                if (r < l) l = Interlocked.Exchange(ref r, l);
                if (b < t) t = Interlocked.Exchange(ref b, t);
                return Rectangle.FromLTRB(l, t, r, b);
            }
            else
            {
                return null;
            }
        }

        protected void DrawZoomRectangle(Graphics g)
        {
            var rect = GetZoomRect();
            if (rect.HasValue)
            {
                using (var fill = new SolidBrush(Color.FromArgb(64, SystemColors.Highlight)))
                using (var outline = new Pen(SystemColors.Highlight, 3.0f))
                {
                    g.FillRectangle(fill, rect.Value);
                    g.DrawRectangle(outline, rect.Value);
                }
            }
        }

        private void Zoom_MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !ZoomStartLocation.HasValue)
            {
                if (CanUnzoom)
                {
                    _View = UnzoomView;
                    _AutoRange = UnzoomAutoRange;
                    CanUnzoom = false;
                    Refresh();
                }
                else
                {
                    PerformAutoRange();
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                Capture = true;
                ZoomStartLocation = e.Location;
                ZoomEndLocation = e.Location;

                if (!CanUnzoom)
                {
                    CanUnzoom = true;
                    UnzoomView = View.Clone();
                    UnzoomAutoRange = _AutoRange;
                    _AutoRange = false;
                }
            }
        }

        private void Zoom_MouseMove(MouseEventArgs e)
        {
            if (ZoomStartLocation.HasValue && ZoomEndLocation.HasValue)
            {
                ZoomEndLocation = e.Location;
                Invalidate();
            }
        }

        private void Zoom_MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Capture = false;
                var zoomTo = GetZoomRect();
                if (zoomTo.HasValue && zoomTo.Value.Width > 3 && zoomTo.Value.Height > 3)
                {
                    _View = View.Unmap(ClientRectangle, zoomTo.Value);
                }
                ZoomStartLocation = null;
                ZoomEndLocation = null;
                Refresh();
            }
        }

        #endregion
    }
}
