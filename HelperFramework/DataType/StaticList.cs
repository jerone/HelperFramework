using System;
using System.Collections.Generic;
using System.Reflection;

namespace HelperFramework.DataType
{
	/// <summary>
	/// Static List
	/// </summary>
	public class StaticList<T>
	{
		/// <summary>
		/// Get all static defined fields
		/// </summary>
		/// <param name="classTypeOverride">Check for this class type</param>
		/// <returns>Static defined fields</returns>
		public static List<T> Get(Type classTypeOverride = null)
		{
			List<T> objectsT = new List<T>();
			T defaultClass = Activator.CreateInstance<T>();
			Type classType = classTypeOverride ?? typeof(T);
			FieldInfo[] classFields = classType.GetFields(BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public);
			foreach (FieldInfo field in classFields)
			{
				T objectT = (T)field.GetValue(defaultClass);
				objectsT.Add(objectT);
			}
			return objectsT;
		}

		/// <summary>
		/// Get a static defined field
		/// </summary>
		/// <param name="fieldName">Name of the field</param>
		/// <param name="classTypeOverride">Check for this class type</param>
		/// <returns>Static defined field</returns>
		public static T Get(String fieldName, Type classTypeOverride = null)
		{
			T defaultClass = Activator.CreateInstance<T>();
			Type classType = classTypeOverride ?? typeof(T);
			FieldInfo[] classFields = classType.GetFields(BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public);
			foreach (FieldInfo field in classFields)
			{
				if (field.Name == fieldName)
				{
					T objectT = (T)field.GetValue(defaultClass);
					return objectT;
				}
			}
			return defaultClass;
		}
	}
}