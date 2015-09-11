using System;
using System.Collections.Generic;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Presentation.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using System.Linq;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class DropLineGesture : GestureBase, IDisposable
    {
        private Diagram _diagram;
        private Line _line;

        private DropGesture _dropGesture;

        public event EventHandler<DroppedEventArgs> Dropped
        {
            add { _dropGesture.Dropped += value; }
            remove { _dropGesture.Dropped -= value; }
        }

        public DropLineGesture(Diagram diagram, params DragLineGesture[] dragLineGestures)
            : this(diagram, (IList<DragLineGesture>)dragLineGestures)
        { }

        public DropLineGesture(Diagram diagram, IList<DragLineGesture> dragLineGestures = null)
            : this(diagram, dragLineGestures, null, 0)
        { }

        public DropLineGesture(Diagram diagram, IList<DragLineGesture> dragLineGestures = null, LineStyle lineStyle = null, int lineZIndex = 0)
        {
            if (diagram == null) throw new NullException(() => diagram);

            _diagram = diagram;
            _line = LineGestureHelper.CreateLine(diagram, lineStyle, lineZIndex);
            _dropGesture = new DropGesture(dragLineGestures.Select(x => x.DragGesture).ToArray());
            _dropGesture.Dropped += _dropGesture_Dropped;
        }

        ~DropLineGesture()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_dropGesture != null)
            {
                _dropGesture.Dropped -= _dropGesture_Dropped;
            }

            GC.SuppressFinalize(this);
        }

        // IGesture

        public override void HandleMouseDown(object sender, MouseEventArgs e)
        {
            _dropGesture.HandleMouseDown(sender, e);
        }

        public override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            _dropGesture.HandleMouseMove(sender, e);
        }

        public override void HandleMouseUp(object sender, MouseEventArgs e)
        {
            _dropGesture.HandleMouseUp(sender, e);
        }

        // Events

        private void _dropGesture_Dropped(object sender, DroppedEventArgs e)
        {
            // TODO: _line.Visible does not seem to work.
            _line.Visible = false;
            _line.LineStyle.Visible = false;
        }
    }
}
