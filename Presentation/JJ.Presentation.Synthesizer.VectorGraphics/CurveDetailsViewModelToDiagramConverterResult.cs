using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class CurveDetailsViewModelToDiagramConverterResult
    {
        public Diagram Diagram { get; private set; }
        public KeyDownGesture KeyDownGesture { get; private set; }
        public SelectNodeGesture SelectNodeGesture { get; private set; }
        public MoveGesture MoveNodeGesture { get; private set; }

        public CurveDetailsViewModelToDiagramConverterResult(
            Diagram diagram, 
            KeyDownGesture keyDownGesture,
            SelectNodeGesture selectNodeGesture,
            MoveGesture moveNodeGesture)
        {
            if (diagram == null) throw new NullException(() => diagram);
            if (keyDownGesture == null) throw new NullException(() => keyDownGesture);
            if (selectNodeGesture == null) throw new NullException(() => selectNodeGesture);
            if (moveNodeGesture == null) throw new NullException(() => moveNodeGesture);

            Diagram = diagram;
            KeyDownGesture = keyDownGesture;
            SelectNodeGesture = selectNodeGesture;
            MoveNodeGesture = moveNodeGesture;
        }
    }
}
