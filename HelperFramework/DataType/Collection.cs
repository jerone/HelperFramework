using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HelperFramework.DataType
{
	/// <summary>
	/// Collection Extensions
	/// </summary>
	/// <remarks>
	/// Use IEnumerable by default, but when altering or getting item at index use IList.
	/// </remarks>
	public static class CollectionExtension
	{

		#region Alter;

		/// <summary>
		/// Swap item to another place within a collection
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="indexA">Index A</param>
		/// <param name="indexB">Index B</param>
		/// <returns>New collection</returns>
		public static IList<T> Swap<T>(this IList<T> @this, Int32 indexA, Int32 indexB)
		{
			T temp = @this[indexA];
			@this[indexA] = @this[indexB];
			@this[indexB] = temp;
			return @this;
		}

		/// <summary>
		/// Swap item to another place within a collection
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="itemA">Item A</param>
		/// <param name="itemB">Item B</param>
		/// <returns>New collection</returns>
		public static IList<T> Swap<T>(this IList<T> @this, T itemA, T itemB)
		{
			Int32 indexA = @this.IndexOf(itemA),
				  indexB = @this.IndexOf(itemB);

			if (indexA == -1)
			{
				throw new ArgumentOutOfRangeException("itemA", "Parameter 'ItemA' is not in list '@this'!");
			}
			if (indexB == -1)
			{
				throw new ArgumentOutOfRangeException("itemB", "Parameter 'ItemB' is not in list '@this'!");
			}

			T temp = @this[indexA];
			@this[indexA] = @this[indexB];
			@this[indexB] = temp;
			return @this;
		}

		/// <summary>
		/// Swap item to the left within a collection
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="index">Index</param>
		/// <returns>New collection</returns>
		public static IList<T> SwapLeft<T>(this IList<T> @this, Int32 index)
		{
			return @this.Swap(index, index - 1);
		}

		/// <summary>
		/// Swap item to the left within a collection
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="item">Item</param>
		/// <returns>New collection</returns>
		public static IList<T> SwapLeft<T>(this IList<T> @this, T item)
		{
			Int32 index = @this.IndexOf(item);

			if (index == -1)
			{
				throw new ArgumentOutOfRangeException("item", "Parameter 'Item' is not in list '@this'!");
			}

			return @this.Swap(index, index - 1);
		}

		/// <summary>
		/// Swap item to the right within a collection
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="index">Index</param>
		/// <returns>New collection</returns>
		public static IList<T> SwapRight<T>(this IList<T> @this, Int32 index)
		{
			return @this.Swap(index, index + 1);
		}

		/// <summary>
		/// Swap item to the right within a collection
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="item">Item</param>
		/// <returns>New collection</returns>
		public static IList<T> SwapRight<T>(this IList<T> @this, T item)
		{
			Int32 index = @this.IndexOf(item);

			if (index == -1)
			{
				throw new ArgumentOutOfRangeException("item", "Parameter 'Item' is not in list '@this'!");
			}

			return @this.Swap(index, index + 1);
		}

		#endregion Alter;

		#region Action;

		/// <summary>
		/// Execute action at specified index
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="index">Index</param>
		/// <param name="actionAt">Action to execute</param>
		/// <returns>New collection</returns>
		public static IList<T> ActionAt<T>(this IList<T> @this, Int32 index, Action<T> actionAt)
		{
			actionAt(@this[index]);
			return @this;
		}

		/// <summary>
		/// Execute action on every item in the list;
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="action">Action over every item</param>
		/// <returns>New collection</returns>
		/// <remarks>Explicitly not called ForEach to not interference with ForEach and its yield function.</remarks>
		public static void Each<T>(this IEnumerable<T> @this, Action<T> action)
		{
			foreach (var item in @this)
			{
				action(item);
			}
		}

		/// <summary>
		/// Execute action on every item in the list and returns new collection;
		/// </summary>
		/// <typeparam name="TSource">Collection source type</typeparam>
		/// <typeparam name="TResult">Collection result type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="function">Action over every item</param>
		/// <returns>New collection</returns>
		public static IEnumerable<TResult> ForEach<TSource, TResult>(this IEnumerable<TSource> @this, Func<TSource, TResult> function)
		{
			foreach (var item in @this)
			{
				yield return function(item);
			}
		}

		/// <summary>
		/// Execute action on every item key in the list and returns new collection;
		/// </summary>
		/// <typeparam name="TKeySource">The type of the original key</typeparam>
		/// <typeparam name="TKeyResult">The type of the returning key</typeparam>
		/// <typeparam name="TValue">The type of the value</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="function">Action over every item key</param>
		/// <returns>New collection</returns>
		public static IEnumerable<KeyValuePair<TKeyResult, TValue>> ForEachKey<TKeySource, TKeyResult, TValue>(this IEnumerable<KeyValuePair<TKeySource, TValue>> @this, Func<TKeySource, TKeyResult> function)
		{
			return @this.ForEach(__item => new KeyValuePair<TKeyResult, TValue>(function(__item.Key), __item.Value));
		}

		/// <summary>
		/// Execute action on every item value in the list and returns new collection;
		/// </summary>
		/// <typeparam name="TKey">The type of the key</typeparam>
		/// <typeparam name="TValueSource">The type of the original value</typeparam>
		/// <typeparam name="TValueResult">The type of the returning value</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="function">Action over every item value</param>
		/// <returns>New collection</returns>
		public static IEnumerable<KeyValuePair<TKey, TValueResult>> ForEachValue<TKey, TValueSource, TValueResult>(this IEnumerable<KeyValuePair<TKey, TValueSource>> @this, Func<TValueSource, TValueResult> function)
		{
			return @this.ForEach(__item => new KeyValuePair<TKey, TValueResult>(__item.Key, function(__item.Value)));
		}

		/// <summary>
		/// Execute action on every item in the list with index as extra variable;
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="action">Action over every item</param>
		public static void Index<T>(this IEnumerable<T> @this, Action<T, Int32> action)
		{
			Int32 index = 0;
			foreach (var item in @this)
			{
				action(item, index++);
			}
		}

		#endregion Action;

		#region Randomize;

		/// <summary>
		/// Take random items
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="count">Number of items to take</param>
		/// <returns>New collection</returns>
		public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> @this, Int32 count)
		{
			return @this.Shuffle().Take(count);
		}

		/// <summary>
		/// Take random item
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <returns>Item</returns>
		public static T TakeRandom<T>(this IEnumerable<T> @this)
		{
			return @this.TakeRandom(1).Single();
		}

		/// <summary>
		/// Shuffle list
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <returns>New collection</returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> @this)
		{
			return @this.OrderBy(__item => Guid.NewGuid());
		}

		#endregion Randomize;

		#region Navigate;

		/// <summary>
		/// Get next item in collection and give first item, when last item is selected;
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="index">Index in collection</param>
		/// <returns>Next item</returns>
		public static T Next<T>(this IList<T> @this, ref Int32 index)
		{
			index = ++index >= 0 && index < @this.Count ? index : 0;
			return @this[index];
		}

		/// <summary>
		/// Get previous item in collection and give last item, when first item is selected;
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="index">Index in collection</param>
		/// <returns>Previous item</returns>
		public static T Previous<T>(this IList<T> @this, ref Int32 index)
		{
			index = --index >= 0 && index < @this.Count ? index : @this.Count - 1;
			return @this[index];
		}

		#endregion Navigate;

		#region Clone;

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <returns>Cloned collection</returns>
		public static IEnumerable<T> Clone<T>(this IEnumerable<T> @this) where T : ICloneable
		{
			return @this.Select(__item => (T)__item.Clone());
		}

		#endregion Clone;

		#region Join;

		/// <summary>
		/// Joins multiple string with Separator
		/// </summary>
		/// <param name="this">Collection</param>
		/// <param name="separator">Separator</param>
		/// <returns>Joined string</returns>
		public static String Join(this IEnumerable<String> @this, String separator = "")
		{
			return String.Join(separator, @this);
		}

		/// <summary>
		/// Joins multiple string with Separator
		/// </summary>
		/// <param name="this">Collection</param>
		/// <param name="separator">Separator</param>
		/// <returns>Joined string</returns>
		public static String Join<T>(this IEnumerable<T> @this, String separator = "")
		{
			return String.Join(separator, @this);
		}

		#endregion Join;

		#region Wrap;

		/// <summary>
		/// Wrap String with Prefix and Postfix
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">Collection</param>
		/// <param name="prefix">Prefix</param>
		/// <param name="postfix">Postfix</param>
		/// <returns>Wrapped String</returns>
		public static IEnumerable<String> Wrap<T>(this IEnumerable<T> @this, String prefix = "", String postfix = "")
		{
			return @this.ForEach(__item => prefix + __item.ToString() + postfix);
		}

		#endregion Wrap;

		#region ArrayList;

		/// <summary>
		/// Convert ArrayList to List
		/// </summary>
		/// <typeparam name="T">Collection type</typeparam>
		/// <param name="this">ArrayList</param>
		/// <returns>Collection</returns>
		public static List<T> ToList<T>(this ArrayList @this)
		{
			List<T> list = new List<T>(@this.Count);
			list.AddRange(@this.Cast<T>());
			return list;
		}

		#endregion ArrayList;

	}
}