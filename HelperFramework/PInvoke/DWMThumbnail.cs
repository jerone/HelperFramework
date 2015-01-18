using System;
using System.Runtime.InteropServices;

namespace HelperFramework.PInvoke
{
	public class DwmThumbnail
	{

		[DllImport("dwmapi.dll")]
		public static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern int DwmUnregisterThumbnail(IntPtr thumb);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern int DwmQueryThumbnailSourceSize(IntPtr thumb, out Win32.PSIZE size);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern int DwmUpdateThumbnailProperties(IntPtr thumb, ref Win32.DWM_THUMBNAIL_PROPERTIES props);

	}
}
