using System;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	/// <summary>
	/// What this class adds to the implementation of double click
	/// is having an event that includes the ID of the entity.
	/// </summary>
	public class ExpandOperatorMouseGesture : GestureBase
	{
		public event EventHandler<IDEventArgs> ExpandOperatorRequested;

		private readonly DoubleClickGesture _doubleClickGesture;

		internal ExpandOperatorMouseGesture(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
		{
			_doubleClickGesture = new DoubleClickGesture(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);
			_doubleClickGesture.DoubleClick += _doubleClickGesture_DoubleClick;
		}

		~ExpandOperatorMouseGesture()
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
			int operatorID = (int)e.Element.Tag;
			ExpandOperatorRequested?.Invoke(this, new IDEventArgs(operatorID));
		}
	}
}
