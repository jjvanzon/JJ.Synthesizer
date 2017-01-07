using System;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class ChangeNodeTypeGesture : GestureBase
    {
        public event EventHandler ChangeNodeTypeRequested;

        protected override void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == KeyCodeEnum.Space)
            {
                if (ChangeNodeTypeRequested != null)
                {
                    ChangeNodeTypeRequested(sender, EventArgs.Empty);
                }
            }
        }
    }
}
