using System;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	public class DeleteGesture : GestureBase
	{
		public event EventHandler DeleteSelectionRequested;

		protected override void HandleKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == KeyCodeEnum.Delete ||
				e.KeyCode == KeyCodeEnum.Back)
			{
				DeleteSelectionRequested?.Invoke(sender, EventArgs.Empty);
			}
		}
	}
}
