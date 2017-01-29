using System;
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Presentation.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using System.Linq;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class DropLineGesture : DropGesture, IDisposable
    {
        private Diagram _diagram;
        private readonly Line _line;

        public DropLineGesture(Diagram diagram, params DragLineGesture[] dragLineGestures)
            : this(diagram, (IList<DragLineGesture>)dragLineGestures)
        { }

        public DropLineGesture(Diagram diagram, IList<DragLineGesture> dragLineGestures = null)
            : this(diagram, dragLineGestures, null, 0)
        { }

        public DropLineGesture(Diagram diagram,
            IList<DragLineGesture> dragLineGestures = null,
            LineStyle lineStyle = null,
            int lineZIndex = 0) 
            : base(dragLineGestures.OfType<DragGesture>().ToArray())
        {
            if (diagram == null) throw new NullException(() => diagram);

            _diagram = diagram;
            _line = LineGestureHelper.CreateLine(diagram, lineStyle, lineZIndex);

            this.Dropped += this_Dropped;
        }

        ~DropLineGesture()
        {
            Dispose();
        }

        public void Dispose()
        {
            this.Dropped -= this_Dropped;

            GC.SuppressFinalize(this);
        }

        // Events

        private void this_Dropped(object sender, DroppedEventArgs e)
        {
            _line.Visible = false;
        }
    }
}
