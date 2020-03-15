using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownLevelEditor.ViewModels
{
    [Serializable]
    public class Point2Int : NotifyBase
    {
        private int _X = 0;
        public int X
        {
            get => _X;
            set { _X = value; NotifyPropertyChanged(); }
        }

        private int _Y = 0;
        public int Y
        {
            get => _Y;
            set { _Y = value; NotifyPropertyChanged(); }
        }

        public static implicit operator Point2Int(Point2 point)
            => new Point2Int((int)point.X, (int)point.Y);

        public static Point2Int operator +(Point2Int left, Point2Int right)
            => new Point2Int(left.X + right.X, left.Y + right.Y);

        public static Point2Int operator -(Point2Int left, Point2Int right)
            => new Point2Int(left.X - right.X, left.Y - right.Y);

        public static Point2Int operator *(Point2Int left, double right)
            => new Point2Int((int)(left.X * right), (int)(left.Y * right));

        public static Point2Int operator *(double left, Point2Int right)
            => new Point2Int((int)(left * right.X), (int)(left * right.Y));

        public static Point2Int operator *(Point2Int left, Point2Int right)
            => new Point2Int(left.X * right.X, left.Y * right.Y);

        public static Point2Int operator /(Point2Int left, Point2Int right)
            => new Point2Int(left.X / right.X, left.Y / right.Y);

        public static Point2Int operator /(Point2Int left, double right)
            => new Point2Int((int)(left.X / right), (int)(left.Y / right));

        public static Point2Int operator /(Point2Int left, int right)
            => new Point2Int((int)(left.X / right), (int)(left.Y / right));

        public Point2Int()
        {

        }

        public Point2Int(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
