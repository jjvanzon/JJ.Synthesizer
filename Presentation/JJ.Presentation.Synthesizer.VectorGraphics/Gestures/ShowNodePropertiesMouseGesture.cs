using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    /// <summary>
    /// What this class adds to the implementation of double click
    /// is having an event that includes the ID of the entity.
    /// </summary>
    public class ShowNodePropertiesMouseGesture : GestureBase
    {
        public event EventHandler<IDEventArgs> ShowNodePropertiesRequested;

        private readonly DoubleClickGesture _doubleClickGesture;

        internal ShowNodePropertiesMouseGesture(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
        {
            _doubleClickGesture = new DoubleClickGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
            _doubleClickGesture.DoubleClick += _doubleClickGesture_DoubleClick;
        }

        ~ShowNodePropertiesMouseGesture()
        {
            if (_doubleClickGesture != null)
            {
                _doubleClickGesture.DoubleClick -= _doubleClickGesture_DoubleClick;
            }
        }

        protected override void HandleMouseDown(object sender, MouseEventArgs e)
        {
            _doubleClickGesture.Internals.HandleMouseDown(sender, e);
        }

        protected override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            _doubleClickGesture.Internals.HandleMouseMove(sender, e);
        }

        protected override void HandleMouseUp(object sender, MouseEventArgs e)
        {
            _doubleClickGesture.Internals.HandleMouseUp(sender, e);
        }

        private void _doubleClickGesture_DoubleClick(object sender, ElementEventArgs e)
        {
            ShowNodePropertiesRequested?.Invoke(this, new IDEventArgs((int)e.Element.Tag));
        }
    }
}
