//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Framework.Presentation.VectorGraphics.Enums;
//using JJ.Framework.Presentation.VectorGraphics.EventArg;
//using JJ.Framework.Presentation.VectorGraphics.Gestures;
//using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;

//namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
//{
//    public class ShowNodePropertiesGesture : GestureBase
//    {
//        public event EventHandler<IDEventArgs> ShowNodePropertiesRequested;

//        private readonly DoubleClickGesture _doubleClickGesture;

//        internal ShowNodePropertiesGesture(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
//        {
//            _doubleClickGesture = new DoubleClickGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
//            _doubleClickGesture.DoubleClick += _doubleClickGesture_DoubleClick;
//        }

//        ~ShowNodePropertiesGesture()
//        {
//            if (_doubleClickGesture != null)
//            {
//                _doubleClickGesture.DoubleClick -= _doubleClickGesture_DoubleClick;
//            }
//        }

//        public int SelectedNodeID { get; set; }

//        protected override void HandleKeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == KeyCodeEnum.Enter)
//            {
//                ShowNodePropertiesRequested?.Invoke(this, new IDEventArgs(SelectedNodeID));
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
//            ShowNodePropertiesRequested?.Invoke(this, new IDEventArgs((int)e.Element.Tag));
//        }
//    }
//}
