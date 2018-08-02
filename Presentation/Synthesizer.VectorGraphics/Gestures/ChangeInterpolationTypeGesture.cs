using System;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	public class ChangeInterpolationTypeGesture : GestureBase
	{
		public event EventHandler ChangeInterpolationOfSelectedNodeRequested;

		protected override void HandleKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == KeyCodeEnum.Space)
			{
				ChangeInterpolationOfSelectedNodeRequested?.Invoke(sender, EventArgs.Empty);
			}
		}
	}
}
