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
        public ShowCurvePropertiesGesture ShowCurvePropertiesGesture { get; private set; }
        public ChangeNodeTypeGesture ChangeNodeTypeGesture { get; private set; }
        public ShowNodePropertiesGesture ShowNodePropertiesGesture { get; private set; }
        public ShowSelectedNodePropertiesGesture ShowSelectedNodePropertiesGesture { get; private set; }

        public CurveDetailsViewModelToDiagramConverterResult(
            Diagram diagram, 
            KeyDownGesture keyDownGesture,
            SelectNodeGesture selectNodeGesture,
            MoveGesture moveNodeGesture,
            ShowCurvePropertiesGesture showCurvePropertiesGesture,
            ChangeNodeTypeGesture changeNodeTypeGesture,
            ShowNodePropertiesGesture showNodePropertiesGesture,
            ShowSelectedNodePropertiesGesture showSelectedNodePropertiesGesture)
        {
            if (diagram == null) throw new NullException(() => diagram);
            if (keyDownGesture == null) throw new NullException(() => keyDownGesture);
            if (selectNodeGesture == null) throw new NullException(() => selectNodeGesture);
            if (moveNodeGesture == null) throw new NullException(() => moveNodeGesture);
            if (showCurvePropertiesGesture == null) throw new NullException(() => showCurvePropertiesGesture);
            if (changeNodeTypeGesture == null) throw new NullException(() => changeNodeTypeGesture);
            if (showNodePropertiesGesture == null) throw new NullException(() => showNodePropertiesGesture);
            if (showSelectedNodePropertiesGesture == null) throw new NullException(() => showSelectedNodePropertiesGesture);

            Diagram = diagram;
            KeyDownGesture = keyDownGesture;
            SelectNodeGesture = selectNodeGesture;
            MoveNodeGesture = moveNodeGesture;
            ShowCurvePropertiesGesture = showCurvePropertiesGesture;
            ChangeNodeTypeGesture = changeNodeTypeGesture;
            ShowNodePropertiesGesture = showNodePropertiesGesture;
            ShowSelectedNodePropertiesGesture = showSelectedNodePropertiesGesture;
        }
    }
}
