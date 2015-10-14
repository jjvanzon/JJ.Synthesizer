using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class DeleteOperatorGesture : GestureBase
    {
        public EventHandler DeleteRequested;

        protected override void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == KeyCodeEnum.Delete ||
                e.KeyCode == KeyCodeEnum.Back)
            {
                if (DeleteRequested != null)
                {
                    DeleteRequested(sender, EventArgs.Empty);
                }
            }
        }
    }
}
