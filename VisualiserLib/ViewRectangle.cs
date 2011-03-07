using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Threading;

namespace VisualiserLib
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ViewRectangle : INotifyPropertyChanged
    {
        #region Constructors and Machinery

        public ViewRectangle()
            : this(0.0, 0.0, 0.0, 0.0)
        { }

        public ViewRectangle(double left, double top, double right, double bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}] - [{2}, {3}]", Left, Top, Right, Bottom);
        }

        public override bool Equals(object obj)
        {
            var vr = obj as ViewRectangle;
            if (vr == null) return false;

            return Left == vr.Left && Top == vr.Top && Right == vr.Right && Bottom == vr.Bottom;
        }

        public override int GetHashCode()
        {
            return RectangleF.FromLTRB((float)Left, (float)Top, (float)Right, (float)Bottom).GetHashCode();
        }

        public ViewRectangle Clone()
        {
            return new ViewRectangle(Left, Top, Right, Bottom);
        }

        #endregion

        #region Size Updaters

        public void ShrinkToWithin(ViewRectangle within)
        {
            ShrinkToWithin(within.Left, within.Top, within.Right, within.Bottom);
        }

        public void ShrinkToWithin(double left, double top, double right, double bottom)
        {
            if ((Left <= Right) != (left <= right)) left = Interlocked.Exchange(ref right, left);
            if ((Top <= Bottom) != (top <= bottom)) top = Interlocked.Exchange(ref bottom, top);

            if (Left <= Right)
            {
                if (Left < left) Left = left;
                if (Right > right) Right = right;
            }
            else
            {
                if (Right < right) Right = right;
                if (Left > left) Left = left;
            }

            if (Top <= Bottom)
            {
                if (Top < top) Top = top;
                if (Bottom > bottom) Bottom = bottom;
            }
            else
            {
                if (Bottom < bottom) Bottom = bottom;
                if (Top > top) Top = top;
            }
        }

        public void GrowToSquare()
        {
            double w = Math.Abs(Width);
            double h = Math.Abs(Height);

            if (w > h)
            {
                double delta = (w - h) * 0.5;
                if (Top <= Bottom) { Top -= delta; Bottom += delta; }
                else { Bottom -= delta; Top += delta; }
            }
            else if (w < h)
            {
                double delta = (h - w) * 0.5;
                if (Left <= Right) { Left -= delta; Right += delta; }
                else { Left += delta; Right -= delta; }
            }
        }

        public void ShrinkToSquare()
        {
            double w = Math.Abs(Width);
            double h = Math.Abs(Height);

            if (w < h)
            {
                double delta = (w - h) * 0.5;
                if (Top <= Bottom) { Top += delta; Bottom -= delta; }
                else { Bottom += delta; Top -= delta; }
            }
            else if (w > h)
            {
                double delta = (h - w) * 0.5;
                if (Left <= Right) { Left -= delta; Right += delta; }
                else { Left += delta; Right -= delta; }
            }
        }

        public void GrowToInclude(ViewRectangle rect)
        {
            GrowToInclude(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public void GrowToInclude(double left, double top, double right, double bottom)
        {
            if ((Left <= Right) != (left <= right)) left = Interlocked.Exchange(ref right, left);
            if ((Top <= Bottom) != (top <= bottom)) top = Interlocked.Exchange(ref bottom, top);

            if (Left <= Right)
            {
                if (left < Left) Left = left;
                if (right > Right) Right = right;
            }
            else
            {
                if (right < Right) Right = right;
                if (left > Left) Left = left;
            }
            if (Top <= Bottom)
            {
                if (top < Top) Top = top;
                if (bottom > Bottom) Bottom = bottom;
            }
            else
            {
                if (bottom < Bottom) Bottom = bottom;
                if (top > Top) Top = top;
            }
        }

        public void GrowToInclude(double? x, double? y, bool preferLeftToRight = true, bool preferTopToBottom = true)
        {
            if (x.HasValue)
            {
                if (Left < Right || Left == Right && preferLeftToRight)
                {
                    if (x.Value < Left) Left = x.Value;
                    if (x.Value > Right) Right = x.Value;
                }
                else
                {
                    if (x.Value < Right) Right = x.Value;
                    if (x.Value > Left) Left = x.Value;
                }
            }
            if (y.HasValue)
            {
                if (Top < Bottom || Top == Bottom && preferTopToBottom)
                {
                    if (y.Value < Top) Top = y.Value;
                    if (y.Value > Bottom) Bottom = y.Value;
                }
                else
                {
                    if (y.Value < Bottom) Bottom = y.Value;
                    if (y.Value > Top) Top = y.Value;
                }
            }
        }

        #endregion

        #region Mapping/Unmapping

        public RectangleF Map(Rectangle clientRect, RectangleF source)
        {
            return Map(clientRect, source.Left, source.Top, source.Width, source.Height);
        }

        public RectangleF Map(Rectangle clientRect, double left, double top, double width, double height)
        {
            double padX = Padding * Width;
            double padY = Padding * Height;

            double l = (left - Left + padX * 0.5) / (Width + padX);
            double t = (top - Top + padY * 0.5) / (Height + padY);
            double w = width / (Width + padX);
            double h = height / (Height + padY);
            if (w < 0.0) { l += w; w = -w; }
            if (h < 0.0) { t += h; h = -h; }
            return new RectangleF(
                (float)(l * clientRect.Width),
                (float)(t * clientRect.Height),
                (float)(w * clientRect.Width),
                (float)(h * clientRect.Height)
            );
        }

        public PointF Map(Rectangle clientRect, PointF source)
        {
            return Map(clientRect, source.X, source.Y);
        }

        public PointF Map(Rectangle clientRect, double x, double y)
        {
            double padX = Padding * Width;
            double padY = Padding * Height;

            return new PointF(
                (float)(((x - Left + padX * 0.5) / (Width + padX)) * clientRect.Width),
                (float)(((y - Top + padY * 0.5) / (Height + padY)) * clientRect.Height)
            );
        }


        public ViewRectangle Unmap(Rectangle clientRect, RectangleF source)
        {
            double padX = Padding * Width;
            double padY = Padding * Height;

            double l = source.Left * (Width + padX) / clientRect.Width + Left - padX * 0.5;
            double t = source.Top * (Height + padY) / clientRect.Height + Top - padY * 0.5;
            double w = source.Width * (Width + padX) / clientRect.Width;
            double h = source.Height * (Height + padY) / clientRect.Height;
            return new ViewRectangle(l, t, l + w, t + h);
        }

        public PointF Unmap(Rectangle clientRect, PointF source)
        {
            double padX = Padding * Width;
            double padY = Padding * Height;

            return new PointF(
                (float)((source.X / clientRect.Width) * (Width + padX) + Left - padX * 0.5),
                (float)((source.Y / clientRect.Height) * (Height + padY) + Top - padY * 0.5)
            );
        }

        #endregion

        #region Extremities

        const double DefaultPadding = 0.1;
        private double _Padding = DefaultPadding;
        [Browsable(true), DefaultValue(DefaultPadding)]
        [Description("The fraction of width or height to add as padding.")]
        public double Padding
        {
            get { return _Padding; }
            set
            {
                _Padding = value;
                OnPropertyChanged("Padding");
            }
        }

        const double DefaultLeft = 0.0;
        private double _Left = DefaultLeft;
        [Browsable(true), DefaultValue(DefaultLeft)]
        [Description("The untransformed value to display at the left edge of the view.")]
        [NotifyParentProperty(true)]
        public double Left
        {
            get { return _Left; }
            set
            {
                _Left = value;
                OnPropertyChanged("Left");
            }
        }

        const double DefaultTop = 0.0;
        private double _Top = DefaultTop;
        [Browsable(true), DefaultValue(DefaultTop)]
        [Description("The untransformed value to display at the top edge of the view.")]
        [NotifyParentProperty(true)]
        public double Top
        {
            get { return _Top; }
            set
            {
                _Top = value;
                OnPropertyChanged("Top");
            }
        }

        const double DefaultRight = 0.0;
        private double _Right = DefaultRight;
        [Browsable(true), DefaultValue(DefaultRight)]
        [Description("The untransformed value to display at the right edge of the view.")]
        [NotifyParentProperty(true)]
        public double Right
        {
            get { return _Right; }
            set
            {
                _Right = value;
                OnPropertyChanged("Right");
            }
        }

        const double DefaultBottom = 0.0;
        private double _Bottom = DefaultBottom;
        [Browsable(true), DefaultValue(DefaultBottom)]
        [Description("The untransformed value to display at the bottom edge of the view.")]
        [NotifyParentProperty(true)]
        public double Bottom
        {
            get { return _Bottom; }
            set
            {
                _Bottom = value;
                OnPropertyChanged("Bottom");
            }
        }

        [Browsable(false)]
        public double Width
        {
            get { return _Right - _Left; }
        }

        [Browsable(false)]
        public double Height
        {
            get { return _Bottom - _Top; }
        }

        [Browsable(false)]
        public bool FlippedHorizontally
        {
            get { return Right < Left; }
        }

        [Browsable(false)]
        public bool FlippedVertically
        {
            get { return Bottom < Top; }
        }

        #endregion

        #region Property Changed Event

        private void OnPropertyChanged(string propertyName)
        {
            var t = PropertyChanged;
            if (t != null)
            {
                t(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
