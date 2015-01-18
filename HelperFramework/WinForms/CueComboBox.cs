using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HelperFramework.PInvoke;

namespace HelperFramework.WinForms
{
	public class CueComboBox : ComboBox
	{

		#region CueText;

		private string _cueText = String.Empty;

		/// <summary>
		/// Gets or sets the text the <see cref="ComboBox"/> will display as a cue to the user.
		/// </summary>
		[Description("The text value to be displayed as a cue to the user.")]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string CueText
		{
			get { return _cueText; }
			set
			{
				if (value == null)
				{
					value = String.Empty;
				}

				if (!_cueText.Equals(value, StringComparison.CurrentCulture))
				{
					_cueText = value;
					UpdateCue();
					OnCueTextChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Occurs when the <see cref="CueText"/> property value changes.
		/// </summary>
		public event EventHandler CueTextChanged;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnCueTextChanged(EventArgs e)
		{
			EventHandler handler = CueTextChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		private void UpdateCue()
		{
			// If the handle isn't yet created, 
			// this will be called when it is created
			if (IsHandleCreated)
			{
				PInvoke.System.SendMessage(new HandleRef(this, Handle), Win32.CB.SETCUEBANNER, IntPtr.Zero, _cueText);
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			UpdateCue();

			base.OnHandleCreated(e);
		}

		#endregion CueText;

	}
}
