using System;

namespace HelperFramework.DataType
{
	/// <summary>
	/// <see cref="T:System.String"/> extensions
	/// </summary>
	public static class StringExtension
	{
		/// <summary>
		/// Remove part of <see cref="T:System.String"/>.
		/// </summary>
		/// <param name="this">The <see cref="T:System.String"/></param>
		/// <param name="value">Part of <see cref="T:System.String"/> to remove</param>
		/// <returns><see cref="T:System.String"/> without part to remove</returns>
		public static String Remove(this String @this, String value)
		{
			return @this.Replace(value, String.Empty);
		}
	}
}
