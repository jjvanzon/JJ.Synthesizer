using System;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	/// <summary>
	/// What this class adds to the implementation of double click
	/// is having an event that includes the ID of the entity.
	/// </summary>
	public class ExpandMouseGesture : GestureBase
	{
		public event EventHandler<IDEventArgs> ExpandRequested;

		private readonly DoubleClickGesture _doubleClickGesture;

		internal ExpandMouseGesture(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
		{
			_doubleClickGesture = new DoubleClickGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
			_doubleClickGesture.DoubleClick += _doubleClickGesture_DoubleClick;
		}

		~ExpandMouseGesture()
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
			int id = (int)e.Element.Tag;
			ExpandRequested?.Invoke(this, new IDEventArgs(id));
		}
	}
}
