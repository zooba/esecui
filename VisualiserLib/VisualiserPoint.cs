using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace VisualiserLib
{
    /// <summary>
    /// The details of a point to draw.
    /// </summary>
    public struct VisualiserPoint
    {
        /// <summary>The unmapped horizontal location of the point.</summary>
        public double X;
        /// <summary>The unmapped vertical location of the point.</summary>
        public double Y;
        /// <summary>The unmapped size of the point. If this is zero, the size
        /// specified by the style is used.</summary>
        public double Z;
        /// <summary>The style to render the point with.</summary>
        public VisualiserPointStyle Style;

        /// <summary>
        /// A point with default values.
        /// </summary>
        public static readonly VisualiserPoint Empty = new VisualiserPoint();

        /// <summary>The style to use when implicitly creating points.</summary>
        public static VisualiserPointStyle DefaultStyle { get; set; }

        static VisualiserPoint()
        {
            DefaultStyle = new VisualiserPointStyle();
        }

        /// <summary>
        /// Initialises a point.
        /// </summary>
        /// <param name="x">The horizonal position.</param>
        /// <param name="y">The vertical position.</param>
        /// <param name="z">The size.</param>
        /// <param name="style">The style to render the point with. If
        /// <c>null</c>, <see cref="DefaultStyle"/> is used.</param>
        public VisualiserPoint(double x, double y, double z = 0.0, VisualiserPointStyle style = null)
        {
            X = x;
            Y = y;
            Z = z;
            Style = style ?? DefaultStyle;
        }

        /// <summary>
        /// Implicitly casts a <see cref="Point"/> to a point for visualisation.
        /// </summary>
        /// <param name="point">The source point.</param>
        /// <returns>A <see cref="VisualiserPoint"/>.</returns>
        public static implicit operator VisualiserPoint(Point point)
        {
            return new VisualiserPoint(point.X, point.Y);
        }
    }
}
