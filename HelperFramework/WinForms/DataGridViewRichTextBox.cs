using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HelperFramework.WinForms
{
	/// <summary>
	/// DataGridView RichTextBox Column
	/// </summary>
	public class DataGridViewRichTextBoxColumn : DataGridViewColumn
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DataGridViewRichTextBoxColumn"/> class.
		/// </summary>
		public DataGridViewRichTextBoxColumn()
			: base(new DataGridViewRichTextBoxCell())
		{
		}

		/// <summary>
		/// Gets or sets CellTemplate.
		/// </summary>
		/// <exception cref="InvalidCastException">
		/// CellTemplate must be a DataGridViewRichTextBoxCell
		/// </exception>
		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				if (!(value is DataGridViewRichTextBoxCell))
				{
					throw new InvalidCastException("CellTemplate must be a DataGridViewRichTextBoxCell");
				}

				base.CellTemplate = value;
			}
		}
	}

	/// <summary>
	/// DataGridView RichTextBox Cell
	/// </summary>
	public class DataGridViewRichTextBoxCell : DataGridViewTextBoxCell
	{
		/// <summary>
		/// Editing Control
		/// </summary>
		private static readonly RichTextBox EditingControl = new RichTextBox();

		/// <summary>
		/// Edit Type
		/// </summary>
		public override Type EditType
		{
			get
			{
				return typeof(DataGridViewRichTextBoxEditingControl);
			}
		}

		/// <summary>
		/// Value Type
		/// </summary>
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
			set
			{
				base.ValueType = value;
			}
		}

		/// <summary>
		/// Formatted Value Type
		/// </summary>
		public override Type FormattedValueType
		{
			get
			{
				return typeof(string);
			}
		}

		/// <summary>
		/// Get Inherited Style
		/// </summary>
		/// <param name="inheritedCellStyle">inherited Cell Style</param>
		/// <param name="rowIndex">row Index</param>
		/// <param name="includeColors">include Colors</param>
		/// <returns>inherited cell style</returns>
		public override DataGridViewCellStyle GetInheritedStyle(
			DataGridViewCellStyle inheritedCellStyle, Int32 rowIndex, Boolean includeColors)
		{
			DataGridViewCellStyle inheritedStyle = base.GetInheritedStyle(inheritedCellStyle, rowIndex, includeColors);
			inheritedStyle.Padding = new Padding(5);
			return inheritedStyle;
		}

		/// <summary>
		/// Initialize Editing Control
		/// </summary>
		/// <param name="rowIndex">
		/// The row index.
		/// </param>
		/// <param name="initialFormattedValue">
		/// The initial formatted value.
		/// </param>
		/// <param name="dataGridViewCellStyle">
		/// The data grid view cell style.
		/// </param>
		public override void InitializeEditingControl(
			Int32 rowIndex, Object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

			RichTextBox ctl = DataGridView.EditingControl as RichTextBox;

			if (ctl != null)
			{
				SetRichTextBoxText(ctl, Convert.ToString(initialFormattedValue));
			}
		}

		/// <summary>
		/// Paint method
		/// </summary>
		/// <param name="graphics">
		/// The graphics.
		/// </param>
		/// <param name="clipBounds">
		/// The clip bounds.
		/// </param>
		/// <param name="cellBounds">
		/// The cell bounds.
		/// </param>
		/// <param name="rowIndex">
		/// The row index.
		/// </param>
		/// <param name="cellState">
		/// The cell state.
		/// </param>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <param name="formattedValue">
		/// The formatted value.
		/// </param>
		/// <param name="errorText">
		/// The error text.
		/// </param>
		/// <param name="cellStyle">
		/// The cell style.
		/// </param>
		/// <param name="advancedBorderStyle">
		/// The advanced border style.
		/// </param>
		/// <param name="paintParts">
		/// The paint parts.
		/// </param>
		protected override void Paint(
			Graphics graphics,
			Rectangle clipBounds,
			Rectangle cellBounds,
			Int32 rowIndex,
			DataGridViewElementStates cellState,
			Object value,
			Object formattedValue,
			String errorText,
			DataGridViewCellStyle cellStyle,
			DataGridViewAdvancedBorderStyle advancedBorderStyle,
			DataGridViewPaintParts paintParts)
		{
			base.Paint(
				graphics,
				clipBounds,
				cellBounds,
				rowIndex,
				cellState,
				null,
				null,
				errorText,
				cellStyle,
				advancedBorderStyle,
				paintParts);

			Image img = GetRtfImage(rowIndex, formattedValue ?? value, this.Selected, cellStyle);

			if (img != null)
			{
				graphics.DrawImage(img, cellBounds.Left, cellBounds.Top);
			}
		}

		/// <summary>
		/// Set RichTextBox Text
		/// </summary>
		/// <param name="ctl">
		/// The ctl.
		/// </param>
		/// <param name="text">
		/// The text.
		/// </param>
		private void SetRichTextBoxText(RichTextBox ctl, String text)
		{
			try
			{
				ctl.Rtf = text;
			}
			catch (ArgumentException)
			{
				ctl.Text = text;
			}
			ToolTipText = ctl.Text;
		}

		/// <summary>
		/// Get Rtf Image
		/// </summary>
		/// <param name="rowIndex">
		/// The row index.
		/// </param>
		/// <param name="value">
		/// The value.
		/// </param>
		/// <param name="selected">
		/// The selected.
		/// </param>
		/// <param name="cellStyle">
		/// The cell style.
		/// </param>
		/// <returns>
		/// Image of the Rtf
		/// </returns>
		private Image GetRtfImage(Int32 rowIndex, Object value, Boolean selected, DataGridViewCellStyle cellStyle)
		{
			Size cellSize = GetSize(rowIndex);

			if (cellSize.Width < 1 || cellSize.Height < 1)
			{
				return null;
			}

			RichTextBox ctl = EditingControl;
			ctl.Size = GetSize(rowIndex);
			ctl.Padding = cellStyle.Padding;
			ctl.Multiline = false;

			SetRichTextBoxText(ctl, Convert.ToString(value));

			// Print the content of RichTextBox to an image.
			Size imgSize = new Size(cellSize.Width - 1, cellSize.Height - 1);
			Image rtfImg;

			if (selected)
			{
				// Selected cell state
				ctl.BackColor = cellStyle.SelectionBackColor;
				ctl.ForeColor = cellStyle.SelectionForeColor;

				// Print image
				rtfImg = RichTextBoxPrinter.Print(ctl, imgSize.Width, imgSize.Height, ctl.Padding);

				// Restore RichTextBox
				ctl.BackColor = cellStyle.BackColor;
				ctl.ForeColor = cellStyle.ForeColor;
			}
			else
			{
				rtfImg = RichTextBoxPrinter.Print(ctl, imgSize.Width, imgSize.Height, ctl.Padding);
			}

			return rtfImg;
		}
	}

	/// <summary>
	/// DataGridView RichTextBox Editing Control
	/// </summary>
	public class DataGridViewRichTextBoxEditingControl : RichTextBox, IDataGridViewEditingControl
	{
		/// <summary>
		/// Value Changed
		/// </summary>
		private Boolean _valueChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataGridViewRichTextBoxEditingControl"/> class.
		/// </summary>
		public DataGridViewRichTextBoxEditingControl()
		{
			BorderStyle = BorderStyle.None;
		}

		/// <summary>
		/// Gets a value indicating whether RepositionEditingControlOnValueChange.
		/// </summary>
		public Boolean RepositionEditingControlOnValueChange
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// Gets or sets EditingControlFormattedValue.
		/// </summary>
		public Object EditingControlFormattedValue
		{
			get
			{
				return Rtf;
			}
			set
			{
				if (value is string)
				{
					Text = value as string;
				}
			}
		}

		/// <summary>
		/// Gets or sets EditingControlRowIndex.
		/// </summary>
		public Int32 EditingControlRowIndex { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether EditingControlValueChanged.
		/// </summary>
		Boolean IDataGridViewEditingControl.EditingControlValueChanged
		{
			get
			{
				return _valueChanged;
			}
			set
			{
				_valueChanged = value;
			}
		}

		/// <summary>
		/// Gets the cursor used when the mouse pointer is over the <see cref="P:System.Windows.Forms.DataGridView.EditingPanel"/> but not over the editing control.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.Cursor"/> that represents the mouse pointer used for the editing panel.
		/// </returns>
		Cursor IDataGridViewEditingControl.EditingPanelCursor
		{
			get
			{
				return Cursor;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="T:System.Windows.Forms.DataGridView"/> that contains the cell.
		/// </summary>
		/// <returns>
		/// The <see cref="T:System.Windows.Forms.DataGridView"/> that contains the <see cref="T:System.Windows.Forms.DataGridViewCell"/> that is being edited; null if there is no associated <see cref="T:System.Windows.Forms.DataGridView"/>.
		/// </returns>
		public DataGridView EditingControlDataGridView { get; set; }

		/// <summary>
		/// Changes the control's user interface (UI) to be consistent with the specified cell style.
		/// </summary>
		/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> to use as the model for the UI.</param>
		void IDataGridViewEditingControl.ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
			Font = dataGridViewCellStyle.Font;
		}

		/// <summary>
		/// Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:System.Windows.Forms.DataGridView"/> should process.
		/// </summary>
		/// <returns>
		/// true if the specified key is a regular input key that should be handled by the editing control; otherwise, false.
		/// </returns>
		/// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys"/> that represents the key that was pressed.</param><param name="dataGridViewWantsInputKey">true when the <see cref="T:System.Windows.Forms.DataGridView"/> wants to process the <see cref="T:System.Windows.Forms.Keys"/> in <paramref name="keyData"/>; otherwise, false.</param>
		Boolean IDataGridViewEditingControl.EditingControlWantsInputKey(Keys keyData, Boolean dataGridViewWantsInputKey)
		{
			switch (keyData & Keys.KeyCode)
			{
				case Keys.Return:
					if (((keyData & (Keys.Alt | Keys.Control | Keys.Shift)) == Keys.Shift) && Multiline)
					{
						return true;
					}
					break;
				case Keys.Left:
				case Keys.Right:
				case Keys.Up:
				case Keys.Down:
					return true;
			}

			return !dataGridViewWantsInputKey;
		}

		/// <summary>
		/// Get Editing Control Formatted Value
		/// </summary>
		/// <param name="context">
		/// The context.
		/// </param>
		/// <returns>
		/// Editing Control Formatted Value
		/// </returns>
		Object IDataGridViewEditingControl.GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return Rtf;
		}

		/// <summary>
		/// Prepares the currently selected cell for editing.
		/// </summary>
		/// <param name="selectAll">true to select all of the cell's content; otherwise, false.</param>
		void IDataGridViewEditingControl.PrepareEditingControlForEdit(Boolean selectAll)
		{
		}

		/// <summary>
		/// On Text Changed
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);

			_valueChanged = true;
			EditingControlDataGridView.NotifyCurrentCellDirty(true);
		}

		/// <summary>
		/// Determines whether the specified key is an input key or a special key that requires preprocessing.
		/// </summary>
		/// <param name="keyData">One of the Keys value.</param>
		/// <returns>
		/// true if the specified key is an input key; otherwise, false.
		/// </returns>
		protected override Boolean IsInputKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			return keys == Keys.Return ? this.Multiline : base.IsInputKey(keyData);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"/> that contains the event data. </param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (e.Control)
			{
				switch (e.KeyCode)
				{
					// Control + B = Bold
					case Keys.B:
						SelectionFont = SelectionFont.Bold
											? new Font(Font.FontFamily, Font.Size, ~FontStyle.Bold & Font.Style)
											: new Font(Font.FontFamily, Font.Size, FontStyle.Bold | Font.Style);
						break;
					// Control + U = Underline
					case Keys.U:
						SelectionFont = SelectionFont.Underline
											? new Font(Font.FontFamily, Font.Size, ~FontStyle.Underline & Font.Style)
											: new Font(Font.FontFamily, Font.Size, FontStyle.Underline | Font.Style);
						break;
					// Control + I = Italic
					// Conflicts with the default shortcut
					// case Keys.I:
					//    if (SelectionFont.Italic)
					//    {
					//        SelectionFont = new Font(Font.FontFamily, Font.Size, ~FontStyle.Italic & Font.Style);
					//    }
					//    else
					//        SelectionFont = new Font(Font.FontFamily, Font.Size, FontStyle.Italic | Font.Style);
					//    break;
				}
			}
		}
	}

	/// <summary>
	/// RichTextBox Printer
	/// </summary>
	/// <remarks>
	/// http://support.microsoft.com/default.aspx?scid=kb;en-us;812425
	/// The RichTextBox control does not provide any method to print the content of the RichTextBox. 
	/// You can extend the RichTextBox class to use EM_FORMATRANGE message 
	/// to send the content of a RichTextBox control to an output device such as printer.
	/// </remarks>
	public class RichTextBoxPrinter
	{
		/// <summary>
		/// Convert the unit used by the .NET framework (1/100 inch) 
		/// and the unit used by Win32 API calls (twips 1/1440 inch)
		/// </summary>
		private const Double AN_INCH = 14.4;

		/// <summary>
		/// RECT
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		private struct Rect
		{
			/// <summary>
			/// Left
			/// </summary>
			public Int32 Left;

			/// <summary>
			/// Top
			/// </summary>
			public Int32 Top;

			/// <summary>
			/// Right
			/// </summary>
			public Int32 Right;

			/// <summary>
			/// Bottom
			/// </summary>
			public Int32 Bottom;
		}

		/// <summary>
		/// CHARRANGE
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		private struct Charrange
		{
			/// <summary>
			/// First character of range (0 for start of doc)
			/// </summary>
			public Int32 cpMin;

			/// <summary>
			/// Last character of range (-1 for end of doc)
			/// </summary>
			public Int32 cpMax;
		}

		/// <summary>
		/// FORMAT RANGE
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		private struct Formatrange
		{
			/// <summary>
			/// Actual DC to draw on
			/// </summary>
			public IntPtr hdc;

			/// <summary>
			/// Target DC for determining text formatting
			/// </summary>
			public IntPtr hdcTarget;

			/// <summary>
			/// Region of the DC to draw to (in twips)
			/// </summary>
			public Rect rc;

			/// <summary>
			/// Region of the whole DC (page size) (in twips)
			/// </summary>
			public Rect rcPage;

			/// <summary>
			/// Range of text to draw (see earlier declaration)
			/// </summary>
			public Charrange chrg;
		}

		/// <summary>
		/// WM USER
		/// </summary>
		private const Int32 WM_USER = 0x0400;

		/// <summary>
		/// EM FORMATRANGE
		/// </summary>
		private const Int32 EM_FORMATRANGE = WM_USER + 57;

		[DllImport("USER32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, Int32 msg, IntPtr wp, IntPtr lp);

		/// <summary>
		/// Render the contents of the RichTextBox for printing
		/// </summary>
		/// <param name="richTextBoxHandle">
		/// The rich text box handle.
		/// </param>
		/// <param name="charFrom">
		/// The char from.
		/// </param>
		/// <param name="charTo">
		/// The char to.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		/// <returns>
		/// Return the last character printed + 1 (printing start from this point for next page)
		/// </returns>
		public static Int32 Print(IntPtr richTextBoxHandle, Int32 charFrom, Int32 charTo, PrintPageEventArgs e)
		{
			// Calculate the area to render and print
			Rect rectToPrint;
			rectToPrint.Top = (int)(e.MarginBounds.Top * AN_INCH);
			rectToPrint.Bottom = (int)(e.MarginBounds.Bottom * AN_INCH);
			rectToPrint.Left = (int)(e.MarginBounds.Left * AN_INCH);
			rectToPrint.Right = (int)(e.MarginBounds.Right * AN_INCH);

			// Calculate the size of the page
			Rect rectPage;
			rectPage.Top = (int)(e.PageBounds.Top * AN_INCH);
			rectPage.Bottom = (int)(e.PageBounds.Bottom * AN_INCH);
			rectPage.Left = (int)(e.PageBounds.Left * AN_INCH);
			rectPage.Right = (int)(e.PageBounds.Right * AN_INCH);

			IntPtr hdc = e.Graphics.GetHdc();

			Formatrange fmtRange;
			fmtRange.chrg.cpMax = charTo; // Indicate character from to character to 
			fmtRange.chrg.cpMin = charFrom;
			fmtRange.hdc = hdc; // Use the same DC for measuring and rendering
			fmtRange.hdcTarget = hdc; // Point at printer hDC
			fmtRange.rc = rectToPrint; // Indicate the area on page to print
			fmtRange.rcPage = rectPage; // Indicate size of page

			IntPtr wparam = new IntPtr(1);

			// Get the pointer to the FORMATRANGE structure in memory
			IntPtr lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
			Marshal.StructureToPtr(fmtRange, lparam, false);

			// Send the rendered data for printing 
			IntPtr res = SendMessage(richTextBoxHandle, EM_FORMATRANGE, wparam, lparam);

			// Free the block of memory allocated
			Marshal.FreeCoTaskMem(lparam);

			// Release the device context handle obtained by a previous call
			e.Graphics.ReleaseHdc(hdc);

			// Release and cached info
			SendMessage(richTextBoxHandle, EM_FORMATRANGE, (IntPtr)0, (IntPtr)0);

			// Return last + 1 character printer
			return res.ToInt32();
		}

		/// <summary>
		/// Print control to image
		/// </summary>
		/// <param name="ctl">
		/// The ctl.
		/// </param>
		/// <param name="width">
		/// The width.
		/// </param>
		/// <param name="height">
		/// The height.
		/// </param>
		/// <param name="padding">
		/// The padding.
		/// </param>
		/// <returns>
		/// Image from control
		/// </returns>
		public static Image Print(RichTextBox ctl, Int32 width, Int32 height, Padding padding)
		{
			Image img = new Bitmap(width, height);

			using (Graphics g = Graphics.FromImage(img))
			{
				// HorizontalResolution is measured in pix/inch         
				float scale = width * 100 / img.HorizontalResolution;
				width = (int)scale;

				// VerticalResolution is measured in pix/inch
				scale = height * 100 / img.VerticalResolution;
				height = (int)scale;

				Rectangle marginBounds = new Rectangle(padding.Left, padding.Top, width - padding.Right, height - padding.Bottom);
				Rectangle pageBounds = new Rectangle(0, 0, width, height);
				PrintPageEventArgs args = new PrintPageEventArgs(g, marginBounds, pageBounds, null);

				Print(ctl.Handle, 0, ctl.Text.Length, args);
			}

			return img;
		}
	}
}