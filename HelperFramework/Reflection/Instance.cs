using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace HelperFramework.Reflection
{
	/// <summary>
	/// Class to extend .NET functionality;
	/// </summary>
	public static class Instance
	{
		/// <summary>
		/// Checks of current window is already running;
		/// </summary>
		/// <param name="windowTitle">Window title</param>
		/// <returns>true if single instance, false if already running</returns>
		public static Boolean IsSingleInstance(String windowTitle)
		{
			return Process.GetProcesses().All(__process => __process.MainWindowTitle != windowTitle);
		}

		public static Object CreateANewInstance(this Type type)
		{
			Assembly a = Assembly.Load(type.Assembly.FullName);
			return a.CreateInstance(type.FullName);
		}
	}
}
