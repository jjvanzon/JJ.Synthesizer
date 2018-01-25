using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Exceptions;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Framework.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	public class DropLineGesture : DropGesture, IDisposable
	{
		private readonly Line _line;

		public DropLineGesture(Diagram diagram, params DragLineGesture[] dragLineGestures)
			: this(diagram, (IList<DragLineGesture>)dragLineGestures)
		{ }

		public DropLineGesture(Diagram diagram, IList<DragLineGesture> dragLineGestures = null)
			: this(diagram, dragLineGestures, null)
		{ }

		public DropLineGesture(Diagram diagram,
			IList<DragLineGesture> dragLineGestures = null,
			LineStyle lineStyle = null,
			int lineZIndex = 0) 
			: base(dragLineGestures?.OfType<DragGesture>().ToArray() ?? new DragGesture[0])
		{
			if (diagram == null) throw new NullException(() => diagram);

			_line = LineGestureHelper.CreateLine(diagram, lineStyle, lineZIndex);

			Dropped += this_Dropped;
		}

		~DropLineGesture()
		{
			Dispose();
		}

		public void Dispose()
		{
			Dropped -= this_Dropped;

			GC.SuppressFinalize(this);
		}

		// Events

		private void this_Dropped(object sender, DroppedEventArgs e)
		{
			_line.Visible = false;
		}
	}
}
