using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

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
        public ToolTipGesture NodeToolTipGesture { get; private set; }

        public CurveDetailsViewModelToDiagramConverterResult(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
        {
            Diagram = new Diagram();
            KeyDownGesture = new KeyDownGesture();
            SelectNodeGesture = new SelectNodeGesture();
            MoveNodeGesture = new MoveGesture();

            ShowCurvePropertiesGesture = new ShowCurvePropertiesGesture(
                doubleClickSpeedInMilliseconds, 
                doubleClickDeltaInPixels);

            ChangeNodeTypeGesture = new ChangeNodeTypeGesture();

            ShowNodePropertiesGesture = new ShowNodePropertiesGesture(
                doubleClickSpeedInMilliseconds, 
                doubleClickDeltaInPixels);

            ShowSelectedNodePropertiesGesture = new ShowSelectedNodePropertiesGesture();

            NodeToolTipGesture = new ToolTipGesture(
                Diagram, 
                StyleHelper.ToolTipBackStyle, 
                StyleHelper.ToolTipLineStyle, 
                StyleHelper.ToolTipTextStyle);

            Diagram.Gestures.Add(KeyDownGesture);
            Diagram.Background.Gestures.Add(ShowCurvePropertiesGesture);
            Diagram.Background.Gestures.Add(ChangeNodeTypeGesture);
            Diagram.Background.Gestures.Add(ShowSelectedNodePropertiesGesture);
        }
    }
}
