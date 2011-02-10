using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

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

            public float Radius { get; private set; }
            public float EdgeLength { get; private set; }
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
                XOffset = xOffset;
                YOffset = yOffset;
                XScale = xScale;
                YScale = yScale;

                if (source.ScaleMode == VisualiserPointScaleMode.Pixels)
                    EdgeLength = (float)source.Size;
                else if (source.ScaleMode == VisualiserPointScaleMode.Real)
                    EdgeLength = (float)Math.Min(source.Size * minScale, 1);
                Radius = 2.0f * (float)Math.Sqrt(0.5 * EdgeLength);
            }

            public PointF GetCenterPoint(double x, double y)
            {
                return new PointF(
                    (float)((x - XOffset) * XScale),
                    (float)((y - YOffset) * YScale));
            }

            public RectangleF GetSquareRectangle(double x, double y)
            {
                return new RectangleF(
                    (float)((x - XOffset) * XScale) - EdgeLength * 0.5f,
                    (float)((y - YOffset) * YScale) - EdgeLength * 0.5f,
                    EdgeLength,
                    EdgeLength);
            }

            public RectangleF GetEllipseRectangle(double x, double y)
            {
                return new RectangleF(
                    (float)((x - XOffset) * XScale) - Radius,
                    (float)((y - YOffset) * YScale) - Radius,
                    Radius * 2,
                    Radius * 2);
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
            var pt1 = RenderObjects.GetCenterPoint(from.X, from.Y);
            var pt2 = RenderObjects.GetCenterPoint(to.X, to.Y);
            g.DrawLine(RenderObjects.LinePen, pt1, pt2);
        }

        public void RenderPoint(Graphics g, VisualiserPoint point)
        {
            if (Shape == VisualiserPointShape.Circle)
            {
                var rect = RenderObjects.GetEllipseRectangle(point.X, point.Y);
                if (rect.IntersectsWith(g.ClipBounds))
                {
                    if (RenderObjects.FillBrush != null) g.FillEllipse(RenderObjects.FillBrush, rect);
                    if (RenderObjects.BorderPen != null) g.DrawEllipse(RenderObjects.BorderPen, rect);
                }
            }
            else if (Shape == VisualiserPointShape.Square)
            {
                var rect = RenderObjects.GetSquareRectangle(point.X, point.Y);
                if (rect.IntersectsWith(g.ClipBounds))
                {
                    if (RenderObjects.FillBrush != null) g.FillRectangle(RenderObjects.FillBrush, rect);
                    if (RenderObjects.BorderPen != null) g.DrawRectangle(RenderObjects.BorderPen, rect.X, rect.Y, rect.Width, rect.Height);
                }
            }
            else if (Shape == VisualiserPointShape.Diamond)
            {
                var rect = RenderObjects.GetSquareRectangle(point.X, point.Y);
                if (rect.IntersectsWith(g.ClipBounds))
                {
                    var oldTransform = g.Transform.Clone();
                    g.RotateTransform(45.0f);
                    if (RenderObjects.FillBrush != null) g.FillRectangle(RenderObjects.FillBrush, rect);
                    if (RenderObjects.BorderPen != null) g.DrawRectangle(RenderObjects.BorderPen, rect.X, rect.Y, rect.Width, rect.Height);
                    g.Transform = oldTransform;
                }
            }
            else if (Shape == VisualiserPointShape.Plus)
            {
                if (RenderObjects.BorderPen == null) throw new InvalidOperationException("Require BorderPen to draw Plus");
                var rect = RenderObjects.GetSquareRectangle(point.X, point.Y);
                if (rect.IntersectsWith(g.ClipBounds))
                {
                    g.DrawLine(RenderObjects.BorderPen, rect.Left, rect.Top, rect.Right, rect.Bottom);
                    g.DrawLine(RenderObjects.BorderPen, rect.Right, rect.Top, rect.Left, rect.Bottom);
                }
            }
            else if (Shape == VisualiserPointShape.Cross)
            {
                if (RenderObjects.BorderPen == null) throw new InvalidOperationException("Require BorderPen to draw Cross");
                var rect = RenderObjects.GetSquareRectangle(point.X, point.Y);
                if (rect.IntersectsWith(g.ClipBounds))
                {
                    float centre = rect.Left + rect.Width * 0.5f;
                    float middle = rect.Top + rect.Height * 0.5f;
                    g.DrawLine(RenderObjects.BorderPen, centre, rect.Top, centre, rect.Bottom);
                    g.DrawLine(RenderObjects.BorderPen, rect.Left, middle, rect.Right, middle);
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
