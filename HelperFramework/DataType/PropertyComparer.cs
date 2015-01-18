using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace HelperFramework.DataType
{
	public class PropertyComparer<T> : IComparer<T>
	{
		private readonly IComparer _comparer;
		private PropertyDescriptor _propertyDescriptor;
		private Int32 _reverse;

		public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
		{
			_propertyDescriptor = property;
			Type comparerForPropertyType = typeof(Comparer<>).MakeGenericType(property.PropertyType);
			_comparer = (IComparer)comparerForPropertyType.InvokeMember("Default", BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.Public, null, null, null);
			SetListSortDirection(direction);
		}

		#region IComparer<T> Members;

		/// <summary>
		/// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
		/// </summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>
		/// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>, as shown in the following table.Value Meaning Less than zero<paramref name="x"/> is less than <paramref name="y"/>.Zero<paramref name="x"/> equals <paramref name="y"/>.Greater than zero<paramref name="x"/> is greater than <paramref name="y"/>.
		/// </returns>
		public int Compare(T x, T y)
		{
			return _reverse * _comparer.Compare(_propertyDescriptor.GetValue(x), _propertyDescriptor.GetValue(y));
		}

		#endregion IComparer<T> Members;

		private void SetPropertyDescriptor(PropertyDescriptor descriptor)
		{
			_propertyDescriptor = descriptor;
		}

		private void SetListSortDirection(ListSortDirection direction)
		{
			_reverse = direction == ListSortDirection.Ascending ? 1 : -1;
		}

		public void SetPropertyAndDirection(PropertyDescriptor descriptor, ListSortDirection direction)
		{
			SetPropertyDescriptor(descriptor);
			SetListSortDirection(direction);
		}
	}
}