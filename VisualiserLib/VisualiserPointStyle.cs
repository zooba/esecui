using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace VisualiserLib
{
    /// <summary>
    /// The shape to draw the point as. The size value is treated as a bounding
    /// circle for non-circular shapes.
    /// </summary>
    public enum VisualiserPointShape
    {
        /// <summary>Points are drawn as circles.</summary>
        Circle,
        /// <summary>Points are drawn as squares.</summary>
        Square,
        /// <summary>Points are drawn as diamonds.</summary>
        Diamond,
        /// <summary>Points are drawn as crosses (X).</summary>
        Cross,
        /// <summary>Points are drawn as plus symbols (+).</summary>
        Plus,
    }

    /// <summary>
    /// The mode used to adjust the size of a point.
    /// </summary>
    public enum VisualiserPointScaleMode
    {
        /// <summary>
        /// Treat the size as a pixel radius. The range does not affect the
        /// size of the point.
        /// </summary>
        Pixels,
        /// <summary>
        /// Treat the size as an unmapped radius. A bigger range will result in
        /// a smaller point.
        /// </summary>
        Real
    }

    /// <summary>
    /// Represents a style class for points in a <see cref="Visualiser"/>.
    /// </summary>
    public class VisualiserPointStyle
    {
        /// <summary>
        /// Initialises the default style. Parameters need to be set before any
        /// point drawn with this style will appear.
        /// </summary>
        public VisualiserPointStyle()
        {
            Size = 5.0;

            Shape = VisualiserPointShape.Circle;
            ScaleMode = VisualiserPointScaleMode.Pixels;

            LineColor = Color.Transparent;
            BorderColor = Color.Transparent;
            FillColor = Color.Transparent;

            LineThickness = 0.0;
            BorderThickness = 0.0;

            LineLoop = false;

            FillBrush = null;

            RenderObjects = null;
        }

        #region Style Properties

        /// <summary>
        /// The size to render points with. This value is affected by
        /// <see cref="ScaleMode"/>.
        /// </summary>
        public double Size { get; set; }

        /// <summary>The shape to render points with.</summary>
        public VisualiserPointShape Shape { get; set; }
        /// <summary>The scale used to adjust size related values.</summary>
        public VisualiserPointScaleMode ScaleMode { get; set; }

        /// <summary>
        /// The color to render connecting lines with. Specifically, the line
        /// leading to this point is drawn with this color.
        /// </summary>
        public Color LineColor { get; set; }
        /// <summary>
        /// The color to render the ouline of the point with. Set to
        /// <see cref="Color.Transparent"/> to skip drawing a border.
        /// </summary>
        public Color BorderColor { get; set; }
        /// <summary>
        /// The color to fill the point with. Set to
        /// <see cref="Color.Transparent"/> to skip drawing a fill.
        /// </summary>
        public Color FillColor { get; set; }

        /// <summary>
        /// The thickness to render connecting lines with. Specifically, the
        /// line leading to this point is drawn with this thickness. This value
        /// is affected by <see cref="ScaleMode"/>.
        /// </summary>
        public double LineThickness { get; set; }
        /// <summary>
        /// The thickness to render the outline of the point. This value is
        /// affected by <see cref="ScaleMode"/>.
        /// </summary>
        public double BorderThickness { get; set; }

        /// <summary>
        /// <c>true</c> to connect the last point to the first point;
        /// otherwise, <c>false</c>. This must be set on the style of the first
        /// point in the sequence.
        /// </summary>
        public bool LineLoop { get; set; }

        /// <summary>
        /// The brush to fill the point with. If <c>null</c>, a solid brush of
        /// color <see cref="FillColor"/> is used. Otherwise, this brush is
        /// used regardless of <see cref="FillColor"/>.
        /// </summary>
        public Brush FillBrush { get; set; }

        #endregion

        #region Style Rendering State

        // A state object containing pens/brushes for the current style.
        private VisualiserPointStyleObjects RenderObjects;

        /// <summary>
        /// Initialises rendering using this style. While rendering is
        /// initialised, modifications to this style have no effect. Each call
        /// to <see cref="BeginRender"/> requires a matching call to
        /// <see cref="EndRender"/>.
        /// </summary>
        public void BeginRender(double xOffset, double yOffset, double xScale, double yScale)
        {
            if (RenderObjects == null)
            {
                var newObj = new VisualiserPointStyleObjects(this, xOffset, yOffset, xScale, yScale);
                if (Interlocked.CompareExchange(ref RenderObjects, newObj, null) != null)
                {
                    newObj.Dispose();
                }
            }
            RenderObjects.AddRef();
        }

        /// <summary>
        /// Ends rendering using this style. <see cref="EndRender"/> must be
        /// called one for each call to <see cref="BeginRender"/>.
        /// </summary>
        public void EndRender()
        {
            if (RenderObjects == null) return;
            if (RenderObjects.DecRef()) RenderObjects = null;
        }

        /// <summary>
        /// Encapsulates the required brush and pen objects for a particular style.
        /// </summary>
        private sealed class VisualiserPointStyleObjects : IDisposable
        {
            private int ReferenceCount;

            /// <summary>
            /// Increments the reference count on this object.
            /// </summary>
            public void AddRef()
            {
                System.Threading.Interlocked.Increment(ref ReferenceCount);
            }

            /// <summary>
            /// Decrements and reference count on this object and disposes the
            /// contents if this is the last reference.
            /// </summary>
            /// <returns>
            /// <c>true</c> if this was the last reference; otherwise,
            /// <c>false</c>.
            /// </returns>
            public bool DecRef()
            {
                if (System.Threading.Interlocked.Decrement(ref ReferenceCount) == 0)
                {
                    Dispose();
                    return true;
                }
                return false;
            }

            public Pen LinePen { get; private set; }
            public Pen BorderPen { get; private set; }
            public Brush FillBrush { get; private set; }

            public double Size { get; private set; }
            public VisualiserPointScaleMode ScaleMode { get; private set; }
            public double XOffset { get; private set; }
            public double YOffset { get; private set; }
            public double XScale { get; private set; }
            public double YScale { get; private set; }

            /// <summary>
            /// Initialises state values for rendering.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="xOffset"></param>
            /// <param name="yOffset"></param>
            /// <param name="xScale"></param>
            /// <param name="yScale"></param>
            public VisualiserPointStyleObjects(VisualiserPointStyle source,
                double xOffset, double yOffset, double xScale, double yScale)
            {
                ReferenceCount = 0;

                if (source.ScaleMode != VisualiserPointScaleMode.Pixels && source.ScaleMode != VisualiserPointScaleMode.Real)
                {
                    throw new NotSupportedException("Unsupported scale mode: " + source.ScaleMode.ToString());
                }

                double minScale = Math.Min(xScale, yScale);

                // Initialise LinePen
                if (source.LineColor != Color.Transparent && source.LineThickness > 0.0)
                {
                    if (source.ScaleMode == VisualiserPointScaleMode.Pixels)
                        LinePen = new Pen(source.LineColor, (float)source.LineThickness);
                    else if (source.ScaleMode == VisualiserPointScaleMode.Real)
                        LinePen = new Pen(source.LineColor, (float)Math.Min(source.LineThickness * minScale, 1));
                }
                else
                {
                    LinePen = null;
                }

                // Initialise BorderPen
                if (source.BorderColor != Color.Transparent && source.BorderThickness > 0.0)
                {
                    if (source.ScaleMode == VisualiserPointScaleMode.Pixels)
                        BorderPen = new Pen(source.BorderColor, (float)source.BorderThickness);
                    else if (source.ScaleMode == VisualiserPointScaleMode.Real)
                        BorderPen = new Pen(source.BorderColor, (float)Math.Min(source.BorderThickness * minScale, 1));
                }
                else
                {
                    BorderPen = null;
                }

                // Initialise FillBrush
                if (source.FillBrush != null)
                {
                    FillBrush = (Brush)source.FillBrush.Clone();
                }
                else if (source.FillColor != Color.Transparent)
                {
                    FillBrush = new SolidBrush(source.FillColor);
                }
                else
                {
                    FillBrush = null;
                }

                // Initialise scaling values
                Size = source.Size;
                ScaleMode = source.ScaleMode;
                XOffset = xOffset;
                YOffset = yOffset;
                XScale = xScale;
                YScale = yScale;
            }

            public PointF GetCenterPoint(VisualiserPoint point)
            {
                return new PointF(
                    (float)((point.X - XOffset) * XScale),
                    (float)((point.Y - YOffset) * YScale));
            }

            public RectangleF GetRectangle(VisualiserPoint point)
            {
                var sx = (point.Z > 0.0) ? point.Z : Size;
                var sy = sx;
                if (ScaleMode == VisualiserPointScaleMode.Real)
                {
                    sx = Math.Max(1.0, sx * XScale);
                    sy = Math.Max(1.0, sy * YScale);
                }

                return new RectangleF(
                    (float)((point.X - XOffset) * XScale - sx * 0.5f),
                    (float)((point.Y - YOffset) * YScale - sy * 0.5f),
                    (float)sx,
                    (float)sy);
            }

            public void Dispose()
            {
                if (LinePen != null) LinePen.Dispose();
                LinePen = null;
                if (BorderPen != null) BorderPen.Dispose();
                BorderPen = null;
                if (FillBrush != null) FillBrush.Dispose();
                FillBrush = null;
            }
        }
        #endregion

        #region Rendering Methods

        public void RenderLine(Graphics g, VisualiserPoint from, VisualiserPoint to)
        {
            if (RenderObjects.LinePen == null) return;
            var pt1 = RenderObjects.GetCenterPoint(from);
            var pt2 = RenderObjects.GetCenterPoint(to);
            g.DrawLine(RenderObjects.LinePen, pt1, pt2);
        }

        public void RenderLinePath(Graphics g, IEnumerable<VisualiserPoint> source)
        {
            if (RenderObjects.LinePen == null) return;

            var points = source.Select(pt => RenderObjects.GetCenterPoint(pt)).ToArray();
            var types = new byte[points.Length];
            types[0] = (byte)PathPointType.Start;
            for (int i = 1; i < types.Length; ++i) types[i] = (byte)PathPointType.Line;
            using (var gp = new GraphicsPath(points, types))
            {
                if (LineLoop) gp.CloseAllFigures();
                g.DrawPath(RenderObjects.LinePen, gp);
            }
        }

        public void RenderPoint(Graphics g, VisualiserPoint point)
        {
            if (Shape == VisualiserPointShape.Circle)
            {
                var rect = RenderObjects.GetRectangle(point);
                if (rect.IntersectsWith(g.ClipBounds))
                {
                    if (RenderObjects.FillBrush != null) g.FillEllipse(RenderObjects.FillBrush, rect);
                    if (RenderObjects.BorderPen != null) g.DrawEllipse(RenderObjects.BorderPen, rect);
                }
            }
            else if (Shape == VisualiserPointShape.Square)
            {
                var rect = RenderObjects.GetRectangle(point);
                if (rect.IntersectsWith(g.ClipBounds))
                {
                    if (RenderObjects.FillBrush != null) g.FillRectangle(RenderObjects.FillBrush, rect);
                    if (RenderObjects.BorderPen != null) g.DrawRectangle(RenderObjects.BorderPen, rect.X, rect.Y, rect.Width, rect.Height);
                }
            }
            else if (Shape == VisualiserPointShape.Diamond)
            {
                var rect = RenderObjects.GetRectangle(point);
                if (rect.IntersectsWith(g.ClipBounds))
                {
                    var p = RenderObjects.GetCenterPoint(point);
                    var oldTransform = g.Transform.Clone();
                    g.TranslateTransform(p.X, p.Y);
                    g.RotateTransform(45.0f);
                    rect = new RectangleF(rect.Width * -0.5f, rect.Height * -0.5f, rect.Width, rect.Height);
                    if (RenderObjects.FillBrush != null) g.FillRectangle(RenderObjects.FillBrush, rect);
                    if (RenderObjects.BorderPen != null) g.DrawRectangle(RenderObjects.BorderPen, rect.X, rect.Y, rect.Width, rect.Height);
                    g.Transform = oldTransform;
                }
            }
            else if (Shape == VisualiserPointShape.Plus)
            {
                if (RenderObjects.BorderPen == null) throw new InvalidOperationException("Require BorderPen to draw Plus");
                var rect = RenderObjects.GetRectangle(point);
                if (rect.IntersectsWith(g.ClipBounds))
                {
                    float centre = rect.Left + rect.Width * 0.5f;
                    float middle = rect.Top + rect.Height * 0.5f;
                    g.DrawLine(RenderObjects.BorderPen, centre, rect.Top, centre, rect.Bottom);
                    g.DrawLine(RenderObjects.BorderPen, rect.Left, middle, rect.Right, middle);
                }
            }
            else if (Shape == VisualiserPointShape.Cross)
            {
                if (RenderObjects.BorderPen == null) throw new InvalidOperationException("Require BorderPen to draw Cross");
                var rect = RenderObjects.GetRectangle(point);
                if (rect.IntersectsWith(g.ClipBounds))
                {
                    g.DrawLine(RenderObjects.BorderPen, rect.Left, rect.Top, rect.Right, rect.Bottom);
                    g.DrawLine(RenderObjects.BorderPen, rect.Right, rect.Top, rect.Left, rect.Bottom);
                }
            }
            else
            {
                throw new NotSupportedException("Unsupported shape: " + Shape.ToString());
            }
        }

        #endregion

    }
}
