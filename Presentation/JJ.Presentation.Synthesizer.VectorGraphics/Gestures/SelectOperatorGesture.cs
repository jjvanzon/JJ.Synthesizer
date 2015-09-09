using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class SelectOperatorGesture : GestureBase
    {
        public event EventHandler<ElementEventArgs> OperatorSelected;

        //public override void HandleMouseDown(object sender, MouseEventArgs e)
        //{
        //    if (OperatorSelected != null)
        //    {
        //        OperatorSelected(sender, new ElementEventArgs(e.Element));
        //    }
        //}

        public override void HandleMouseUp(object sender, MouseEventArgs e)
        {
            if (e == null) throw new NullException(() => e);

            // This event is handled, in case a mouse down causes a regeneration of the diagram.
            // upon which the element upon which mouse down went off is gone.

            if (OperatorSelected != null)
            {
                OperatorSelected(sender, new ElementEventArgs(e.Element));
            }
        }
    }
}
