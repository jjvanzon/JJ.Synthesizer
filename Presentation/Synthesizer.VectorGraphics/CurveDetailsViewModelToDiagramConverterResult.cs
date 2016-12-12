using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class CurveDetailsViewModelToDiagramConverterResult
    {
        public Diagram Diagram { get; }
        public KeyDownGesture KeyDownGesture { get; }
        public SelectNodeGesture SelectNodeGesture { get; }
        public MoveGesture MoveNodeGesture { get; }
        public ShowCurvePropertiesGesture ShowCurvePropertiesGesture { get; }
        public ChangeNodeTypeGesture ChangeNodeTypeGesture { get; }
        public ShowNodePropertiesMouseGesture ShowNodePropertiesMouseGesture { get; }
        public ShowNodePropertiesKeyboardGesture ShowNodePropertiesKeyboardGesture { get; }
        public ToolTipGesture NodeToolTipGesture { get; }

        public CurveDetailsViewModelToDiagramConverterResult(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
        {
            Diagram = new Diagram();

            KeyDownGesture = new KeyDownGesture();
            SelectNodeGesture = new SelectNodeGesture();
            MoveNodeGesture = new MoveGesture();
            ShowNodePropertiesKeyboardGesture = new ShowNodePropertiesKeyboardGesture();
            ChangeNodeTypeGesture = new ChangeNodeTypeGesture();

            ShowCurvePropertiesGesture = new ShowCurvePropertiesGesture(
                doubleClickSpeedInMilliseconds,
                doubleClickDeltaInPixels);

            ShowNodePropertiesMouseGesture = new ShowNodePropertiesMouseGesture(
                doubleClickSpeedInMilliseconds,
                doubleClickDeltaInPixels);

            NodeToolTipGesture = new ToolTipGesture(
                Diagram,
                StyleHelper.ToolTipBackStyle,
                StyleHelper.ToolTipLineStyle,
                StyleHelper.ToolTipTextStyle);

            Diagram.Gestures.Add(KeyDownGesture);
            Diagram.Background.Gestures.Add(ShowCurvePropertiesGesture);
            Diagram.Background.Gestures.Add(ChangeNodeTypeGesture);
            Diagram.Background.Gestures.Add(ShowNodePropertiesKeyboardGesture);
        }
    }
}