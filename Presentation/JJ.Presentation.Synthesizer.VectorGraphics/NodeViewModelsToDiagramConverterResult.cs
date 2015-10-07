using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class NodeViewModelsToDiagramConverterResult
    {
        public Diagram Diagram { get; private set; }
        public KeyDownGesture KeyDownGesture { get; private set; }
        public SelectNodeGesture SelectNodeGesture { get; private set; }

        public NodeViewModelsToDiagramConverterResult(
            Diagram diagram, 
            KeyDownGesture keyDownGesture,
            SelectNodeGesture selectNodeGesture)
        {
            if (diagram == null) throw new NullException(() => diagram);
            if (keyDownGesture == null) throw new NullException(() => keyDownGesture);
            if (selectNodeGesture == null) throw new NullException(() => selectNodeGesture);

            Diagram = diagram;
            KeyDownGesture = keyDownGesture;
            SelectNodeGesture = selectNodeGesture;
        }
    }
}
