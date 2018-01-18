using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	/// <summary>
	/// Keyboard gesture must be separated from the mouse gesture,
	/// because keyboard gesture is tied to the whole diagram,
	/// while mouse gesture is tied to a specific element.
	/// </summary>
	public class ExpandNodeKeyboardGesture : GestureBase
	{
		public event EventHandler<IDEventArgs> ExpandNodeRequested;

		public int SelectedNodeID { get; set; }

		protected override void HandleKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == KeyCodeEnum.Enter)
			{
				ExpandNodeRequested?.Invoke(this, new IDEventArgs(SelectedNodeID));
			}
		}
	}
}
