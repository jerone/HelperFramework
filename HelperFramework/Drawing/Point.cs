using System.Drawing;

namespace HelperFramework.Drawing
{
	public static class PointExtension
	{

		/// <summary>
		/// Add two <see cref="T:System.Drawing.Point"/> together.
		/// </summary>
		/// <param name="pt1">The <see cref="T:System.Drawing.Point"/> to add.</param>
		/// <param name="pt2">The <see cref="T:System.Drawing.Size"/> to add</param>
		/// <returns>
		/// The <see cref="T:System.Drawing.Point"/> that is the result of the addition operation.
		/// </returns>
		public static Point Add(this Point pt1, Point pt2)
		{
			return new Point(pt1.X + pt2.X, pt1.Y + pt2.Y);
		}
	}
}
