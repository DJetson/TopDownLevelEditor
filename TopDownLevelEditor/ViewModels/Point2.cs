using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public class Point2 : NotifyBase
    {
        private double _X = 0;
        public double X
        {
            get => _X;
            set { _X = value; NotifyPropertyChanged(); }
        }

        private double _Y = 0;
        public double Y
        {
            get => _Y;
            set { _Y = value; NotifyPropertyChanged(); }
        }

        public static implicit operator Point2(Point2Int point)
           => new Point2((double)point.X, (double)point.Y);

        public static Point2 operator +(Point2 left, Point2 right)
            => new Point2(left.X + right.X, left.Y + right.Y);

        public static Point2 operator -(Point2 left, Point2 right)
            => new Point2(left.X - right.X, left.Y - right.Y);

        public static Point2 operator *(Point2 left, double right)
            => new Point2(left.X * right, left.Y * right);

        public static Point2 operator *(double left, Point2 right)
            => new Point2(left * right.X, left * right.Y);

        public static Point2 operator *(Point2 left, Point2 right)
            => new Point2(left.X * right.X, left.Y * right.Y);

        public static Point2 operator /(Point2 left, Point2 right)
            => new Point2(left.X / right.X, left.Y / right.Y);

        public static Point2 operator /(Point2 left, double right)
            => new Point2(left.X / right, left.Y / right);

        public static Point2 operator /(Point2 left, int right)
            => new Point2((int)(left.X / right), (int)(left.Y / right));

        public Point2()
        {

        }

        public Point2(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
