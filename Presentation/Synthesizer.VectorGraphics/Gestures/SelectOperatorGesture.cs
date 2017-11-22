using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	/// <summary>
	/// It is handy to abstract a mouse event to a specialized gesture,
	/// in case you want to switch between mouse down or mouse up,
	/// or other ways of selecting it.
	/// </summary>
	public class SelectOperatorGesture : GestureBase
	{
		public event EventHandler<ElementEventArgs> OperatorSelected;

		// Handle MouseUp instead of MouseDown as a work-around,
		// because in WinForms the MouseDown event of one control goes off
		// before the LostFocus event of the other control,
		// so SelectOperator goes off overwriting the properties
		// before saving the properties.
		protected override void HandleMouseUp(object sender, MouseEventArgs e)
		{
			OperatorSelected?.Invoke(sender, new ElementEventArgs(e.Element));
		}
	}
}
