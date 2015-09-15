using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class SelectOperatorGesture : GestureBase
    {
        public event EventHandler<ElementEventArgs> OperatorSelected;

        public override void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (OperatorSelected != null)
            {
                OperatorSelected(sender, new ElementEventArgs(e.Element));
            }
        }
    }
}
