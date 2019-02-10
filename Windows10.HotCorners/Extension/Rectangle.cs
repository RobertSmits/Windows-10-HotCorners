using System.Collections.Generic;
using System.Drawing;
using Windows10.HotCorners.Models;

namespace Windows10.HotCorners.Extension
{
    internal static class RectangleExtensions
    {
        public static IEnumerable<Corner> GetCorners(this Rectangle rectangle)
        {
            return new[]
            {
                new Corner(CornerType.LeftTop,     new Point(rectangle.X, rectangle.Y)),
                new Corner(CornerType.LeftBottom, new Point(rectangle.X, rectangle.Y + rectangle.Height - 1)),
                new Corner(CornerType.RightTop, new Point(rectangle.X + rectangle.Width - 1, rectangle.Y)),
                new Corner(CornerType.RightBottom, new Point(rectangle.X + rectangle.Width - 1, rectangle.Y + rectangle.Height - 1)),
            };
        }
    }
}
