using System;

namespace HelperFramework.DataType
{
	// <summary>
	// Number Helper
	// </summary>
	// <remarks>
	// There is no identiefier for all numeric datatypes. 
	// Using the using-shortcut allows us to copy &amp; paste the code below for all numeric datatypes.
	// Downside is that we need to copy &amp; paste the code and define another namespace, but we don't need to alter the code;
	// </remarks>
	namespace Number
	{
		namespace Int16
		{
			// Define nummeric datatype here;
			using XNumber = System.Int16;

			/// <summary>
			/// Number Helper
			/// </summary>
			public static class NumberHelper
			{

				#region Between;

				/// <summary>
				/// Check if number is between a lower and higher number.
				/// </summary>
				/// <param name="this">Number</param>
				/// <param name="lower">Lower number</param>
				/// <param name="upper">Upper number</param>
				/// <returns>True when number is between arguments, other false</returns>
				public static Boolean Between(this XNumber @this, XNumber lower, XNumber upper)
				{
					return lower < @this && @this < upper;
				}

				/// <summary>
				/// Check if number is between or equal to a lower and higher number.
				/// </summary>
				/// <param name="this">Number</param>
				/// <param name="lower">Lower number</param>
				/// <param name="upper">Upper number</param>
				/// <returns>True when number is between arguments, other false</returns>
				public static Boolean BetweenOrEqual(this XNumber @this, XNumber lower, XNumber upper)
				{
					return lower <= @this && @this <= upper;
				}

				/// <summary>
				/// Check if number is between or on the lower side equal to a lower and higher number.
				/// </summary>
				/// <param name="this">Number</param>
				/// <param name="lower">Lower number</param>
				/// <param name="upper">Upper number</param>
				/// <returns>True when number is between arguments, other false</returns>
				public static Boolean BetweenLeft(this XNumber @this, XNumber lower, XNumber upper)
				{
					return lower <= @this && @this < upper;
				}

				/// <summary>
				/// Check if number is between or on the upper side equal to a lower and higher number.
				/// </summary>
				/// <param name="this">Number</param>
				/// <param name="lower">Lower number</param>
				/// <param name="upper">Upper number</param>
				/// <returns>True when number is between arguments, other false</returns>
				public static Boolean BetweenRight(this XNumber @this, XNumber lower, XNumber upper)
				{
					return lower < @this && @this <= upper;
				}

				#endregion Between;

			}
		}
		namespace Int32
		{
			// Define nummeric datatype here;
			using XNumber = System.Int32;

			/// <summary>
			/// Number Helper
			/// </summary>
			public static class NumberHelper
			{

				#region Between;

				/// <summary>
				/// Check if number is between a lower and higher number.
				/// </summary>
				/// <param name="this">Number</param>
				/// <param name="lower">Lower number</param>
				/// <param name="upper">Upper number</param>
				/// <returns>True when number is between arguments, other false</returns>
				public static Boolean Between(this XNumber @this, XNumber lower, XNumber upper)
				{
					return lower < @this && @this < upper;
				}

				/// <summary>
				/// Check if number is between or equal to a lower and higher number.
				/// </summary>
				/// <param name="this">Number</param>
				/// <param name="lower">Lower number</param>
				/// <param name="upper">Upper number</param>
				/// <returns>True when number is between arguments, other false</returns>
				public static Boolean BetweenOrEqual(this XNumber @this, XNumber lower, XNumber upper)
				{
					return lower <= @this && @this <= upper;
				}

				/// <summary>
				/// Check if number is between or on the lower side equal to a lower and higher number.
				/// </summary>
				/// <param name="this">Number</param>
				/// <param name="lower">Lower number</param>
				/// <param name="upper">Upper number</param>
				/// <returns>True when number is between arguments, other false</returns>
				public static Boolean BetweenLeft(this XNumber @this, XNumber lower, XNumber upper)
				{
					return lower <= @this && @this < upper;
				}

				/// <summary>
				/// Check if number is between or on the upper side equal to a lower and higher number.
				/// </summary>
				/// <param name="this">Number</param>
				/// <param name="lower">Lower number</param>
				/// <param name="upper">Upper number</param>
				/// <returns>True when number is between arguments, other false</returns>
				public static Boolean BetweenRight(this XNumber @this, XNumber lower, XNumber upper)
				{
					return lower < @this && @this <= upper;
				}

				#endregion Between;

			}
		}
		namespace Int64
		{
			// Define nummeric datatype here;
			using XNumber = System.Int64;

			/// <summary>
			/// Number Helper
			/// </summary>
			public static class NumberHelper
			{

				#region Between;

				/// <summary>
				/// Check if number is between a lower and higher number.
				/// </summary>
				/// <param name="this">Number</param>
				/// <param name="lower">Lower number</param>
				/// <param name="upper">Upper number</param>
				/// <returns>True when number is between arguments, other false</returns>
				public static Boolean Between(this XNumber @this, XNumber lower, XNumber upper)
				{
					return lower < @this && @this < upper;
				}

				/// <summary>
				/// Check if number is between or equal to a lower and higher number.
				/// </summary>
				/// <param name="this">Number</param>
				/// <param name="lower">Lower number</param>
				/// <param name="upper">Upper number</param>
				/// <returns>True when number is between arguments, other false</returns>
				public static Boolean BetweenOrEqual(this XNumber @this, XNumber lower, XNumber upper)
				{
					return lower <= @this && @this <= upper;
				}

				/// <summary>
				/// Check if number is between or on the lower side equal to a lower and higher number.
				/// </summary>
				/// <param name="this">Number</param>
				/// <param name="lower">Lower number</param>
				/// <param name="upper">Upper number</param>
				/// <returns>True when number is between arguments, other false</returns>
				public static Boolean BetweenLeft(this XNumber @this, XNumber lower, XNumber upper)
				{
					return lower <= @this && @this < upper;
				}

				/// <summary>
				/// Check if number is between or on the upper side equal to a lower and higher number.
				/// </summary>
				/// <param name="this">Number</param>
				/// <param name="lower">Lower number</param>
				/// <param name="upper">Upper number</param>
				/// <returns>True when number is between arguments, other false</returns>
				public static Boolean BetweenRight(this XNumber @this, XNumber lower, XNumber upper)
				{
					return lower < @this && @this <= upper;
				}

				#endregion Between;

			}
		}
	}
}