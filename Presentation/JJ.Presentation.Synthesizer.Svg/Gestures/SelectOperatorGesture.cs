using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Framework.Presentation.Svg.Gestures;
using JJ.Presentation.Synthesizer.Svg.EventArg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Svg.Gestures
{
    public class SelectOperatorGesture : GestureBase
    {
        public event EventHandler<ElementEventArgs> OperatorSelected;

        public override void HandleMouseUp(object sender, MouseEventArgs e)
        {
            if (OperatorSelected != null)
            {
                OperatorSelected(sender, new ElementEventArgs(e.Element));
            }
        }
    }
}
