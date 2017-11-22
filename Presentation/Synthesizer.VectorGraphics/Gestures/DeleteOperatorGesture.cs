using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	public class DeleteOperatorGesture : GestureBase
	{
		public event EventHandler DeleteRequested;

		protected override void HandleKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == KeyCodeEnum.Delete ||
				e.KeyCode == KeyCodeEnum.Back)
			{
				DeleteRequested?.Invoke(sender, EventArgs.Empty);
			}
		}
	}
}
