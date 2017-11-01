//using System;
//using JJ.Framework.Presentation.VectorGraphics.EventArg;
//using JJ.Framework.Presentation.VectorGraphics.Gestures;

//namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
//{
//    public class DoubleClickBackgroundGesture : GestureBase
//    {
//        public event EventHandler DoubleClick;

//        private readonly DoubleClickGesture _doubleClickGesture;

//        internal DoubleClickBackgroundGesture(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
//        {
//            _doubleClickGesture = new DoubleClickGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
//            _doubleClickGesture.DoubleClick += _doubleClickGesture_DoubleClick;
//        }

//        ~DoubleClickBackgroundGesture()
//        {
//            if (_doubleClickGesture != null)
//            {
//                _doubleClickGesture.DoubleClick -= _doubleClickGesture_DoubleClick;
//            }
//        }

//        protected override void HandleMouseDown(object sender, MouseEventArgs e)
//        {
//            _doubleClickGesture.Internals.HandleMouseDown(sender, e);
//        }

//        protected override void HandleMouseMove(object sender, MouseEventArgs e)
//        {
//            _doubleClickGesture.Internals.HandleMouseMove(sender, e);
//        }

//        protected override void HandleMouseUp(object sender, MouseEventArgs e)
//        {
//            _doubleClickGesture.Internals.HandleMouseUp(sender, e);
//        }

//        private void _doubleClickGesture_DoubleClick(object sender, ElementEventArgs e)
//        {
//            DoubleClick?.Invoke(this, EventArgs.Empty);
//        }
//    }
//}