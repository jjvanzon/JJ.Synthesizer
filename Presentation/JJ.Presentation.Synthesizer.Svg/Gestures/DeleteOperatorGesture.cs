using JJ.Framework.Presentation.Svg.Enums;
using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Framework.Presentation.Svg.Gestures;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Presentation.Synthesizer.Svg.Gestures
{
    public class DeleteOperatorGesture : GestureBase
    {
        public EventHandler DeleteRequested;

        public override void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e == null) throw new NullException(() => e);

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
