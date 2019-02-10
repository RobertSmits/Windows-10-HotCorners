using System.Drawing;

namespace Windows10.HotCorners.Models
{
    internal class Corner
    {
        public CornerType CornerType { get; set; }
        public Point Point { get; set; }

        public Corner(CornerType cornerType, Point point)
        {
            CornerType = cornerType;
            Point = point;
        }
    }
}
