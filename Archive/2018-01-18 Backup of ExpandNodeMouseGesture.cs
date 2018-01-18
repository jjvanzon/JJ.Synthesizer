//using JJ.Framework.Presentation.VectorGraphics.EventArg;
//using JJ.Framework.Presentation.VectorGraphics.Gestures;
//using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
//using System;

//namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
//{
//	/// <summary>
//	/// What this class adds to the implementation of double click
//	/// is having an event that includes the ID of the entity.
//	/// </summary>
//	public class ExpandNodeMouseGesture : GestureBase
//	{
//		public event EventHandler<IDEventArgs> ExpandNodeRequested;

//		private readonly DoubleClickGesture _doubleClickGesture;

//		internal ExpandNodeMouseGesture(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
//		{
//			_doubleClickGesture = new DoubleClickGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
//			_doubleClickGesture.DoubleClick += _doubleClickGesture_DoubleClick;
//		}

//		~ExpandNodeMouseGesture()
//		{
//			if (_doubleClickGesture != null)
//			{
//				_doubleClickGesture.DoubleClick -= _doubleClickGesture_DoubleClick;
//			}
//		}

//		protected override void HandleMouseDown(object sender, MouseEventArgs e)
//		{
//			_doubleClickGesture.Internals.HandleMouseDown(sender, e);
//		}

//		protected override void HandleMouseMove(object sender, MouseEventArgs e)
//		{
//			_doubleClickGesture.Internals.HandleMouseMove(sender, e);
//		}

//		protected override void HandleMouseUp(object sender, MouseEventArgs e)
//		{
//			_doubleClickGesture.Internals.HandleMouseUp(sender, e);
//		}

//		private void _doubleClickGesture_DoubleClick(object sender, ElementEventArgs e)
//		{
//			ExpandNodeRequested?.Invoke(this, new IDEventArgs((int)e.Element.Tag));
//		}
//	}
//}
