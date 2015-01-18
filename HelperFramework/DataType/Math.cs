using System;
using System.Drawing;
using HelperFramework.Drawing;
using Point = System.Windows.Point;

namespace HelperFramework.DataType
{
	/// <summary>
	/// Math Helpers
	/// </summary>
	public static class MathHelper
	{
		/// <summary>
		/// Degrees To Radians
		/// </summary>
		/// <param name="degrees">The degrees.</param>
		/// <returns>Radians</returns>
		public static Double DegreesToRadians(Double degrees)
		{
			return (degrees / 360) * 2 * Math.PI;
		}
		/// <summary>
		/// Radians To Degrees
		/// </summary>
		/// <param name="radians">The radians.</param>
		/// <returns>Degrees</returns>
		public static Double RadiansToDegrees(Double radians)
		{
			return radians * ((360 / 2) / Math.PI);
		}

		/// <summary>
		/// Create Vector From Angle
		/// </summary>
		/// <param name="angleInDegrees">The angle in degrees.</param>
		/// <param name="length">The length.</param>
		/// <returns>Vector</returns>
		public static Vector CreateVectorFromAngle(Double angleInDegrees, Double length)
		{
			Double x = Math.Sin(DegreesToRadians(angleInDegrees)) * length;
			Double y = Math.Cos(DegreesToRadians(angleInDegrees)) * length;
			return new Vector(x, y);
		}
		/// <summary>
		/// Create Vector From Angle
		/// </summary>
		/// <param name="angleInDegrees">The angle in degrees.</param>
		/// <param name="vector">The vector.</param>
		/// <returns>Vector</returns>
		public static Vector CreateVectorFromAngle(Double angleInDegrees, Vector vector)
		{
			angleInDegrees = GetAngleFromVector(vector) + DegreesToRadians(angleInDegrees);
			Double length = GetLengthFromVector(vector);
			Double x = Math.Sin(angleInDegrees) * length;
			Double y = Math.Cos(angleInDegrees) * length;
			return new Vector(x, y);
		}
		/// <summary>
		/// Create Vector From Angle
		/// </summary>
		/// <param name="angleInDegrees">The angle in degrees.</param>
		/// <param name="vector">The vector.</param>
		/// <param name="length">The length.</param>
		/// <returns>Vector</returns>
		public static Vector CreateVectorFromAngle(Double angleInDegrees, Vector vector, Double length)
		{
			angleInDegrees = GetAngleFromVector(vector) + DegreesToRadians(angleInDegrees);
			Double x = Math.Sin(angleInDegrees) * length;
			Double y = Math.Cos(angleInDegrees) * length;
			return new Vector(x, y);
		}

		/// <summary>
		/// Get Length From Vector
		/// </summary>
		/// <param name="vector">The vector.</param>
		/// <returns>Length</returns>
		public static Double GetLengthFromVector(Vector vector)
		{
			Double x = vector.X * vector.X;
			Double y = vector.Y * vector.Y;
			return Math.Sqrt(x + y);
		}
		/// <summary>
		/// Get Angle From Vector
		/// </summary>
		/// <param name="vector">The vector.</param>
		/// <returns>Angle</returns>
		public static Double GetAngleFromVector(Vector vector)
		{
			return Math.Atan(vector.X / vector.Y);
		}

		/// <summary>
		/// Contains Point
		/// </summary>
		/// <param name="centre">The centre.</param>
		/// <param name="radius">The radius.</param>
		/// <param name="hitTest">The hit test.</param>
		/// <returns>true if circle contains point, otherwise false.</returns>
		public static Boolean ContainsPoint(Point centre, Double radius, Point hitTest)
		{
			Double x = hitTest.X - centre.X;
			Double x2 = x * x;
			Double y = hitTest.Y - centre.Y;
			Double y2 = y * y;
			return radius * radius >= (x2 + y2);
		}

		/// <summary>
		/// Divides a rectangle with an value;
		/// </summary>
		/// <param name="source">The rectangle;</param>
		/// <param name="divider">The divider;</param>
		/// <returns>A new rectangle divided by the <paramref name="divider"/>;</returns>
		public static Rectangle Divide(Rectangle source, Int32 divider)
		{
			return new Rectangle(source.Left / divider, source.Top / divider, source.Width / divider, source.Height / divider);
		}
	}
}
