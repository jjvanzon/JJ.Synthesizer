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
        public event EventHandler<OperatorSelectedEventArgs> OperatorSelected;

        public override void FireMouseDown(object sender, MouseEventArgs e)
        { }

        public override void FireMouseMove(object sender, MouseEventArgs e)
        { }

        public override void FireMouseUp(object sender, MouseEventArgs e)
        {
            if (OperatorSelected != null)
            {
                OperatorSelected(sender, new OperatorSelectedEventArgs(e.Element));
            }
        }
    }
}
