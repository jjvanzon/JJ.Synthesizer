using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class ShowNodePropertiesGesture : GestureBase
    {
        public event EventHandler<IDEventArgs> ShowNodePropertiesRequested;

        private DoubleClickGesture _doubleClickGesture;

        internal ShowNodePropertiesGesture(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
        {
            _doubleClickGesture = new DoubleClickGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
            _doubleClickGesture.DoubleClick += _doubleClickGesture_DoubleClick;
        }

        ~ShowNodePropertiesGesture()
        {
            if (_doubleClickGesture != null)
            {
                _doubleClickGesture.DoubleClick -= _doubleClickGesture_DoubleClick;
            }
        }

        protected override void HandleMouseDown(object sender, MouseEventArgs e)
        {
            ((IGesture)_doubleClickGesture).HandleMouseDown(sender, e);
        }

        protected override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            ((IGesture)_doubleClickGesture).HandleMouseMove(sender, e);
        }

        protected override void HandleMouseUp(object sender, MouseEventArgs e)
        {
            ((IGesture)_doubleClickGesture).HandleMouseUp(sender, e);
        }

        private void _doubleClickGesture_DoubleClick(object sender, ElementEventArgs e)
        {
            if (ShowNodePropertiesRequested != null)
            {
                int nodeID = (int)e.Element.Tag;
                ShowNodePropertiesRequested(this, new IDEventArgs(nodeID));
            }
        }
    }
}
