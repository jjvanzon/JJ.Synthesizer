using System;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	/// <summary>
	/// It is handy to abstract a mouse event to a specialized gesture,
	/// in case you want to switch between mouse down or mouse up,
	/// or other ways of selecting it.
	/// </summary>
	public class SelectGesture : GestureBase
	{
		public event EventHandler<ElementEventArgs> SelectRequested;

		// Do note that MouseDown might not work,
		// because in WinForms the MouseDown event of one control goes off
		// before the LostFocus event of another control.
		// Also: when a mousedown would regenerate the diagram,
		// the move gesture does not work, because it tries to move the already disappeared element.
		protected override void HandleMouseUp(object sender, MouseEventArgs e)
		{
			SelectRequested?.Invoke(sender, new ElementEventArgs(e.Element));
		}
	}
}
