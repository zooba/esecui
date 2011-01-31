using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace esecui
{
    /// <summary>
    /// The shape to draw the point as. The size value is treated as a bounding circle for non-circular shapes.
    /// </summary>
    public enum VisualiserPointShape
    {
        Circle,
        FilledCircle,
        Square,
        FilledSquare,
        Diamond,
        FilledDiamond,
        Cross,
        Plus
    }

    /// <summary>
    /// The mode used to adjust the size of a point.
    /// </summary>
    public enum VisualiserPointScaleMode
    {
        /// <summary>
        /// Treat the size as a pixel radius. The range does not affect the size of the point.
        /// </summary>
        Pixels,
        /// <summary>
        /// Treat the size as an unmapped radius. A bigger range will result in a smaller point.
        /// </summary>
        Real
    }

    /// <summary>
    /// The details of a point to draw.
    /// </summary>
    public struct VisualiserPoint
    {
        /// <summary>The unmapped horizontal location of the point.</summary>
        public double X;
        /// <summary>The unmapped vertical location of the point.</summary>
        public double Y;
        /// <summary>The color to render the point with.</summary>
        public Color Color;
        /// <summary>The size to render the point.</summary>
        public double Size;
        /// <summary>The shape to render the point.</summary>
        public VisualiserPointShape Shape;
        /// <summary>The scale mode used to adjust <see cref="Size"/>.</summary>
        public VisualiserPointScaleMode Scale;

        /// <summary>
        /// A point with default values.
        /// </summary>
        public static readonly VisualiserPoint Empty = new VisualiserPoint();

        /// <summary>
        /// Instantiates a fully specified point.
        /// </summary>
        /// <param name="x">The horizontal position.</param>
        /// <param name="y">The vertical position.</param>
        /// <param name="color">The color of the point.</param>
        /// <param name="size">The size of the point.</param>
        /// <param name="shape">The shape of the point.</param>
        /// <param name="scale">The scale of the point.</param>
        public VisualiserPoint(double x, double y, Color color, double size,
            VisualiserPointShape shape, VisualiserPointScaleMode scale)
        {
            X = x;
            Y = y;
            Color = color;
            Size = size;
            Shape = shape;
            Scale = scale;
        }

        /// <summary>The color to use when implicitly creating points.</summary>
        public static Color DefaultColor { get; set; }
        /// <summary>The size to use when implicitly creating points.</summary>
        public static double DefaultSize { get; set; }
        /// <summary>The shape to use when implicitly creating points.</summary>
        public static VisualiserPointShape DefaultShape { get; set; }
        /// <summary>The scale mode to use when implicitly creating points.</summary>
        public static VisualiserPointScaleMode DefaultScale { get; set; }

        static VisualiserPoint()
        {
            DefaultColor = Color.RoyalBlue;
            DefaultSize = 5.0;
            DefaultShape = VisualiserPointShape.Circle;
            DefaultScale = VisualiserPointScaleMode.Pixels;
        }

        public VisualiserPoint(double x, double y)
            : this(x, y, DefaultColor, DefaultSize, DefaultShape, DefaultScale)
        { }
        /// <summary>
        /// Implicitly casts a <see cref="Point"/> to a point for visualisation.
        /// </summary>
        /// <param name="point">The source point.</param>
        /// <returns>A <see cref="VisualiserPoint"/>.</returns>
        public static implicit operator VisualiserPoint(Point point)
        {
            return new VisualiserPoint(point.X, point.Y, DefaultColor, DefaultSize, DefaultShape, DefaultScale);
        }
    }
}
