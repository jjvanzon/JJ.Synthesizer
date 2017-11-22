using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	/// <summary>
	/// Keyboard gesture must be separated from the mouse gesture,
	/// because keyword gesture is tied to the whole diagram,
	/// while mouse gesture is tied to a specific element.
	/// </summary>
	public class ExpandOperatorKeyboardGesture : GestureBase
	{
		public event EventHandler<IDEventArgs> ExpandOperatorRequested;

		public int? SelectedOperatorID { get; set; }

		protected override void HandleKeyDown(object sender, KeyEventArgs e)
		{
			if (!SelectedOperatorID.HasValue)
			{
				return;
			}

			if (e.KeyCode == KeyCodeEnum.Enter)
			{
				ExpandOperatorRequested?.Invoke(sender, new IDEventArgs(SelectedOperatorID.Value));
			}
		}
	}
}
