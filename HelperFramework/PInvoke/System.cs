using System;
using System.Runtime.InteropServices;

namespace HelperFramework.PInvoke
{
	public class System
	{
		[DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		public static extern IntPtr SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, String lParam);

		[DllImport("user32.dll")]
		public static extern int GetSystemMetrics(int smIndex);
	}
}
