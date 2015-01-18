using System;
using System.Windows.Forms;

namespace HelperFramework.Event
{
	static class Invoke
	{

		public static void SafeInvoke(this Control @this, Action action)
		{
			if (@this.InvokeRequired)
			{
				@this.BeginInvoke(action);
			}
			else
			{
				action();
			}
		}

		public static void Raise(this EventHandler @this, Object sender)
		{
			if (@this != null)
			{
				@this(sender, EventArgs.Empty);
			}
		}

		public static void Raise<T>(EventHandler<T> @this, Object sender, T e) where T : EventArgs
		{
			if (@this != null)
			{
				@this(sender, e);
			}
		}

		public static void RaiseEvent(this Object @this, EventHandler eventHandler)
		{
			var handler = eventHandler;
			if (handler != null)
			{
				handler(@this, EventArgs.Empty);
			}
		}

		public static void RaiseEvent<T>(this Object @this, EventHandler<T> eventHandler, T e) where T : EventArgs
		{
			var handler = eventHandler;
			if (handler != null)
			{
				handler(@this, e);
			}
		}

	}
}
