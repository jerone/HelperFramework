using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HelperFramework.PInvoke;

namespace HelperFramework.WinForms
{
	public class CueTextBox : TextBox
	{

		#region CueText;

		private String _cueText = String.Empty;

		/// <summary>
		/// Gets or sets the text the <see cref="TextBox"/> will display as a cue to the user.
		/// </summary>
		[Description("The text value to be displayed as a cue to the user.")]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public String CueText
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
				PInvoke.System.SendMessage(new HandleRef(this, Handle), Win32.EM.SETCUEBANNER, (_showCueTextWithFocus) ? new IntPtr(1) : IntPtr.Zero, _cueText);
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			UpdateCue();

			base.OnHandleCreated(e);
		}

		#endregion CueText;

		#region CharacterCasing;
		// CharacterCasing enforces also the cue text, so we need to override that;

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			switch (CharacterCasing)
			{
				case CharacterCasing.Lower:
					if (Char.IsUpper(e.KeyChar))
					{
						e.KeyChar = Char.ToLower(e.KeyChar);
					}
					break;
				case CharacterCasing.Upper:
					if (Char.IsLower(e.KeyChar))
					{
						e.KeyChar = Char.ToUpper(e.KeyChar);
					}
					break;
			}
			base.OnKeyPress(e);
		}

		private CharacterCasing _characterCasing = CharacterCasing.Normal;
		/// <summary>
		/// Gets or sets whether the <see cref="T:System.Windows.Forms.TextBox"/> control modifies the case of characters as they are typed.
		/// </summary>
		/// 
		/// <returns>
		/// One of the <see cref="T:System.Windows.Forms.CharacterCasing"/> enumeration values that specifies whether the <see cref="T:System.Windows.Forms.TextBox"/> control modifies the case of characters. The default is CharacterCasing.Normal.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[DefaultValue(CharacterCasing.Normal)]
		public new CharacterCasing CharacterCasing
		{
			get
			{
				return _characterCasing;
			}
			set
			{
				_characterCasing = value;
			}
		}

		#endregion CharacterCasing;

		#region ShowCueTextOnFocus;

		private Boolean _showCueTextWithFocus;

		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="TextBox"/> will display the <see cref="CueText"/>
		/// even when the control has focus.
		/// </summary>
		[Description("Indicates whether the CueText will be displayed even when the control has focus.")]
		[Category("Appearance")]
		[DefaultValue(false)]
		[Localizable(true)]
		public Boolean ShowCueTextWithFocus
		{
			get { return _showCueTextWithFocus; }
			set
			{
				if (_showCueTextWithFocus != value)
				{
					_showCueTextWithFocus = value;
					UpdateCue();
					OnShowCueTextWithFocusChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Occurs when the <see cref="ShowCueTextWithFocus"/> property value changes.
		/// </summary>
		public event EventHandler ShowCueTextWithFocusChanged;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnShowCueTextWithFocusChanged(EventArgs e)
		{
			EventHandler handler = ShowCueTextWithFocusChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		#endregion ShowCueTextOnFocus;

	}
}