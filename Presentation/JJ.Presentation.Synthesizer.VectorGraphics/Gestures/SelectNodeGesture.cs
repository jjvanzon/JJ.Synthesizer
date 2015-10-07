using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    /// <summary>
    /// It is handy to abstract a mouse event to a specialized gesture,
    /// in case you want to switch between mouse down or mouse up,
    /// or other ways of selecting it.
    /// </summary>
    public class SelectNodeGesture : GestureBase
    {
        public event EventHandler<ElementEventArgs> SelectNodeRequested;

        // Do note that MouseDown might not work,
        // because in WinForms the MouseDown event of one control goes off
        // before the LostFocus event of another control.
        // Also: when a mousedown would regenerate the diagram,
        // the move gesture does not work, because it tries to move the already disappeared element.
        public override void HandleMouseUp(object sender, MouseEventArgs e)
        {
            if (SelectNodeRequested != null)
            {
                SelectNodeRequested(sender, new ElementEventArgs(e.Element));
            }
        }
    }
}
