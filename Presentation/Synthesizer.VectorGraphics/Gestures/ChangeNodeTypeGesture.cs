using System;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	public class ChangeNodeTypeGesture : GestureBase
	{
		public event EventHandler ChangeNodeTypeRequested;

		protected override void HandleKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == KeyCodeEnum.Space)
			{
				ChangeNodeTypeRequested?.Invoke(sender, EventArgs.Empty);
			}
		}
	}
}
