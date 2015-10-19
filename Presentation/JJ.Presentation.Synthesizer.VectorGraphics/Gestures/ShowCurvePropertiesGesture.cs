using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    // TODO: Perhaps not derive from DoubleClickGesture, but wrap DoubleClickGesture and pass Diagram to the constructor,
    // upon which the private DoubleClickGesture is added to Diagram.BackGround.Gestures
    // and responded to upone which ShowCurvePropertiesRequested is raised.
    public class ShowCurvePropertiesGesture : DoubleClickGesture
    {
        public event EventHandler ShowCurvePropertiesRequested;

        internal ShowCurvePropertiesGesture(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
            : base(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels)
        {
            DoubleClick += this_DoubleClick;
        }

        ~ShowCurvePropertiesGesture()
        {
            DoubleClick -= this_DoubleClick;
        }

        private void this_DoubleClick(object sender, ElementEventArgs e)
        {
            if (ShowCurvePropertiesRequested != null)
            {
                ShowCurvePropertiesRequested(this, EventArgs.Empty);
            }
        }
    }
}
