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

            if (MaintainSquareAspect)
            {
                var xscale = ClientSize.Width / _HorizontalRange;
                var yscale = ClientSize.Height / _VerticalRange;
                var scale = Math.Min(xscale, yscale);
                if (scale <= 0.0) scale = 1.0;  // should never happen, but we don't want to crash if it does
                var xcentre = _HorizontalOffset + 0.5 * _HorizontalRange;
                var ycentre = _VerticalOffset + 0.5 * _VerticalRange;
                _HorizontalRange = ClientSize.Width / scale;
                _VerticalRange = ClientSize.Height / scale;
                _HorizontalOffset = xcentre - 0.5 * _HorizontalRange;
                _VerticalOffset = ycentre - 0.5 * _VerticalRange;
            }

            if (_HorizontalOffset < _MinimumHorizontalOffset) _HorizontalOffset = _MinimumHorizontalOffset;
            if (_HorizontalOffset > _MaximumHorizontalOffset) _HorizontalOffset = _MaximumHorizontalOffset;
            if (_HorizontalRange < _MinimumHorizontalRange) _HorizontalRange = _MinimumHorizontalRange;
            if (_HorizontalRange > _MaximumHorizontalRange) _HorizontalRange = _MaximumHorizontalRange;
            if (_VerticalOffset < _MinimumVerticalOffset) _VerticalOffset = _MinimumVerticalOffset;
            if (_VerticalOffset > _MaximumVerticalOffset) _VerticalOffset = _MaximumVerticalOffset;
            if (_VerticalRange < _MinimumVerticalRange) _VerticalRange = _MinimumVerticalRange;
            if (_VerticalRange > _MaximumVerticalRange) _VerticalRange = _MaximumVerticalRange;


            base.Refresh();
        }

        #endregion

        #region Range Properties and Methods

        const bool DefaultAutoRange = false;
        const AutoSizeMode DefaultAutoRangeMode = AutoSizeMode.GrowOnly;
        const double DefaultHorizontalOffset = 0.0;
        const double DefaultHorizontalRange = 100.0;
        const bool DefaultFlipHorizontal = false;
        const double DefaultMinimumHorizontalOffset = -1.0e5;
        const double DefaultMaximumHorizontalOffset = 1.0e5;
        const double DefaultMinimumHorizontalRange = 0.0;
        const double DefaultMaximumHorizontalRange = 1.0e6;
        const double DefaultVerticalOffset = 0.0;
        const double DefaultVerticalRange = 100.0;
        const bool DefaultFlipVertical = false;
        const double DefaultMinimumVerticalOffset = -1.0e5;
        const double DefaultMaximumVerticalOffset = 1.0e5;
        const double DefaultMinimumVerticalRange = 0.0;
        const double DefaultMaximumVerticalRange = 1.0e6;
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
                if (value < _MinimumHorizontalRange) _HorizontalRange = _MinimumHorizontalRange;
                else if (value > _MinimumHorizontalRange) _HorizontalRange = _MaximumHorizontalRange;
                _HorizontalRange = value;
                Refresh();
            }
        }

        private bool _FlipHorizontal = DefaultFlipHorizontal;
        [Browsable(true), DefaultValue(DefaultFlipHorizontal)]
        [Description("True to invert values against the horizontal axis.")]
        public bool FlipHorizontal
        {
            get { return _FlipHorizontal; }
            set
            {
                _FlipHorizontal = value;
                Refresh();
            }
        }

        private double _MinimumHorizontalOffset = DefaultMinimumHorizontalOffset;
        [Browsable(true), DefaultValue(DefaultMinimumHorizontalOffset)]
        [Description("The minimum value to allow HorizontalOffset to take.")]
        public double MinimumHorizontalOffset
        {
            get { return _MinimumHorizontalOffset; }
            set
            {
                _MinimumHorizontalOffset = value;
                if (HorizontalOffset < value)
                {
                    HorizontalOffset = value;
                    Refresh();
                }
            }
        }

        private double _MaximumHorizontalOffset = DefaultMaximumHorizontalOffset;
        [Browsable(true), DefaultValue(DefaultMaximumHorizontalOffset)]
        [Description("The maximum value to allow HorizontalOffset to take.")]
        public double MaximumHorizontalOffset
        {
            get { return _MaximumHorizontalOffset; }
            set
            {
                _MaximumHorizontalOffset = value;
                if (HorizontalOffset > value)
                {
                    HorizontalOffset = value;
                    Refresh();
                }
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
                if (HorizontalRange < value)
                {
                    HorizontalRange = value;
                    Refresh();
                }
            }
        }

        private double _MaximumHorizontalRange = DefaultMaximumHorizontalRange;
        [Browsable(true), DefaultValue(DefaultMaximumHorizontalRange)]
        [Description("The maximum value to allow HorizontalRange to take.")]
        public double MaximumHorizontalRange
        {
            get { return _MaximumHorizontalRange; }
            set
            {
                _MaximumHorizontalRange = value;
                if (HorizontalRange > value)
                {
                    HorizontalRange = value;
                    Refresh();
                }
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
                if (value < _MinimumVerticalRange) _VerticalRange = _MinimumVerticalRange;
                else if (value > _MinimumVerticalRange) _VerticalRange = _MaximumVerticalRange;
                else _VerticalRange = value;
                Refresh();
            }
        }

        private bool _FlipVertical = DefaultFlipVertical;
        [Browsable(true), DefaultValue(DefaultFlipVertical)]
        [Description("True to invert values against the vertical axis.")]
        public bool FlipVertical
        {
            get { return _FlipVertical; }
            set
            {
                _FlipVertical = value;
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
                if (VerticalRange < value)
                {
                    VerticalRange = value;
                    Refresh();
                }
            }
        }

        private double _MaximumVerticalRange = DefaultMaximumVerticalRange;
        [Browsable(true), DefaultValue(DefaultMaximumVerticalRange)]
        [Description("The maximum value to allow VerticalRange to take.")]
        public double MaximumVerticalRange
        {
            get { return _MaximumVerticalRange; }
            set
            {
                _MaximumVerticalRange = value;
                if (VerticalRange > value)
                {
                    VerticalRange = value;
                    Refresh();
                }
            }
        }

        private double _MinimumVerticalOffset = DefaultMinimumVerticalOffset;
        [Browsable(true), DefaultValue(DefaultMinimumVerticalOffset)]
        [Description("The minimum value to allow VerticalOffset to take.")]
        public double MinimumVerticalOffset
        {
            get { return _MinimumVerticalOffset; }
            set
            {
                _MinimumVerticalOffset = value;
                if (VerticalOffset < value)
                {
                    VerticalOffset = value;
                    Refresh();
                }
            }
        }

        private double _MaximumVerticalOffset = DefaultMaximumVerticalOffset;
        [Browsable(true), DefaultValue(DefaultMaximumVerticalOffset)]
        [Description("The maximum value to allow VerticalOffset to take.")]
        public double MaximumVerticalOffset
        {
            get { return _MaximumVerticalOffset; }
            set
            {
                _MaximumVerticalOffset = value;
                if (VerticalOffset > value)
                {
                    VerticalOffset = value;
                    Refresh();
                }
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
                if (value && HorizontalOffset > 0.0)
                {
                    _HorizontalRange += HorizontalOffset;
                    HorizontalOffset = 0.0;
                    Refresh();
                }
                else if (value && HorizontalOffset + HorizontalRange < 0.0)
                {
                    HorizontalRange = -HorizontalOffset;
                    Refresh();
                }
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
                if (value && VerticalOffset > 0.0)
                {
                    _VerticalRange += VerticalOffset;
                    VerticalOffset = 0.0;
                    Refresh();
                }
                else if (value && VerticalOffset + VerticalRange < 0.0)
                {
                    VerticalRange = -VerticalOffset;
                    Refresh();
                }
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

            double horizontalMin = 0, horizontalMax = 0, verticalMin = 0, verticalMax = 0;

            bool firstPoint = true;
            foreach (var pt in Series.Where(s => s.Visible).SelectMany(s => s.Points))
            {
                horizontalMin = (firstPoint || pt.X < horizontalMin) ? pt.X : horizontalMin;
                horizontalMax = (firstPoint || pt.X > horizontalMax) ? pt.X : horizontalMax;
                verticalMin = (firstPoint || pt.Y < verticalMin) ? pt.Y : verticalMin;
                verticalMax = (firstPoint || pt.Y > verticalMax) ? pt.Y : verticalMax;
                firstPoint = false;
            }
            if (firstPoint) return;

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

            if (_MustIncludeHorizontalZero)
            {
                if (_HorizontalOffset > 0.0)
                {
                    _HorizontalRange += _HorizontalOffset;
                    _HorizontalOffset = 0.0;
                    adjusted = true;
                }
                else if (_HorizontalOffset + _HorizontalRange < 0.0)
                {
                    _HorizontalRange = -_HorizontalOffset;
                    adjusted = true;
                }
            }

            if (_MustIncludeVerticalZero)
            {
                if (_VerticalOffset > 0.0)
                {
                    _VerticalRange += _VerticalOffset;
                    _VerticalOffset = 0.0;
                    adjusted = true;
                }
                else if (_VerticalOffset + _VerticalRange < 0.0)
                {
                    _VerticalRange = -_VerticalOffset;
                    adjusted = true;
                }
            }

            if (adjusted && !suppressRefresh) Refresh();
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

        private RectangleF GetScaledRange()
        {
            double horizontalPadding = _HorizontalRange * 0.1;
            double verticalPadding = _VerticalRange * 0.1;

            float horizontalOffset = (float)(_HorizontalOffset - horizontalPadding);
            float verticalOffset = (float)(_VerticalOffset - verticalPadding);
            float horizontalScale = (float)(ClientSize.Width / (_HorizontalRange + horizontalPadding * 2));
            float verticalScale = (float)(ClientSize.Height / (_VerticalRange + verticalPadding * 2));
            if (float.IsInfinity(horizontalScale)) horizontalScale = 1.0f;
            if (float.IsInfinity(verticalScale)) verticalScale = 1.0f;

            return new RectangleF(horizontalOffset, verticalOffset, horizontalScale, verticalScale);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!AllowUpdate) return;

            if (!Series.AnyPoints(true)) return;


            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            if (FlipVertical)
            {
                e.Graphics.TranslateTransform(0.0f, ClientSize.Height);
                e.Graphics.ScaleTransform(1.0f, -1.0f);
            }

            var scale = GetScaledRange();

            using (var horizontalAxisPen = new Pen(ForeColor, (float)_HorizontalAxisThickness))
            using (var verticalAxisPen = new Pen(ForeColor, (float)_VerticalAxisThickness))
            using (var gridPen = new Pen(GridColor, (float)_GridThickness))
                DrawAxes(e.Graphics, horizontalAxisPen, verticalAxisPen, gridPen, scale);

            var visibleSeries = Series.Where(s => s.Visible).ToList();

            var defStyle = VisualiserPoint.DefaultStyle;
            defStyle.BeginRender(scale.X, scale.Y, scale.Width, scale.Height);
            foreach (var s in visibleSeries)
            {
                if (s.Style != null) s.Style.BeginRender(scale.X, scale.Y, scale.Width, scale.Height);
                foreach (var pt in s.Points)
                {
                    if (pt.Style != null) pt.Style.BeginRender(scale.X, scale.Y, scale.Width, scale.Height);
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
                if(s.Style != null) s.Style.EndRender();
                foreach (var pt in s.Points)
                {
                    if (pt.Style != null) pt.Style.EndRender();
                }
            }

            DrawZoomRectangle(e.Graphics);

            e.Graphics.ResetTransform();
            using (var textBrush = new SolidBrush(ForeColor))
            {
                DrawMouseLocation(e.Graphics, textBrush, scale);
            }
        }

        private void DrawMouseLocation(Graphics g, Brush textBrush, RectangleF scale)
        {
            if (_ShowMouseCoordinates && CurrentMouseLocation.HasValue)
            {
                var cml = CurrentMouseLocation.Value;

                int precisionX = (int)Math.Floor(Math.Log10(scale.Width));
                int precisionY = (int)Math.Floor(Math.Log10(scale.Height));

                double x = ((_FlipHorizontal) ? (ClientSize.Width - cml.X) : cml.X) / scale.Width + scale.X;
                double y = ((_FlipVertical) ? (ClientSize.Height - cml.Y) : cml.Y) / scale.Height + scale.Y;

                string formatX = (precisionX > 0) ? "." + new string('0', precisionX) : "";
                string formatY = (precisionY > 0) ? "." + new string('0', precisionY) : "";

                var formatString = "{0:0" + formatX + "}, {1:0" + formatY + "}";
                var display = string.Format(formatString, x, y);

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

        private void DrawAxes(Graphics g, Pen horizontalAxisPen, Pen verticalAxisPen, Pen gridPen, RectangleF scale)
        {
            if (!_HorizontalAxis && !_VerticalAxis) return;

            float x = -scale.X * scale.Width;
            float y = -scale.Y * scale.Height;

            if (_HorizontalAxis && y < ClientSize.Height) g.DrawLine(horizontalAxisPen, 0.0f, y, ClientSize.Width, y);
            if (_VerticalAxis && x < ClientSize.Width) g.DrawLine(verticalAxisPen, x, 0.0f, x, ClientSize.Height);

            if (_HorizontalAxisTicks)
            {
                double tickInterval = _HorizontalAxisTickInterval;
                int tickSize = _HorizontalAxisTickSize;
                double autoTickInterval = 0.01;
                int autoTickSize = 2;
                if (tickInterval <= 0.0 || tickSize == 0)
                {
                    while (ClientSize.Width > 50 * (scale.Width * autoTickInterval) && autoTickInterval < 100000)
                    {
                        autoTickInterval *= 10;
                        autoTickSize *= 2;
                    }
                    if (tickInterval <= 0.0) tickInterval = autoTickInterval;
                    if (tickSize == 0) tickSize = autoTickSize;
                }
                if (tickInterval < 100000)
                {
                    y += (float)_HorizontalAxisThickness * 0.5f;
                    float d = (float)(tickInterval * scale.Width);
                    for (int step = 1; x - step * d > 0; step += 1)
                    {
                        g.DrawLine(gridPen, x - step * d, 0, x - step * d, ClientSize.Height);
                        g.DrawLine(horizontalAxisPen, x - step * d, y, x - step * d, y + tickSize);
                    }
                    for (int step = 1; x + step * d < ClientSize.Width; step += 1)
                    {
                        g.DrawLine(gridPen, x + step * d, 0, x + step * d, ClientSize.Height);
                        g.DrawLine(horizontalAxisPen, x + step * d, y, x + step * d, y + tickSize);
                    }
                    y -= (float)_HorizontalAxisThickness * 0.5f;
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
                    while (ClientSize.Height > 50 * (scale.Height * autoTickInterval) && autoTickInterval < 100000)
                    {
                        autoTickInterval *= 10;
                        autoTickSize *= 2;
                    }
                    if (tickInterval <= 0.0) tickInterval = autoTickInterval;
                    if (tickSize == 0) tickSize = autoTickSize;
                }
                if (tickInterval < 100000)
                {
                    x += (float)_VerticalAxisThickness * 0.5f;
                    float d = (float)(tickInterval * scale.Height);
                    for (int step = 1; y - step * d > 0; step += 1)
                    {
                        g.DrawLine(gridPen, 0, y - step * d, ClientSize.Width, y - step * d);
                        g.DrawLine(verticalAxisPen, x, y - step * d, x + tickSize, y - step * d);
                    }
                    for (int step = 1; y + step * d < ClientSize.Height; step += 1)
                    {
                        g.DrawLine(gridPen, 0, y + step * d, ClientSize.Width, y + step * d);
                        g.DrawLine(verticalAxisPen, x, y + step * d, x + tickSize, y + step * d);
                    }
                    x -= (float)_VerticalAxisThickness * 0.5f;
                }
            }
        }

        #endregion

        #region Zoom and Translate

        protected Point? ZoomStartLocation = null;
        protected Point? ZoomEndLocation = null;
        private bool CanUnzoom = false;
        private double UnzoomHorizontalOffset, UnzoomVerticalOffset;
        private double UnzoomHorizontalRange, UnzoomVerticalRange;
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
                if (FlipHorizontal)
                {
                    l = ClientSize.Width - Interlocked.Exchange(ref r, ClientSize.Width - l);
                }
                if (FlipVertical)
                {
                    t = ClientSize.Height - Interlocked.Exchange(ref b, ClientSize.Height - t);
                }
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
                    _HorizontalOffset = UnzoomHorizontalOffset;
                    _HorizontalRange = UnzoomHorizontalRange;
                    _VerticalOffset = UnzoomVerticalOffset;
                    _VerticalRange = UnzoomVerticalRange;
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
                    UnzoomHorizontalOffset = _HorizontalOffset;
                    UnzoomHorizontalRange = _HorizontalRange;
                    UnzoomVerticalOffset = _VerticalOffset;
                    UnzoomVerticalRange = _VerticalRange;
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
                    var scale = GetScaledRange();

                    _HorizontalOffset = zoomTo.Value.Left / scale.Width + scale.X;
                    _VerticalOffset = zoomTo.Value.Top / scale.Height + scale.Y;
                    _HorizontalRange = Math.Abs(zoomTo.Value.Width / scale.Width);
                    _VerticalRange = Math.Abs(zoomTo.Value.Height / scale.Height);
                }
                ZoomStartLocation = null;
                ZoomEndLocation = null;
                Refresh();
            }
        }

        #endregion
    }
}
