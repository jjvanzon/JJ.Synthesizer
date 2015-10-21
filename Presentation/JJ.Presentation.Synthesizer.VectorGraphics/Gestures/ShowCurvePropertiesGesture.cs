using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class ShowCurvePropertiesGesture : GestureBase
    {
        public event EventHandler ShowCurvePropertiesRequested;

        private DoubleClickGesture _doubleClickGesture;

        internal ShowCurvePropertiesGesture(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
        {
            _doubleClickGesture = new DoubleClickGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
            _doubleClickGesture.DoubleClick += _doubleClickGesture_DoubleClick;
        }

        ~ShowCurvePropertiesGesture()
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
            if (ShowCurvePropertiesRequested != null)
            {
                ShowCurvePropertiesRequested(this, EventArgs.Empty);
            }
        }
    }
}