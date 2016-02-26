using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class OperatorEnterKeyGesture : GestureBase
    {
        public EventHandler EnterKeyPressed;

        protected override void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == KeyCodeEnum.Enter)
            {
                if (EnterKeyPressed != null)
                {
                    EnterKeyPressed(sender, EventArgs.Empty);
                }
            }
        }
    }
}
