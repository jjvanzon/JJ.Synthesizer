using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Framework.Presentation.Svg.Gestures;
using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Svg.Gestures
{
    // TODO: Make this internal if it proves we are never going to expose this type.
    public class LineGesture : IGesture, IDisposable
    {
        private DragGesture _dragGesture;
        private DropGesture _dropGesture;

        private IList<IGesture> _baseGestures;

        private Line _line;

        private float _mouseDownX;
        private float _mouseDownY;

        // TODO: Don't I need multiple drag gestures and drop gestures?
        // TODO: I hoped I could encapsulate drag and drop gestures, and create them
        // privately, but the drags and drops involve different elements,
        // so maybe I need a DragLine and DropLine gesture?
        // I am having difficulty managing drag and drop as separate gestures
        // anyway, so maybe I should make it a little less polymorphic and
        // make drag and drop a single gesture with booleans CanDrag and CanDropOn?
        // Anyway, to make this gesture stand on its own, you would have to 
        // first get rid of the bugs in drag and drop.
        // I am very much in doubt about turning Drag and Drop into a single
        // Gesture again (because of the polymorphic smell).

        public LineGesture(DragGesture dragGesture, DropGesture dropGesture, Diagram diagram)
        {
            if (dragGesture == null) throw new NullException(() => dragGesture);
            if (dropGesture == null) throw new NullException(() => dropGesture);
            if (diagram == null) throw new NullException(() => diagram);

            _dragGesture = dragGesture;
            _dropGesture = dropGesture;

            _line = InitializeLine(diagram);

            _baseGestures = InitializeGestures(_dragGesture, _dropGesture);
        }

        public LineGesture(DragGesture dragGesture, DropGesture dropGesture, Line line)
        {
            if (dragGesture == null) throw new NullException(() => dragGesture);
            if (dropGesture == null) throw new NullException(() => dropGesture);
            if (line == null) throw new NullException(() => line);

            _dragGesture = dragGesture;
            _dropGesture = dropGesture;
            _line = line;

            InitializeLine(_line);

            _baseGestures = InitializeGestures(_dragGesture, _dropGesture);
        }

        private IList<IGesture> InitializeGestures(DragGesture dragGesture, DropGesture dropGesture)
        {
            IList<IGesture> baseGestures = new IGesture[] { dragGesture, dropGesture };
            dragGesture.OnDragging += _dragGesture_OnDragging;
            return baseGestures;
        }

        private Line InitializeLine(Diagram diagram)
        {
            Line line = new Line();
            line.Diagram = diagram;
            line.PointA.Diagram = diagram;
            line.PointB.Diagram = diagram;
            InitializeLine(line);
            return line;
        }

        private void InitializeLine(Line line)
        {
            if (line.Diagram == null) throw new NullException(() => line.Diagram);

            //line.LineStyle.Visible = false;
            line.PointA.Parent = line.Diagram.RootRectangle;
            line.PointB.Parent = line.Diagram.RootRectangle;

            line.Tag = "THIS IS THE LINE GESTURE LINE!";
        }

        ~LineGesture()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_dragGesture != null)
            {
                _dragGesture.OnDragging -= _dragGesture_OnDragging;
            }

            GC.SuppressFinalize(this);
        }

        // IGesture

        bool IGesture.MouseCaptureRequired
        {
            get { return true; }
        }

        void IGesture.MouseDown(object sender, MouseEventArgs e)
        {
            // I should only do this if I own the base events,
            // which I don't. Now the base events go off twice.
            foreach (IGesture baseGesture in _baseGestures)
            {
                baseGesture.MouseDown(sender, e);
            }

            if (e.Element != null)
            {
                _mouseDownX = e.X;
                _mouseDownY = e.Y;
            }
        }

        void IGesture.MouseMove(object sender, MouseEventArgs e)
        {
            foreach (IGesture baseGesture in _baseGestures)
            {
                baseGesture.MouseMove(sender, e);
            }
        }

        void IGesture.MouseUp(object sender, MouseEventArgs e)
        {
            foreach (IGesture baseGesture in _baseGestures)
            {
                baseGesture.MouseUp(sender, e);
            }

            //_line.Visible = false;
        }

        // Events

        private void _dragGesture_OnDragging(object sender, DraggingEventArgs e)
        {
            _line.PointA.X = _mouseDownX;
            _line.PointA.Y = _mouseDownY;

            _line.PointB.X = e.X;
            _line.PointB.Y = e.Y;

            _line.Visible = true;
        }
    }
}
